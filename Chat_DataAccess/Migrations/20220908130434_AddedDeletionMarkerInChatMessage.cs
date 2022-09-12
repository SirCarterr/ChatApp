using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_DataAccess.Migrations
{
    public partial class AddedDeletionMarkerInChatMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToUsers",
                table: "ChatMessages");

            migrationBuilder.RenameColumn(
                name: "IsSeen",
                table: "ChatMessages",
                newName: "DeletedForCaller");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DeletedForCaller",
                table: "ChatMessages",
                newName: "IsSeen");

            migrationBuilder.AddColumn<string>(
                name: "ToUsers",
                table: "ChatMessages",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
