using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_DataAccess.Migrations
{
    public partial class AddUsersIdsToChatEntity : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UsersIds",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UsersIds",
                table: "Chats");
        }
    }
}
