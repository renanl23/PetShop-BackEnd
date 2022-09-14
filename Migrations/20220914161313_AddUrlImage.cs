﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PetShop_BackEnd.Migrations
{
    public partial class AddUrlImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "url_image",
                table: "Products",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "url_image",
                table: "Products");
        }
    }
}
