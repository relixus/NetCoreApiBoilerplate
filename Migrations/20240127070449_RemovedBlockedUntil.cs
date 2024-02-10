using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCoreApiBoilerplate.Migrations
{
    /// <inheritdoc />
    public partial class RemovedBlockedUntil : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BlockedUntil",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "BlockedUntil",
                table: "AspNetUsers",
                type: "datetime2",
                nullable: true);
        }
    }
}
