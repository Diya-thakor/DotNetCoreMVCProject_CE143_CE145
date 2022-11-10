using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineDiscussionForum.Migrations
{
    public partial class AnswerUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "text",
                table: "Answers",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "text",
                table: "Answers",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));
        }
    }
}
