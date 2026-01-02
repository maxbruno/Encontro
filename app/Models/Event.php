<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class Event extends Model
{
    use HasFactory, SoftDeletes;

    protected $fillable = [
        'name',
        'event_type',
        'patron_saint_name',
        'patron_saint_image_url',
        'is_deleted',
        'deleted_by',
    ];

    protected $casts = [
        'event_type' => 'integer',
        'is_deleted' => 'boolean',
    ];

    /**
     * Get the participants for the event.
     */
    public function participants()
    {
        return $this->hasMany(EventParticipant::class);
    }
}
