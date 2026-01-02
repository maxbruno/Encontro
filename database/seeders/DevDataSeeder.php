<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use App\Models\Event;
use App\Models\Person;
use App\Models\Team;
use App\Models\Role;
use App\Models\EventParticipant;
use Illuminate\Support\Facades\Log;

class DevDataSeeder extends Seeder
{
    /**
     * Run the database seeds.
     * 
     * Replicates the DevDataSeeder from the .NET Infrastructure project
     * Creates comprehensive test data for development environment
     */
    public function run(): void
    {
        // Check if test event already exists
        $testEventName = 'ECC 2024 - Teste Ordenação Completo';
        $existingEvent = Event::where('name', $testEventName)->first();

        if ($existingEvent) {
            Log::info('Test data already exists. Skipping seed.');
            return;
        }

        Log::info('Seeding comprehensive test data for development environment...');

        // Create test event
        $testEvent = Event::create([
            'name' => $testEventName,
            'event_type' => 2, // ECC = 2 (from .NET enum)
        ]);

        // Create 65 test people with Brazilian names
        $peopleNames = [
            // Names starting with A-E (for alphabetical testing)
            'Amanda Oliveira', 'André Santos', 'Beatriz Silva', 'Bruno Costa',
            'Camila Rodrigues', 'Carlos Mendes', 'Diana Rocha', 'Daniel Ferreira',
            'Eduardo Lima', 'Eliane Martins',
            // Names starting with F-J
            'Fernanda Santos', 'Felipe Almeida', 'Gabriela Souza', 'Gustavo Pereira',
            'Helena Carvalho', 'Hugo Nascimento', 'Isabela Ribeiro', 'Igor Castro',
            'Juliana Dias', 'João Araújo',
            // Names starting with K-O
            'Karla Gomes', 'Lucas Barbosa', 'Larissa Monteiro', 'Marcelo Cardoso',
            'Mariana Freitas', 'Nicolas Teixeira', 'Natália Correia', 'Otávio Moreira',
            'Olivia Azevedo', 'Paulo Barros',
            // Names starting with P-T
            'Patrícia Cunha', 'Rafael Moura', 'Renata Lopes', 'Roberto Ramos',
            'Sabrina Vieira', 'Sérgio Farias', 'Tatiana Medeiros', 'Thiago Nunes',
            'Vanessa Campos', 'Vítor Duarte',
            // Additional names for complete coverage
            'Wellington Pinto', 'Yara Borges', 'Alice Macedo', 'Arthur Soares',
            'Bianca Rezende', 'Caio Mendonça', 'Débora Cavalcanti', 'Erick Nogueira',
            'Fábio Xavier', 'Giovana Santana', 'Henrique Batista', 'Íris Magalhães',
            'Júlio Tavares', 'Kátia Melo', 'Leonardo Siqueira', 'Lúcia Guimarães',
            'Márcio Amaral', 'Melissa Pires', 'Nilton Braga', 'Priscila Fonseca',
            'Rodrigo Pacheco', 'Sílvia Matos', 'Túlio Andrade', 'Valéria Moraes',
            'Wilson Torres'
        ];

        $people = [];
        foreach ($peopleNames as $name) {
            $people[] = Person::create(['name' => $name]);
        }

        // Get all teams and roles
        $teams = Team::orderBy('order')->get();
        $roles = Role::orderBy('order')->get();

        $participants = [];
        $personIndex = 0;
        $baseDate = now()->parse('2024-01-01 08:00:00');

        // 1. NULL team, NULL role scenarios (5 people)
        for ($i = 0; $i < 5; $i++) {
            $participants[] = [
                'event_id' => $testEvent->id,
                'person_id' => $people[$personIndex++]->id,
                'team_id' => null,
                'role_id' => null,
                'registered_at' => $baseDate->copy()->addHours($i),
                'stage' => ($i % 10) + 1,
                'notes' => "NULL/NULL - Teste " . ($i + 1),
                'created_at' => now(),
                'updated_at' => now(),
            ];
        }

        // 2. NULL team, WITH role scenarios (5 people - using first 5 roles)
        for ($i = 0; $i < 5; $i++) {
            $participants[] = [
                'event_id' => $testEvent->id,
                'person_id' => $people[$personIndex++]->id,
                'team_id' => null,
                'role_id' => $roles[$i]->id,
                'registered_at' => $baseDate->copy()->addDays(1)->addHours($i),
                'stage' => ($i % 10) + 1,
                'notes' => "NULL/{$roles[$i]->name}",
                'created_at' => now(),
                'updated_at' => now(),
            ];
        }

        // 3. WITH team, NULL role scenarios (5 people - using first 5 teams)
        for ($i = 0; $i < 5; $i++) {
            $participants[] = [
                'event_id' => $testEvent->id,
                'person_id' => $people[$personIndex++]->id,
                'team_id' => $teams[$i]->id,
                'role_id' => null,
                'registered_at' => $baseDate->copy()->addDays(2)->addHours($i),
                'stage' => ($i % 10) + 1,
                'notes' => "{$teams[$i]->name}/NULL",
                'created_at' => now(),
                'updated_at' => now(),
            ];
        }

        // 4. Complete coverage: 2 people per team with different roles (42 people)
        for ($teamIdx = 0; $teamIdx < $teams->count(); $teamIdx++) {
            for ($p = 0; $p < 2; $p++) {
                if ($personIndex >= count($people)) break;
                
                $roleIdx = ($teamIdx * 2 + $p) % $roles->count();
                $participants[] = [
                    'event_id' => $testEvent->id,
                    'person_id' => $people[$personIndex++]->id,
                    'team_id' => $teams[$teamIdx]->id,
                    'role_id' => $roles[$roleIdx]->id,
                    'registered_at' => $baseDate->copy()->addDays(3 + $teamIdx)->addHours($p * 2),
                    'stage' => (($teamIdx + $p) % 10) + 1,
                    'notes' => "{$teams[$teamIdx]->order}-{$teams[$teamIdx]->name}/{$roles[$roleIdx]->order}-{$roles[$roleIdx]->name}",
                    'created_at' => now(),
                    'updated_at' => now(),
                ];
            }
        }

        // 5. Same team, same role, different names (3 people - Círculo/Círculo Verde)
        $circulo = $teams->firstWhere('order', '00');
        $circuloVerde = $roles->firstWhere('order', '12');
        
        if ($circulo && $circuloVerde && $personIndex + 2 < count($people)) {
            for ($i = 0; $i < 3; $i++) {
                $participants[] = [
                    'event_id' => $testEvent->id,
                    'person_id' => $people[$personIndex++]->id,
                    'team_id' => $circulo->id,
                    'role_id' => $circuloVerde->id,
                    'registered_at' => $baseDate->copy()->addDays(30)->addHours($i),
                    'stage' => 5,
                    'notes' => 'Same team/role - test name sorting',
                    'created_at' => now(),
                    'updated_at' => now(),
                ];
            }
        }

        EventParticipant::insert($participants);

        Log::info('✅ Complete test data seeded successfully!');
        Log::info("   Created " . count($people) . " people and " . count($participants) . " participants");
        Log::info("   Coverage: {$teams->count()} teams, {$roles->count()} roles");
        Log::info("   NULL scenarios: 15 participants");
        Log::info("   Full coverage: " . (count($participants) - 15) . " participants with team/role");
        Log::info('');
        Log::info('Sort order validation:');
        Log::info('1️⃣  NULL team, NULL role (5 people) - sorted by name');
        Log::info('2️⃣  NULL team, with role (5 people) - sorted by role order');
        Log::info('3️⃣  With team, NULL role (5 people) - sorted by team order');
        Log::info('4️⃣  All teams covered (21 teams x 2 people) - sorted by team/role/name');
        Log::info('5️⃣  Same team/role (3 people) - sorted alphabetically');
    }
}
