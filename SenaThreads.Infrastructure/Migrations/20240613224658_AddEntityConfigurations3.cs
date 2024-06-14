using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddEntityConfigurations3 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "UpdatedBy",
            table: "Follow",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AddColumn<DateTime>(
            name: "UpdatedOnUtc",
            table: "Follow",
            type: "datetime(6)",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "UpdatedBy",
            table: "Follow");

        migrationBuilder.DropColumn(
            name: "UpdatedOnUtc",
            table: "Follow");
    }
}
