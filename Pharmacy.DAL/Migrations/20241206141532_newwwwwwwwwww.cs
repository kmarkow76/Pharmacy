using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.DAL.Migrations
{
    /// <inheritdoc />
    public partial class newwwwwwwwwww : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedAt",
                table: "MedicineDb",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(TimeSpan),
                oldType: "interval");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "CreatedAt",
                table: "MedicineDb",
                type: "interval",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone");
        }
    }
}
