<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class Team extends Model
{
    use HasFactory, SoftDeletes;

    protected $fillable = [
        'order',
        'name',
        'is_deleted',
        'deleted_by',
    ];

    protected $casts = [
        'is_deleted' => 'boolean',
    ];

    public function people()
    {
        // This relationship was in Team.cs but likely via EventParticipant? 
        // Original Team.cs had ICollection<Person> People.
        // In clean architecture this often implies a Many-to-Many potentially, 
        // but typically a person belongs to a team in the context of an event.
        // For now, removing direct relation unless there's a pivot or foreign key on Person (which there isn't).
        // It's likely via EventParticipant.
    }
}
