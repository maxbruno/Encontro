@extends('layouts.app')

@section('title', 'Acesso Negado')

@section('content')
<div class="container mt-5">
    <div class="row">
        <div class="col-md-8 offset-md-2">
            <div class="card shadow">
                <div class="card-header bg-danger text-white">
                    <h5 class="mb-0"><i class="bi bi-shield-exclamation"></i> Acesso Negado</h5>
                </div>
                <div class="card-body text-center py-5">
                    <i class="bi bi-shield-exclamation text-danger" style="font-size: 5rem;"></i>
                    <h2 class="mt-4">Acesso Negado</h2>
                    <p class="lead">Você não tem permissão para acessar esta página.</p>
                    <p class="text-muted">Se você acredita que isso é um erro, entre em contato com o administrador do sistema.</p>
                    <div class="mt-4">
                        <a href="{{ url('/') }}" class="btn btn-primary">
                            <i class="bi bi-house-fill"></i> Voltar para a Página Inicial
                        </a>
                    </div>
                </div>
            </div>
        </div>
    </div>
</div>
@endsection
