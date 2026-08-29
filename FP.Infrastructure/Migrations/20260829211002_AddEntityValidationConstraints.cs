using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FP.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddEntityValidationConstraints : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomRequirement_ExtinguisherTypes_ExtinguisherTypeId",
                table: "RoomRequirement");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomRequirement_Rooms_RoomId",
                table: "RoomRequirement");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomRequirement",
                table: "RoomRequirement");

            migrationBuilder.RenameTable(
                name: "RoomRequirement",
                newName: "RoomRequirements");

            migrationBuilder.RenameIndex(
                name: "IX_RoomRequirement_RoomId_ExtinguisherTypeId",
                table: "RoomRequirements",
                newName: "IX_RoomRequirements_RoomId_ExtinguisherTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomRequirement_ExtinguisherTypeId",
                table: "RoomRequirements",
                newName: "IX_RoomRequirements_ExtinguisherTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Rooms",
                type: "character varying(150)",
                maxLength: 150,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(1000)",
                oldMaxLength: 1000,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtinguisherTypes",
                type: "character varying(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "WorkNumber",
                table: "Employees",
                type: "character varying(4)",
                maxLength: 4,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomRequirements",
                table: "RoomRequirements",
                column: "Id");

            migrationBuilder.AddCheckConstraint(
                name: "CK_RoomRequirements_RequiredCount",
                table: "RoomRequirements",
                sql: "\"RequiredCount\" BETWEEN 1 AND 30");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRequirements_ExtinguisherTypes_ExtinguisherTypeId",
                table: "RoomRequirements",
                column: "ExtinguisherTypeId",
                principalTable: "ExtinguisherTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRequirements_Rooms_RoomId",
                table: "RoomRequirements",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_RoomRequirements_ExtinguisherTypes_ExtinguisherTypeId",
                table: "RoomRequirements");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomRequirements_Rooms_RoomId",
                table: "RoomRequirements");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomRequirements",
                table: "RoomRequirements");

            migrationBuilder.DropCheckConstraint(
                name: "CK_RoomRequirements_RequiredCount",
                table: "RoomRequirements");

            migrationBuilder.RenameTable(
                name: "RoomRequirements",
                newName: "RoomRequirement");

            migrationBuilder.RenameIndex(
                name: "IX_RoomRequirements_RoomId_ExtinguisherTypeId",
                table: "RoomRequirement",
                newName: "IX_RoomRequirement_RoomId_ExtinguisherTypeId");

            migrationBuilder.RenameIndex(
                name: "IX_RoomRequirements_ExtinguisherTypeId",
                table: "RoomRequirement",
                newName: "IX_RoomRequirement_ExtinguisherTypeId");

            migrationBuilder.AlterColumn<string>(
                name: "Notes",
                table: "Rooms",
                type: "character varying(1000)",
                maxLength: 1000,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(150)",
                oldMaxLength: 150,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "ExtinguisherTypes",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(30)",
                oldMaxLength: 30);

            migrationBuilder.AlterColumn<string>(
                name: "WorkNumber",
                table: "Employees",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(4)",
                oldMaxLength: 4);

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomRequirement",
                table: "RoomRequirement",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRequirement_ExtinguisherTypes_ExtinguisherTypeId",
                table: "RoomRequirement",
                column: "ExtinguisherTypeId",
                principalTable: "ExtinguisherTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomRequirement_Rooms_RoomId",
                table: "RoomRequirement",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
