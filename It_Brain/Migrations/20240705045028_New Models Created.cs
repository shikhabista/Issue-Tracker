using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace IT_Web.Migrations
{
    /// <inheritdoc />
    public partial class NewModelsCreated : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "it");

            migrationBuilder.CreateTable(
                name: "label",
                schema: "it",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    code = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_label", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "repository",
                schema: "it",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    visibility = table.Column<string>(type: "text", nullable: false),
                    status = table.Column<int>(type: "integer", nullable: false),
                    rec_by_id = table.Column<long>(type: "bigint", nullable: true),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    branch = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_repository", x => x.id);
                    table.ForeignKey(
                        name: "fk_repository_user_rec_by_id",
                        column: x => x.rec_by_id,
                        principalSchema: "Base",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "issue",
                schema: "it",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "text", nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    assignee_id = table.Column<long>(type: "bigint", nullable: true),
                    issue_status = table.Column<int>(type: "integer", nullable: false),
                    date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    repository_id = table.Column<long>(type: "bigint", nullable: false),
                    last_updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_issue", x => x.id);
                    table.ForeignKey(
                        name: "fk_issue_repository_repository_id",
                        column: x => x.repository_id,
                        principalSchema: "it",
                        principalTable: "repository",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_user_assignee_id",
                        column: x => x.assignee_id,
                        principalSchema: "Base",
                        principalTable: "user",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "issue_label",
                schema: "it",
                columns: table => new
                {
                    id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    issue_id = table.Column<long>(type: "bigint", nullable: false),
                    label_id = table.Column<long>(type: "bigint", nullable: false),
                    rec_date = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_issue_label", x => x.id);
                    table.ForeignKey(
                        name: "fk_issue_label_issue_issue_id",
                        column: x => x.issue_id,
                        principalSchema: "it",
                        principalTable: "issue",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "fk_issue_label_label_label_id",
                        column: x => x.label_id,
                        principalSchema: "it",
                        principalTable: "label",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_issue_assignee_id",
                schema: "it",
                table: "issue",
                column: "assignee_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_repository_id",
                schema: "it",
                table: "issue",
                column: "repository_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_label_issue_id",
                schema: "it",
                table: "issue_label",
                column: "issue_id");

            migrationBuilder.CreateIndex(
                name: "ix_issue_label_label_id",
                schema: "it",
                table: "issue_label",
                column: "label_id");

            migrationBuilder.CreateIndex(
                name: "ix_repository_rec_by_id",
                schema: "it",
                table: "repository",
                column: "rec_by_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "issue_label",
                schema: "it");

            migrationBuilder.DropTable(
                name: "issue",
                schema: "it");

            migrationBuilder.DropTable(
                name: "label",
                schema: "it");

            migrationBuilder.DropTable(
                name: "repository",
                schema: "it");
        }
    }
}
