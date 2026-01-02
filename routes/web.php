<?php

use Illuminate\Support\Facades\Route;
use App\Http\Controllers\DashboardController;
use App\Http\Controllers\PersonController;
use App\Http\Controllers\EventController;
use App\Http\Controllers\EventParticipantController;
use App\Http\Controllers\Auth\LoginController;

// Authentication Routes
Route::get('/login', [LoginController::class, 'showLoginForm'])->name('login');
Route::post('/login', [LoginController::class, 'login']);
Route::post('/logout', [LoginController::class, 'logout'])->name('logout');

// Protected Routes (require authentication)
Route::middleware(['auth'])->group(function () {
    // Dashboard
    Route::get('/', [DashboardController::class, 'index'])->name('dashboard');

    Route::resource('people', PersonController::class);
    Route::get('people/{id}/delete', [PersonController::class, 'delete'])->name('people.delete');
    
    Route::resource('events', EventController::class);
    Route::get('events/{id}/delete', [EventController::class, 'delete'])->name('events.delete');

    // Event Participants routes
    Route::get('events/{eventId}/participants/create', [EventParticipantController::class, 'create'])->name('participants.create');
    Route::post('participants', [EventParticipantController::class, 'store'])->name('participants.store');
    Route::get('participants/{id}/edit', [EventParticipantController::class, 'edit'])->name('participants.edit');
    Route::put('participants/{id}', [EventParticipantController::class, 'update'])->name('participants.update');
    Route::get('participants/{id}/delete', [EventParticipantController::class, 'delete'])->name('participants.delete');
    Route::delete('participants/{id}', [EventParticipantController::class, 'destroy'])->name('participants.destroy');
});
