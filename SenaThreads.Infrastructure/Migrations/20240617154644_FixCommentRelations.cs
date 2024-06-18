using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class FixCommentRelations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropForeignKey(
            name: "FK_Comment_Tweet_TweetId1",
            table: "Comment");

        migrationBuilder.DropIndex(
            name: "IX_Comment_TweetId1",
            table: "Comment");

        migrationBuilder.DropColumn(
            name: "TweetId1",
            table: "Comment");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "TweetId1",
            table: "Comment",
            type: "char(36)",
            nullable: true,
            collation: "ascii_general_ci");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_TweetId1",
            table: "Comment",
            column: "TweetId1");

        migrationBuilder.AddForeignKey(
            name: "FK_Comment_Tweet_TweetId1",
            table: "Comment",
            column: "TweetId1",
            principalTable: "Tweet",
            principalColumn: "Id");
    }
}
