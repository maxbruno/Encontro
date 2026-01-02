<?php

namespace App\Http\Requests;

use Illuminate\Foundation\Http\FormRequest;

class StorePersonRequest extends FormRequest
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
            'name' => 'required|string|max:150',
            'type' => 'nullable|string|max:50|in:Casal,Jovem,Adolescente,Individual',
            'spouse' => 'nullable|required_if:type,Casal|string|max:150',
            'birth_date' => 'nullable|date|before_or_equal:today',
            'cell_phone' => 'nullable|string|max:20',
            'email' => 'nullable|email|max:150',
            'address' => 'nullable|string|max:200',
            'phone' => 'nullable|string|max:20',
            'district' => 'nullable|string|max:100',
            'zip_code' => 'nullable|string|max:10',
            'group' => 'nullable|string|max:100',
            'father_name' => 'nullable|string|max:150',
            'mother_name' => 'nullable|string|max:150',
            'notes' => 'nullable|string|max:500',
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
            'name.required' => 'O nome é obrigatório',
            'name.max' => 'O nome deve ter no máximo 150 caracteres',
            'type.in' => 'O tipo selecionado é inválido',
            'spouse.required_if' => 'O cônjuge é obrigatório para o tipo Casal',
            'spouse.max' => 'O nome do cônjuge deve ter no máximo 150 caracteres',
            'birth_date.date' => 'Data de nascimento inválida',
            'birth_date.before_or_equal' => 'A data de nascimento não pode ser futura',
            'cell_phone.max' => 'O celular deve ter no máximo 20 caracteres',
            'email.email' => 'Formato de email inválido',
            'email.max' => 'O email deve ter no máximo 150 caracteres',
            'address.max' => 'O endereço deve ter no máximo 200 caracteres',
            'phone.max' => 'O telefone deve ter no máximo 20 caracteres',
            'district.max' => 'O bairro deve ter no máximo 100 caracteres',
            'zip_code.max' => 'O CEP deve ter no máximo 10 caracteres',
            'group.max' => 'O grupo deve ter no máximo 100 caracteres',
            'father_name.max' => 'O nome do pai deve ter no máximo 150 caracteres',
            'mother_name.max' => 'O nome da mãe deve ter no máximo 150 caracteres',
            'notes.max' => 'As observações devem ter no máximo 500 caracteres',
            'photo.image' => 'O arquivo deve ser uma imagem',
            'photo.mimes' => 'A foto deve ser nos formatos: JPG, PNG, GIF, BMP',
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
            'type' => 'tipo',
            'spouse' => 'cônjuge',
            'birth_date' => 'data de nascimento',
            'cell_phone' => 'celular',
            'email' => 'email',
            'address' => 'endereço',
            'phone' => 'telefone',
            'district' => 'bairro',
            'zip_code' => 'CEP',
            'group' => 'grupo',
            'father_name' => 'nome do pai',
            'mother_name' => 'nome da mãe',
            'notes' => 'observações',
            'photo' => 'foto',
        ];
    }
}
