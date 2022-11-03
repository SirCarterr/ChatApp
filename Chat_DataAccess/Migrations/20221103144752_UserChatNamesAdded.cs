using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Chat_DataAccess.Migrations
{
    public partial class UserChatNamesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Chats",
                newName: "CommunicationType");

            migrationBuilder.AddColumn<string>(
                name: "GroupName",
                table: "Chats",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserChatNames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ChatId = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserChatNames", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserChatNames");

            migrationBuilder.DropColumn(
                name: "GroupName",
                table: "Chats");

            migrationBuilder.RenameColumn(
                name: "CommunicationType",
                table: "Chats",
                newName: "Name");
        }
    }
}
