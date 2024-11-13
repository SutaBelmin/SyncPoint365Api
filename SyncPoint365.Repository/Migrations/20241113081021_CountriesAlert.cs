using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPoint365.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CountriesAlert : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Capital",
                table: "Countries");

            migrationBuilder.RenameColumn(
                name: "Continent",
                table: "Countries",
                newName: "DisplayName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DisplayName",
                table: "Countries",
                newName: "Continent");

            migrationBuilder.AddColumn<string>(
                name: "Capital",
                table: "Countries",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
