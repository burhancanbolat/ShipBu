using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ShipBu.Migrations
{
    /// <inheritdoc />
    public partial class PaymentCalculation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "WareHouses",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "States",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "IsCheckedWareHouse",
                table: "ProductFeatures",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "CalculationVariableId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "LocationInfoId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "ProductFeatureId",
                table: "Payments",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Packageprocesses",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Countries",
                type: "decimal(18,2)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CalculationVariables",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Variable = table.Column<float>(type: "real", nullable: false),
                    PrivateAdressPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationVariables", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SendingGenres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    TotalPrice = table.Column<decimal>(type: "decimal(18,2)", nullable: true),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PaymentId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendingGenres", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendingGenres_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SendingGenres_Payments_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "Payments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Payments_CalculationVariableId",
                table: "Payments",
                column: "CalculationVariableId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_LocationInfoId",
                table: "Payments",
                column: "LocationInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_Payments_ProductFeatureId",
                table: "Payments",
                column: "ProductFeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_SendingGenres_PaymentId",
                table: "SendingGenres",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_SendingGenres_UserId",
                table: "SendingGenres",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_CalculationVariables_CalculationVariableId",
                table: "Payments",
                column: "CalculationVariableId",
                principalTable: "CalculationVariables",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_Locationinfos_LocationInfoId",
                table: "Payments",
                column: "LocationInfoId",
                principalTable: "Locationinfos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Payments_ProductFeatures_ProductFeatureId",
                table: "Payments",
                column: "ProductFeatureId",
                principalTable: "ProductFeatures",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Payments_CalculationVariables_CalculationVariableId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_Locationinfos_LocationInfoId",
                table: "Payments");

            migrationBuilder.DropForeignKey(
                name: "FK_Payments_ProductFeatures_ProductFeatureId",
                table: "Payments");

            migrationBuilder.DropTable(
                name: "CalculationVariables");

            migrationBuilder.DropTable(
                name: "SendingGenres");

            migrationBuilder.DropIndex(
                name: "IX_Payments_CalculationVariableId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_LocationInfoId",
                table: "Payments");

            migrationBuilder.DropIndex(
                name: "IX_Payments_ProductFeatureId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "WareHouses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "States");

            migrationBuilder.DropColumn(
                name: "IsCheckedWareHouse",
                table: "ProductFeatures");

            migrationBuilder.DropColumn(
                name: "CalculationVariableId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "LocationInfoId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "ProductFeatureId",
                table: "Payments");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Packageprocesses");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Countries");
        }
    }
}
