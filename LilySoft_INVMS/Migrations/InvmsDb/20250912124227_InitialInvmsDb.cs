using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LilySoft_INVMS.Migrations.InvmsDb
{
    /// <inheritdoc />
    public partial class InitialInvmsDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    catagory_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    catagory_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    catagory_description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.id);
                    table.UniqueConstraint("AK_Categories_catagory_id", x => x.catagory_id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    customer_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.id);
                    table.UniqueConstraint("AK_Customers_customer_id", x => x.customer_id);
                });

            migrationBuilder.CreateTable(
                name: "PurchaseRequests",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchase_request_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RequestDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseRequests", x => x.id);
                    table.UniqueConstraint("AK_PurchaseRequests_purchase_request_id", x => x.purchase_request_id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    supplier_name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.id);
                    table.UniqueConstraint("AK_Suppliers_supplier_id", x => x.supplier_id);
                });

            migrationBuilder.CreateTable(
                name: "Warehouses",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    warehouse_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    warehouse_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    warehouse_location = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Warehouses", x => x.id);
                    table.UniqueConstraint("AK_Warehouses_warehouse_id", x => x.warehouse_id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    product_code = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_image = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: true),
                    catagory_id = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.id);
                    table.UniqueConstraint("AK_Products_product_id", x => x.product_id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_catagory_id",
                        column: x => x.catagory_id,
                        principalTable: "Categories",
                        principalColumn: "catagory_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerContacts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerContacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_CustomerContacts_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    customer_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    orderDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.id);
                    table.UniqueConstraint("AK_Orders_order_id", x => x.order_id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_customer_id",
                        column: x => x.customer_id,
                        principalTable: "Customers",
                        principalColumn: "customer_id");
                });

            migrationBuilder.CreateTable(
                name: "PurchaseOrders",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    purchase_order_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    purchaseDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Supplierid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PurchaseOrders", x => x.id);
                    table.UniqueConstraint("AK_PurchaseOrders_purchase_order_id", x => x.purchase_order_id);
                    table.ForeignKey(
                        name: "FK_PurchaseOrders_Suppliers_Supplierid",
                        column: x => x.Supplierid,
                        principalTable: "Suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierContacts",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_contact_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    supplier_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    contact_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_phone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    contact_address = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierContacts", x => x.id);
                    table.ForeignKey(
                        name: "FK_SupplierContacts_Suppliers_supplier_id",
                        column: x => x.supplier_id,
                        principalTable: "Suppliers",
                        principalColumn: "supplier_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PRDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    pr_detail_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purchase_request_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PRDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_PRDetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_PRDetails_PurchaseRequests_purchase_request_id",
                        column: x => x.purchase_request_id,
                        principalTable: "PurchaseRequests",
                        principalColumn: "purchase_request_id");
                });

            migrationBuilder.CreateTable(
                name: "Stocks",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    stock_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    warehouse_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    last_updated = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stocks", x => x.id);
                    table.ForeignKey(
                        name: "FK_Stocks_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_Stocks_Warehouses_warehouse_id",
                        column: x => x.warehouse_id,
                        principalTable: "Warehouses",
                        principalColumn: "warehouse_id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerReturns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_return_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    order_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Customerid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReturns", x => x.id);
                    table.UniqueConstraint("AK_CustomerReturns_Customer_return_id", x => x.Customer_return_id);
                    table.ForeignKey(
                        name: "FK_CustomerReturns_Customers_Customerid",
                        column: x => x.Customerid,
                        principalTable: "Customers",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_CustomerReturns_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    order_detail_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    order_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_order_id",
                        column: x => x.order_id,
                        principalTable: "Orders",
                        principalColumn: "order_id");
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "PODetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    po_detail_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    purchase_order_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PODetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_PODetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_PODetails_PurchaseOrders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchase_order_id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierReturns",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    supplier_return_id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    purchase_order_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Purchase_ReturnDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Supplierid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierReturns", x => x.id);
                    table.UniqueConstraint("AK_SupplierReturns_supplier_return_id", x => x.supplier_return_id);
                    table.ForeignKey(
                        name: "FK_SupplierReturns_PurchaseOrders_purchase_order_id",
                        column: x => x.purchase_order_id,
                        principalTable: "PurchaseOrders",
                        principalColumn: "purchase_order_id");
                    table.ForeignKey(
                        name: "FK_SupplierReturns_Suppliers_Supplierid",
                        column: x => x.Supplierid,
                        principalTable: "Suppliers",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "CustomerReturnDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    customer_return_detail_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    customer_return_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerReturnDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_CustomerReturnDetails_CustomerReturns_customer_return_id",
                        column: x => x.customer_return_id,
                        principalTable: "CustomerReturns",
                        principalColumn: "Customer_return_id");
                    table.ForeignKey(
                        name: "FK_CustomerReturnDetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                });

            migrationBuilder.CreateTable(
                name: "SupplierReturnDetails",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Supplier_return_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    product_id = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    quantity = table.Column<int>(type: "int", nullable: false),
                    unit_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false),
                    total_price = table.Column<decimal>(type: "decimal(18,2)", precision: 18, scale: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierReturnDetails", x => x.id);
                    table.ForeignKey(
                        name: "FK_SupplierReturnDetails_Products_product_id",
                        column: x => x.product_id,
                        principalTable: "Products",
                        principalColumn: "product_id");
                    table.ForeignKey(
                        name: "FK_SupplierReturnDetails_SupplierReturns_Supplier_return_id",
                        column: x => x.Supplier_return_id,
                        principalTable: "SupplierReturns",
                        principalColumn: "supplier_return_id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomerContacts_customer_id",
                table: "CustomerContacts",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReturnDetails_customer_return_id",
                table: "CustomerReturnDetails",
                column: "customer_return_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReturnDetails_product_id",
                table: "CustomerReturnDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReturns_Customerid",
                table: "CustomerReturns",
                column: "Customerid");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerReturns_order_id",
                table: "CustomerReturns",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_order_id",
                table: "OrderDetails",
                column: "order_id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_product_id",
                table: "OrderDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_customer_id",
                table: "Orders",
                column: "customer_id");

            migrationBuilder.CreateIndex(
                name: "IX_PODetails_product_id",
                table: "PODetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_PODetails_purchase_order_id",
                table: "PODetails",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_PRDetails_product_id",
                table: "PRDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_PRDetails_purchase_request_id",
                table: "PRDetails",
                column: "purchase_request_id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_catagory_id",
                table: "Products",
                column: "catagory_id");

            migrationBuilder.CreateIndex(
                name: "IX_PurchaseOrders_Supplierid",
                table: "PurchaseOrders",
                column: "Supplierid");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_product_id",
                table: "Stocks",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_warehouse_id",
                table: "Stocks",
                column: "warehouse_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierContacts_supplier_id",
                table: "SupplierContacts",
                column: "supplier_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReturnDetails_product_id",
                table: "SupplierReturnDetails",
                column: "product_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReturnDetails_Supplier_return_id",
                table: "SupplierReturnDetails",
                column: "Supplier_return_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReturns_purchase_order_id",
                table: "SupplierReturns",
                column: "purchase_order_id");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierReturns_Supplierid",
                table: "SupplierReturns",
                column: "Supplierid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomerContacts");

            migrationBuilder.DropTable(
                name: "CustomerReturnDetails");

            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "PODetails");

            migrationBuilder.DropTable(
                name: "PRDetails");

            migrationBuilder.DropTable(
                name: "Stocks");

            migrationBuilder.DropTable(
                name: "SupplierContacts");

            migrationBuilder.DropTable(
                name: "SupplierReturnDetails");

            migrationBuilder.DropTable(
                name: "CustomerReturns");

            migrationBuilder.DropTable(
                name: "PurchaseRequests");

            migrationBuilder.DropTable(
                name: "Warehouses");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "SupplierReturns");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "PurchaseOrders");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
