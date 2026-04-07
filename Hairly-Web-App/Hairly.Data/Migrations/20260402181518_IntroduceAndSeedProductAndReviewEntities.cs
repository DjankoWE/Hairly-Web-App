using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hairly.Data.Migrations
{
    /// <inheritdoc />
    public partial class IntroduceAndSeedProductAndReviewEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    QuantityInStock = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    AppointmentId = table.Column<int>(type: "int", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reviews_Appointments_AppointmentId",
                        column: x => x.AppointmentId,
                        principalTable: "Appointments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reviews_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9545bd5-d9a5-4e33-a9e3-14a174654fe1", "AQAAAAIAAYagAAAAECIHtEoApQQDtY18/Iv1FNc58JIm+Pqy5z72V90CQtu3TUc7m3AcR7wI3Qhhb88jRQ==", "562bc431-6712-4380-946e-476d07e49db4" });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CreatedOn", "Description", "ImageUrl", "IsDeleted", "Name", "Price", "QuantityInStock" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "Професионален шампоан за увредена коса с възстановяващо действие и незабавен ефект на заглаждане.", "/images/products/loreal-absolut-repair-shampoo.jpg", false, "L'Oreal Serie Expert Absolut Repair Shampoo", 29.90m, 50 },
                    { 2, new DateTime(2025, 11, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "Подсилващ шампоан за слаба и увредена коса, който възстановява структурата и здравината.", "/images/products/kerastase-shampoo.jpg", false, "Kerastase Resistance Bain Force Architecte Shampoo", 39.90m, 40 },
                    { 3, new DateTime(2025, 11, 21, 0, 0, 0, 0, DateTimeKind.Unspecified), "Подхранващ шампоан за суха и изтощена коса с дълбоко хидратиращ ефект.", "/images/products/wella-invigo-shampoo.jpg", false, "Wella Invigo Nutri-Enrich Shampoo", 24.50m, 60 },
                    { 4, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Възстановяващ шампоан с веган кератин за силно увредена коса.", "/images/products/schwarzkopf-bcbonacure-shampoo.jpg", false, "Schwarzkopf BC Bonacure Repair Rescue Shampoo", 22.90m, 55 },
                    { 5, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Дълбоко възстановяваща маска за силно увредена коса, която възвръща еластичността и блясъка.", "/images/products/kerastase-masque.jpg", false, "Kerastase Masque Therapiste", 54.90m, 30 },
                    { 6, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Професионална маска за интензивно възстановяване и подхранване на косата.", "/images/products/loreal-absolut-repair-mask.jpg", false, "L'Oreal Absolut Repair Golden Mask", 34.90m, 45 },
                    { 7, new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Интензивна маска за възстановяване на косата и защита от накъсване.", "/images/products/wella-fusion-mask.jpg", false, "Wella Fusion Intense Repair Mask", 29.90m, 35 },
                    { 8, new DateTime(2025, 12, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Силен фиксиращ продукт с матов ефект за оформяне на модерни прически.", "/images/products/american-crew.jpg", false, "American Crew Fiber", 21.90m, 50 },
                    { 9, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "Гел със силна фиксация за структурирани и дълготрайни прически.", "/images/products/loreal-tecni-art.jpg", false, "L'Oreal Tecni Art Fix Max Gel", 19.90m, 40 },
                    { 10, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Матираща пудра за придаване на обем и текстура на косата.", "/images/products/schwarzkopf-osis-dust-it.jpg", false, "Schwarzkopf Osis+ Dust It", 18.50m, 60 },
                    { 11, new DateTime(2026, 2, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Лак за коса със силна фиксация за дълготраен контрол и завършен стил.", "/images/products/wella-eimi-spray.jpg", false, "Wella EIMI Super Set Spray", 17.90m, 70 }
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "Id", "AppointmentId", "ClientId", "Comment", "CreatedOn", "IsDeleted", "Rating" },
                values: new object[,]
                {
                    { 1, 1, 1, "Много съм доволен! Бързо и качествено обслужване.", new DateTime(2025, 12, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 2, 2, 2, "Цветът стана страхотен, но отне малко повече време.", new DateTime(2025, 12, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4 },
                    { 3, 3, 3, "Перфектно подстригване, точно както го исках.", new DateTime(2026, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 4, 4, 4, "Най-добрият балеаж, който съм имала!", new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 5 },
                    { 5, 5, 1, "Отново съм доволен, ще посетя пак.", new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_AppointmentId",
                table: "Reviews",
                column: "AppointmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_ClientId",
                table: "Reviews",
                column: "ClientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47e65203-324d-449f-9c0d-dc5055c29976", "AQAAAAIAAYagAAAAEECjjkK2DK/dFsWvR1EkLbIiea0NDdzWnGjKf1+c0vo7wU3s+MWsioukg6JgxS3oqw==", "3e58e1b5-8a14-4001-97c8-fccfd13b671d" });
        }
    }
}
