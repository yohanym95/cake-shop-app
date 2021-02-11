 using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace shopapp.Migrations
{
    public partial class addpicturebyte : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "PictureByte",
                schema: "Identity",
                table: "Pies",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PictureByte",
                schema: "Identity",
                table: "Pies");
        }
    }
}
