using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace UsageDemo.Migrations
{
    /// <inheritdoc />
    public partial class UsageInfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsageInformation",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UsageName = table.Column<string>(type: "text", nullable: true),
                    UsageKey = table.Column<string>(type: "text", nullable: true),
                    UsageServices = table.Column<string>(type: "text", nullable: true),
                    UsageCharge = table.Column<string>(type: "text", nullable: true),
                    UsageDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsageInformation", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsageInformation");
        }
    }
}
