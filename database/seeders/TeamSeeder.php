<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Team;
use Illuminate\Support\Facades\Log;

class TeamSeeder extends Seeder
{
    /**
     * Run the database seeds.
     * 
     * Replicates the SeedTeamsAsync from the .NET DbInitializer
     */
    public function run(): void
    {
        if (Team::count() > 0) {
            Log::info('Teams já existem. Skipping seed.');
            return;
        }

        $teams = [
            ['order' => '00', 'name' => 'Círculo', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '01a', 'name' => 'Conselho Arquidiocesano', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '01b', 'name' => 'Conselho Regional Centro Oeste', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '01c', 'name' => 'Equipe Dirigente', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '02', 'name' => 'Casal Coordenador', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '03', 'name' => 'Casal Espiritualizador', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '04', 'name' => 'Equipe de Círculos', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '05', 'name' => 'Equipe de Sala', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '06', 'name' => 'Equipe de Compras', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '07', 'name' => 'Equipe de Café e Minimercado', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '08', 'name' => 'Equipe de Ordem e Limpeza', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '09', 'name' => 'Equipe de Liturgia e Vigília', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '10', 'name' => 'Equipe de Secretaria', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '11', 'name' => 'Equipe de Cozinha', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '12', 'name' => 'Equipe de Visitação', 'created_at' => now(), 'updated_at' => now()],
            ['order' => '13', 'name' => 'Equipe de Acolhida', 'created_at' => now(), 'updated_at' => now()],
        ];

        Team::insert($teams);

        Log::info('✅ ' . count($teams) . ' Teams criados com sucesso.');
    }
}
