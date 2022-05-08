using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndFinalExam.Migrations
{
    public partial class CreatedTax : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "EcoTax",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "VAT",
                table: "Products",
                type: "money",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EcoTax",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "VAT",
                table: "Products");
        }
    }
}
