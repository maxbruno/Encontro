@extends('layouts.app')

@section('title', 'Excluir Evento - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <h1 class="mb-4">Excluir Evento</h1>

                <div class="alert alert-danger" role="alert">
                    <h5 class="alert-heading">Confirmar exclusão</h5>
                    <p>Tem certeza que deseja excluir este evento?</p>
                </div>

                <div class="card">
                    <div class="card-body">
                        <dl class="row">
                            <dt class="col-sm-3">Nome</dt>
                            <dd class="col-sm-9">{{ $event->name }}</dd>

                            <dt class="col-sm-3">Tipo</dt>
                            <dd class="col-sm-9">
                                @if($event->event_type == 1)
                                    <span class="badge bg-primary">Segue-me</span>
                                @else
                                    <span class="badge bg-success">ECC</span>
                                @endif
                            </dd>

                            <dt class="col-sm-3">Criado em</dt>
                            <dd class="col-sm-9">{{ $event->created_at->format('d/m/Y H:i:s') }}</dd>
                        </dl>

                        <form method="POST" action="{{ route('events.destroy', $event->id) }}">
                            @csrf
                            @method('DELETE')
                            <button type="submit" class="btn btn-danger">
                                <i class="bi bi-trash"></i> Confirmar Exclusão
                            </button>
                            <a href="{{ route('events.index') }}" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Cancelar
                            </a>
                        </form>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
