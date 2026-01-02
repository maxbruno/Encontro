@extends('layouts.app')

@section('title', 'Dashboard - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row mb-4">
            <div class="col">
                <h1 class="display-5">
                    <i class="bi bi-speedometer2"></i> Dashboard
                </h1>
                <p class="text-muted">Bem-vindo ao Sistema de Gestão de Encontros</p>
            </div>
        </div>

        {{-- Quick Access Cards --}}
        <div class="row g-4 mb-4">
            {{-- New Event Card --}}
            <div class="col-md-4">
                <div class="card border-primary shadow-sm h-100">
                    <div class="card-body text-center">
                        <div class="display-1 text-primary mb-3">
                            <i class="bi bi-calendar-plus"></i>
                        </div>
                        <h5 class="card-title">Novo Evento</h5>
                        <p class="card-text text-muted">Criar um novo encontro ou retiro</p>
                        <a href="{{ route('events.create') }}" class="btn btn-primary btn-lg">
                            <i class="bi bi-plus-circle"></i> Criar Evento
                        </a>
                    </div>
                </div>
            </div>

            {{-- New Person Card --}}
            <div class="col-md-4">
                <div class="card border-success shadow-sm h-100">
                    <div class="card-body text-center">
                        <div class="display-1 text-success mb-3">
                            <i class="bi bi-person-plus"></i>
                        </div>
                        <h5 class="card-title">Nova Pessoa</h5>
                        <p class="card-text text-muted">Cadastrar novo participante</p>
                        <a href="{{ route('people.create') }}" class="btn btn-success btn-lg">
                            <i class="bi bi-plus-circle"></i> Cadastrar Pessoa
                        </a>
                    </div>
                </div>
            </div>

            {{-- Active Events Card --}}
            <div class="col-md-4">
                <div class="card border-info shadow-sm h-100">
                    <div class="card-body text-center">
                        <div class="display-1 text-info mb-3">
                            <i class="bi bi-calendar-event"></i>
                        </div>
                        <h5 class="card-title">Eventos</h5>
                        <p class="card-text text-muted">Visualizar e gerenciar eventos</p>
                        <a href="{{ route('events.index') }}" class="btn btn-info btn-lg">
                            <i class="bi bi-list"></i> Ver Eventos
                        </a>
                    </div>
                </div>
            </div>
        </div>

        {{-- Quick Access and Information Section --}}
        <div class="row">
            {{-- Quick Access --}}
            <div class="col-md-6">
                <div class="card shadow-sm">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">
                            <i class="bi bi-lightning"></i> Acesso Rápido
                        </h5>
                    </div>
                    <div class="card-body">
                        <div class="list-group list-group-flush">
                            <a href="{{ route('events.index') }}" class="list-group-item list-group-item-action">
                                <i class="bi bi-calendar-event text-primary"></i> Lista de Eventos
                            </a>
                            <a href="{{ route('people.index') }}" class="list-group-item list-group-item-action">
                                <i class="bi bi-people text-success"></i> Lista de Pessoas
                            </a>
                        </div>
                    </div>
                </div>
            </div>

            {{-- Information --}}
            <div class="col-md-6">
                <div class="card shadow-sm">
                    <div class="card-header bg-light">
                        <h5 class="mb-0">
                            <i class="bi bi-info-circle"></i> Informações
                        </h5>
                    </div>
                    <div class="card-body">
                        <p class="mb-2">
                            <strong>Total de Eventos:</strong> 
                            <span class="badge bg-primary">{{ $totalEvents }}</span>
                        </p>
                        <p class="mb-2">
                            <strong>Total de Pessoas:</strong> 
                            <span class="badge bg-success">{{ $totalPeople }}</span>
                        </p>
                        <p class="mb-0">
                            <strong>Total de Participações:</strong> 
                            <span class="badge bg-info">{{ $totalParticipants }}</span>
                        </p>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
