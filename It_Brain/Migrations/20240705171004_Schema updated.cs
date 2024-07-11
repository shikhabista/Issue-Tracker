using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IT_Web.Migrations
{
    /// <inheritdoc />
    public partial class Schemaupdated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "base");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "Base",
                newName: "user",
                newSchema: "base");

            migrationBuilder.RenameTable(
                name: "organization_info",
                schema: "Base",
                newName: "organization_info",
                newSchema: "base");

            migrationBuilder.RenameTable(
                name: "branch",
                schema: "Base",
                newName: "branch",
                newSchema: "base");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Base");

            migrationBuilder.RenameTable(
                name: "user",
                schema: "base",
                newName: "user",
                newSchema: "Base");

            migrationBuilder.RenameTable(
                name: "organization_info",
                schema: "base",
                newName: "organization_info",
                newSchema: "Base");

            migrationBuilder.RenameTable(
                name: "branch",
                schema: "base",
                newName: "branch",
                newSchema: "Base");
        }
    }
}
