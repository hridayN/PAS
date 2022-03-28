using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PAS.API.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "policy_administration_system");

            migrationBuilder.CreateTable(
                name: "code_list",
                schema: "policy_administration_system",
                columns: table => new
                {
                    code_list_id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    code_list_title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code_list_reference = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    code_list_version = table.Column<int>(type: "int", nullable: false),
                    code_list_description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    enumeration_code_list = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_code_list", x => x.code_list_id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "code_list",
                schema: "policy_administration_system");
        }
    }
}
