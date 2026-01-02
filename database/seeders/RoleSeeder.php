<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Role;
use Illuminate\Support\Facades\Log;

class RoleSeeder extends Seeder
{
    /**
     * Run the database seeds.
     * 
     * Replicates the SeedRolesAsync from the .NET DbInitializer
     */
    public function run(): void
    {
        if (Role::count() > 0) {
            Log::info('Roles do domínio já existem. Skipping seed.');
            return;
        }

        $roles = [
            ['order' => '00', 'name' => 'Diretor Espiritual', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '01', 'name' => 'Casal Montagem', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '02', 'name' => 'Casal Fichas', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '03', 'name' => 'Casal Finanças', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '04', 'name' => 'Casal Palestras', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '05', 'name' => 'Casal Pós Encontro', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '06', 'name' => 'Coordenador(es) Geral', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '07', 'name' => 'Coordenador(es)', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '08', 'name' => 'Membro', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '09', 'name' => 'Círculo Amarelo', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '10', 'name' => 'Círculo Azul', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '11', 'name' => 'Círculo Vermelho', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '12', 'name' => 'Círculo Verde', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '13', 'name' => 'Círculo Laranja', 'created_at' => now(), 'updated_at' => now()],
        ];

        Role::insert($roles);

        Log::info('✅ ' . count($roles) . ' Roles do domínio criados com sucesso.');
    }
}
