<?php

namespace App\Http\Controllers;

use App\Http\Requests\StoreEventParticipantRequest;
use App\Http\Requests\UpdateEventParticipantRequest;
use App\Models\Event;
use App\Models\Person;
use App\Models\Team;
use App\Models\Role;
use App\Services\EventParticipantService;
use App\Services\EventService;
use Illuminate\Http\Request;

class EventParticipantController extends Controller
{
    protected EventParticipantService $service;
    protected EventService $eventService;

    public function __construct(EventParticipantService $service, EventService $eventService)
    {
        $this->service = $service;
        $this->eventService = $eventService;
        // Note: Role middleware will be enforced via Blade directives and manual checks
    }

    public function create(int $eventId)
    {
        $event = $this->eventService->getById($eventId);
        if (!$event) {
            return redirect()->route('events.index')->with('error', 'Evento não encontrado.');
        }

        $people = Person::orderBy('name')->get();
        $teams = Team::orderBy('name')->get();
        $roles = Role::orderBy('name')->get();
        $participantCount = $this->service->countByEventId($eventId);

        return view('participants.create', compact('event', 'people', 'teams', 'roles', 'participantCount'));
    }

    public function store(StoreEventParticipantRequest $request)
    {
        try {
            $participant = $this->service->create($request->validated());

            $successMessage = "Participante '{$participant->person->name}' adicionado com sucesso!";

            // Check which button was clicked
            if ($request->input('action') === 'saveAndNew') {
                return redirect()
                    ->route('participants.create', $request->event_id)
                    ->with('success', $successMessage);
            }

            // Default: save and return to event details
            return redirect()
                ->route('events.show', $request->event_id)
                ->with('success', $successMessage);
                
        } catch (\Exception $e) {
            return back()
                ->withInput()
                ->with('error', $e->getMessage());
        }
    }

    public function edit(int $id)
    {
        $participant = $this->service->getById($id);
        if (!$participant) {
            return redirect()->route('events.index')->with('error', 'Participante não encontrado.');
        }

        $people = Person::orderBy('name')->get();
        $teams = Team::orderBy('name')->get();
        $roles = Role::orderBy('name')->get();

        return view('participants.edit', compact('participant', 'people', 'teams', 'roles'));
    }

    public function update(UpdateEventParticipantRequest $request, int $id)
    {
        try {
            $participant = $this->service->getById($id);
            if (!$participant) {
                return redirect()->route('events.index')->with('error', 'Participante não encontrado.');
            }

            $this->service->update($participant, $request->validated());

            return redirect()
                ->route('events.show', $participant->event_id)
                ->with('success', "Participante '{$participant->person->name}' atualizado com sucesso!");
                
        } catch (\Exception $e) {
            return back()
                ->withInput()
                ->with('error', $e->getMessage());
        }
    }

    public function delete(int $id)
    {
        $participant = $this->service->getById($id);
        if (!$participant) {
            return redirect()->route('events.index')->with('error', 'Participante não encontrado.');
        }

        // Load relationships
        $participant->load(['person', 'event', 'team', 'role']);
        $event = $participant->event;
        $person = $participant->person;

        return view('participants.delete', compact('participant', 'event', 'person'));
    }

    public function destroy(int $id)
    {
        try {
            $participant = $this->service->getById($id);
            $eventId = $participant ? $participant->event_id : null;
            $name = $participant ? $participant->person->name : 'Participante';
            
            $this->service->delete($id);
            
            if ($eventId) {
                return redirect()
                    ->route('events.show', $eventId)
                    ->with('success', "{$name} removido do evento com sucesso!");
            }
            
            return redirect()
                ->route('events.index')
                ->with('success', "{$name} removido com sucesso!");
                
        } catch (\Exception $e) {
            return back()->with('error', $e->getMessage());
        }
    }
}
