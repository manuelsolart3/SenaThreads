using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class UpdateUserProperty : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateOnly>(
            name: "DateOfBirth",
            table: "User",
            type: "date",
            nullable: false,
            defaultValueSql: "NULL",
            oldClrType: typeof(DateOnly),
            oldType: "date");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<DateOnly>(
            name: "DateOfBirth",
            table: "User",
            type: "date",
            nullable: false,
            oldClrType: typeof(DateOnly),
            oldType: "date",
            oldDefaultValueSql: "NULL");
    }
}
