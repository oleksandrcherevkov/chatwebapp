using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChatApi.EF.Migrations
{
    public partial class RenamedAllReceiverProperties : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessages_Users_ReciverId",
                table: "PrivateMessages");

            migrationBuilder.RenameColumn(
                name: "ReciverId",
                table: "PrivateMessages",
                newName: "ReceiverId");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateMessages_ReciverId",
                table: "PrivateMessages",
                newName: "IX_PrivateMessages_ReceiverId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "GroupImageUrl",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessages_Users_ReceiverId",
                table: "PrivateMessages",
                column: "ReceiverId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PrivateMessages_Users_ReceiverId",
                table: "PrivateMessages");

            migrationBuilder.RenameColumn(
                name: "ReceiverId",
                table: "PrivateMessages",
                newName: "ReciverId");

            migrationBuilder.RenameIndex(
                name: "IX_PrivateMessages_ReceiverId",
                table: "PrivateMessages",
                newName: "IX_PrivateMessages_ReciverId");

            migrationBuilder.AlterColumn<string>(
                name: "ProfileImageUrl",
                table: "Users",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GroupImageUrl",
                table: "Groups",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PrivateMessages_Users_ReciverId",
                table: "PrivateMessages",
                column: "ReciverId",
                principalTable: "Users",
                principalColumn: "UserId");
        }
    }
}
