<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('people', function (Blueprint $table) {
            $table->id();
            $table->string('name', 150);
            $table->string('type', 50)->nullable();
            $table->string('spouse', 150)->nullable();
            $table->date('birth_date')->nullable();
            $table->string('cell_phone', 20)->nullable();
            $table->string('email', 150)->nullable();
            $table->string('address', 200)->nullable();
            $table->string('phone', 20)->nullable();
            $table->string('district', 100)->nullable();
            $table->string('zip_code', 10)->nullable();
            $table->string('group', 100)->nullable();
            $table->string('father_name', 150)->nullable();
            $table->string('mother_name', 150)->nullable();
            $table->text('notes')->nullable(); // Using text for 500 chars+ flexibility
            $table->string('photo_url', 255)->nullable();
            
            // Soft Deletes map
            $table->boolean('is_deleted')->default(false);
            $table->string('deleted_by')->nullable();
            $table->softDeletes(); // Creates deleted_at
            
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('people');
    }
};
