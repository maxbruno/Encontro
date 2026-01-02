@extends('layouts.app')

@section('title', 'Novo Evento - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-8 offset-md-2">
                {{-- Success Message --}}
                @if (session('success'))
                    <div class="alert alert-success alert-dismissible fade show" role="alert">
                        <i class="bi bi-check-circle-fill"></i> {{ session('success') }}
                        <button type="button" class="btn-close" data-bs-dismiss="alert" aria-label="Close"></button>
                    </div>
                @endif

                {{-- Header --}}
                <div class="d-flex justify-content-between align-items-center mb-4">
                    <h1><i class="bi bi-calendar-plus-fill"></i> Novo Evento</h1>
                    <a href="{{ route('events.index') }}" class="btn btn-outline-secondary">
                        <i class="bi bi-arrow-left"></i> Voltar à Lista
                    </a>
                </div>

                <form method="POST" action="{{ route('events.store') }}" enctype="multipart/form-data" id="eventForm">
                    @csrf
                    
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

                    {{-- Card: Informações do Evento --}}
                    <div class="card mb-3">
                        <div class="card-header bg-primary text-white">
                            <h5 class="mb-0"><i class="bi bi-info-circle"></i> Informações do Evento</h5>
                        </div>
                        <div class="card-body">
                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="name" class="form-label">
                                        <i class="bi bi-card-text"></i> Nome <span class="text-danger">*</span>
                                    </label>
                                    <input type="text" name="name" id="name" class="form-control @error('name') is-invalid @enderror" 
                                           value="{{ old('name') }}" required maxlength="100" 
                                           placeholder="Ex: Encontro de Casais 2025" autofocus>
                                    @error('name')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <small class="text-muted"><span id="charCount">0</span>/100 caracteres</small>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="event_type" class="form-label">
                                        <i class="bi bi-tag-fill"></i> Tipo de Evento <span class="text-danger">*</span>
                                    </label>
                                    <select name="event_type" id="event_type" class="form-select @error('event_type') is-invalid @enderror" required>
                                        <option value="">-- Selecione o tipo --</option>
                                        <option value="1" {{ old('event_type') == '1' ? 'selected' : '' }}>Segue-me</option>
                                        <option value="2" {{ old('event_type') == '2' ? 'selected' : '' }}>ECC</option>
                                    </select>
                                    @error('event_type')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <div id="typePreview" class="mt-2" style="display: none;">
                                        <small class="text-muted">Preview: </small>
                                        <span id="previewBadge"></span>
                                    </div>
                                </div>
                            </div>

                            <div class="row mb-3">
                                <div class="col-md-12">
                                    <label for="patron_saint_name" class="form-label">
                                        <i class="bi bi-star"></i> Santo Padroeiro
                                    </label>
                                    <input type="text" name="patron_saint_name" id="patron_saint_name" 
                                           class="form-control @error('patron_saint_name') is-invalid @enderror" 
                                           value="{{ old('patron_saint_name') }}" maxlength="100" 
                                           placeholder="Ex: São Francisco de Assis">
                                    @error('patron_saint_name')
                                        <span class="invalid-feedback">{{ $message }}</span>
                                    @enderror
                                    <small class="text-muted"><span id="patronCharCount">0</span>/100 caracteres</small>
                                </div>
                            </div>

                            <div class="row">
                                <div class="col-md-12">
                                    <label class="form-label">
                                        <i class="bi bi-image"></i> Imagem do Santo Padroeiro
                                    </label>
                                    <div class="border rounded p-3" id="dropZone" style="background-color: #f8f9fa; cursor: pointer;">
                                        <input type="file" name="photo" class="form-control d-none" id="photoInput" accept="image/*">
                                        <div class="text-center" id="dropZoneContent">
                                            <div id="photoPreview" class="mb-2" style="display: none;">
                                                <img id="previewImage" src="#" alt="Preview" class="img-thumbnail" 
                                                     style="max-width: 200px; max-height: 200px; object-fit: cover;">
                                            </div>
                                            <div id="dropZoneText">
                                                <i class="bi bi-cloud-upload" style="font-size: 2rem; color: #6c757d;"></i>
                                                <p class="mb-0 mt-2">Clique para selecionar uma imagem ou arraste aqui</p>
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

                    {{-- Card: Botões de Ação --}}
                    <div class="card mb-4">
                        <div class="card-body">
                            <div class="d-flex justify-content-between align-items-center">
                                <div>
                                    <button type="submit" name="action" value="save" class="btn btn-primary btn-lg">
                                        <i class="bi bi-check-circle"></i> Salvar e Voltar
                                    </button>
                                    <button type="submit" name="action" value="saveAndViewDetails" class="btn btn-success btn-lg">
                                        <i class="bi bi-arrow-right-circle"></i> Salvar e Ir para Evento
                                    </button>
                                </div>
                                <div>
                                    <a href="{{ route('events.index') }}" class="btn btn-secondary btn-lg">
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
    // Character counter for event name
    const eventName = document.getElementById('name');
    const charCount = document.getElementById('charCount');
    
    if (eventName && charCount) {
        eventName.addEventListener('input', function() {
            charCount.textContent = this.value.length;
        });
        
        // Initialize counter
        charCount.textContent = eventName.value.length;
    }

    // Character counter for patron saint name
    const patronName = document.getElementById('patron_saint_name');
    const patronCharCount = document.getElementById('patronCharCount');
    
    if (patronName && patronCharCount) {
        patronName.addEventListener('input', function() {
            patronCharCount.textContent = this.value.length;
        });
        
        // Initialize counter
        patronCharCount.textContent = patronName.value.length;
    }

    // Event type preview
    const eventType = document.getElementById('event_type');
    const typePreview = document.getElementById('typePreview');
    const previewBadge = document.getElementById('previewBadge');
    
    if (eventType && typePreview && previewBadge) {
        eventType.addEventListener('change', function() {
            if (this.value === '1') {
                previewBadge.className = 'badge bg-primary';
                previewBadge.textContent = 'Segue-me';
                typePreview.style.display = 'block';
            } else if (this.value === '2') {
                previewBadge.className = 'badge bg-success';
                previewBadge.textContent = 'ECC';
                typePreview.style.display = 'block';
            } else {
                typePreview.style.display = 'none';
            }
        });
    }

    // Photo upload drag & drop functionality
    const dropZone = document.getElementById('dropZone');
    const photoInput = document.getElementById('photoInput');
    const photoPreview = document.getElementById('photoPreview');
    const previewImage = document.getElementById('previewImage');
    const dropZoneText = document.getElementById('dropZoneText');

    // Click to select file
    dropZone.addEventListener('click', () => {
        photoInput.click();
    });

    // Handle file selection
    photoInput.addEventListener('change', function(e) {
        if (this.files && this.files[0]) {
            handleFile(this.files[0]);
        }
    });

    // Prevent default drag behaviors
    ['dragenter', 'dragover', 'dragleave', 'drop'].forEach(eventName => {
        dropZone.addEventListener(eventName, preventDefaults, false);
    });

    function preventDefaults(e) {
        e.preventDefault();
        e.stopPropagation();
    }

    // Highlight drop zone when dragging over it
    ['dragenter', 'dragover'].forEach(eventName => {
        dropZone.addEventListener(eventName, () => {
            dropZone.style.backgroundColor = '#e9ecef';
        }, false);
    });

    ['dragleave', 'drop'].forEach(eventName => {
        dropZone.addEventListener(eventName, () => {
            dropZone.style.backgroundColor = '#f8f9fa';
        }, false);
    });

    // Handle dropped files
    dropZone.addEventListener('drop', function(e) {
        const dt = e.dataTransfer;
        const file = dt.files[0];
        
        if (file && file.type.startsWith('image/')) {
            const dataTransfer = new DataTransfer();
            dataTransfer.items.add(file);
            photoInput.files = dataTransfer.files;
            handleFile(file);
        } else {
            alert('Por favor, selecione apenas arquivos de imagem.');
        }
    }, false);

    function handleFile(file) {
        if (!file) return;

        // Validate file type
        if (!file.type.match('image.*')) {
            alert('Por favor, selecione apenas arquivos de imagem.');
            return;
        }

        // Validate file size (5MB)
        if (file.size > 5 * 1024 * 1024) {
            alert('O arquivo deve ter no máximo 5MB.');
            return;
        }

        // Preview image
        const reader = new FileReader();
        reader.onload = function(e) {
            previewImage.src = e.target.result;
            photoPreview.style.display = 'block';
            dropZoneText.style.display = 'none';
        };
        reader.readAsDataURL(file);
    }

    // Form change detection for unsaved warning
    let formChanged = false;
    const formInputs = document.querySelectorAll('#eventForm input, #eventForm select');
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
    document.getElementById('eventForm').addEventListener('submit', () => {
        formChanged = false;
    });
</script>
@endsection
