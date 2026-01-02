<?php

namespace App\Providers;

use Illuminate\Support\Facades\Blade;
use Illuminate\Support\ServiceProvider;

class BladeServiceProvider extends ServiceProvider
{
    /**
     * Register services.
     */
    public function register(): void
    {
        //
    }

    /**
     * Bootstrap services.
     */
    public function boot(): void
    {
        // Custom Blade directive to check user role
        // Usage: @role('Administrador') ... @endrole
        Blade::if('role', function (string $role) {
            return auth()->check() && auth()->user()->hasRole($role);
        });

        // Custom Blade directive to check if user is admin
        // Usage: @admin ... @endadmin
        Blade::if('admin', function () {
            return auth()->check() && auth()->user()->isAdmin();
        });

        // Custom Blade directive to check if user is NOT admin
        // Usage: @notadmin ... @endnotadmin
        Blade::if('notadmin', function () {
            return auth()->check() && !auth()->user()->isAdmin();
        });
    }
}
