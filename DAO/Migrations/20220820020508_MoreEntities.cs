using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAO.Migrations
{
    public partial class MoreEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "datetime2",
                table: "CHAMADOS",
                newName: "DataInicio");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataFim",
                table: "CHAMADOS",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataFim",
                table: "CHAMADOS");

            migrationBuilder.RenameColumn(
                name: "DataInicio",
                table: "CHAMADOS",
                newName: "datetime2");
        }
    }
}
