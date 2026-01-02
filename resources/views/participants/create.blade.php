@extends('layouts.app')

@section('title', 'Adicionar Participante - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-10 offset-md-1">
                {{-- Success Message --}}
                @if (session('success'))
                    <div class="alert alert-success alert-dismissible fade show" role="alert">
                        <i class="bi bi-check-circle-fill"></i> {{ session('success') }}
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                @endif

                {{-- Error Message --}}
                @if (session('error'))
                    <div class="alert alert-danger alert-dismissible fade show" role="alert">
                        <i class="bi bi-exclamation-triangle-fill"></i> {{ session('error') }}
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                @endif

                {{-- Header --}}
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h1><i class="bi bi-person-plus-fill"></i> Adicionar Participante</h1>
                    <a href="{{ route('events.show', $event->id) }}" class="btn btn-outline-secondary">
                        <i class="bi bi-arrow-left"></i> Voltar ao Evento
                    </a>
                </div>

                {{-- Event Info --}}
                <div class="alert alert-info d-flex justify-content-between align-items-center mb-4">
                    <div>
                        <i class="bi bi-calendar-event"></i>
                        <strong>Evento:</strong> {{ $event->name }}
                        @if ($event->event_type == 1)
                            <span class="badge bg-primary ms-2">Segue-me</span>
                        @else
                            <span class="badge bg-success ms-2">ECC</span>
                        @endif
                    </div>
                    @if ($participantCount > 0)
                        <div>
                            <span class="badge bg-secondary">{{ $participantCount }} participante(s) já cadastrado(s)</span>
                        </div>
                    @endif
                </div>

                <form method="POST" action="{{ route('participants.store') }}" id="participantForm">
                    @csrf
                    <input type="hidden" name="event_id" value="{{ $event->id }}">
                    
                    {{-- Validation Summary --}}
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

                    {{-- Card 1: Seleção do Participante --}}
                    <div class="card mb-3">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0"><i class="bi bi-person-badge"></i> Seleção do Participante</h5>
                        </div>
                        <div class="card-body">
                            <div class="row">
                                <div class="col-md-12">
                                    <label for="person_id" class="form-label">
                                        <i class="bi bi-person"></i> Pessoa <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" id="personSearch" class="form-control mb-2" placeholder="🔍 Filtrar pessoas por nome...">
                                    <select name="person_id" id="personSelect" class="form-select @error('person_id') is-invalid @enderror" required>
                                        <option value="">-- Selecione uma pessoa --</option>
                                        @foreach($people as $person)
                                            <option value="{{ $person->id }}" {{ old('person_id') == $person->id ? 'selected' : '' }}>
                                                {{ $person->name }}
                                            </option>
                                        @endforeach
                                    </select>
                                    @error('person_id')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Card 2: Função e Equipe --}}
                    <div class="card mb-3">
                        <div class="card-header bg-success text-white">
                            <h5 class="mb-0"><i class="bi bi-people-fill"></i> Função e Equipe</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label for="team_id" class="form-label">
                                        <i class="bi bi-people"></i> Equipe
                                    </label>
                                    <input type="text" id="teamSearch" class="form-control mb-2" placeholder="🔍 Filtrar equipes...">
                                    <select name="team_id" id="teamSelect" class="form-select @error('team_id') is-invalid @enderror">
                                        <option value="">-- Sem Equipe --</option>
                                        @foreach($teams as $team)
                                            <option value="{{ $team->id }}" {{ old('team_id') == $team->id ? 'selected' : '' }}>
                                                {{ $team->name }}
                                            </option>
                                        @endforeach
                                    </select>
                                    @error('team_id')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-6">
                                    <label for="role_id" class="form-label">
                                        <i class="bi bi-briefcase"></i> Função
                                    </label>
                                    <input type="text" id="roleSearch" class="form-control mb-2" placeholder="🔍 Filtrar funções...">
                                    <select name="role_id" id="roleSelect" class="form-select @error('role_id') is-invalid @enderror">
                                        <option value="">-- Sem Função --</option>
                                        @foreach($roles as $role)
                                            <option value="{{ $role->id }}" {{ old('role_id') == $role->id ? 'selected' : '' }}>
                                                {{ $role->name }}
                                            </option>
                                        @endforeach
                                    </select>
                                    @error('role_id')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <small class="form-text text-muted">
                                        <i class="bi bi-info-circle"></i> Equipes e Funções são opcionais. Deixe em branco se não aplicável.
                                    </small>
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Card 3: Informações Adicionais --}}
                    <div class="card mb-4">
                        <div class="card-header bg-warning">
                            <h5 class="mb-0"><i class="bi bi-info-circle"></i> Informações Adicionais</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="notes" class="form-label">
                                        <i class="bi bi-journal-text"></i> Observações
                                    </label>
                                    <textarea name="notes" id="notes" class="form-control @error('notes') is-invalid @enderror" 
                                              rows="3" maxlength="500" placeholder="Observações adicionais sobre a participação...">{{ old('notes') }}</textarea>
                                    @error('notes')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <small class="text-muted"><span id="charCount">0</span>/500 caracteres</small>
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-4">
                                    <label for="stage" class="form-label">
                                        <i class="bi bi-flag"></i> Etapa <span class="text-danger">*</span>
                                    </label>
                                    <select name="stage" id="stage" class="form-select @error('stage') is-invalid @enderror" required>
                                        <option value="">-- Selecione --</option>
                                        @for($i = 1; $i <= 10; $i++)
                                            <option value="{{ $i }}" {{ old('stage') == $i ? 'selected' : '' }}>Etapa {{ $i }}</option>
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
                                        <option value="1" {{ old('indicated') === '1' ? 'selected' : '' }}>Sim</option>
                                        <option value="0" {{ old('indicated') === '0' ? 'selected' : '' }}>Não</option>
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
                                        <option value="1" {{ old('accepted') === '1' ? 'selected' : '' }}>Sim</option>
                                        <option value="0" {{ old('accepted') === '0' ? 'selected' : '' }}>Não</option>
                                    </select>
                                    @error('accepted')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <div class="alert alert-light mb-0">
                                        <i class="bi bi-calendar-check"></i> <strong>Data de Inscrição:</strong> Será registrada automaticamente como {{ now()->format('d/m/Y H:i') }}
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Action Buttons --}}
                    <div class="card mb-4">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <button type="submit" name="action" value="save" class="btn btn-primary btn-lg">
                                        <i class="bi bi-check-circle"></i> Salvar e Voltar
                                    </button>
                                    <button type="submit" name="action" value="saveAndNew" class="btn btn-success btn-lg">
                                        <i class="bi bi-plus-circle"></i> Salvar e Adicionar Outro
                                    </button>
                                </div>
                                <div>
                                    <a href="{{ route('events.show', $event->id) }}" class="btn btn-secondary btn-lg">
                                        <i class="bi bi-x-circle"></i> Cancelar
                                    </a>
                                </div>
                            </div>
                            <div class="mt-3">
                                <small class="text-muted"><span class="text-danger">*</span> Campos obrigatórios</small>
                            </div>
                        </div>
                    </div>
                </form>
            </div>
        </div>
    </div>
@endsection

@section('scripts')
<script>
    // Generic filter function for select dropdowns
    function setupSelectFilter(searchInputId, selectId, expandedSize) {
        const searchInput = document.getElementById(searchInputId);
        const select = document.getElementById(selectId);
        
        if (!searchInput || !select) return;
        
        searchInput.addEventListener('input', function() {
            const searchTerm = this.value.toLowerCase();
            const options = select.options;
            
            for (let i = 0; i < options.length; i++) {
                const option = options[i];
                const text = option.text.toLowerCase();
                
                if (text.includes(searchTerm) || option.value === '') {
                    option.style.display = '';
                } else {
                    option.style.display = 'none';
                }
            }
            
            // Expand select to show filtered results
            select.size = searchTerm ? expandedSize : 1;
        });
        
        // Collapse select after selection
        select.addEventListener('change', function() {
            if (this.value) {
                this.size = 1;
                searchInput.value = '';
            }
        });
    }

    // Setup filters for all dropdowns
    setupSelectFilter('personSearch', 'personSelect', 8);
    setupSelectFilter('teamSearch', 'teamSelect', 6);
    setupSelectFilter('roleSearch', 'roleSelect', 6);

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

    // Form change detection for unsaved warning
    let formChanged = false;
    const formInputs = document.querySelectorAll('#participantForm input, #participantForm select, #participantForm textarea');
    formInputs.forEach(input => {
        input.addEventListener('change', () => {
            formChanged = true;
        });
    });

    // Warn before leaving with unsaved changes
    window.addEventListener('beforeunload', (e) => {
        if (formChanged) {
            e.preventDefault();
            e.returnValue = '';
        }
    });

    // Don't warn when form is submitted
    document.getElementById('participantForm').addEventListener('submit', () => {
        formChanged = false;
    });
</script>
@endsection
