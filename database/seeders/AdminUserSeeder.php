<?php

namespace Database\Seeders;

use Illuminate\Database\Seeder;
use Illuminate\Support\Facades\Hash;
use App\Models\User;
use Illuminate\Support\Facades\Log;

class AdminUserSeeder extends Seeder
{
    /**
     * Run the database seeds.
     * 
     * Replicates the DbInitializer from the .NET project
     */
    public function run(): void
    {
        $adminEmail = 'brunomax18@gmail.com';
        $adminPassword = '=Katano+2007';

        // Check if admin user already exists
        $adminUser = User::where('email', $adminEmail)->first();

        if (!$adminUser) {
            // Create admin user
            $adminUser = User::create([
                'name' => 'Administrador',
                'email' => $adminEmail,
                'password' => Hash::make($adminPassword),
                'email_verified_at' => now(), // Email confirmed by default
                'role' => 'Administrador', // Assign admin role
            ]);

            Log::info("Usuário administrador '{$adminEmail}' criado com sucesso.");
        } else {
            // Update email verification if not confirmed
            if (!$adminUser->hasVerifiedEmail()) {
                $adminUser->email_verified_at = now();
                $adminUser->save();
                
                Log::info("Email do usuário '{$adminEmail}' confirmado.");
            }

            // Ensure user has admin role
            if ($adminUser->role !== 'Administrador') {
                $adminUser->role = 'Administrador';
                $adminUser->save();
                
                Log::info("Role 'Administrador' atribuída ao usuário '{$adminEmail}'.");
            }
        }

        Log::info('✅ Seed de usuário administrador concluído.');
    }
}
