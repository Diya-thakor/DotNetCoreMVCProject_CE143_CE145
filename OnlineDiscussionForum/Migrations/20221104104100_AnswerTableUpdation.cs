using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineDiscussionForum.Migrations
{
    public partial class AnswerTableUpdation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            migrationBuilder.AddColumn<int>(
                name: "noOfLike",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "Answers",
                nullable: false,
                defaultValue: 0);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.DropColumn(
                name: "noOfLike",
                table: "Answers");

            migrationBuilder.DropColumn(
                name: "status",
                table: "Answers");

        }
    }
}
