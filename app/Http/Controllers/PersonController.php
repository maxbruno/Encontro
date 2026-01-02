<?php

namespace App\Http\Controllers;

use App\Http\Requests\StorePersonRequest;
use App\Http\Requests\UpdatePersonRequest;
use App\Models\Person;
use App\Services\PersonService;
use Illuminate\Http\Request;

class PersonController extends Controller
{
    protected PersonService $service;

    public function __construct(PersonService $service)
    {
        $this->service = $service;
        // Note: Middleware is applied in routes/web.php
    }

    public function index(Request $request)
    {
        $query = Person::query()->where('is_deleted', false);

        // Apply search filter
        if ($request->has('search')) {
            $search = $request->get('search');
            $query->where(function($q) use ($search) {
                $q->where('name', 'like', '%' . $search . '%')
                  ->orWhere('email', 'like', '%' . $search . '%')
                  ->orWhere('cell_phone', 'like', '%' . $search . '%')
                  ->orWhere('type', 'like', '%' . $search . '%')
                  ->orWhere('group', 'like', '%' . $search . '%');
            });
        }

        // Apply sorting
        $sortBy = $request->get('sort_by', 'name');
        $sortOrder = $request->get('sort_order', 'asc');
        
        // Validate sort parameters
        $allowedSortFields = ['name', 'type', 'email', 'created_at'];
        $sortBy = in_array($sortBy, $allowedSortFields) ? $sortBy : 'name';
        $sortOrder = in_array($sortOrder, ['asc', 'desc']) ? $sortOrder : 'asc';
        
        $people = $query->orderBy($sortBy, $sortOrder)->get();
        
        return view('people.index', compact('people'));
    }

    public function create()
    {
        return view('people.create');
    }

    public function store(StorePersonRequest $request)
    {
        $person = $this->service->create($request->validated(), $request->file('photo'));

        $successMessage = "Pessoa '{$person->name}' cadastrada com sucesso!";

        // Check which button was clicked
        if ($request->input('action') === 'saveAndNew') {
            return redirect()->route('people.create')->with('success', $successMessage);
        }

        // Default: save and return to list
        return redirect()->route('people.index')->with('success', $successMessage);
    }

    public function show(int $id)
    {
        $person = $this->service->getById($id);
        if (!$person) {
            return redirect()->route('people.index')->with('error', 'Pessoa não encontrada.');
        }
        return view('people.show', compact('person'));
    }

    public function edit(int $id)
    {
        $person = $this->service->getById($id);
        if (!$person) {
            return redirect()->route('people.index')->with('error', 'Pessoa não encontrada.');
        }
        return view('people.edit', compact('person'));
    }

    public function update(UpdatePersonRequest $request, int $id)
    {
        $person = $this->service->getById($id);
        if (!$person) {
            return redirect()->route('people.index')->with('error', 'Pessoa não encontrada.');
        }

        $this->service->update($person, $request->validated(), $request->file('photo'));

        return redirect()->route('people.index')->with('success', "Pessoa '{$person->name}' atualizada com sucesso!");
    }

    public function delete(int $id)
    {
        $person = $this->service->getById($id);
        if (!$person) {
            return redirect()->route('people.index')->with('error', 'Pessoa não encontrada.');
        }

        // Get event participations
        $eventParticipations = $person->eventParticipants()
            ->with(['event', 'team', 'role'])
            ->orderBy('registered_at', 'desc')
            ->get();

        return view('people.delete', compact('person', 'eventParticipations'));
    }

    public function destroy(int $id)
    {
        $person = $this->service->getById($id);
        $name = $person ? $person->name : 'Pessoa';
        
        $this->service->delete($id);
        
        return redirect()->route('people.index')->with('success', "{$name} excluída com sucesso!");
    }
}
