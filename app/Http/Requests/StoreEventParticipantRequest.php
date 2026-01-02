<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreEventParticipantRequest extends FormRequest
{
    /**
     * Determine if the user is authorized to make this request.
     */
    public function authorize(): bool
    {
        // TODO: Implement authorization when auth system is ready
        return true;
    }

    /**
     * Get the validation rules that apply to the request.
     *
     * @return array<string, \Illuminate\Contracts\Validation\ValidationRule|array<mixed>|string>
     */
    public function rules(): array
    {
        return [
            'event_id' => 'required|integer|exists:events,id',
            'person_id' => 'required|integer|exists:people,id',
            'team_id' => 'nullable|integer|exists:teams,id',
            'role_id' => 'nullable|integer|exists:roles,id',
            'stage' => 'required|integer|min:1|max:10',
            'notes' => 'nullable|string|max:500',
            'indicated' => 'nullable|boolean',
            'accepted' => 'nullable|boolean',
        ];
    }

    /**
     * Get custom error messages for validation rules.
     *
     * @return array<string, string>
     */
    public function messages(): array
    {
        return [
            'event_id.required' => 'O evento é obrigatório',
            'event_id.exists' => 'O evento selecionado não existe',
            'person_id.required' => 'A pessoa é obrigatória',
            'person_id.exists' => 'A pessoa selecionada não existe',
            'team_id.exists' => 'A equipe selecionada não existe',
            'role_id.exists' => 'A função selecionada não existe',
            'stage.required' => 'A etapa é obrigatória',
            'stage.min' => 'A etapa deve estar entre 1 e 10',
            'stage.max' => 'A etapa deve estar entre 1 e 10',
            'notes.max' => 'As observações devem ter no máximo 500 caracteres',
        ];
    }

    /**
     * Get custom attribute names for error messages.
     *
     * @return array<string, string>
     */
    public function attributes(): array
    {
        return [
            'event_id' => 'evento',
            'person_id' => 'pessoa',
            'team_id' => 'equipe',
            'role_id' => 'função',
            'stage' => 'etapa',
            'notes' => 'observações',
            'indicated' => 'indicado',
            'accepted' => 'aceitou',
        ];
    }
}
