using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TovanyUchetV2.Migrations
{
    /// <inheritdoc />
    public partial class InvenOpUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryOperations_Employees_EmployeeId",
                table: "InventoryOperations");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "InventoryOperations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "AuditLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Entity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    User = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuditLogs", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryOperations_Employees_EmployeeId",
                table: "InventoryOperations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InventoryOperations_Employees_EmployeeId",
                table: "InventoryOperations");

            migrationBuilder.DropTable(
                name: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "InventoryOperations");

            migrationBuilder.AddForeignKey(
                name: "FK_InventoryOperations_Employees_EmployeeId",
                table: "InventoryOperations",
                column: "EmployeeId",
                principalTable: "Employees",
                principalColumn: "Id");
        }
    }
}
