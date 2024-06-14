using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class AddEntityConfigurations2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "CreatedBy",
            table: "Follow",
            type: "longtext",
            nullable: false)
            .Annotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedBy",
            table: "Follow");
    }
}
