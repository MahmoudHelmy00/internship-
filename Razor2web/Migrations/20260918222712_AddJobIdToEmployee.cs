using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Razor2web.Migrations
{
    /// <inheritdoc />
    public partial class AddJobIdToEmployee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "JobId",
                table: "Employeest",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Employeest");
        }
    }
}
