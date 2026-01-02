# 🎯 Encontro - Sistema de Gerenciamento de Eventos

Sistema completo de gerenciamento de eventos religiosos (Segue-me e ECC), desenvolvido em **Laravel 12** com arquitetura em camadas (Repository/Service Pattern).

---

## 📋 Sobre o Projeto

**Encontro** é uma aplicação web para gerenciar eventos religiosos, participantes, equipes e funções. O sistema permite:

- ✅ Cadastro e gerenciamento de **Eventos** (Segue-me/ECC)
- ✅ Cadastro e gerenciamento de **Pessoas**
- ✅ Gerenciamento de **Participantes** por evento
- ✅ Organização de **Equipes** e **Funções**
- ✅ Sistema de **Autenticação** e **Autorização** por roles
- ✅ Filtros e busca avançada
- ✅ Upload de imagens (fotos de pessoas e santos padroeiros)
- ✅ Interface responsiva com Bootstrap 5

---

## 🛠️ Tecnologias Utilizadas

| Tecnologia | Versão | Descrição |
|------------|--------|-----------|
| **PHP** | 8.2+ | Linguagem backend |
| **Laravel** | 12.44.0 | Framework PHP |
| **MySQL** | 8.0+ | Banco de dados |
| **Bootstrap** | 5.3 | Framework CSS |
| **Bootstrap Icons** | 1.11 | Ícones |
| **Composer** | 2.x | Gerenciador de dependências PHP |

---

## 🏗️ Arquitetura

O projeto segue o padrão **Repository/Service** para separação de responsabilidades:

```
app/
├── Http/
│   ├── Controllers/          # Controladores (recebem requests)
│   ├── Middleware/           # Middlewares de autenticação
│   └── Requests/             # Form Requests (validação)
├── Models/                   # Modelos Eloquent
├── Repositories/             # Camada de acesso a dados
│   ├── PersonRepository.php
│   ├── EventRepository.php
│   └── EventParticipantRepository.php
└── Services/                 # Lógica de negócio
    ├── PersonService.php
    ├── EventService.php
    └── EventParticipantService.php
```

### Fluxo de Requisição

```
Request → Controller → Service → Repository → Model → Database
                ↓
            Response
```

---

## 📦 Entidades Principais

### 1. **People** (Pessoas)
- Nome, CPF, Email, Telefone
- Data de nascimento, Endereço
- Foto de perfil
- Soft deletes

### 2. **Events** (Eventos)
- Nome do evento
- Tipo (Segue-me = 1, ECC = 2)
- Santo padroeiro (nome e imagem)
- Soft deletes

### 3. **Event Participants** (Participantes)
- Pessoa vinculada ao evento
- Equipe e Função
- Data de inscrição
- Etapa, Status (Indicado/Aceitou/Recusou)
- Observações
- Soft deletes

### 4. **Teams** (Equipes)
- Nome da equipe
- Cor (hexadecimal)
- Soft deletes

### 5. **Roles** (Funções)
- Nome da função
- Ordem de exibição
- Soft deletes

### 6. **Users** (Usuários)
- Nome, Email, Senha
- Role (Administrador/Visualizador)
- Email verificado

---

## 🚀 Instalação e Configuração

### Pré-requisitos

- PHP 8.2 ou superior
- MySQL 8.0 ou superior
- Composer
- XAMPP (ou outro servidor local)

### Passo a Passo

1. **Clone o repositório**
   ```bash
   git clone <repository-url>
   cd Encontro.PHP
   ```

2. **Instale as dependências**
   ```bash
   composer install
   ```

3. **Configure o arquivo `.env`**
   ```bash
   cp .env.example .env
   ```

   Edite o `.env` com suas configurações de banco de dados:
   ```env
   DB_CONNECTION=mysql
   DB_HOST=127.0.0.1
   DB_PORT=3306
   DB_DATABASE=encontro
   DB_USERNAME=root
   DB_PASSWORD=sua_senha
   ```

4. **Gere a chave da aplicação**
   ```bash
   php artisan key:generate
   ```

5. **Execute as migrations**
   ```bash
   php artisan migrate
   ```

6. **Execute os seeders**
   ```bash
   php artisan db:seed --class=TeamSeeder
   php artisan db:seed --class=RoleSeeder
   php artisan db:seed --class=AdminUserSeeder
   php artisan db:seed --class=DevDataSeeder  # Opcional: dados de teste
   ```

7. **Crie o link simbólico para storage**
   ```bash
   php artisan storage:link
   ```

8. **Inicie o servidor**
   ```bash
   php artisan serve
   ```

9. **Acesse a aplicação**
   ```
   http://127.0.0.1:8000
   ```

---

## 🔐 Autenticação e Autorização

### Roles Disponíveis

| Role | Permissões |
|------|-----------|
| **Administrador** | Acesso completo (criar, editar, excluir) |
| **Visualizador** | Apenas visualização (sem botões de ação) |

### Blade Directives

O sistema possui directives customizadas para controle de acesso:

```blade
@admin
    <!-- Conteúdo visível apenas para administradores -->
    <button>Editar</button>
@endadmin

@role('Administrador')
    <!-- Conteúdo visível para role específica -->
@endrole

@notadmin
    <!-- Conteúdo visível para não-administradores -->
@endnotadmin
```

---

## 📂 Estrutura de Pastas

```
Encontro.PHP/
├── app/                      # Código da aplicação
│   ├── Http/                # Controllers, Middleware, Requests
│   ├── Models/              # Modelos Eloquent
│   ├── Providers/           # Service Providers
│   ├── Repositories/        # Camada de Repositório
│   └── Services/            # Camada de Serviço
├── bootstrap/               # Arquivos de inicialização
├── config/                  # Arquivos de configuração
├── database/                # Migrations, Seeders, Factories
│   ├── migrations/          # Migrations do banco
│   └── seeders/             # Seeders de dados
├── public/                  # Arquivos públicos
│   ├── index.php           # Entry point
│   └── storage/            # Link simbólico para storage
├── resources/               # Views, CSS, JS
│   ├── css/                # Estilos
│   ├── js/                 # JavaScript
│   └── views/              # Blade templates
│       ├── auth/           # Views de autenticação
│       ├── events/         # Views de eventos
│       ├── people/         # Views de pessoas
│       ├── participants/   # Views de participantes
│       └── layouts/        # Layouts principais
├── routes/                  # Definição de rotas
│   └── web.php             # Rotas web
├── storage/                 # Logs, cache, uploads
│   └── app/public/         # Arquivos públicos (fotos)
├── tests/                   # Testes automatizados
├── .env                     # Variáveis de ambiente
├── artisan                  # CLI do Laravel
├── composer.json            # Dependências PHP
└── README.md               # Este arquivo
```

---

## 🎨 Funcionalidades

### 1. Gerenciamento de Pessoas

- ✅ Listagem com filtros (nome, CPF, email, telefone)
- ✅ Cadastro com validação de CPF único
- ✅ Edição de dados
- ✅ Upload de foto de perfil
- ✅ Visualização detalhada
- ✅ Soft delete (exclusão lógica)

### 2. Gerenciamento de Eventos

- ✅ Listagem com filtros (nome, tipo, santo padroeiro)
- ✅ Cadastro de eventos (Segue-me/ECC)
- ✅ Upload de imagem do santo padroeiro
- ✅ Edição de eventos
- ✅ Visualização com estatísticas de participantes
- ✅ Soft delete

### 3. Gerenciamento de Participantes

- ✅ Adicionar participantes a eventos
- ✅ Busca de pessoas com Select2 (searchable dropdown)
- ✅ Atribuição de equipe e função
- ✅ Controle de etapa e status
- ✅ Filtros por nome, equipe, função
- ✅ Ordenação por colunas (nome, equipe, função, data)
- ✅ Edição de participantes
- ✅ Estatísticas (total, com equipe, com função, equipes ativas)

### 4. Autenticação

- ✅ Login com email e senha
- ✅ "Lembrar de mim"
- ✅ Logout
- ✅ Proteção de rotas com middleware `auth`
- ✅ Verificação de email (MustVerifyEmail)

### 5. Autorização

- ✅ Sistema de roles (Administrador/Visualizador)
- ✅ Blade directives customizadas
- ✅ Controle de acesso na UI
- ✅ Proteção de rotas por role

---

## 🗃️ Banco de Dados

### Diagrama de Relacionamentos

```
users (1) ──────────────────────┐
                                │
people (1) ────┐                │
               │                │
               ├─── event_participants (N) ─── events (1)
               │            │
teams (1) ─────┤            │
               │            │
roles (1) ─────┘            │
                            │
                     (soft deletes)
```

### Migrations Disponíveis

1. `create_users_table` - Tabela de usuários
2. `create_people_table` - Tabela de pessoas
3. `create_events_table` - Tabela de eventos
4. `create_teams_table` - Tabela de equipes
5. `create_roles_table` - Tabela de funções
6. `create_event_participants_table` - Tabela de participantes
7. `add_order_to_roles_table` - Adiciona coluna de ordenação
8. `add_role_to_users_table` - Adiciona sistema de roles

---

## 🧪 Seeders

### Seeders Disponíveis

| Seeder | Descrição | Comando |
|--------|-----------|---------|
| `TeamSeeder` | Cria equipes padrão (Vermelho, Azul, etc.) | `php artisan db:seed --class=TeamSeeder` |
| `RoleSeeder` | Cria funções padrão (Coordenador, Músico, etc.) | `php artisan db:seed --class=RoleSeeder` |
| `AdminUserSeeder` | Cria usuário administrador | `php artisan db:seed --class=AdminUserSeeder` |
| `DevDataSeeder` | Cria dados de teste (50 pessoas, 1 evento) | `php artisan db:seed --class=DevDataSeeder` |

### Executar Todos os Seeders

```bash
php artisan db:seed
```

---

## 🔧 Comandos Úteis

### Migrations

```bash
# Executar migrations
php artisan migrate

# Verificar status
php artisan migrate:status

# Rollback última migration
php artisan migrate:rollback

# Resetar e executar novamente
php artisan migrate:fresh --seed
```

### Seeders

```bash
# Executar todos os seeders
php artisan db:seed

# Executar seeder específico
php artisan db:seed --class=AdminUserSeeder
```

### Cache

```bash
# Limpar cache
php artisan cache:clear

# Limpar config cache
php artisan config:clear

# Limpar route cache
php artisan route:clear

# Limpar view cache
php artisan view:clear
```

### Storage

```bash
# Criar link simbólico
php artisan storage:link
```

### Tinker (Console Interativo)

```bash
# Abrir tinker
php artisan tinker

# Verificar usuários
App\Models\User::all();

# Verificar roles
App\Models\User::select('id', 'name', 'email', 'role')->get();
```

---

## 📝 Rotas Principais

### Autenticação

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/login` | Página de login |
| POST | `/login` | Processar login |
| POST | `/logout` | Fazer logout |

### Pessoas

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/people` | Listar pessoas |
| GET | `/people/create` | Formulário de criação |
| POST | `/people` | Salvar pessoa |
| GET | `/people/{id}` | Visualizar pessoa |
| GET | `/people/{id}/edit` | Formulário de edição |
| PUT | `/people/{id}` | Atualizar pessoa |
| GET | `/people/{id}/delete` | Excluir pessoa |

### Eventos

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/events` | Listar eventos |
| GET | `/events/create` | Formulário de criação |
| POST | `/events` | Salvar evento |
| GET | `/events/{id}` | Visualizar evento |
| GET | `/events/{id}/edit` | Formulário de edição |
| PUT | `/events/{id}` | Atualizar evento |
| GET | `/events/{id}/delete` | Excluir evento |

### Participantes

| Método | Rota | Descrição |
|--------|------|-----------|
| GET | `/participants/create/{eventId}` | Adicionar participante |
| POST | `/participants` | Salvar participante |
| GET | `/participants/{id}/edit` | Editar participante |
| PUT | `/participants/{id}` | Atualizar participante |
| GET | `/participants/{id}/delete` | Excluir participante |

---


## 📄 Licença

Este projeto é de código fechado e propriedade privada.

---

## 👨‍💻 Desenvolvedor

**Bruno Max da Silva Matos**  
Email: brunomax18@gmail.com

---

## 📅 Histórico de Versões

| Versão | Data | Descrição |
|--------|------|-----------|
| 1.0.0 | 2026-01-02 | Versão inicial - Migração completa do .NET para Laravel |

---
