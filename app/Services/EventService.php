<?php

namespace App\Services;

use App\Models\Event;
use App\Models\EventParticipant;
use App\Repositories\EventRepository;
use Illuminate\Http\UploadedFile;
use Illuminate\Support\Facades\Storage;
use Exception;

class EventService
{
    protected EventRepository $repository;

    public function __construct(EventRepository $repository)
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

    public function create(array $data, ?UploadedFile $photo = null): Event
    {
        if ($photo) {
            $data['patron_saint_image_url'] = $photo->store('events', 'public');
        }

        return $this->repository->create($data);
    }

    public function update(Event $event, array $data, ?UploadedFile $photo = null): Event
    {
        if ($photo) {
            if ($event->patron_saint_image_url) {
                Storage::disk('public')->delete($event->patron_saint_image_url);
            }
            $data['patron_saint_image_url'] = $photo->store('events', 'public');
        }

        return $this->repository->update($event, $data);
    }

    public function delete(int $id): bool
    {
        $event = $this->repository->getById($id);
        
        if (!$event) {
            return false;
        }

        // Check for participants
        $hasParticipants = EventParticipant::where('event_id', $id)->exists();
        if ($hasParticipants) {
             throw new Exception("Não é possível excluir evento com participante(s). Remova todos os participantes do evento primeiro.");
        }

        if ($event->patron_saint_image_url) {
            Storage::disk('public')->delete($event->patron_saint_image_url);
        }

        return $this->repository->delete($event);
    }
}
