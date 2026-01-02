<?php

namespace App\Http\Requests;

class UpdatePersonRequest extends StorePersonRequest
{
    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        // Same rules as StorePersonRequest
        return parent::rules();
    }
}
