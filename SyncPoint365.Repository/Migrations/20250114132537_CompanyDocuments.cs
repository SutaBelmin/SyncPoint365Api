using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPoint365.Repository.Migrations
{
    /// <inheritdoc />
    public partial class CompanyDocuments : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Extension",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "FilePath",
                table: "CompanyDocuments");

            migrationBuilder.DropColumn(
                name: "FileSize",
                table: "CompanyDocuments");

            migrationBuilder.AddColumn<byte[]>(
                name: "File",
                table: "CompanyDocuments",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "File",
                table: "CompanyDocuments");

            migrationBuilder.AddColumn<string>(
                name: "Extension",
                table: "CompanyDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "CompanyDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FilePath",
                table: "CompanyDocuments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<long>(
                name: "FileSize",
                table: "CompanyDocuments",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);
        }
    }
}
