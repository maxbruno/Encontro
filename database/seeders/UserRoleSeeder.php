<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;

class UserRoleSeeder extends Seeder
{
    /**
     * Run the database seeds.
     */
    public function run(): void
    {
        // Criar usuário administrador padrão
        $adminEmail = 'brunomax18@gmail.com';
        
        $admin = User::firstOrCreate(
            ['email' => $adminEmail],
            [
                'name' => 'Bruno Max',
                'password' => Hash::make('=Katano+2007'),
                'role' => 'Administrador',
                'email_verified_at' => now(),
            ]
        );

        if ($admin->wasRecentlyCreated) {
            $this->command->info("✅ Usuário administrador '{$adminEmail}' criado com sucesso.");
        } else {
            // Garantir que o usuário existente tenha a role de administrador
            if ($admin->role !== 'Administrador') {
                $admin->update(['role' => 'Administrador']);
                $this->command->info("✅ Role 'Administrador' atribuída ao usuário '{$adminEmail}'.");
            }
            
            // Garantir que o email esteja verificado
            if (!$admin->email_verified_at) {
                $admin->update(['email_verified_at' => now()]);
                $this->command->info("✅ Email do usuário '{$adminEmail}' verificado.");
            }
        }

        // Criar usuário visualizador de exemplo (apenas em desenvolvimento)
        if (app()->environment('local')) {
            $viewerEmail = 'viewer@example.com';
            
            $viewer = User::firstOrCreate(
                ['email' => $viewerEmail],
                [
                    'name' => 'Usuário Visualizador',
                    'password' => Hash::make('password'),
                    'role' => 'Visualizador',
                    'email_verified_at' => now(),
                ]
            );

            if ($viewer->wasRecentlyCreated) {
                $this->command->info("✅ Usuário visualizador '{$viewerEmail}' criado para testes.");
            }
        }
    }
}
