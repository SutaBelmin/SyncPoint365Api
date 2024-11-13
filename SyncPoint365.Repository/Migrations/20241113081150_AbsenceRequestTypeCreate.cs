using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPoint365.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AbsenceRequestTypeCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbsenceRequestType",
                table: "AbsenceRequestType");

            migrationBuilder.RenameTable(
                name: "AbsenceRequestType",
                newName: "AbsenceRequestTypes");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbsenceRequestTypes",
                table: "AbsenceRequestTypes",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AbsenceRequestTypes",
                table: "AbsenceRequestTypes");

            migrationBuilder.RenameTable(
                name: "AbsenceRequestTypes",
                newName: "AbsenceRequestType");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AbsenceRequestType",
                table: "AbsenceRequestType",
                column: "Id");
        }
    }
}
