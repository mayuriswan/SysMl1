using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project__1.Migrations
{
    /// <inheritdoc />
    public partial class last : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_AssignedBadges",
                table: "AssignedBadges");

            migrationBuilder.RenameTable(
                name: "AssignedBadges",
                newName: "vw_assignedbadge");

            migrationBuilder.AlterColumn<string>(
                name: "BadgeId",
                table: "Stamps",
                type: "text",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");

            migrationBuilder.AddPrimaryKey(
                name: "PK_vw_assignedbadge",
                table: "vw_assignedbadge",
                column: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_vw_assignedbadge",
                table: "vw_assignedbadge");

            migrationBuilder.RenameTable(
                name: "vw_assignedbadge",
                newName: "AssignedBadges");

            migrationBuilder.AlterColumn<float>(
                name: "BadgeId",
                table: "Stamps",
                type: "real",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddPrimaryKey(
                name: "PK_AssignedBadges",
                table: "AssignedBadges",
                column: "Id");
        }
    }
}
