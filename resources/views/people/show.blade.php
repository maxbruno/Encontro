@extends('layouts.app')

@section('title', 'Detalhes da Pessoa - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="row">
            <div class="col-md-10 offset-md-1">
                <h1 class="mb-4">Detalhes da Pessoa</h1>

                <div class="card">
                    <div class="card-body">
                        @if($person->photo_url)
                            <div class="text-center mb-4">
                                <img src="{{ asset('storage/' . $person->photo_url) }}" 
                                     alt="Foto de {{ $person->name }}" 
                                     class="img-thumbnail" 
                                     style="max-width: 300px; max-height: 300px; object-fit: cover;">
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

                            <dt class="col-sm-3">Endereço</dt>
                            <dd class="col-sm-9">{{ $person->address ?? '-' }}</dd>

                            <dt class="col-sm-3">Bairro</dt>
                            <dd class="col-sm-9">{{ $person->district ?? '-' }}</dd>

                            <dt class="col-sm-3">CEP</dt>
                            <dd class="col-sm-9">{{ $person->zip_code ?? '-' }}</dd>

                            <dt class="col-sm-3">Núcleo</dt>
                            <dd class="col-sm-9">{{ $person->group ?? '-' }}</dd>

                            <dt class="col-sm-3">Nome do Pai</dt>
                            <dd class="col-sm-9">{{ $person->father_name ?? '-' }}</dd>

                            <dt class="col-sm-3">Nome da Mãe</dt>
                            <dd class="col-sm-9">{{ $person->mother_name ?? '-' }}</dd>

                            <dt class="col-sm-3">Observações</dt>
                            <dd class="col-sm-9">
                                @if($person->notes)
                                    <pre class="mb-0">{{ $person->notes }}</pre>
                                @else
                                    -
                                @endif
                            </dd>
                        </dl>

                        <div class="mt-4">
                            @admin
                                <a href="{{ route('people.edit', $person->id) }}" class="btn btn-warning">
                                    <i class="bi bi-pencil"></i> Editar
                                </a>
                            @endadmin
                            <a href="{{ route('people.index') }}" class="btn btn-secondary">
                                <i class="bi bi-arrow-left"></i> Voltar
                            </a>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
@endsection
