using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class RevertTextCanBeNull : Migration
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
            type: "varchar(255)", 
            nullable: false, 
            oldClrType: typeof(string),
            oldType: "text",
            oldNullable: true);

    }
}
