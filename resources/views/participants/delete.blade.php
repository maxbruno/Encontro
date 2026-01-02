@extends('layouts.app')

@section('title', 'Remover Participante - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <h1 class="mb-4 text-danger">
                    <i class="bi bi-exclamation-triangle"></i> Remover Participante
                </h1>

                <div class="alert alert-warning">
                    <strong>Atenção:</strong> Ao confirmar, esta participação será <strong>inativada</strong>. 
                    Os dados serão mantidos no histórico, mas o participante não aparecerá mais nas listagens do evento.
                </div>

                <div class="card">
                    <div class="card-body">
                        <dl class="row">
                            <dt class="col-sm-3">Evento</dt>
                            <dd class="col-sm-9">
                                {{ $event->name }}
                                @if($event->event_type == 1)
                                    <span class="badge bg-primary ms-2">Segue-me</span>
                                @else
                                    <span class="badge bg-success ms-2">ECC</span>
                                @endif
                            </dd>

                            <dt class="col-sm-3">Pessoa</dt>
                            <dd class="col-sm-9">
                                <div class="d-flex align-items-center">
                                    @if($person->photo_url)
                                        <img src="{{ asset('storage/' . $person->photo_url) }}" 
                                             alt="{{ $person->name }}" 
                                             class="rounded-circle me-3" 
                                             style="width: 50px; height: 50px; object-fit: cover;">
                                    @else
                                        <div class="rounded-circle bg-secondary d-flex align-items-center justify-content-center text-white me-3" 
                                             style="width: 50px; height: 50px;">
                                            <i class="bi bi-person-fill"></i>
                                        </div>
                                    @endif
                                    <strong>{{ $person->name }}</strong>
                                </div>
                            </dd>

                            <dt class="col-sm-3">Equipe</dt>
                            <dd class="col-sm-9">
                                @if($participant->team)
                                    <span class="badge bg-info">{{ $participant->team->order }} - {{ $participant->team->name }}</span>
                                @else
                                    <span class="badge bg-secondary">Sem Equipe</span>
                                @endif
                            </dd>

                            <dt class="col-sm-3">Função</dt>
                            <dd class="col-sm-9">
                                @if($participant->role)
                                    <span class="badge bg-warning text-dark">{{ $participant->role->order }} - {{ $participant->role->name }}</span>
                                @else
                                    <span class="badge bg-secondary">Sem Função</span>
                                @endif
                            </dd>

                            <dt class="col-sm-3">Data de Inscrição</dt>
                            <dd class="col-sm-9">{{ $participant->registered_at->format('d/m/Y H:i') }}</dd>

                            <dt class="col-sm-3">Etapa</dt>
                            <dd class="col-sm-9"><span class="badge bg-primary">Etapa {{ $participant->stage }}</span></dd>

                            @if($participant->notes)
                                <dt class="col-sm-3">Observações</dt>
                                <dd class="col-sm-9">{{ $participant->notes }}</dd>
                            @endif
                        </dl>

                        <form method="POST" action="{{ route('participants.destroy', $participant->id) }}">
                            @csrf
                            @method('DELETE')
                            <div class="mt-4">
                                <button type="submit" class="btn btn-danger">
                                    <i class="bi bi-trash"></i> Confirmar Exclusão
                                </button>
                                <a href="{{ route('events.show', $participant->event_id) }}" class="btn btn-secondary">
                                    <i class="bi bi-x-circle"></i> Cancelar
                                </a>
                            </div>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
