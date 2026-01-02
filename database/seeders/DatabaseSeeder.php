<?php

namespace Database\Seeders;

use App\Models\User;
use Illuminate\Database\Console\Seeds\WithoutModelEvents;
use Illuminate\Database\Seeder;

class DatabaseSeeder extends Seeder
{
    use WithoutModelEvents;

    /**
     * Seed the application's database.
     * 
     * Order matches the .NET DbInitializer:
     * 1. Teams
     * 2. Roles (domain)
     * 3. Admin User
     * 4. Dev Test Data (only in development)
     */
    public function run(): void
    {
        $this->call([
            TeamSeeder::class,
            RoleSeeder::class,
            AdminUserSeeder::class,
        ]);

        // Seed test data only in development environment
        if (app()->environment('local', 'development')) {
            $this->call([
                DevDataSeeder::class,
            ]);
        }
    }
}
