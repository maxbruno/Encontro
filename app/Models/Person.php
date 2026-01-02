<?php

namespace App\Models;

use Illuminate\Database\Eloquent\Factories\HasFactory;
use Illuminate\Database\Eloquent\Model;
use Illuminate\Database\Eloquent\SoftDeletes;

class Person extends Model
{
    use HasFactory, SoftDeletes;

    protected $fillable = [
        'name',
        'type',
        'spouse',
        'birth_date',
        'cell_phone',
        'email',
        'address',
        'phone',
        'district',
        'zip_code',
        'group',
        'father_name',
        'mother_name',
        'notes',
        'photo_url',
        'is_deleted',
        'deleted_by',
    ];

    protected $casts = [
        'birth_date' => 'date',
        'is_deleted' => 'boolean',
    ];

    // Relationships can be defined here if needed
}
