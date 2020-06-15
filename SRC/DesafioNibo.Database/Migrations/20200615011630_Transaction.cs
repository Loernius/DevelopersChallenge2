using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Desafio.Database.Migrations
{
    public partial class Transaction : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Extrato",
                columns: table => new
                {
                    id = table.Column<Guid>(nullable: false),
                    active = table.Column<bool>(nullable: false),
                    type = table.Column<string>(nullable: true),
                    date = table.Column<DateTime>(nullable: false),
                    memo = table.Column<string>(nullable: true),
                    amount = table.Column<float>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Extrato", x => x.id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Extrato");
        }
    }
}
