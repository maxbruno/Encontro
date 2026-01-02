<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('event_participants', function (Blueprint $table) {
            $table->id();
            
            // Foreign Keys
            $table->foreignId('event_id')->constrained('events')->onDelete('cascade');
            $table->foreignId('person_id')->constrained('people')->onDelete('restrict');
            $table->foreignId('team_id')->nullable()->constrained('teams');
            $table->foreignId('role_id')->nullable()->constrained('roles');
            
            $table->dateTime('registered_at');
            $table->integer('stage');
            $table->text('notes')->nullable();
            $table->boolean('indicated')->nullable();
            $table->boolean('accepted')->nullable();
            
            // Soft Deletes
            $table->boolean('is_deleted')->default(false);
            $table->string('deleted_by')->nullable();
            $table->softDeletes();
            
            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('event_participants');
    }
};
