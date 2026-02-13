using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Hairly.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialDbDesignAndDataSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clients",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Note = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HairdresserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clients", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Clients_AspNetUsers_HairdresserId",
                        column: x => x.HairdresserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Services",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    Price = table.Column<decimal>(type: "DECIMAL(18,2)", nullable: false),
                    DurationInMinutes = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    HairdresserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Services", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Services_AspNetUsers_HairdresserId",
                        column: x => x.HairdresserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Appointments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AppointmentDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ClientId = table.Column<int>(type: "int", nullable: false),
                    ServiceId = table.Column<int>(type: "int", nullable: false),
                    HairdresserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Note = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointments_AspNetUsers_HairdresserId",
                        column: x => x.HairdresserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Clients_ClientId",
                        column: x => x.ClientId,
                        principalTable: "Clients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointments_Services_ServiceId",
                        column: x => x.ServiceId,
                        principalTable: "Services",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", 0, "47e65203-324d-449f-9c0d-dc5055c29976", "stylist@hairly.com", true, false, null, "STYLIST@HAIRLY.COM", "STYLIST@HAIRLY.COM", "AQAAAAIAAYagAAAAEECjjkK2DK/dFsWvR1EkLbIiea0NDdzWnGjKf1+c0vo7wU3s+MWsioukg6JgxS3oqw==", null, false, "3e58e1b5-8a14-4001-97c8-fccfd13b671d", false, "stylist@hairly.com" });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "CreatedOn", "Email", "FirstName", "HairdresserId", "IsDeleted", "LastName", "Note", "PhoneNumber" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 10, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "ivan.petrov@example.com", "Иван", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Петров", "Предпочита късо подстригване, алергия към силни парфюми", "+359888123456" },
                    { 2, new DateTime(2024, 11, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "maria.georgieva@example.com", "Мария", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Георгиева", "Алергия към амоняк - използвай безамонячна боя", "0877654321" },
                    { 3, new DateTime(2024, 12, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "georgi.ivanov@gmail.com", "Георги", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Иванов", null, "0898765432" },
                    { 4, new DateTime(2025, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "elena.dimitrova@abv.bg", "Елена", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Димитрова", "Предпочита топли тонове при боядисване", "+359887111222" },
                    { 5, new DateTime(2025, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Петър", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Стоянов", null, "0878333444" },
                    { 6, new DateTime(2025, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "anna.todorova@mail.bg", "Анна", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Тодорова", "Много чувствителен скалп - внимавай с температурата", "+359899555666" },
                    { 7, new DateTime(2025, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Димитър", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Василев", null, "0877777888" },
                    { 8, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "sofia.nikolova@yahoo.com", "София", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Николова", "Винаги иска съвет за нови прически", "+359888999000" },
                    { 9, new DateTime(2026, 2, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), null, "Стефан", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Йорданов", null, "0898111222" },
                    { 10, new DateTime(2026, 2, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "viki.hristova@gmail.com", "Виктория", "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Христова", "Дълга коса, иска да запази дължината", "+359877333555" }
                });

            migrationBuilder.InsertData(
                table: "Services",
                columns: new[] { "Id", "Description", "DurationInMinutes", "HairdresserId", "IsDeleted", "Name", "Price" },
                values: new object[,]
                {
                    { 1, "Класическо мъжко подстригване с машинка и ножица", 30, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Подстригване - мъже", 25.00m },
                    { 2, "Подстригване и оформяне на къса коса", 40, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Подстригване - жени (къса коса)", 30.00m },
                    { 3, "Подстригване и оформяне на дълга коса", 60, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Подстригване - жени (дълга коса)", 40.00m },
                    { 4, "Пълно боядисване с професионална боя", 120, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Боядисване - цяла глава", 80.00m },
                    { 5, "Частично освежаване с кичури - фолио техника", 90, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Кичури (фолио)", 60.00m },
                    { 6, "Балеаж техника за естествен ефект", 150, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Балеаж", 100.00m },
                    { 7, "Измиване, подсушаване и оформяне със сешоар", 30, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Сешоар", 20.00m },
                    { 8, "Изправяне с преса за гладка коса", 45, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Преса", 25.00m },
                    { 9, "Къдрене с маша или ролки", 50, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Къдрене", 30.00m },
                    { 10, "Подстригване за деца до 12 години", 20, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Детско подстригване", 15.00m },
                    { 11, "Подстригване и оформяне на брада и мустаци", 20, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Оформяне на брада", 15.00m },
                    { 12, "Кератиново изправяне за дълготрайна гладкост", 180, "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Кератинова терапия", 150.00m }
                });

            migrationBuilder.InsertData(
                table: "Appointments",
                columns: new[] { "Id", "AppointmentDate", "ClientId", "CreatedOn", "HairdresserId", "IsDeleted", "Note", "ServiceId", "Status" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 12, 10, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2025, 12, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Първо посещение", 1, 1 },
                    { 2, new DateTime(2025, 12, 15, 14, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2025, 12, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 4, 1 },
                    { 3, new DateTime(2026, 1, 5, 11, 30, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2025, 12, 28, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 1, 1 },
                    { 4, new DateTime(2026, 1, 20, 15, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Клиентката е много доволна от резултата", 5, 1 },
                    { 5, new DateTime(2026, 2, 1, 10, 0, 0, 0, DateTimeKind.Unspecified), 1, new DateTime(2026, 1, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 1, 1 },
                    { 6, new DateTime(2026, 1, 25, 12, 0, 0, 0, DateTimeKind.Unspecified), 5, new DateTime(2026, 1, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Клиентът отмени заради болест", 11, 2 },
                    { 7, new DateTime(2026, 2, 8, 9, 0, 0, 0, DateTimeKind.Unspecified), 7, new DateTime(2026, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Не се е появил и не е обадил", 1, 3 },
                    { 8, new DateTime(2026, 2, 18, 10, 0, 0, 0, DateTimeKind.Unspecified), 2, new DateTime(2026, 2, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 7, 0 },
                    { 9, new DateTime(2026, 2, 20, 14, 30, 0, 0, DateTimeKind.Unspecified), 6, new DateTime(2026, 2, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Иска да свали 5см и да оформи бретон", 3, 0 },
                    { 10, new DateTime(2026, 2, 22, 11, 0, 0, 0, DateTimeKind.Unspecified), 8, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Първи път балеаж - покажи примери", 6, 0 },
                    { 11, new DateTime(2026, 2, 25, 16, 0, 0, 0, DateTimeKind.Unspecified), 4, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 8, 0 },
                    { 12, new DateTime(2026, 2, 28, 10, 30, 0, 0, DateTimeKind.Unspecified), 10, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, "Само почистване на краищата", 3, 0 },
                    { 13, new DateTime(2026, 3, 5, 9, 30, 0, 0, DateTimeKind.Unspecified), 3, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 11, 0 },
                    { 14, new DateTime(2026, 3, 8, 12, 0, 0, 0, DateTimeKind.Unspecified), 9, new DateTime(2026, 2, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d", false, null, 1, 0 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ClientId",
                table: "Appointments",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_HairdresserId",
                table: "Appointments",
                column: "HairdresserId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointments_ServiceId",
                table: "Appointments",
                column: "ServiceId");

            migrationBuilder.CreateIndex(
                name: "IX_Clients_HairdresserId",
                table: "Clients",
                column: "HairdresserId");

            migrationBuilder.CreateIndex(
                name: "IX_Services_HairdresserId",
                table: "Services",
                column: "HairdresserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointments");

            migrationBuilder.DropTable(
                name: "Clients");

            migrationBuilder.DropTable(
                name: "Services");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3dbb52f6-6024-4dd6-ad4b-e1c782bbd23d");
        }
    }
}
