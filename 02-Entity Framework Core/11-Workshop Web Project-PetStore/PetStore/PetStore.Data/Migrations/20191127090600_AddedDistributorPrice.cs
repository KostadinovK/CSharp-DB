﻿using Microsoft.EntityFrameworkCore.Migrations;

namespace PetStore.Data.Migrations
{
    public partial class AddedDistributorPrice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "DistributorPrice",
                table: "Toys",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DistributorPrice",
                table: "Pets",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "DistributorPrice",
                table: "Food",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DistributorPrice",
                table: "Toys");

            migrationBuilder.DropColumn(
                name: "DistributorPrice",
                table: "Pets");

            migrationBuilder.DropColumn(
                name: "DistributorPrice",
                table: "Food");
        }
    }
}
