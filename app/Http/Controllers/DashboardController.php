<?php

namespace App\Http\Controllers;

use App\Models\Event;
use App\Models\Person;
use App\Models\EventParticipant;
use Illuminate\Http\Request;

class DashboardController extends Controller
{
    public function index()
    {
        // Get statistics
        $totalEvents = Event::count();
        $totalPeople = Person::count();
        $totalParticipants = EventParticipant::count();

        return view('dashboard', compact('totalEvents', 'totalPeople', 'totalParticipants'));
    }
}
