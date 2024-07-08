using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateSearchHistoryTable : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "SearchUserHistory",
            columns: table => new
            {
                Id = table.Column<int>(type: "int", nullable: false)
                    .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                SearchedUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                SearchedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SearchUserHistory", x => x.Id);
                table.ForeignKey(
                    name: "FK_SearchUserHistory_User_SearchedUserId",
                    column: x => x.SearchedUserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
                table.ForeignKey(
                    name: "FK_SearchUserHistory_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Restrict);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_SearchUserHistory_SearchedUserId",
            table: "SearchUserHistory",
            column: "SearchedUserId");

        migrationBuilder.CreateIndex(
            name: "IX_SearchUserHistory_UserId",
            table: "SearchUserHistory",
            column: "UserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SearchUserHistory");
    }
}
