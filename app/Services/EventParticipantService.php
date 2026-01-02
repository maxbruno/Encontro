<?php

namespace App\Services;

use App\Models\EventParticipant;
use App\Repositories\EventParticipantRepository;
use Illuminate\Database\Eloquent\Collection;

class EventParticipantService
{
    protected EventParticipantRepository $repository;

    public function __construct(EventParticipantRepository $repository)
    {
        $this->repository = $repository;
    }

    public function getAll(): Collection
    {
        return $this->repository->getAll();
    }

    public function getById(int $id): ?EventParticipant
    {
        return $this->repository->getById($id);
    }

    public function getByEventId(int $eventId): Collection
    {
        return $this->repository->getByEventId($eventId);
    }

    public function create(array $data): EventParticipant
    {
        // Set registered_at if not provided
        if (!isset($data['registered_at'])) {
            $data['registered_at'] = now();
        }

        // Check if participant already exists for this event
        if ($this->repository->existsForEventAndPerson($data['event_id'], $data['person_id'])) {
            throw new \Exception('Esta pessoa já está cadastrada neste evento.');
        }

        return $this->repository->create($data);
    }

    public function update(EventParticipant $participant, array $data): EventParticipant
    {
        // Check if changing person and if new person already exists for this event
        if (isset($data['person_id']) && $data['person_id'] != $participant->person_id) {
            if ($this->repository->existsForEventAndPerson($participant->event_id, $data['person_id'])) {
                throw new \Exception('Esta pessoa já está cadastrada neste evento.');
            }
        }

        return $this->repository->update($participant, $data);
    }

    public function delete(int $id): bool
    {
        $participant = $this->repository->getById($id);
        
        if (!$participant) {
            return false;
        }

        return $this->repository->delete($participant);
    }

    public function countByEventId(int $eventId): int
    {
        return $this->repository->countByEventId($eventId);
    }
}
