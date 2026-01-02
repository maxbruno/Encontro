<?php

namespace App\Repositories;

use App\Models\EventParticipant;
use Illuminate\Database\Eloquent\Collection;

class EventParticipantRepository
{
    public function getAll(): Collection
    {
        return EventParticipant::with(['event', 'person', 'team', 'role'])
            ->orderBy('registered_at', 'desc')
            ->get();
    }

    public function getById(int $id): ?EventParticipant
    {
        return EventParticipant::with(['event', 'person', 'team', 'role'])->find($id);
    }

    public function getByEventId(int $eventId): Collection
    {
        return EventParticipant::with(['person', 'team', 'role'])
            ->where('event_id', $eventId)
            ->orderBy('registered_at', 'desc')
            ->get();
    }

    public function create(array $data): EventParticipant
    {
        return EventParticipant::create($data);
    }

    public function update(EventParticipant $participant, array $data): EventParticipant
    {
        $participant->update($data);
        return $participant->fresh(['event', 'person', 'team', 'role']);
    }

    public function delete(EventParticipant $participant): bool
    {
        return $participant->delete();
    }

    public function countByEventId(int $eventId): int
    {
        return EventParticipant::where('event_id', $eventId)->count();
    }

    public function existsForEventAndPerson(int $eventId, int $personId): bool
    {
        return EventParticipant::where('event_id', $eventId)
            ->where('person_id', $personId)
            ->exists();
    }
}
