using Microsoft.EntityFrameworkCore.Migrations;

namespace OefenRelations.Migrations
{
    public partial class _2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenten_Country_CountryId",
                table: "Studenten");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Studenten",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_Studenten_Country_CountryId",
                table: "Studenten",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Studenten_Country_CountryId",
                table: "Studenten");

            migrationBuilder.AlterColumn<int>(
                name: "CountryId",
                table: "Studenten",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Studenten_Country_CountryId",
                table: "Studenten",
                column: "CountryId",
                principalTable: "Country",
                principalColumn: "CountryId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
