using Microsoft.EntityFrameworkCore.Migrations;

namespace TimeApp.DataAccess.Migrations
{
    public partial class AddLanguage_IdToDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "LanguageId",
                table: "Users",
                newName: "Language_Id");

            migrationBuilder.RenameIndex(
                name: "IX_Users_LanguageId",
                table: "Users",
                newName: "IX_Users_Language_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_Language_Id",
                table: "Users",
                column: "Language_Id",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Languages_Language_Id",
                table: "Users");

            migrationBuilder.RenameColumn(
                name: "Language_Id",
                table: "Users",
                newName: "LanguageId");

            migrationBuilder.RenameIndex(
                name: "IX_Users_Language_Id",
                table: "Users",
                newName: "IX_Users_LanguageId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Languages_LanguageId",
                table: "Users",
                column: "LanguageId",
                principalTable: "Languages",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
