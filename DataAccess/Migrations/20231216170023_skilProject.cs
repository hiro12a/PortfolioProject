using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class skilProject : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Projects_ProjectsId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Skills_SkillsId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProjectsId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "SkillsId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProjectId",
                table: "Pictures",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Projects_ProjectId",
                table: "Pictures",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Skills_SkillsId",
                table: "Pictures",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Projects_ProjectId",
                table: "Pictures");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Skills_SkillsId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProjectId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Pictures");

            migrationBuilder.AlterColumn<int>(
                name: "SkillsId",
                table: "Pictures",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ProjectsId",
                table: "Pictures",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ProjectsId",
                table: "Pictures",
                column: "ProjectsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Projects_ProjectsId",
                table: "Pictures",
                column: "ProjectsId",
                principalTable: "Projects",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Skills_SkillsId",
                table: "Pictures",
                column: "SkillsId",
                principalTable: "Skills",
                principalColumn: "Id");
        }
    }
}
