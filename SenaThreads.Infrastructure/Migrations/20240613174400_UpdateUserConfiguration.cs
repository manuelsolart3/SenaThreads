using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class UpdateUserConfiguration : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterColumn<string>(
            name: "ProfilePictureS3Key",
            table: "User",
            type: "longtext",
            nullable: true,
            oldClrType: typeof(string),
            oldType: "longtext")
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "User",
            type: "varchar(20)",
            maxLength: 20,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(20)",
            oldMaxLength: 20)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "User",
            type: "varchar(25)",
            maxLength: 25,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(25)",
            oldMaxLength: 25)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.AlterColumn<string>(
            name: "Biography",
            table: "User",
            type: "varchar(50)",
            maxLength: 50,
            nullable: true,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "ProfilePictureS3Key",
            keyValue: null,
            column: "ProfilePictureS3Key",
            value: "");

        migrationBuilder.AlterColumn<string>(
            name: "ProfilePictureS3Key",
            table: "User",
            type: "longtext",
            nullable: false,
            oldClrType: typeof(string),
            oldType: "longtext",
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "PhoneNumber",
            keyValue: null,
            column: "PhoneNumber",
            value: "");

        migrationBuilder.AlterColumn<string>(
            name: "PhoneNumber",
            table: "User",
            type: "varchar(20)",
            maxLength: 20,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(20)",
            oldMaxLength: 20,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "City",
            keyValue: null,
            column: "City",
            value: "");

        migrationBuilder.AlterColumn<string>(
            name: "City",
            table: "User",
            type: "varchar(25)",
            maxLength: 25,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(25)",
            oldMaxLength: 25,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.UpdateData(
            table: "User",
            keyColumn: "Biography",
            keyValue: null,
            column: "Biography",
            value: "");

        migrationBuilder.AlterColumn<string>(
            name: "Biography",
            table: "User",
            type: "varchar(50)",
            maxLength: 50,
            nullable: false,
            oldClrType: typeof(string),
            oldType: "varchar(50)",
            oldMaxLength: 50,
            oldNullable: true)
            .Annotation("MySql:CharSet", "utf8mb4")
            .OldAnnotation("MySql:CharSet", "utf8mb4");
    }
}
