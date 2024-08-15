﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class TextCanBeNull2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
          name: "Text",
          table: "Tweet",
          type: "varchar(300)",
          maxLength: 300,
          nullable: true,
          oldClrType: typeof(string),
          oldType: "varchar(300)",
          oldMaxLength: 300)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
                migrationBuilder.AlterColumn<string>(
          name: "Text",
          table: "Tweet",
          type: "varchar(300)",
          maxLength: 300,
          nullable: false,
          oldClrType: typeof(string),
          oldType: "varchar(300)",
          oldMaxLength: 300)
          .Annotation("MySql:CharSet", "utf8mb4")
          .OldAnnotation("MySql:CharSet", "utf8mb4");

    }
}
