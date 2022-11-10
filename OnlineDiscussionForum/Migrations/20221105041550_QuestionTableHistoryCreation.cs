using Microsoft.EntityFrameworkCore.Migrations;

namespace OnlineDiscussionForum.Migrations
{
    public partial class QuestionTableHistoryCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           
            
            migrationBuilder.CreateTable(
                name: "QuestionsHistory",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(nullable: false),
                    userId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QuestionsHistory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_QuestionsHistory_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_QuestionsHistory_userId",
                table: "QuestionsHistory",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_QuestionsHistory_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "QuestionsHistory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
            migrationBuilder.DropTable(
                name: "QuestionsHistory");

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    text = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Users_userId",
                        column: x => x.userId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_userId",
                table: "Questions",
                column: "userId");

            migrationBuilder.AddForeignKey(
                name: "FK_Answers_Questions_QuestionId",
                table: "Answers",
                column: "QuestionId",
                principalTable: "Questions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
