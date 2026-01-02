@extends('layouts.app')

@section('title', 'Editar Pessoa - Encontro')

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

                {{-- Header --}}
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h1><i class="bi bi-pencil-fill"></i> Editar Pessoa</h1>
                    <a href="{{ route('people.index') }}" class="btn btn-outline-secondary">
                        <i class="bi bi-arrow-left"></i> Voltar à Lista
                    </a>
                </div>

                <form method="POST" action="{{ route('people.update', $person->id) }}" enctype="multipart/form-data" id="personForm">
                    @csrf
                    @method('PUT')
                    
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

                    {{-- Card 1: Informações Básicas --}}
                    <div class="card mb-3">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0"><i class="bi bi-person-badge"></i> Informações Básicas</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label for="name" class="form-label">
                                        <i class="bi bi-person"></i> Nome <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" name="name" id="name" class="form-control @error('name') is-invalid @enderror" 
                                           value="{{ old('name', $person->name) }}" required maxlength="150" placeholder="Nome completo">
                                    @error('name')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-3">
                                    <label for="type" class="form-label">
                                        <i class="bi bi-tag"></i> Tipo <span class="text-danger">*</span>
                                    </label>
                                    <select name="type" id="type" class="form-select @error('type') is-invalid @enderror" required>
                                        <option value="">Selecione...</option>
                                        <option value="Casal" {{ old('type', $person->type) == 'Casal' ? 'selected' : '' }}>Casal</option>
                                        <option value="Jovem" {{ old('type', $person->type) == 'Jovem' ? 'selected' : '' }}>Jovem</option>
                                        <option value="Adolescente" {{ old('type', $person->type) == 'Adolescente' ? 'selected' : '' }}>Adolescente</option>
                                        <option value="Individual" {{ old('type', $person->type) == 'Individual' ? 'selected' : '' }}>Individual</option>
                                    </select>
                                    @error('type')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-3">
                                    <label for="birth_date" class="form-label">
                                        <i class="bi bi-calendar-event"></i> Data de Nascimento
                                    </label>
                                    <input type="date" name="birth_date" id="birth_date" class="form-control @error('birth_date') is-invalid @enderror" 
                                           value="{{ old('birth_date', $person->birth_date?->format('Y-m-d')) }}" max="{{ date('Y-m-d') }}">
                                    @error('birth_date')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>

                            <div class="row mb-3" id="spouseRow" style="display: none;">
                                <div class="col-md-12">
                                    <label for="spouse" class="form-label">
                                        <i class="bi bi-heart"></i> Cônjuge <span class="text-danger" id="spouseRequired">*</span>
                                    </label>
                                    <input type="text" name="spouse" id="spouse" class="form-control @error('spouse') is-invalid @enderror" 
                                           value="{{ old('spouse', $person->spouse) }}" maxlength="150" placeholder="Nome do cônjuge">
                                    @error('spouse')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label class="form-label">
                                        <i class="bi bi-camera"></i> Foto
                                    </label>
                                    @if($person->photo_url)
                                        <div class="mb-2">
                                            <img src="{{ asset('storage/' . $person->photo_url) }}" alt="Foto atual" class="img-thumbnail" style="max-width: 200px; max-height: 200px; object-fit: cover;">
                                            <p class="text-muted small mt-1">Foto atual - selecione uma nova para substituir</p>
                                        </div>
                                    @endif
                                    <div class="border rounded p-3" id="dropZone" style="background-color: #f8f9fa; cursor: pointer;">
                                        <input type="file" name="photo" class="form-control d-none" id="photoInput" accept="image/*">
                                        <div class="text-center" id="dropZoneContent">
                                            <div id="photoPreview" class="mb-2" style="display: none;">
                                                <img id="previewImage" src="#" alt="Preview" class="img-thumbnail" 
                                                     style="max-width: 200px; max-height: 200px; object-fit: cover;">
                                            </div>
                                            <div id="dropZoneText">
                                                <i class="bi bi-cloud-upload" style="font-size: 2rem; color: #6c757d;"></i>
                                                <p class="mb-0 mt-2">Clique para selecionar uma nova foto ou arraste aqui</p>
                                                <small class="text-muted">Formatos: JPG, PNG, GIF, BMP (máx. 5MB)</small>
                                            </div>
                                        </div>
                                    </div>
                                    @error('photo')
                                        <span class="text-danger">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Card 2: Contatos --}}
                    <div class="card mb-3">
                        <div class="card-header bg-success text-white">
                            <h5 class="mb-0"><i class="bi bi-telephone"></i> Contatos</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-4">
                                    <label for="cell_phone" class="form-label">
                                        <i class="bi bi-phone"></i> Celular
                                    </label>
                                    <input type="text" name="cell_phone" id="cell_phone" class="form-control @error('cell_phone') is-invalid @enderror" 
                                           value="{{ old('cell_phone', $person->cell_phone) }}" maxlength="20" placeholder="(00) 00000-0000">
                                    @error('cell_phone')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-4">
                                    <label for="phone" class="form-label">
                                        <i class="bi bi-telephone"></i> Telefone
                                    </label>
                                    <input type="text" name="phone" id="phone" class="form-control @error('phone') is-invalid @enderror" 
                                           value="{{ old('phone', $person->phone) }}" maxlength="20" placeholder="(00) 0000-0000">
                                    @error('phone')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-4">
                                    <label for="email" class="form-label">
                                        <i class="bi bi-envelope"></i> Email
                                    </label>
                                    <input type="email" name="email" id="email" class="form-control @error('email') is-invalid @enderror" 
                                           value="{{ old('email', $person->email) }}" maxlength="150" placeholder="exemplo@email.com">
                                    @error('email')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Card 3: Endereço --}}
                    <div class="card mb-3">
                        <div class="card-header bg-info text-white">
                            <h5 class="mb-0"><i class="bi bi-house"></i> Endereço</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-8">
                                    <label for="address" class="form-label">
                                        <i class="bi bi-geo-alt"></i> Endereço
                                    </label>
                                    <input type="text" name="address" id="address" class="form-control @error('address') is-invalid @enderror" 
                                           value="{{ old('address', $person->address) }}" maxlength="200" placeholder="Rua, número, complemento">
                                    @error('address')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-4">
                                    <label for="district" class="form-label">
                                        <i class="bi bi-pin-map"></i> Bairro
                                    </label>
                                    <input type="text" name="district" id="district" class="form-control @error('district') is-invalid @enderror" 
                                           value="{{ old('district', $person->district) }}" maxlength="100" placeholder="Bairro">
                                    @error('district')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-4">
                                    <label for="zip_code" class="form-label">
                                        <i class="bi bi-mailbox"></i> CEP
                                    </label>
                                    <input type="text" name="zip_code" id="zip_code" class="form-control @error('zip_code') is-invalid @enderror" 
                                           value="{{ old('zip_code', $person->zip_code) }}" maxlength="10" placeholder="00000-000">
                                    @error('zip_code')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Card 4: Informações Adicionais --}}
                    <div class="card mb-4">
                        <div class="card-header bg-warning">
                            <h5 class="mb-0"><i class="bi bi-info-circle"></i> Informações Adicionais</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="group" class="form-label">
                                        <i class="bi bi-people"></i> Grupo
                                    </label>
                                    <input type="text" name="group" id="group" class="form-control @error('group') is-invalid @enderror" 
                                           value="{{ old('group', $person->group) }}" maxlength="100" placeholder="Núcleo ou grupo">
                                    @error('group')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                            <div class="row mb-3">
                                <div class="col-md-6">
                                    <label for="father_name" class="form-label">
                                        <i class="bi bi-person"></i> Nome do Pai
                                    </label>
                                    <input type="text" name="father_name" id="father_name" class="form-control @error('father_name') is-invalid @enderror" 
                                           value="{{ old('father_name', $person->father_name) }}" maxlength="150" placeholder="Nome do pai">
                                    @error('father_name')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                                <div class="col-md-6">
                                    <label for="mother_name" class="form-label">
                                        <i class="bi bi-person"></i> Nome da Mãe
                                    </label>
                                    <input type="text" name="mother_name" id="mother_name" class="form-control @error('mother_name') is-invalid @enderror" 
                                           value="{{ old('mother_name', $person->mother_name) }}" maxlength="150" placeholder="Nome da mãe">
                                    @error('mother_name')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-md-12">
                                    <label for="notes" class="form-label">
                                        <i class="bi bi-journal-text"></i> Observações
                                    </label>
                                    <textarea name="notes" id="notes" class="form-control @error('notes') is-invalid @enderror" 
                                              rows="3" maxlength="500" placeholder="Observações gerais...">{{ old('notes', $person->notes) }}</textarea>
                                    @error('notes')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <small class="text-muted">Máximo 500 caracteres</small>
                                </div>
                            </div>
                        </div>
                    </div>

                    {{-- Action Buttons --}}
                    <div class="card mb-4">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <button type="submit" class="btn btn-primary btn-lg">
                                        <i class="bi bi-check-circle"></i> Atualizar Pessoa
                                    </button>
                                </div>
                                <div>
                                    <a href="{{ route('people.index') }}" class="btn btn-secondary btn-lg">
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
    // Photo preview and drag-and-drop (same as create)
    const dropZone = document.getElementById('dropZone');
    const photoInput = document.getElementById('photoInput');
    const photoPreview = document.getElementById('photoPreview');
    const previewImage = document.getElementById('previewImage');
    const dropZoneText = document.getElementById('dropZoneText');

    dropZone.addEventListener('click', () => {
        photoInput.click();
    });

    photoInput.addEventListener('change', function(e) {
        if (this.files && this.files[0]) {
            handleFile(this.files[0]);
        }
    });

    dropZone.addEventListener('dragover', (e) => {
        e.preventDefault();
        dropZone.style.backgroundColor = '#e9ecef';
        dropZone.style.borderColor = '#0d6efd';
    });

    dropZone.addEventListener('dragleave', (e) => {
        e.preventDefault();
        dropZone.style.backgroundColor = '#f8f9fa';
        dropZone.style.borderColor = '#dee2e6';
    });

    dropZone.addEventListener('drop', (e) => {
        e.preventDefault();
        dropZone.style.backgroundColor = '#f8f9fa';
        dropZone.style.borderColor = '#dee2e6';
        
        if (e.dataTransfer.files && e.dataTransfer.files[0]) {
            const file = e.dataTransfer.files[0];
            if (file.type.startsWith('image/')) {
                const dataTransfer = new DataTransfer();
                dataTransfer.items.add(file);
                photoInput.files = dataTransfer.files;
                handleFile(file);
            } else {
                alert('Por favor, selecione apenas arquivos de imagem.');
            }
        }
    });

    function handleFile(file) {
        if (file.size > 5 * 1024 * 1024) {
            alert('O arquivo é muito grande. O tamanho máximo é 5MB.');
            return;
        }

        const reader = new FileReader();
        reader.onload = function(e) {
            previewImage.src = e.target.result;
            photoPreview.style.display = 'block';
            dropZoneText.style.display = 'none';
        };
        reader.readAsDataURL(file);
    }

    // Email validation feedback
    const emailInput = document.querySelector('input[type="email"]');
    if (emailInput) {
        emailInput.addEventListener('input', function() {
            if (this.validity.valid) {
                this.classList.remove('is-invalid');
                this.classList.add('is-valid');
            } else if (this.value !== '') {
                this.classList.remove('is-valid');
                this.classList.add('is-invalid');
            } else {
                this.classList.remove('is-valid', 'is-invalid');
            }
        });
    }

    // Form change detection for unsaved warning
    let formChanged = false;
    const formInputs = document.querySelectorAll('#personForm input, #personForm select, #personForm textarea');
    formInputs.forEach(input => {
        input.addEventListener('change', () => {
            formChanged = true;
        });
    });

    window.addEventListener('beforeunload', (e) => {
        if (formChanged) {
            e.preventDefault();
            e.returnValue = '';
        }
    });

    document.getElementById('personForm').addEventListener('submit', () => {
        formChanged = false;
    });

    // Spouse field conditional logic
    const typeSelect = document.getElementById('type');
    const spouseRow = document.getElementById('spouseRow');
    const spouseInput = document.getElementById('spouse');

    function toggleSpouseField() {
        if (typeSelect.value === 'Casal') {
            spouseRow.style.display = '';
            spouseInput.setAttribute('required', '');
        } else {
            spouseRow.style.display = 'none';
            spouseInput.removeAttribute('required');
        }
    }

    // Initialize on page load
    toggleSpouseField();

    // Toggle when type changes
    typeSelect.addEventListener('change', toggleSpouseField);
</script>
@endsection
