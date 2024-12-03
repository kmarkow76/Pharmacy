using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.DAL.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CategoryDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    PathImage = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RecordDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UserDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Login = table.Column<string>(type: "text", nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false),
                    Email = table.Column<string>(type: "text", nullable: false),
                    PartImage = table.Column<string>(type: "text", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp", nullable: false),
                    Role = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDb", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MedicineDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Description = table.Column<string>(type: "text", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    PrescriptionRequired = table.Column<bool>(type: "boolean", nullable: false),
                    Image = table.Column<string>(type: "text", nullable: false),
                    CreatedAt = table.Column<TimeSpan>(type: "interval", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uuid", nullable: false),
                    CategoryDbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MedicineDb_CategoryDb_CategoryDbId",
                        column: x => x.CategoryDbId,
                        principalTable: "CategoryDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "numeric", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<TimeSpan>(type: "interval", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderDb_UserDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartDb",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    MedicineId = table.Column<Guid>(type: "uuid", nullable: false),
                    Quantity = table.Column<int>(type: "integer", nullable: false),
                    Price = table.Column<decimal>(type: "numeric", nullable: false),
                    AddedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    MedicineDbId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CartDb_MedicineDb_MedicineDbId",
                        column: x => x.MedicineDbId,
                        principalTable: "MedicineDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartDb_UserDb_UserId",
                        column: x => x.UserId,
                        principalTable: "UserDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });
           
            migrationBuilder.CreateTable(
                name: "MedicineDbOrderDb",
                columns: table => new
                {
                    MedicinesId = table.Column<Guid>(type: "uuid", nullable: false),
                    OrdersId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicineDbOrderDb", x => new { x.MedicinesId, x.OrdersId });
                    table.ForeignKey(
                        name: "FK_MedicineDbOrderDb_MedicineDb_MedicinesId",
                        column: x => x.MedicinesId,
                        principalTable: "MedicineDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MedicineDbOrderDb_OrderDb_OrdersId",
                        column: x => x.OrdersId,
                        principalTable: "OrderDb",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CartDb_MedicineDbId",
                table: "CartDb",
                column: "MedicineDbId");

            migrationBuilder.CreateIndex(
                name: "IX_CartDb_UserId",
                table: "CartDb",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineDb_CategoryDbId",
                table: "MedicineDb",
                column: "CategoryDbId");

            migrationBuilder.CreateIndex(
                name: "IX_MedicineDbOrderDb_OrdersId",
                table: "MedicineDbOrderDb",
                column: "OrdersId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDb_UserId",
                table: "OrderDb",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartDb");

            migrationBuilder.DropTable(
                name: "MedicineDbOrderDb");

            migrationBuilder.DropTable(
                name: "RecordDb");

            migrationBuilder.DropTable(
                name: "MedicineDb");

            migrationBuilder.DropTable(
                name: "OrderDb");

            migrationBuilder.DropTable(
                name: "CategoryDb");

            migrationBuilder.DropTable(
                name: "UserDb");
        }
    }
}
