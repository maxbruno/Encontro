@extends('layouts.app')

@section('title', 'Editar Participante - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                <h1 class="mb-4"><i class="bi bi-pencil-fill"></i> Editar Participante</h1>

                {{-- Event Info --}}
                <div class="alert alert-info">
                    <i class="bi bi-info-circle"></i>
                    <strong>Evento:</strong> {{ $participant->event->name }}
                    @if ($participant->event->event_type == 1)
                        <span class="badge bg-primary ms-2">Segue-me</span>
                    @else
                        <span class="badge bg-success ms-2">ECC</span>
                    @endif
                </div>

                {{-- Error Message --}}
                @if (session('error'))
                    <div class="alert alert-danger alert-dismissible fade show" role="alert">
                        <i class="bi bi-exclamation-triangle-fill"></i> {{ session('error') }}
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                @endif

                {{-- Validation Errors --}}
                @if ($errors->any())
                    <div class="alert alert-danger" role="alert">
                        <strong>Erro de validação:</strong>
                        <ul class="mb-0">
                            @foreach ($errors->all() as $error)
                                <li>{{ $error }}</li>
                            @endforeach
                        </ul>
                    </div>
                @endif

                <div class="card">
                    <div class="card-body">
                        <form method="POST" action="{{ route('participants.update', $participant->id) }}">
                            @csrf
                            @method('PUT')
                            <input type="hidden" name="event_id" value="{{ $participant->event_id }}">
                            <input type="hidden" name="person_id" value="{{ $participant->person_id }}">

                            {{-- Person Display (Read-only) --}}
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label class="form-label">Pessoa</label>
                                    <div class="d-flex align-items-center">
                                        @if($participant->person->photo_url)
                                            <img src="{{ asset('storage/' . $participant->person->photo_url) }}" 
                                                 alt="{{ $participant->person->name }}" 
                                                 class="rounded-circle me-3" 
                                                 style="width: 50px; height: 50px; object-fit: cover;">
                                        @else
                                            <div class="rounded-circle bg-secondary d-flex align-items-center justify-content-center text-white me-3" 
                                                 style="width: 50px; height: 50px;">
                                                <i class="bi bi-person-fill"></i>
                                            </div>
                                        @endif
                                        <strong>{{ $participant->person->name }}</strong>
                                    </div>
                                    <small class="form-text text-muted">A pessoa não pode ser alterada após a inscrição.</small>
                                </div>
                            </div>

                            {{-- Registered At Display (Read-only) --}}
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label class="form-label">Data de Inscrição</label>
                                    <input type="text" class="form-control" 
                                           value="{{ $participant->registered_at->format('d/m/Y H:i') }}" readonly>
                                </div>
                            </div>

                            {{-- Team --}}
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="team_id" class="form-label">
                                        <i class="bi bi-people"></i> Equipe
                                    </label>
                                    <select name="team_id" id="team_id" class="form-select @error('team_id') is-invalid @enderror">
                                        <option value="">-- Sem Equipe --</option>
                                        @foreach($teams as $team)
                                            <option value="{{ $team->id }}" {{ old('team_id', $participant->team_id) == $team->id ? 'selected' : '' }}>
                                                {{ $team->name }}
                                            </option>
                                        @endforeach
                                    </select>
                                    @error('team_id')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>

                            {{-- Role --}}
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="role_id" class="form-label">
                                        <i class="bi bi-briefcase"></i> Função
                                    </label>
                                    <select name="role_id" id="role_id" class="form-select @error('role_id') is-invalid @enderror">
                                        <option value="">-- Sem Função --</option>
                                        @foreach($roles as $role)
                                            <option value="{{ $role->id }}" {{ old('role_id', $participant->role_id) == $role->id ? 'selected' : '' }}>
                                                {{ $role->name }}
                                            </option>
                                        @endforeach
                                    </select>
                                    @error('role_id')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>

                            {{-- Notes --}}
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="notes" class="form-label">
                                        <i class="bi bi-journal-text"></i> Observações
                                    </label>
                                    <textarea name="notes" id="notes" class="form-control @error('notes') is-invalid @enderror" 
                                              rows="3" maxlength="500" placeholder="Observações adicionais sobre a participação...">{{ old('notes', $participant->notes) }}</textarea>
                                    @error('notes')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <small class="text-muted"><span id="charCount">0</span>/500 caracteres</small>
                                </div>
                            </div>

                            {{-- Stage, Indicated, Accepted --}}
                            <div class="row mb-3">
                                <div class="col-md-4">
                                    <label for="stage" class="form-label">
                                        <i class="bi bi-flag"></i> Etapa <span class="text-danger">*</span>
                                    </label>
                                    <select name="stage" id="stage" class="form-select @error('stage') is-invalid @enderror" required>
                                        <option value="">-- Selecione --</option>
                                        @for($i = 1; $i <= 10; $i++)
                                            <option value="{{ $i }}" {{ old('stage', $participant->stage) == $i ? 'selected' : '' }}>Etapa {{ $i }}</option>
                                        @endfor
                                    </select>
                                    @error('stage')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-4">
                                    <label for="indicated" class="form-label">
                                        <i class="bi bi-person-check"></i> Indicado
                                    </label>
                                    <select name="indicated" id="indicated" class="form-select @error('indicated') is-invalid @enderror">
                                        <option value="">-- Não definido --</option>
                                        <option value="1" {{ old('indicated', $participant->indicated) === 1 || old('indicated', $participant->indicated) === '1' ? 'selected' : '' }}>Sim</option>
                                        <option value="0" {{ old('indicated', $participant->indicated) === 0 || old('indicated', $participant->indicated) === '0' ? 'selected' : '' }}>Não</option>
                                    </select>
                                    @error('indicated')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-4">
                                    <label for="accepted" class="form-label">
                                        <i class="bi bi-check-circle"></i> Aceitou
                                    </label>
                                    <select name="accepted" id="accepted" class="form-select @error('accepted') is-invalid @enderror">
                                        <option value="">-- Não definido --</option>
                                        <option value="1" {{ old('accepted', $participant->accepted) === 1 || old('accepted', $participant->accepted) === '1' ? 'selected' : '' }}>Sim</option>
                                        <option value="0" {{ old('accepted', $participant->accepted) === 0 || old('accepted', $participant->accepted) === '0' ? 'selected' : '' }}>Não</option>
                                    </select>
                                    @error('accepted')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>

                            {{-- Action Buttons --}}
                            <div class="mt-4">
                                <button type="submit" class="btn btn-primary">
                                    <i class="bi bi-check-circle"></i> Salvar Alterações
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

@section('scripts')
<script>
    // Character counter for Notes field
    const notesField = document.getElementById('notes');
    const charCount = document.getElementById('charCount');
    
    if (notesField && charCount) {
        notesField.addEventListener('input', function() {
            charCount.textContent = this.value.length;
        });
        
        // Initialize counter
        charCount.textContent = notesField.value.length;
    }
</script>
@endsection
