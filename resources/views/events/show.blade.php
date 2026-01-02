@extends('layouts.app')

@section('title', 'Detalhes do Evento - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-12">
                {{-- Success Message --}}
                @if (session('success'))
                    <div class="alert alert-success alert-dismissible fade show" role="alert">
                        <i class="bi bi-check-circle-fill"></i> {{ session('success') }}
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                @endif

                {{-- Event Details Card --}}
                <div class="card mb-4">
                    <div class="card-header bg-primary text-white">
                        <div class="d-flex justify-content-between align-items-center">
                            <h4 class="mb-0">
                                <i class="bi bi-calendar-event-fill"></i> {{ $event->name }}
                            </h4>
                            <div>
                                @if ($event->event_type == 1)
                                    <span class="badge bg-light text-primary fs-6">Segue-me</span>
                                @else
                                    <span class="badge bg-light text-success fs-6">ECC</span>
                                @endif
                            </div>
                        </div>
                    </div>
                    <div class="card-body">
                        <div class="row">
                            <div class="col-md-6">
                                <p class="mb-2">
                                    <i class="bi bi-calendar-plus text-muted"></i>
                                    <strong>Criado em:</strong> {{ $event->created_at->format('d/m/Y H:i') }}
                                </p>
                            </div>
                            <div class="col-md-6">
                                <p class="mb-2">
                                    <i class="bi bi-people-fill text-muted"></i>
                                    <strong>Participantes:</strong> 
                                    <span class="badge bg-info">{{ $totalParticipants }}</span>
                                </p>
                            </div>
                        </div>
                        @if ($event->patron_saint_name || $event->patron_saint_image_url)
                            <div class="row mt-3">
                                <div class="col-md-12">
                                    <div class="alert alert-light border" role="alert">
                                        <div class="d-flex align-items-center">
                                            @if ($event->patron_saint_image_url)
                                                <div class="me-3">
                                                    <img src="{{ asset('storage/' . $event->patron_saint_image_url) }}" 
                                                         alt="{{ $event->patron_saint_name }}" 
                                                         class="rounded" 
                                                         style="width: 80px; height: 80px; object-fit: cover;">
                                                </div>
                                            @endif
                                            <div>
                                                <h6 class="mb-0">
                                                    <i class="bi bi-star-fill text-warning"></i> 
                                                    <strong>Santo Padroeiro:</strong>
                                                </h6>
                                                <p class="mb-0 text-muted">{{ $event->patron_saint_name }}</p>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        @endif
                        <div class="mt-3">
                            @admin
                                <a href="{{ route('events.edit', $event->id) }}" class="btn btn-warning">
                                    <i class="bi bi-pencil"></i> Editar Evento
                                </a>
                            @endadmin
                            <a href="{{ route('events.index') }}" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Voltar à Lista
                            </a>
                        </div>
                    </div>
                </div>

                {{-- Statistics Cards --}}
                <div class="row mb-4">
                    <div class="col-md-3">
                        <div class="card text-center border-primary">
                            <div class="card-body">
                                <h3 class="text-primary">{{ $totalParticipants }}</h3>
                                <p class="mb-0 text-muted">Total de Participantes</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card text-center border-success">
                            <div class="card-body">
                                <h3 class="text-success">{{ $participantsWithTeam }}</h3>
                                <p class="mb-0 text-muted">Com Equipe</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card text-center border-warning">
                            <div class="card-body">
                                <h3 class="text-warning">{{ $participantsWithRole }}</h3>
                                <p class="mb-0 text-muted">Com Função</p>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-3">
                        <div class="card text-center border-info">
                            <div class="card-body">
                                <h3 class="text-info">{{ $teamsCount }}</h3>
                                <p class="mb-0 text-muted">Equipes Ativas</p>
                            </div>
                        </div>
                    </div>
                </div>

                {{-- Participants Section --}}
                <div class="card">
                    <div class="card-header bg-light">
                        <div class="d-flex justify-content-between align-items-center">
                            <h5 class="mb-0">
                                <i class="bi bi-people-fill"></i> Participantes
                                <span class="badge bg-secondary ms-2">Mostrando {{ $participants->count() }} de {{ $totalParticipants }}</span>
                            </h5>
                            @admin
                                <a href="{{ route('participants.create', $event->id) }}" class="btn btn-primary btn-sm">
                                    <i class="bi bi-plus-circle"></i> Adicionar Participante
                                </a>
                            @endadmin
                        </div>
                    </div>
                    <div class="card-body">
                        {{-- Filters --}}
                        <div class="card mb-3">
                            <div class="card-header bg-light">
                                <h6 class="mb-0"><i class="bi bi-funnel"></i> Filtros e Busca</h6>
                            </div>
                            <div class="card-body">
                                <form method="GET" action="{{ route('events.show', $event->id) }}" class="row g-3">
                                    <div class="col-md-4">
                                        <label for="search" class="form-label">Buscar por Nome</label>
                                        <div class="input-group">
                                            <span class="input-group-text"><i class="bi bi-search"></i></span>
                                            <input type="text" class="form-control" id="search" name="search" 
                                                   value="{{ request('search') }}" placeholder="Digite o nome...">
                                        </div>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="team_id" class="form-label">Filtrar por Equipe</label>
                                        <select class="form-select" id="team_id" name="team_id">
                                            <option value="">Todas as Equipes</option>
                                            @foreach($teams as $team)
                                                <option value="{{ $team->id }}" {{ request('team_id') == $team->id ? 'selected' : '' }}>
                                                    {{ $team->name }}
                                                </option>
                                            @endforeach
                                        </select>
                                    </div>
                                    <div class="col-md-3">
                                        <label for="role_id" class="form-label">Filtrar por Função</label>
                                        <select class="form-select" id="role_id" name="role_id">
                                            <option value="">Todas as Funções</option>
                                            @foreach($roles as $role)
                                                <option value="{{ $role->id }}" {{ request('role_id') == $role->id ? 'selected' : '' }}>
                                                    {{ $role->name }}
                                                </option>
                                            @endforeach
                                        </select>
                                    </div>
                                    <div class="col-md-2 d-flex align-items-end">
                                        <div class="d-grid gap-2 w-100">
                                            <button type="submit" class="btn btn-primary">
                                                <i class="bi bi-search"></i> Buscar
                                            </button>
                                            <a href="{{ route('events.show', $event->id) }}" class="btn btn-secondary">
                                                <i class="bi bi-x-circle"></i> Limpar
                                            </a>
                                        </div>
                                    </div>
                                </form>
                            </div>
                        </div>

                        {{-- Participants Table --}}
                        <div class="table-responsive">
                            <table class="table table-striped table-hover">
                                <thead class="table-dark">
                                    <tr>
                                        <th style="width: 80px;">Foto</th>
                                        <th>
                                            <a href="{{ route('events.show', array_merge(['event' => $event->id], request()->except(['sort_by', 'sort_order']), ['sort_by' => 'person_name', 'sort_order' => (request('sort_by') == 'person_name' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                               class="text-decoration-none text-white">
                                                Nome
                                                @if(request('sort_by') == 'person_name')
                                                    <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                                @endif
                                            </a>
                                        </th>
                                        <th style="width: 150px;">
                                            <a href="{{ route('events.show', array_merge(['event' => $event->id], request()->except(['sort_by', 'sort_order']), ['sort_by' => 'team', 'sort_order' => (request('sort_by') == 'team' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                               class="text-decoration-none text-white">
                                                Equipe
                                                @if(request('sort_by') == 'team')
                                                    <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                                @endif
                                            </a>
                                        </th>
                                        <th style="width: 180px;">
                                            <a href="{{ route('events.show', array_merge(['event' => $event->id], request()->except(['sort_by', 'sort_order']), ['sort_by' => 'role', 'sort_order' => (request('sort_by') == 'role' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                               class="text-decoration-none text-white">
                                                Função
                                                @if(request('sort_by') == 'role')
                                                    <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                                @endif
                                            </a>
                                        </th>
                                        <th style="width: 140px;">
                                            <a href="{{ route('events.show', array_merge(['event' => $event->id], request()->except(['sort_by', 'sort_order']), ['sort_by' => 'registered_at', 'sort_order' => (request('sort_by') == 'registered_at' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                               class="text-decoration-none text-white">
                                                Data Inscrição
                                                @if(request('sort_by') == 'registered_at')
                                                    <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                                @endif
                                            </a>
                                        </th>
                                        <th style="width: 80px;">Etapa</th>
                                        <th style="width: 100px;">Status</th>
                                        <th style="width: 180px;">Observações</th>
                                        <th style="width: 130px;">Ações</th>
                                    </tr>
                                </thead>
                                <tbody>
                                    @forelse($participants as $participant)
                                        <tr>
                                            <td>
                                                @if($participant->person->photo_url)
                                                    <img src="{{ asset('storage/' . $participant->person->photo_url) }}" 
                                                         alt="{{ $participant->person->name }}" 
                                                         class="rounded-circle" 
                                                         style="width: 50px; height: 50px; object-fit: cover;"
                                                         onerror="this.style.display='none'; this.nextElementSibling.style.display='flex';">
                                                    <div class="rounded-circle bg-secondary align-items-center justify-content-center text-white" 
                                                         style="width: 50px; height: 50px; display: none;">
                                                        <i class="bi bi-person-fill"></i>
                                                    </div>
                                                @else
                                                    <div class="rounded-circle bg-secondary d-flex align-items-center justify-content-center text-white" 
                                                         style="width: 50px; height: 50px;">
                                                        <i class="bi bi-person-fill"></i>
                                                    </div>
                                                @endif
                                            </td>
                                            <td class="align-middle">{{ $participant->person->name }}</td>
                                            <td class="align-middle">
                                                @if($participant->team)
                                                    <span class="badge bg-info">{{ $participant->team->name }}</span>
                                                @else
                                                    <span class="badge bg-secondary">Sem Equipe</span>
                                                @endif
                                            </td>
                                            <td class="align-middle">
                                                @if($participant->role)
                                                    <span class="badge bg-warning text-dark">{{ $participant->role->name }}</span>
                                                @else
                                                    <span class="badge bg-secondary">Sem Função</span>
                                                @endif
                                            </td>
                                            <td class="align-middle">{{ $participant->registered_at->format('d/m/Y H:i') }}</td>
                                            <td class="align-middle">
                                                <span class="badge bg-primary">{{ $participant->stage }}</span>
                                            </td>
                                            <td class="align-middle">
                                                @if($participant->indicated !== null)
                                                    @if($participant->indicated)
                                                        <span class="badge bg-primary" title="Indicado"><i class="bi bi-person-check"></i></span>
                                                    @endif
                                                @endif
                                                @if($participant->accepted !== null)
                                                    @if($participant->accepted)
                                                        <span class="badge bg-success" title="Aceitou"><i class="bi bi-check-circle-fill"></i></span>
                                                    @else
                                                        <span class="badge bg-danger" title="Recusou"><i class="bi bi-x-circle-fill"></i></span>
                                                    @endif
                                                @endif
                                            </td>
                                            <td class="align-middle">
                                                @if($participant->notes)
                                                    @if(strlen($participant->notes) > 50)
                                                        <span title="{{ $participant->notes }}">{{ substr($participant->notes, 0, 50) }}...</span>
                                                    @else
                                                        {{ $participant->notes }}
                                                    @endif
                                                @endif
                                            </td>
                                            <td class="align-middle">
                                                @admin
                                                    <a href="{{ route('participants.edit', $participant->id) }}" class="btn btn-warning btn-sm">
                                                        <i class="bi bi-pencil"></i>
                                                    </a>
                                                    <a href="{{ route('participants.delete', $participant->id) }}" class="btn btn-danger btn-sm">
                                                        <i class="bi bi-trash"></i>
                                                    </a>
                                                @else
                                                    <span class="text-muted">-</span>
                                                @endadmin
                                            </td>
                                        </tr>
                                    @empty
                                        <tr>
                                            <td colspan="9" class="text-center text-muted py-4">
                                                @if($totalParticipants > 0)
                                                    <i class="bi bi-search"></i> Nenhum participante encontrado com os filtros aplicados.
                                                @else
                                                    <i class="bi bi-people"></i> Nenhum participante inscrito neste evento.
                                                @endif
                                            </td>
                                        </tr>
                                    @endforelse
                                </tbody>
                            </table>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection

@section('scripts')
<script>
    // Auto-hide success message after 5 seconds
    setTimeout(function() {
        var alert = document.querySelector('.alert-success');
        if (alert) {
            var bsAlert = new bootstrap.Alert(alert);
            bsAlert.close();
        }
    }, 5000);
</script>
@endsection
