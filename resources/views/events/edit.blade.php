@extends('layouts.app')

@section('title', 'Editar Evento - Encontro')

@section('content')
    <div class="header">
        <h1 class="page-title">Editar Evento</h1>
        <a href="{{ route('events.index') }}" class="btn" style="background: #e2e8f0;">Voltar</a>
    </div>

    <div class="card" style="max-width: 800px;">
        <form action="{{ route('events.update', $event->id) }}" method="POST" enctype="multipart/form-data">
            @csrf
            @method('PUT')
            
            <div class="form-group">
                <label class="form-label">Nome do Evento *</label>
                <input type="text" name="name" class="form-control" required value="{{ old('name', $event->name) }}">
                @error('name') <span style="color: red; font-size: 0.8em;">{{ $message }}</span> @enderror
            </div>

            <div class="form-group">
                <label class="form-label">Tipo de Evento *</label>
                <select name="event_type" class="form-control" required>
                    <option value="1" {{ $event->event_type == 1 ? 'selected' : '' }}>Segui-me</option>
                    <option value="2" {{ $event->event_type == 2 ? 'selected' : '' }}>ECC</option>
                </select>
            </div>

            <div class="form-group">
                <label class="form-label">Santo Padroeiro</label>
                <input type="text" name="patron_saint_name" class="form-control" value="{{ old('patron_saint_name', $event->patron_saint_name) }}">
            </div>
            
            @if($event->patron_saint_image_url)
            <div style="margin-bottom: 1rem;">
                <img src="{{ asset('storage/' . $event->patron_saint_image_url) }}" alt="" style="height: 100px; border-radius: var(--radius);">
            </div>
            @endif

            <div class="form-group">
                <label class="form-label">Imagem do Padroeiro</label>
                <input type="file" name="photo" class="form-control">
                <small style="color: var(--text-muted);">Deixe em branco para manter a atual</small>
            </div>

            <div style="margin-top: 1.5rem; text-align: right;">
                <button type="submit" class="btn btn-primary">Atualizar Evento</button>
            </div>
        </form>
    </div>
@endsection
