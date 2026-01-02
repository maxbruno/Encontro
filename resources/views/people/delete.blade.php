@extends('layouts.app')

@section('title', 'Excluir Pessoa - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-10 offset-md-1">
                <h1 class="mb-4 text-danger">Excluir Pessoa</h1>

                <div class="alert alert-warning" role="alert">
                    <h4 class="alert-heading">
                        <i class="bi bi-exclamation-triangle"></i> Atenção!
                    </h4>
                    <p class="mb-0">Ao confirmar, esta pessoa será <strong>inativada</strong>. Os dados e vínculos com eventos serão mantidos, mas a pessoa não aparecerá mais nas listagens.</p>
                </div>

                {{-- Card de Eventos Vinculados --}}
                @if($eventParticipations->count() > 0)
                    <div class="card mb-3 border-info">
                        <div class="card-header bg-info text-white">
                            <h5 class="mb-0">
                                <i class="bi bi-calendar-event"></i> 
                                Participações em Eventos ({{ $eventParticipations->count() }})
                            </h5>
                        </div>
                        <div class="card-body">
                            <p class="text-muted mb-3">
                                Esta pessoa está inscrita nos seguintes eventos. Ao inativar, os vínculos serão mantidos no histórico.
                            </p>
                            <div class="list-group">
                                @foreach($eventParticipations as $participation)
                                    <div class="list-group-item">
                                        <div class="d-flex justify-content-between align-items-center">
                                            <div>
                                                <h6 class="mb-1">{{ $participation->event->name }}</h6>
                                                <small class="text-muted">
                                                    <i class="bi bi-calendar"></i> {{ $participation->registered_at->format('d/m/Y') }}
                                                    @if($participation->team)
                                                        <span class="ms-2">
                                                            <i class="bi bi-people"></i> {{ $participation->team->name }}
                                                        </span>
                                                    @endif
                                                    @if($participation->role)
                                                        <span class="ms-2">
                                                            <i class="bi bi-briefcase"></i> {{ $participation->role->name }}
                                                        </span>
                                                    @endif
                                                </small>
                                            </div>
                                            <a href="{{ route('events.show', $participation->event_id) }}" 
                                               class="btn btn-sm btn-outline-primary">
                                                <i class="bi bi-eye"></i> Ver Evento
                                            </a>
                                        </div>
                                    </div>
                                @endforeach
                            </div>
                        </div>
                    </div>
                @endif

                <div class="card">
                    <div class="card-body">
                        @if($person->photo_url)
                            <div class="text-center mb-4">
                                <img src="{{ asset('storage/' . $person->photo_url) }}" 
                                     alt="Foto de {{ $person->name }}" 
                                     class="img-thumbnail" 
                                     style="max-width: 200px; max-height: 200px; object-fit: cover;">
                            </div>
                        @endif

                        <dl class="row">
                            <dt class="col-sm-3">Nome</dt>
                            <dd class="col-sm-9">{{ $person->name }}</dd>

                            <dt class="col-sm-3">Tipo</dt>
                            <dd class="col-sm-9">
                                @if($person->type)
                                    <span class="badge bg-info">{{ $person->type }}</span>
                                @else
                                    -
                                @endif
                            </dd>

                            <dt class="col-sm-3">Data de Nascimento</dt>
                            <dd class="col-sm-9">
                                {{ $person->birth_date ? $person->birth_date->format('d/m/Y') : '-' }}
                            </dd>

                            <dt class="col-sm-3">Celular</dt>
                            <dd class="col-sm-9">{{ $person->cell_phone ?? '-' }}</dd>

                            <dt class="col-sm-3">Telefone</dt>
                            <dd class="col-sm-9">{{ $person->phone ?? '-' }}</dd>

                            <dt class="col-sm-3">Email</dt>
                            <dd class="col-sm-9">{{ $person->email ?? '-' }}</dd>

                            <dt class="col-sm-3">Núcleo</dt>
                            <dd class="col-sm-9">{{ $person->group ?? '-' }}</dd>
                        </dl>

                        <form method="POST" action="{{ route('people.destroy', $person->id) }}">
                            @csrf
                            @method('DELETE')
                            <div class="mt-4">
                                <button type="submit" class="btn btn-warning">
                                    <i class="bi bi-person-x"></i> Confirmar Inativação
                                </button>
                                <a href="{{ route('people.index') }}" class="btn btn-secondary">
                                    <i class="bi bi-arrow-left"></i> Cancelar
                                </a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
