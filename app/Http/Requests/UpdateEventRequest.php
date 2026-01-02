<?php

namespace App\Http\Requests;

class UpdateEventRequest extends StoreEventRequest
{
    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        // Same rules as StoreEventRequest
        return parent::rules();
    }
}
