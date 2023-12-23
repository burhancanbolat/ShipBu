using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipBu.Migrations
{
    /// <inheritdoc />
    public partial class EditPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SendingGenres_Payments_PaymentId",
                table: "SendingGenres");

            migrationBuilder.DropIndex(
                name: "IX_SendingGenres_PaymentId",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WareHouses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "States");

            migrationBuilder.DropColumn(
                name: "PaymentId",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Countries");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "SendingGenres",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "DeliveryTıme",
                table: "SendingGenres",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "SendingGenreId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Payments_SendingGenreId",
                table: "Payments",
                column: "SendingGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SendingGenres_SendingGenreId",
                table: "Payments",
                column: "SendingGenreId",
                principalTable: "SendingGenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SendingGenres_SendingGenreId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_SendingGenreId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "DeliveryTıme",
                table: "SendingGenres");

            migrationBuilder.DropColumn(
                name: "SendingGenreId",
                table: "Payments");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "WareHouses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "States",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "States",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<Guid>(
                name: "PaymentId",
                table: "SendingGenres",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "SendingGenres",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "SendingGenres",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SendingGenres_PaymentId",
                table: "SendingGenres",
                column: "PaymentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SendingGenres_Payments_PaymentId",
                table: "SendingGenres",
                column: "PaymentId",
                principalTable: "Payments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
