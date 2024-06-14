using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SenaThreads.Infrastructure.Migrations;

/// <inheritdoc />
public partial class CreateDataBase : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AlterDatabase()
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "User",
            columns: table => new
            {
                Id = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FirstName = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                LastName = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ProfilePictureS3Key = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PhoneNumber = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Biography = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                City = table.Column<string>(type: "varchar(25)", maxLength: 25, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                DateOfBirth = table.Column<DateOnly>(type: "date", nullable: false),
                UserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                NormalizedUserName = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Email = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                NormalizedEmail = table.Column<string>(type: "varchar(256)", maxLength: 256, nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                PasswordHash = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                SecurityStamp = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                AccessFailedCount = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_User", x => x.Id);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Event",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Title = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Description = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Image = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                EventDate = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Event", x => x.Id);
                table.ForeignKey(
                    name: "FK_Event_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Follow",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                FollowerUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                FollowedByUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Follow", x => x.Id);
                table.ForeignKey(
                    name: "FK_Follow_User_FollowedByUserId",
                    column: x => x.FollowedByUserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Follow_User_FollowerUserId",
                    column: x => x.FollowerUserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Notification",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<int>(type: "int", nullable: false),
                Path = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                IsRead = table.Column<bool>(type: "tinyint(1)", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Notification", x => x.Id);
                table.ForeignKey(
                    name: "FK_Notification_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Tweet",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Text = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Tweet", x => x.Id);
                table.ForeignKey(
                    name: "FK_Tweet_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "UserBlock",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                BlockedUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BlockByUserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                BlockSatus = table.Column<int>(type: "int", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserBlock", x => x.Id);
                table.ForeignKey(
                    name: "FK_UserBlock_User_BlockByUserId",
                    column: x => x.BlockByUserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserBlock_User_BlockedUserId",
                    column: x => x.BlockedUserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Comment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TweetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Text = table.Column<string>(type: "varchar(180)", maxLength: 180, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                TweetId1 = table.Column<Guid>(type: "char(36)", nullable: true, collation: "ascii_general_ci"),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Comment", x => x.Id);
                table.ForeignKey(
                    name: "FK_Comment_Tweet_TweetId",
                    column: x => x.TweetId,
                    principalTable: "Tweet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Comment_Tweet_TweetId1",
                    column: x => x.TweetId1,
                    principalTable: "Tweet",
                    principalColumn: "Id");
                table.ForeignKey(
                    name: "FK_Comment_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Reaction",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TweetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                UserId = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Type = table.Column<int>(type: "int", nullable: false),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Reaction", x => x.Id);
                table.ForeignKey(
                    name: "FK_Reaction_Tweet_TweetId",
                    column: x => x.TweetId,
                    principalTable: "Tweet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Reaction_User_UserId",
                    column: x => x.UserId,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "Retweet",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TweetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                RetweetedById = table.Column<string>(type: "varchar(255)", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                Comment = table.Column<string>(type: "varchar(300)", maxLength: 300, nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Retweet", x => x.Id);
                table.ForeignKey(
                    name: "FK_Retweet_Tweet_TweetId",
                    column: x => x.TweetId,
                    principalTable: "Tweet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_Retweet_User_RetweetedById",
                    column: x => x.RetweetedById,
                    principalTable: "User",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateTable(
            name: "TweetAttachment",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                TweetId = table.Column<Guid>(type: "char(36)", nullable: false, collation: "ascii_general_ci"),
                Key = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                CreatedOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                CreatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4"),
                UpdateOnUtc = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                UpdatedBy = table.Column<string>(type: "longtext", nullable: false)
                    .Annotation("MySql:CharSet", "utf8mb4")
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_TweetAttachment", x => x.Id);
                table.ForeignKey(
                    name: "FK_TweetAttachment_Tweet_TweetId",
                    column: x => x.TweetId,
                    principalTable: "Tweet",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            })
            .Annotation("MySql:CharSet", "utf8mb4");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_TweetId",
            table: "Comment",
            column: "TweetId");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_TweetId1",
            table: "Comment",
            column: "TweetId1");

        migrationBuilder.CreateIndex(
            name: "IX_Comment_UserId",
            table: "Comment",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Event_UserId",
            table: "Event",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Follow_FollowedByUserId",
            table: "Follow",
            column: "FollowedByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Follow_FollowerUserId",
            table: "Follow",
            column: "FollowerUserId");

        migrationBuilder.CreateIndex(
            name: "IX_Notification_UserId",
            table: "Notification",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Reaction_TweetId",
            table: "Reaction",
            column: "TweetId");

        migrationBuilder.CreateIndex(
            name: "IX_Reaction_UserId",
            table: "Reaction",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_Retweet_RetweetedById",
            table: "Retweet",
            column: "RetweetedById");

        migrationBuilder.CreateIndex(
            name: "IX_Retweet_TweetId",
            table: "Retweet",
            column: "TweetId");

        migrationBuilder.CreateIndex(
            name: "IX_Tweet_UserId",
            table: "Tweet",
            column: "UserId");

        migrationBuilder.CreateIndex(
            name: "IX_TweetAttachment_TweetId",
            table: "TweetAttachment",
            column: "TweetId");

        migrationBuilder.CreateIndex(
            name: "EmailIndex",
            table: "User",
            column: "NormalizedEmail");

        migrationBuilder.CreateIndex(
            name: "UserNameIndex",
            table: "User",
            column: "NormalizedUserName",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_UserBlock_BlockByUserId",
            table: "UserBlock",
            column: "BlockByUserId");

        migrationBuilder.CreateIndex(
            name: "IX_UserBlock_BlockedUserId",
            table: "UserBlock",
            column: "BlockedUserId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Comment");

        migrationBuilder.DropTable(
            name: "Event");

        migrationBuilder.DropTable(
            name: "Follow");

        migrationBuilder.DropTable(
            name: "Notification");

        migrationBuilder.DropTable(
            name: "Reaction");

        migrationBuilder.DropTable(
            name: "Retweet");

        migrationBuilder.DropTable(
            name: "TweetAttachment");

        migrationBuilder.DropTable(
            name: "UserBlock");

        migrationBuilder.DropTable(
            name: "Tweet");

        migrationBuilder.DropTable(
            name: "User");
    }
}
