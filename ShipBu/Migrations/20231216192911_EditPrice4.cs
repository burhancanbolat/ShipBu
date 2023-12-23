using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipBu.Migrations
{
    /// <inheritdoc />
    public partial class EditPrice4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_SendingGenres_SendingGenreId",
                table: "Payments");

            migrationBuilder.RenameColumn(
                name: "SendingGenreId",
                table: "Payments",
                newName: "PriceEditId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_SendingGenreId",
                table: "Payments",
                newName: "IX_Payments_PriceEditId");

            migrationBuilder.CreateTable(
                name: "PriceEdits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    CountryId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    StateId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    WareHouseId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    SendingGenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    PrimaryKG = table.Column<int>(type: "int", nullable: false),
                    SecondaryKg = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PriceEdits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PriceEdits_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceEdits_Countries_CountryId",
                        column: x => x.CountryId,
                        principalTable: "Countries",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceEdits_SendingGenres_SendingGenreId",
                        column: x => x.SendingGenreId,
                        principalTable: "SendingGenres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceEdits_States_StateId",
                        column: x => x.StateId,
                        principalTable: "States",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PriceEdits_WareHouses_WareHouseId",
                        column: x => x.WareHouseId,
                        principalTable: "WareHouses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PriceEdits_CountryId",
                table: "PriceEdits",
                column: "CountryId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEdits_SendingGenreId",
                table: "PriceEdits",
                column: "SendingGenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEdits_StateId",
                table: "PriceEdits",
                column: "StateId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEdits_UserId",
                table: "PriceEdits",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PriceEdits_WareHouseId",
                table: "PriceEdits",
                column: "WareHouseId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_PriceEdits_PriceEditId",
                table: "Payments",
                column: "PriceEditId",
                principalTable: "PriceEdits",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_PriceEdits_PriceEditId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "PriceEdits");

            migrationBuilder.RenameColumn(
                name: "PriceEditId",
                table: "Payments",
                newName: "SendingGenreId");

            migrationBuilder.RenameIndex(
                name: "IX_Payments_PriceEditId",
                table: "Payments",
                newName: "IX_Payments_SendingGenreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_SendingGenres_SendingGenreId",
                table: "Payments",
                column: "SendingGenreId",
                principalTable: "SendingGenres",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
