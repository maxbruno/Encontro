<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StoreEventRequest extends FormRequest
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
            'name' => 'required|string|max:100',
            'event_type' => 'required|integer|in:1,2', // 1=Segue-me, 2=ECC
            'patron_saint_name' => 'nullable|string|max:100',
            'photo' => 'nullable|image|mimes:jpeg,png,jpg,gif,bmp|max:5120', // 5MB max
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
            'name.required' => 'O nome do evento é obrigatório',
            'name.max' => 'O nome deve ter no máximo 100 caracteres',
            'event_type.required' => 'O tipo de evento é obrigatório',
            'event_type.in' => 'O tipo de evento selecionado é inválido',
            'patron_saint_name.max' => 'O nome do santo padroeiro deve ter no máximo 100 caracteres',
            'photo.image' => 'O arquivo deve ser uma imagem',
            'photo.mimes' => 'A imagem deve ser nos formatos: JPG, PNG, GIF, BMP',
            'photo.max' => 'O arquivo é muito grande. O tamanho máximo é 5MB',
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
            'name' => 'nome',
            'event_type' => 'tipo de evento',
            'patron_saint_name' => 'santo padroeiro',
            'photo' => 'imagem',
        ];
    }
}
