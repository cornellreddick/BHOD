using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BHOD.Migrations
{
    public partial class IntitialEntityModels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PaymentMethodId",
                table: "Customers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ShopId",
                table: "Customers",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "PaymentMethods",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Charges = table.Column<decimal>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentMethods", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Shops",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(maxLength: 30, nullable: false),
                    Address = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OpenDate = table.Column<DateTime>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shops", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Statuses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Statuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ShopHourses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    LocationId = table.Column<int>(nullable: true),
                    DayOfWeek = table.Column<int>(nullable: false),
                    OpenTime = table.Column<int>(nullable: false),
                    CloseTime = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopHourses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopHourses_Shops_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ShopPersonals",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopName = table.Column<string>(nullable: false),
                    Address = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    State = table.Column<string>(nullable: false),
                    PostalCode = table.Column<string>(nullable: false),
                    PhoneNumber = table.Column<string>(nullable: false),
                    Email = table.Column<string>(nullable: false),
                    Url = table.Column<string>(nullable: false),
                    ImageUrl = table.Column<string>(nullable: true),
                    LocationId = table.Column<int>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Hairstylist_FirstName = table.Column<string>(nullable: true),
                    Hairstylist_LastName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ShopPersonals", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ShopPersonals_Shops_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Shops",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AppointmentHistories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false),
                    CheckedOut = table.Column<DateTime>(nullable: false),
                    CheckedIn = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentHistories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppointmentHistories_PaymentMethods_PaymentId",
                        column: x => x.PaymentId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppointmentHistories_ShopPersonals_ShopId",
                        column: x => x.ShopId,
                        principalTable: "ShopPersonals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Appointmentses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopPersonalId = table.Column<int>(nullable: false),
                    PaymentMethodId = table.Column<int>(nullable: true),
                    Since = table.Column<DateTime>(nullable: false),
                    Until = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointmentses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointmentses_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointmentses_ShopPersonals_ShopPersonalId",
                        column: x => x.ShopPersonalId,
                        principalTable: "ShopPersonals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PreBookedAppointmentses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    ShopPersonalId = table.Column<int>(nullable: true),
                    PaymentMethodId = table.Column<int>(nullable: true),
                    PreBookedPlaced = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PreBookedAppointmentses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PreBookedAppointmentses_PaymentMethods_PaymentMethodId",
                        column: x => x.PaymentMethodId,
                        principalTable: "PaymentMethods",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PreBookedAppointmentses_ShopPersonals_ShopPersonalId",
                        column: x => x.ShopPersonalId,
                        principalTable: "ShopPersonals",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Customers_PaymentMethodId",
                table: "Customers",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ShopId",
                table: "Customers",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentHistories_PaymentId",
                table: "AppointmentHistories",
                column: "PaymentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentHistories_ShopId",
                table: "AppointmentHistories",
                column: "ShopId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointmentses_PaymentMethodId",
                table: "Appointmentses",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointmentses_ShopPersonalId",
                table: "Appointmentses",
                column: "ShopPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_PreBookedAppointmentses_PaymentMethodId",
                table: "PreBookedAppointmentses",
                column: "PaymentMethodId");

            migrationBuilder.CreateIndex(
                name: "IX_PreBookedAppointmentses_ShopPersonalId",
                table: "PreBookedAppointmentses",
                column: "ShopPersonalId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopHourses_LocationId",
                table: "ShopHourses",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ShopPersonals_LocationId",
                table: "ShopPersonals",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_PaymentMethods_PaymentMethodId",
                table: "Customers",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Shops_ShopId",
                table: "Customers",
                column: "ShopId",
                principalTable: "Shops",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_PaymentMethods_PaymentMethodId",
                table: "Customers");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Shops_ShopId",
                table: "Customers");

            migrationBuilder.DropTable(
                name: "AppointmentHistories");

            migrationBuilder.DropTable(
                name: "Appointmentses");

            migrationBuilder.DropTable(
                name: "PreBookedAppointmentses");

            migrationBuilder.DropTable(
                name: "ShopHourses");

            migrationBuilder.DropTable(
                name: "Statuses");

            migrationBuilder.DropTable(
                name: "PaymentMethods");

            migrationBuilder.DropTable(
                name: "ShopPersonals");

            migrationBuilder.DropTable(
                name: "Shops");

            migrationBuilder.DropIndex(
                name: "IX_Customers_PaymentMethodId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ShopId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "PaymentMethodId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "ShopId",
                table: "Customers");
        }
    }
}
