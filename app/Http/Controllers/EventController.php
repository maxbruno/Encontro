<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreEventRequest;
use App\Http\Requests\UpdateEventRequest;
use App\Models\Event;
use App\Services\EventService;
use Illuminate\Http\Request;

class EventController extends Controller
{
    protected EventService $service;

    public function __construct(EventService $service)
    {
        $this->service = $service;
        // Note: Middleware is applied in routes/web.php
    }

    public function index(Request $request)
    {
        // Load events with participant count, excluding soft deleted
        $query = \App\Models\Event::where('is_deleted', false)
            ->withCount('participants');

        // Apply search filter
        if ($request->filled('search')) {
            $search = $request->search;
            $query->where(function($q) use ($search) {
                $q->where('name', 'like', '%' . $search . '%')
                  ->orWhere('patron_saint_name', 'like', '%' . $search . '%');
            });
        }

        // Apply event type filter
        if ($request->filled('event_type')) {
            $query->where('event_type', $request->event_type);
        }

        // Apply sorting
        $sortBy = $request->get('sort_by', 'created_at');
        $sortOrder = $request->get('sort_order', 'desc');
        
        // Validate sort parameters
        $allowedSortFields = ['name', 'event_type', 'created_at'];
        $sortBy = in_array($sortBy, $allowedSortFields) ? $sortBy : 'created_at';
        $sortOrder = in_array($sortOrder, ['asc', 'desc']) ? $sortOrder : 'desc';

        $events = $query->orderBy($sortBy, $sortOrder)->get();
        
        return view('events.index', compact('events'));
    }

    public function create()
    {
        return view('events.create');
    }

    public function store(StoreEventRequest $request)
    {
        $event = $this->service->create($request->validated(), $request->file('photo'));

        $successMessage = "Evento '{$event->name}' criado com sucesso!";

        // Check which button was clicked
        if ($request->input('action') === 'saveAndViewDetails') {
            return redirect()->route('events.show', $event->id)->with('success', $successMessage);
        }

        // Default: save and return to list
        return redirect()->route('events.index')->with('success', $successMessage);
    }

    public function show(Request $request, int $id)
    {
        $event = $this->service->getById($id);
        if (!$event) {
            return redirect()->route('events.index')->with('error', 'Evento não encontrado.');
        }

        // Get all participants for this event with relationships
        $query = \App\Models\EventParticipant::with(['person', 'team', 'role'])
            ->where('event_id', $id);

        // Apply filters
        if ($request->filled('search')) {
            $query->whereHas('person', function($q) use ($request) {
                $q->where('name', 'like', '%' . $request->search . '%');
            });
        }

        if ($request->filled('team_id')) {
            $query->where('team_id', $request->team_id);
        }

        if ($request->filled('role_id')) {
            $query->where('role_id', $request->role_id);
        }

        // Apply sorting
        $sortBy = $request->get('sort_by', 'registered_at');
        $sortOrder = $request->get('sort_order', 'desc');
        
        switch ($sortBy) {
            case 'person_name':
                $query->join('people', 'event_participants.person_id', '=', 'people.id')
                      ->orderBy('people.name', $sortOrder)
                      ->select('event_participants.*');
                break;
            case 'team':
                $query->leftJoin('teams', 'event_participants.team_id', '=', 'teams.id')
                      ->orderBy('teams.name', $sortOrder)
                      ->select('event_participants.*');
                break;
            case 'role':
                $query->leftJoin('roles', 'event_participants.role_id', '=', 'roles.id')
                      ->orderBy('roles.name', $sortOrder)
                      ->select('event_participants.*');
                break;
            case 'registered_at':
            default:
                $query->orderBy('registered_at', $sortOrder);
                break;
        }

        $participants = $query->get();

        // Calculate statistics
        $totalParticipants = \App\Models\EventParticipant::where('event_id', $id)->count();
        $participantsWithTeam = \App\Models\EventParticipant::where('event_id', $id)->whereNotNull('team_id')->count();
        $participantsWithRole = \App\Models\EventParticipant::where('event_id', $id)->whereNotNull('role_id')->count();
        $teamsCount = \App\Models\EventParticipant::where('event_id', $id)
            ->whereNotNull('team_id')
            ->distinct('team_id')
            ->count('team_id');

        // Get teams and roles for filters
        $teams = \App\Models\Team::orderBy('name')->get();
        $roles = \App\Models\Role::orderBy('name')->get();

        return view('events.show', compact(
            'event', 
            'participants', 
            'totalParticipants',
            'participantsWithTeam',
            'participantsWithRole',
            'teamsCount',
            'teams',
            'roles'
        ));
    }


    public function edit(int $id)
    {
        $event = $this->service->getById($id);
        if (!$event) {
            return redirect()->route('events.index')->with('error', 'Evento não encontrado.');
        }
        return view('events.edit', compact('event'));
    }

    public function update(UpdateEventRequest $request, int $id)
    {
        $event = $this->service->getById($id);
        if (!$event) {
            return redirect()->route('events.index')->with('error', 'Evento não encontrado.');
        }

        $this->service->update($event, $request->validated(), $request->file('photo'));

        return redirect()->route('events.index')->with('success', "Evento '{$event->name}' atualizado com sucesso!");
    }

    public function delete(int $id)
    {
        $event = $this->service->getById($id);
        if (!$event) {
            return redirect()->route('events.index')->with('error', 'Evento não encontrado.');
        }

        return view('events.delete', compact('event'));
    }

    public function destroy(int $id)
    {
        try {
            $event = $this->service->getById($id);
            $name = $event ? $event->name : 'Evento';
            
            $this->service->delete($id);
            
            return redirect()->route('events.index')->with('success', "{$name} excluído com sucesso!");
        } catch (\Exception $e) {
            return redirect()->route('events.index')->with('error', $e->getMessage());
        }
    }
}
