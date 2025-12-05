using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LearningApp.Data.Migrations
{
    /// <inheritdoc />
    public partial class VocabSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "SetID",
                table: "VocabCards",
                newName: "VocabSetId");

            migrationBuilder.CreateTable(
                name: "VocabSets",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VocabSets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_VocabCards_VocabSetId",
                table: "VocabCards",
                column: "VocabSetId");

            migrationBuilder.AddForeignKey(
                name: "FK_VocabCards_VocabSets_VocabSetId",
                table: "VocabCards",
                column: "VocabSetId",
                principalTable: "VocabSets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VocabCards_VocabSets_VocabSetId",
                table: "VocabCards");

            migrationBuilder.DropTable(
                name: "VocabSets");

            migrationBuilder.DropIndex(
                name: "IX_VocabCards_VocabSetId",
                table: "VocabCards");

            migrationBuilder.RenameColumn(
                name: "VocabSetId",
                table: "VocabCards",
                newName: "SetID");
        }
    }
}
