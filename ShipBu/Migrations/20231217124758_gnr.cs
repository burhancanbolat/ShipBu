using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipBu.Migrations
{
    /// <inheritdoc />
    public partial class gnr : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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
                name: "SendingGenreId",
                table: "Payments");
        }
    }
}
