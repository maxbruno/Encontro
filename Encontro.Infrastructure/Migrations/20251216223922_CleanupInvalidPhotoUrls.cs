using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Encontro.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CleanupInvalidPhotoUrls : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Limpar PhotoUrls que referenciam arquivos que não existem mais
            migrationBuilder.Sql(@"
                UPDATE People 
                SET PhotoUrl = NULL 
                WHERE PhotoUrl IS NOT NULL 
                AND PhotoUrl LIKE '/uploads/%'
                AND PhotoUrl NOT IN (
                    '/uploads/04aea89b-8b42-4389-a333-d56650e73b13.jpg',
                    '/uploads/314a51fc-c49a-4e4b-9d14-a078bb4ace77.jpg',
                    '/uploads/5297b624-89d9-41d8-84b9-4205a61bc759.jpg',
                    '/uploads/595ac005-e48c-4b92-b7c2-3f8bb8f7b8ce.jpg',
                    '/uploads/5c0416f7-20c6-470c-b318-dc35bdabd864.jpg',
                    '/uploads/6df0b6bf-2ca9-482b-92a2-9802797f8f7c.jpg',
                    '/uploads/7d1de09c-5dce-4ee4-be5d-1283224bc292.jpg',
                    '/uploads/b7141968-30e3-438c-a76b-1deca5749e55.jpg',
                    '/uploads/dd828525-8db3-46e5-af7c-cc79d9b4d320.jpg',
                    '/uploads/ee681c1b-d7f3-4ee1-a4ef-3e9c32ff218d.jpg',
                    '/uploads/f27facd7-2ff4-497b-9b49-038a8c988bfd.jpg',
                    '/uploads/ffbc49b7-0b89-40b9-ac51-03741149384f.jpg'
                );
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
