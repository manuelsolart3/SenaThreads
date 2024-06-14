using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddEntityConfigurations : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Follow");

        migrationBuilder.DropColumn(
            name: "UpdateOnUtc",
            table: "Follow");

        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Follow");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "UserBlock",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "TweetAttachment",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Tweet",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Retweet",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Reaction",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Notification",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Event",
            newName: "UpdatedOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdateOnUtc",
            table: "Comment",
            newName: "UpdatedOnUtc");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "UserBlock",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "TweetAttachment",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Tweet",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Retweet",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Reaction",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Notification",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Event",
            newName: "UpdateOnUtc");

        migrationBuilder.RenameColumn(
            name: "UpdatedOnUtc",
            table: "Comment",
            newName: "UpdateOnUtc");

        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Follow",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdateOnUtc",
            table: "Follow",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Follow",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");
    }
}
