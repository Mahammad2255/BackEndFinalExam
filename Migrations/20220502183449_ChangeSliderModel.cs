using Microsoft.EntityFrameworkCore.Migrations;

namespace BackEndFinalExam.Migrations
{
    public partial class ChangeSliderModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Address",
                table: "Sliders");

            migrationBuilder.DropColumn(
                name: "Logo",
                table: "Sliders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Address",
                table: "Sliders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Logo",
                table: "Sliders",
                type: "nvarchar(255)",
                maxLength: 255,
                nullable: true);
        }
    }
}
