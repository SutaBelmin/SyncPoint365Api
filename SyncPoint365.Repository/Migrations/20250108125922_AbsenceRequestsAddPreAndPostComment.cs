using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SyncPoint365.Repository.Migrations
{
    /// <inheritdoc />
    public partial class AbsenceRequestsAddPreAndPostComment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "AbsenceRequests",
                newName: "PreComment");

            migrationBuilder.AddColumn<string>(
                name: "PostComment",
                table: "AbsenceRequests",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PostComment",
                table: "AbsenceRequests");

            migrationBuilder.RenameColumn(
                name: "PreComment",
                table: "AbsenceRequests",
                newName: "Comment");
        }
    }
}
