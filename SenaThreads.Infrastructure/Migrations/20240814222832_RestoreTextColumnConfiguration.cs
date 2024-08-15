using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RestoreTextColumnConfiguration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {

    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
name: "Text",
table: "Tweets",
type: "varchar(300)",
maxLength: 300,
nullable: false,
oldClrType: typeof(string),
oldType: "varchar(300)",
oldMaxLength: 300,
oldNullable: true)
.Annotation("MySql:CharSet", "utf8mb4");

    }
}
