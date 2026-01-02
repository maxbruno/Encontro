@extends('layouts.app')

@section('title', 'Eventos - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1><i class="bi bi-calendar-event-fill"></i> Eventos</h1>
            @admin
                <a href="{{ route('events.create') }}" class="btn btn-primary">
                    <i class="bi bi-plus-circle"></i> Novo Evento
                </a>
            @endadmin
        </div>

        <div class="card shadow-sm">
            <div class="card-body">
                {{-- Search Form --}}
                <form action="{{ route('events.index') }}" method="GET" class="mb-4">
                    <div class="row g-3">
                        <div class="col-md-5">
                            <input type="text" name="search" class="form-control" 
                                   placeholder="Buscar por nome do evento ou santo padroeiro..." 
                                   value="{{ request('search') }}">
                        </div>
                        <div class="col-md-3">
                            <select name="event_type" class="form-select">
                                <option value="">Todos os tipos</option>
                                <option value="1" {{ request('event_type') == '1' ? 'selected' : '' }}>Segue-me</option>
                                <option value="2" {{ request('event_type') == '2' ? 'selected' : '' }}>ECC</option>
                            </select>
                        </div>
                        <div class="col-md-2">
                            <button type="submit" class="btn btn-primary w-100">
                                <i class="bi bi-search"></i> Buscar
                            </button>
                        </div>
                        <div class="col-md-2">
                            <a href="{{ route('events.index') }}" class="btn btn-outline-secondary w-100">
                                <i class="bi bi-x-circle"></i> Limpar
                            </a>
                        </div>
                    </div>
                </form>

                {{-- Table --}}
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead class="table-light">
                            <tr>
                                <th>
                                    <a href="{{ route('events.index', array_merge(request()->except(['sort_by', 'sort_order']), ['sort_by' => 'name', 'sort_order' => (request('sort_by') == 'name' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                       class="text-decoration-none text-dark">
                                        Nome
                                        @if(request('sort_by') == 'name')
                                            <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                        @endif
                                    </a>
                                </th>
                                <th>
                                    <a href="{{ route('events.index', array_merge(request()->except(['sort_by', 'sort_order']), ['sort_by' => 'event_type', 'sort_order' => (request('sort_by') == 'event_type' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                       class="text-decoration-none text-dark">
                                        Tipo
                                        @if(request('sort_by') == 'event_type')
                                            <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                        @endif
                                    </a>
                                </th>
                                <th>Santo Padroeiro</th>
                                <th>Participantes</th>
                                <th style="width: 200px;">Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            @forelse($events as $event)
                                <tr>
                                    <td>
                                        <div class="d-flex align-items-center gap-2">
                                            @if($event->patron_saint_image_url)
                                                <img src="{{ asset('storage/' . $event->patron_saint_image_url) }}" 
                                                     alt="{{ $event->name }}" 
                                                     class="rounded" 
                                                     style="width: 40px; height: 40px; object-fit: cover;">
                                            @else
                                                <div class="rounded bg-light d-flex align-items-center justify-content-center text-secondary" 
                                                     style="width: 40px; height: 40px;">
                                                    <i class="bi bi-calendar-event"></i>
                                                </div>
                                            @endif
                                            <span>{{ $event->name }}</span>
                                        </div>
                                    </td>
                                    <td>
                                        @if($event->event_type == 1)
                                            <span class="badge bg-primary">Segue-me</span>
                                        @elseif($event->event_type == 2)
                                            <span class="badge bg-success">ECC</span>
                                        @else
                                            <span class="badge bg-secondary">{{ $event->event_type }}</span>
                                        @endif
                                    </td>
                                    <td>{{ $event->patron_saint_name ?? '-' }}</td>
                                    <td>
                                        <span class="badge bg-info">
                                            {{ $event->participants_count ?? 0 }}
                                        </span>
                                    </td>
                                    <td>
                                        <div class="btn-group" role="group">
                                            <a href="{{ route('events.show', $event->id) }}" 
                                               class="btn btn-sm btn-outline-info" 
                                               title="Ver Detalhes">
                                                <i class="bi bi-eye"></i>
                                            </a>
                                            @admin
                                                <a href="{{ route('events.edit', $event->id) }}" 
                                                   class="btn btn-sm btn-outline-primary" 
                                                   title="Editar">
                                                    <i class="bi bi-pencil"></i>
                                                </a>
                                                <a href="{{ route('events.delete', $event->id) }}" 
                                                   class="btn btn-sm btn-outline-danger" 
                                                   title="Excluir">
                                                    <i class="bi bi-trash"></i>
                                                </a>
                                            @endadmin
                                        </div>
                                    </td>
                                </tr>
                            @empty
                                <tr>
                                    <td colspan="5" class="text-center text-muted py-4">
                                        <i class="bi bi-calendar-x"></i> Nenhum evento encontrado.
                                    </td>
                                </tr>
                            @endforelse
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
@endsection
