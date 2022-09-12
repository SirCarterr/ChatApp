using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_DataAccess.Migrations
{
    public partial class AddEditedFlagToMessage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEdited",
                table: "ChatMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsEdited",
                table: "ChatMessages");
        }
    }
}
