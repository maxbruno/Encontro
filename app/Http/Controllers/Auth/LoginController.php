<?php

namespace App\Http\Controllers\Auth;

use App\Http\Controllers\Controller;
use Illuminate\Http\Request;
use Illuminate\Support\Facades\Auth;
use Illuminate\Support\Facades\Log;
use Illuminate\Validation\ValidationException;

class LoginController extends Controller
{
    /**
     * Display the login view.
     */
    public function showLoginForm()
    {
        return view('auth.login');
    }

    /**
     * Handle an authentication attempt.
     */
    public function login(Request $request)
    {
        $credentials = $request->validate([
            'email' => ['required', 'email'],
            'password' => ['required'],
        ], [
            'email.required' => 'O email é obrigatório.',
            'email.email' => 'Email inválido.',
            'password.required' => 'A senha é obrigatória.',
        ]);

        // Rate limiting - proteção contra brute force
        $key = 'login.' . $request->ip();
        
        if (\Illuminate\Support\Facades\RateLimiter::tooManyAttempts($key, 5)) {
            $seconds = \Illuminate\Support\Facades\RateLimiter::availableIn($key);
            throw ValidationException::withMessages([
                'email' => "Muitas tentativas de login. Tente novamente em {$seconds} segundos.",
            ]);
        }

        $remember = $request->boolean('remember');

        if (Auth::attempt($credentials, $remember)) {
            $user = Auth::user();
            
            // Verificar se o email foi confirmado (conforme .NET RequireConfirmedAccount)
            if (!$user->hasVerifiedEmail()) {
                Auth::logout();
                
                \Illuminate\Support\Facades\RateLimiter::hit($key, 300); // Bloqueia por 5 minutos
                
                throw ValidationException::withMessages([
                    'email' => 'Você precisa confirmar seu email antes de fazer login. Verifique sua caixa de entrada.',
                ]);
            }
            
            // Login bem-sucedido - limpar rate limiter
            \Illuminate\Support\Facades\RateLimiter::clear($key);
            
            $request->session()->regenerate();
            
            Log::info('Usuário logado.', ['email' => $request->email]);

            return redirect()->intended('/');
        }

        // Login falhou - incrementar contador de tentativas
        \Illuminate\Support\Facades\RateLimiter::hit($key, 60); // Bloqueia por 60 segundos após 5 tentativas

        throw ValidationException::withMessages([
            'email' => 'Email ou senha inválidos.',
        ]);
    }

    /**
     * Log the user out of the application.
     */
    public function logout(Request $request)
    {
        $user = Auth::user();
        
        Auth::logout();

        $request->session()->invalidate();
        $request->session()->regenerateToken();

        Log::info('Usuário deslogado.', ['email' => $user?->email]);

        return redirect('/');
    }
}
