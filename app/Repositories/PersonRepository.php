<?php

namespace App\Repositories;

use App\Models\Person;
use Illuminate\Database\Eloquent\Collection;

class PersonRepository
{
    public function getAll(): Collection
    {
        return Person::all();
    }

    public function getById(int $id): ?Person
    {
        return Person::find($id);
    }

    public function create(array $data): Person
    {
        return Person::create($data);
    }

    public function update(Person $person, array $data): Person
    {
        $person->update($data);
        return $person;
    }

    public function delete(Person $person): bool
    {
        return $person->delete();
    }

    public function search(string $term): Collection
    {
        return Person::where('name', 'like', "%{$term}%")
            ->orWhere('email', 'like', "%{$term}%")
            ->orWhere('cell_phone', 'like', "%{$term}%")
            ->get();
    }
}
