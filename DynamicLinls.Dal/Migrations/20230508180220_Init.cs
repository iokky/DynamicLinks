using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DynamicLinks.Dal.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DynamicLinks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ShortLink = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WebLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AndroidLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AndroidDefaultLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IOSLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IOSDefaultLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Enable = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DynamicLinks", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DynamicLinks");
        }
    }
}
