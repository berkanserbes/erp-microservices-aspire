using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ERP.ProductService.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "products",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    lot_tracking = table.Column<short>(type: "smallint", nullable: false),
                    location_tracking = table.Column<short>(type: "smallint", nullable: false),
                    variant_tracking = table.Column<short>(type: "smallint", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_products", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "warehouses",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    number = table.Column<int>(type: "int", nullable: false),
                    name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_warehouses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "variants",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    code = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    name = table.Column<string>(type: "varchar(40)", maxLength: 40, nullable: false),
                    product_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp", nullable: false, defaultValueSql: "NOW()"),
                    updated_at = table.Column<DateTime>(type: "timestamp", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_variants", x => x.id);
                    table.ForeignKey(
                        name: "FK_variants_products_product_id",
                        column: x => x.product_id,
                        principalTable: "products",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Location",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WarehouseId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Location", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Location_warehouses_WarehouseId",
                        column: x => x.WarehouseId,
                        principalTable: "warehouses",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Location_WarehouseId",
                table: "Location",
                column: "WarehouseId");

            migrationBuilder.CreateIndex(
                name: "IX_variants_product_id",
                table: "variants",
                column: "product_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Location");

            migrationBuilder.DropTable(
                name: "variants");

            migrationBuilder.DropTable(
                name: "warehouses");

            migrationBuilder.DropTable(
                name: "products");
        }
    }
}
