using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Projects_ProjectId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProjectId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Projects_ProjectsId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ProjectsId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ProjectsId",
                table: "Pictures");

            migrationBuilder.AlterColumn<string>(
                name: "type",
                table: "Skills",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
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
        }
    }
}
