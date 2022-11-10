using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineDiscussionForum.Migrations
{
    public partial class QU : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_QuestionsHistory_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_QuestionsHistory_Users_userId",
                table: "QuestionsHistory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_QuestionsHistory",
                table: "QuestionsHistory");

            migrationBuilder.RenameTable(
                name: "QuestionsHistory",
                newName: "Questions");

            migrationBuilder.RenameIndex(
                name: "IX_QuestionsHistory_userId",
                table: "Questions",
                newName: "IX_Questions_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Questions",
                table: "Questions",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Questions_Users_userId",
                table: "Questions",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers");

            migrationBuilder.DropForeignKey(
                name: "FK_Questions_Users_userId",
                table: "Questions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Questions",
                table: "Questions");

            migrationBuilder.RenameTable(
                name: "Questions",
                newName: "QuestionsHistory");

            migrationBuilder.RenameIndex(
                name: "IX_Questions_userId",
                table: "QuestionsHistory",
                newName: "IX_QuestionsHistory_userId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_QuestionsHistory",
                table: "QuestionsHistory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionsHistory_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "QuestionsHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QuestionsHistory_Users_userId",
                table: "QuestionsHistory",
                column: "userId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
