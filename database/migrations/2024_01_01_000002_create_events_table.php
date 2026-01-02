<?php

use Illuminate\Database\Migrations\Migration;
use Illuminate\Database\Schema\Blueprint;
use Illuminate\Support\Facades\Schema;

return new class extends Migration
{
    public function up(): void
    {
        Schema::create('events', function (Blueprint $table) {
            $table->id();
            $table->string('name', 100);
            $table->integer('event_type'); // Enum stored as int
            $table->string('patron_saint_name', 100)->nullable();
            $table->string('patron_saint_image_url', 255)->nullable();
            
            // Soft Deletes
            $table->boolean('is_deleted')->default(false);
            $table->string('deleted_by')->nullable();
            $table->softDeletes();

            $table->timestamps();
        });
    }

    public function down(): void
    {
        Schema::dropIfExists('events');
    }
};
