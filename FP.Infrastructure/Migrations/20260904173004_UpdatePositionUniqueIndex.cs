using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UpdatePositionUniqueIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Positions_DepartmentId_Name",
                table: "Positions");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId_Name",
                table: "Positions",
                columns: new[] { "DepartmentId", "Name" },
                unique: true,
                filter: "\"IsDeleted\" = false");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Positions_DepartmentId_Name",
                table: "Positions");

            migrationBuilder.CreateIndex(
                name: "IX_Positions_DepartmentId_Name",
                table: "Positions",
                columns: new[] { "DepartmentId", "Name" },
                unique: true);
        }
    }
}
