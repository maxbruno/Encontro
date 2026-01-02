<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class EventParticipant extends Model
{
    use HasFactory, SoftDeletes;

    protected $fillable = [
        'event_id',
        'person_id',
        'team_id',
        'role_id',
        'registered_at',
        'stage',
        'notes',
        'indicated',
        'accepted',
        'is_deleted',
        'deleted_by',
    ];

    protected $casts = [
        'registered_at' => 'datetime',
        'indicated' => 'boolean',
        'accepted' => 'boolean',
        'is_deleted' => 'boolean',
    ];

    public function event()
    {
        return $this->belongsTo(Event::class);
    }

    public function person()
    {
        return $this->belongsTo(Person::class);
    }

    public function team()
    {
        return $this->belongsTo(Team::class);
    }

    public function role()
    {
        return $this->belongsTo(Role::class);
    }
}
