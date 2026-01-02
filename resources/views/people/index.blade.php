@extends('layouts.app')

@section('title', 'Pessoas - Encontro')

@section('content')
    <div class="container mt-4">
        <div class="d-flex justify-content-between align-items-center mb-4">
            <h1><i class="bi bi-people-fill"></i> Pessoas</h1>
            @admin
                <a href="{{ route('people.create') }}" class="btn btn-primary">
                    <i class="bi bi-plus-circle"></i> Nova Pessoa
                </a>
            @endadmin
        </div>

        <div class="card shadow-sm">
            <div class="card-body">
                {{-- Search Form --}}
                <form action="{{ route('people.index') }}" method="GET" class="mb-4">
                    <div class="row g-3">
                        <div class="col-md-10">
                            <input type="text" name="search" class="form-control" 
                                   placeholder="Buscar por nome, email, telefone..." 
                                   value="{{ request('search') }}">
                        </div>
                        <div class="col-md-2">
                            <button type="submit" class="btn btn-primary w-100">
                                <i class="bi bi-search"></i> Buscar
                            </button>
                        </div>
                    </div>
                </form>

                {{-- Table --}}
                <div class="table-responsive">
                    <table class="table table-hover">
                        <thead class="table-light">
                            <tr>
                                <th>
                                    <a href="{{ route('people.index', array_merge(request()->except(['sort_by', 'sort_order']), ['sort_by' => 'name', 'sort_order' => (request('sort_by') == 'name' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                       class="text-decoration-none text-dark">
                                        Nome
                                        @if(request('sort_by') == 'name')
                                            <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                        @endif
                                    </a>
                                </th>
                                <th>
                                    <a href="{{ route('people.index', array_merge(request()->except(['sort_by', 'sort_order']), ['sort_by' => 'type', 'sort_order' => (request('sort_by') == 'type' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                       class="text-decoration-none text-dark">
                                        Tipo
                                        @if(request('sort_by') == 'type')
                                            <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                        @endif
                                    </a>
                                </th>
                                <th>
                                    <a href="{{ route('people.index', array_merge(request()->except(['sort_by', 'sort_order']), ['sort_by' => 'email', 'sort_order' => (request('sort_by') == 'email' && request('sort_order') == 'asc') ? 'desc' : 'asc'])) }}" 
                                       class="text-decoration-none text-dark">
                                        Email
                                        @if(request('sort_by') == 'email')
                                            <i class="bi bi-arrow-{{ request('sort_order') == 'asc' ? 'up' : 'down' }}"></i>
                                        @endif
                                    </a>
                                </th>
                                <th>Celular</th>
                                <th style="width: 150px;">Ações</th>
                            </tr>
                        </thead>
                        <tbody>
                            @forelse($people as $person)
                                <tr>
                                    <td>
                                        <div class="d-flex align-items-center gap-2">
                                            @if($person->photo_url)
                                                <img src="{{ asset('storage/' . $person->photo_url) }}" 
                                                     alt="{{ $person->name }}" 
                                                     class="rounded-circle" 
                                                     style="width: 40px; height: 40px; object-fit: cover;">
                                            @else
                                                <div class="rounded-circle bg-secondary d-flex align-items-center justify-content-center text-white" 
                                                     style="width: 40px; height: 40px; font-weight: 600;">
                                                    {{ strtoupper(substr($person->name, 0, 1)) }}
                                                </div>
                                            @endif
                                            <span>{{ $person->name }}</span>
                                        </div>
                                    </td>
                                    <td>
                                        @if($person->type)
                                            <span class="badge bg-info">{{ $person->type }}</span>
                                        @endif
                                    </td>
                                    <td>{{ $person->email }}</td>
                                    <td>{{ $person->cell_phone }}</td>
                                    <td>
                                        <div class="btn-group" role="group">
                                            <a href="{{ route('people.show', $person->id) }}" 
                                               class="btn btn-sm btn-outline-info" 
                                               title="Ver Detalhes">
                                                <i class="bi bi-eye"></i>
                                            </a>
                                            @admin
                                                <a href="{{ route('people.edit', $person->id) }}" 
                                                   class="btn btn-sm btn-outline-primary" 
                                                   title="Editar">
                                                    <i class="bi bi-pencil"></i>
                                                </a>
                                                <a href="{{ route('people.delete', $person->id) }}" 
                                                   class="btn btn-sm btn-outline-danger" 
                                                   title="Excluir">
                                                    <i class="bi bi-trash"></i>
                                                </a>
                                            @endadmin
                                        </div>
                                    </td>
                                    </td>
                                </tr>
                            @empty
                                <tr>
                                    <td colspan="5" class="text-center text-muted py-4">
                                        <i class="bi bi-people"></i> Nenhuma pessoa encontrada.
                                    </td>
                                </tr>
                            @endforelse
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>
@endsection
