<?php

namespace App\Services;

use App\Models\Person;
use App\Repositories\PersonRepository;
use Illuminate\Http\UploadedFile;
use Illuminate\Support\Facades\Storage;

class PersonService
{
    protected PersonRepository $repository;

    public function __construct(PersonRepository $repository)
    {
        $this->repository = $repository;
    }

    public function getAll()
    {
        return $this->repository->getAll();
    }

    public function getById(int $id)
    {
        return $this->repository->getById($id);
    }

    public function create(array $data, ?UploadedFile $photo = null): Person
    {
        if ($photo) {
            $data['photo_url'] = $photo->store('people', 'public');
        }

        return $this->repository->create($data);
    }

    public function update(Person $person, array $data, ?UploadedFile $photo = null): Person
    {
        if ($photo) {
            if ($person->photo_url) {
                Storage::disk('public')->delete($person->photo_url);
            }
            $data['photo_url'] = $photo->store('people', 'public');
        }

        return $this->repository->update($person, $data);
    }

    public function delete(int $id): bool
    {
        $person = $this->repository->getById($id);
        
        if (!$person) {
            return false;
        }

        // Soft delete doesn't need image deletion usually, matching .NET logic
        return $this->repository->delete($person);
    }

    public function search(string $term)
    {
        return $this->repository->search($term);
    }
}
