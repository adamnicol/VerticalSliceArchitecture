using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VerticalSliceArchitecture.Infrastructure.Migrations;

/// <inheritdoc />
public partial class _20261112193745_InitialCreate : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Users",
            columns: table => new
            {
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                ExternalId = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                EmailAddress = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: true),
                Administrator = table.Column<bool>(type: "bit", nullable: false),
                CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                ModifiedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                Status = table.Column<int>(type: "int", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Users", x => x.UserId);
            });

        migrationBuilder.CreateIndex(
            name: "IX_Users_EmailAddress",
            table: "Users",
            column: "EmailAddress",
            unique: true);

        migrationBuilder.CreateIndex(
            name: "IX_Users_ExternalId",
            table: "Users",
            column: "ExternalId",
            unique: true,
            filter: "[ExternalId] IS NOT NULL");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "Users");
    }
}
