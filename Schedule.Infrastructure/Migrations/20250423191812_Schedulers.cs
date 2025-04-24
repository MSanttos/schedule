using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Schedule.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Schedulers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedulers",
                table: "Schedulers");

            migrationBuilder.RenameTable(
                name: "Schedulers",
                newName: "Schedulers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedulers",
                table: "Schedulers",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Schedulers",
                table: "Schedulers");

            migrationBuilder.RenameTable(
                name: "Schedulers",
                newName: "Schedulers");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Schedulers",
                table: "Schedulers",
                column: "Id");
        }
    }
}
