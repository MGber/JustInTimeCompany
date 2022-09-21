using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustInTimeCompany.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Airports",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Latitude = table.Column<double>(type: "float", nullable: false),
                    Longitude = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Airports", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SecurityStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "bit", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "bit", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "bit", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Helicopters",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeatCount = table.Column<int>(type: "int", nullable: false),
                    Speed = table.Column<int>(type: "int", nullable: false),
                    Engine = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FlightCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Helicopters", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ClaimType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ClaimValue = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderKey = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    RoleId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    LoginProvider = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false),
                    Value = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserMessages",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    Message = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserMessages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserMessages_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FromId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ToId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    HelicopterId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PilotId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ScheduledDeparture = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RealDeparture = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ScheduledArrival = table.Column<DateTime>(type: "datetime2", nullable: true),
                    RealArrival = table.Column<DateTime>(type: "datetime2", nullable: true),
                    WasLate = table.Column<bool>(type: "bit", nullable: false),
                    DelayReason = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_FromId",
                        column: x => x.FromId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Airports_ToId",
                        column: x => x.ToId,
                        principalTable: "Airports",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_AspNetUsers_PilotId",
                        column: x => x.PilotId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Flights_Helicopters_HelicopterId",
                        column: x => x.HelicopterId,
                        principalTable: "Helicopters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FlightLog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    OldJson = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NewJson = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FlightLog", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FlightLog_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    SeatAmount = table.Column<int>(type: "int", nullable: false),
                    FlightId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Reservations_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id");
                });

            migrationBuilder.InsertData(
                table: "Airports",
                columns: new[] { "Id", "Latitude", "Longitude", "Name" },
                values: new object[,]
                {
                    { new Guid("7172b844-ebcb-45c3-94bc-34c14dbd7b4f"), 50.898339999999997, 4.4823700000000004, "Bruxelles" },
                    { new Guid("76391ff6-412f-4c1e-b57f-4ccf86648cff"), 50.455998176000001, 4.4516648600000002, "Charleroi" },
                    { new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00"), 50.63583079, 5.4393315759999998, "Liège" },
                    { new Guid("c2af481a-b71d-473c-8082-e1170b1551de"), 51.200400000000002, 2.8741699999999999, "Oostende" }
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "4635ec8f-2c34-444e-9bb8-fb646e7bd1da", null, "Pilot", "PILOT" },
                    { "4e50f06a-e14c-4657-ae21-5ba760cd3a51", null, "Administrator", "ADMINISTRATOR" },
                    { "570245bc-2723-4392-9213-68202d71dad4", null, "User", "USER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "FirstName", "Gender", "LastName", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "319a3971-483c-4ed9-a0cc-2bec19a07b06", 0, "ccd6a6ca-ed23-485b-a40d-5e32e0020211", "M.Ney@jitc.com", true, "Mo", 1, "Ney", false, null, null, "MONEY", "AQAAAAEAACcQAAAAEDh9Q61uFWiPuIFN/Ii5iNLuXp+IC8E/vkcQCYcPNsGb82C1kIL41cBBhZYzb4WveQ==", null, false, "c5070b7e-970f-443e-a1fd-9dead3642823", false, "money" },
                    { "3ac33a35-d3d6-4a04-a79b-aded19a205db", 0, "6808c6c2-881e-44bd-806f-0ed6c4bf19c2", "T.Sabine@jitc.com", true, "Thierry", 0, "Sabine", false, null, null, "THISAB", "AQAAAAEAACcQAAAAEJj9o5t9kTav7VC8tFTDYov43JTH0mAFGTbzGYEi4mA2+YUnEom0NBe9Jv5nfEEK6w==", null, false, "dae3e5ad-5650-442e-a6ae-2d360ed07267", false, "thisab" },
                    { "4b40d3d6-bd20-43f2-a5b8-082009c13682", 0, "c5ddbe69-9e0e-43ba-89d5-cade6888c492", "c.gaber@jitc.com", true, "Christian", 0, "Gaber", false, null, null, "CHRGAB", "AQAAAAEAACcQAAAAEP2PZpq7VGgsq1xM9KVUtesjwzBrRdN0hwhnak7Jv788fKeduYXY7k2pzY4u3lZB5A==", null, false, "661cb6f2-434f-496f-8916-db09ac22d651", false, "chrgab" },
                    { "51d08f0c-0c50-4e47-8e8b-55f7125019bc", 0, "6d804c13-17d5-4375-a6f9-c7e6d21040b9", "f.gaber@jitc.com", true, "Florence", 1, "Gaber", false, null, null, "FLOGAB", "AQAAAAEAACcQAAAAENVemRP5y37PloG0ltwC5sWiRVjYJDX9DtUr80xn6OCn7HodvpiR0DmFRRF0OxXjZA==", null, false, "28892ef9-c5d7-4087-977c-5c4c5b7d75fa", false, "flogab" },
                    { "821f98b0-ff1e-458a-b15b-a9ea85750e45", 0, "821d99eb-2a0e-4f59-b020-ce6c47ad515e", "D.Balav@jitc.com", true, "Danièle", 1, "Balav", false, null, null, "DANBAL", "AQAAAAEAACcQAAAAEEhtx6uwEb6Q0we6drGtoEV35gtjtFHxWLZUUOcm5MZmsNnRyioZJ3wgPSMGwdoXcQ==", null, false, "4f5bc9fb-f05f-4284-a76e-c4e41b16ab82", false, "danbal" },
                    { "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa", 0, "2a098ed6-6532-4200-b5ae-7b68007d908a", "m.gaber@jitc.com", true, "Maxime", 2, "Gaber", false, null, null, "MAXGAB", "AQAAAAEAACcQAAAAEAv8OI+L0vcDi1cdvOyvcHEMlQb1mk5roSitQZCiI0nw5Lv6Ex4May5FgQuPBhdyRg==", null, false, "41d68c2c-e6c2-4cab-ba42-51fd9047f37b", false, "maxgab" },
                    { "efb6c53f-48d0-4981-a3c7-ef714fa2b508", 0, "830b388c-79e2-4f4a-85d0-b779c4ed860c", "E.Coptère@jitc.com", true, "Eli", 0, "Coptère", false, null, null, "ELICOP", "AQAAAAEAACcQAAAAEN4N7eJqAxByqlieirXpwNcHK0Shg170vQZf1bzbTwOqF83anvLYx8i97776naAgwg==", null, false, "0e2b0c0c-a0d9-4eaf-a6a7-9047ed75d0d8", false, "elicop" }
                });

            migrationBuilder.InsertData(
                table: "Helicopters",
                columns: new[] { "Id", "Engine", "FlightCount", "Model", "SeatCount", "Speed", "Status" },
                values: new object[,]
                {
                    { new Guid("05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3"), "Une turbine du type Rolls Royce 250-C20B", 5, "Bell 206 JetRanger", 4, 190, "WARNING" },
                    { new Guid("479e5015-b520-4307-bf25-b75c873f8975"), "Un piston du type Lycoming modèle IO-540", 3, "Robinson R44 Raven II", 3, 190, "OK" },
                    { new Guid("f7ad5264-3054-4889-9b9a-a1b598e579d7"), "Deux turbines du modèle de Rolls Royce 250-C20F", 6, "Eurocopter AS 355 F1/F2 Ecureuil III", 6, 220, "DANGER" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "4e50f06a-e14c-4657-ae21-5ba760cd3a51", "319a3971-483c-4ed9-a0cc-2bec19a07b06" },
                    { "4635ec8f-2c34-444e-9bb8-fb646e7bd1da", "3ac33a35-d3d6-4a04-a79b-aded19a205db" },
                    { "570245bc-2723-4392-9213-68202d71dad4", "4b40d3d6-bd20-43f2-a5b8-082009c13682" },
                    { "570245bc-2723-4392-9213-68202d71dad4", "51d08f0c-0c50-4e47-8e8b-55f7125019bc" },
                    { "4635ec8f-2c34-444e-9bb8-fb646e7bd1da", "821f98b0-ff1e-458a-b15b-a9ea85750e45" },
                    { "570245bc-2723-4392-9213-68202d71dad4", "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa" },
                    { "4635ec8f-2c34-444e-9bb8-fb646e7bd1da", "efb6c53f-48d0-4981-a3c7-ef714fa2b508" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "DelayReason", "FromId", "HelicopterId", "PilotId", "RealArrival", "RealDeparture", "ScheduledArrival", "ScheduledDeparture", "ToId", "WasLate" },
                values: new object[,]
                {
                    { new Guid("138d28a4-5cb2-4792-8aaa-2357310cfcc4"), "The pilot again overslept.", new Guid("76391ff6-412f-4c1e-b57f-4ccf86648cff"), new Guid("05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3"), "3ac33a35-d3d6-4a04-a79b-aded19a205db", new DateTime(2022, 8, 2, 20, 29, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 2, 18, 29, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 2, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 2, 18, 23, 0, 0, DateTimeKind.Unspecified), new Guid("7172b844-ebcb-45c3-94bc-34c14dbd7b4f"), true },
                    { new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6"), null, new Guid("c2af481a-b71d-473c-8082-e1170b1551de"), new Guid("f7ad5264-3054-4889-9b9a-a1b598e579d7"), "821f98b0-ff1e-458a-b15b-a9ea85750e45", null, null, new DateTime(2022, 8, 11, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 11, 18, 23, 0, 0, DateTimeKind.Unspecified), new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00"), false },
                    { new Guid("6809c4c1-3714-4037-8d3d-cf06b3b6555c"), "The pilot overslept.", new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00"), new Guid("f7ad5264-3054-4889-9b9a-a1b598e579d7"), "821f98b0-ff1e-458a-b15b-a9ea85750e45", new DateTime(2022, 8, 1, 20, 29, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 18, 29, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 1, 18, 23, 0, 0, DateTimeKind.Unspecified), new Guid("76391ff6-412f-4c1e-b57f-4ccf86648cff"), true },
                    { new Guid("97550023-4dd5-425b-8285-420b3bd52ad1"), null, new Guid("7172b844-ebcb-45c3-94bc-34c14dbd7b4f"), new Guid("479e5015-b520-4307-bf25-b75c873f8975"), "efb6c53f-48d0-4981-a3c7-ef714fa2b508", null, null, new DateTime(2022, 8, 10, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 10, 18, 23, 0, 0, DateTimeKind.Unspecified), new Guid("c2af481a-b71d-473c-8082-e1170b1551de"), false },
                    { new Guid("c7bdc7a7-8a10-46e2-8058-3f9b645662c6"), null, new Guid("ac8aa908-1fa9-4abd-ad51-32353a4c4a00"), new Guid("05e1ae09-0a16-4e76-8b2d-a70fcdd1c7e3"), "3ac33a35-d3d6-4a04-a79b-aded19a205db", null, null, new DateTime(2022, 8, 12, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 12, 18, 23, 0, 0, DateTimeKind.Unspecified), new Guid("c2af481a-b71d-473c-8082-e1170b1551de"), false }
                });

            migrationBuilder.InsertData(
                table: "UserMessages",
                columns: new[] { "Id", "Message", "UserId" },
                values: new object[,]
                {
                    { new Guid("2e27db70-be58-4c64-9c85-17320d79d1ce"), "This is a testMessage3", "4b40d3d6-bd20-43f2-a5b8-082009c13682" },
                    { new Guid("2fba385c-5aac-4d98-8b2e-b3c3c23b6acc"), "This is a testMessage2", "51d08f0c-0c50-4e47-8e8b-55f7125019bc" },
                    { new Guid("46314476-0cbd-4535-b257-1ab51b582ec2"), "This is a testMessage5", "51d08f0c-0c50-4e47-8e8b-55f7125019bc" },
                    { new Guid("59b2ecaa-b258-4e87-9d0c-33d87c17cfd9"), "This is a testMessage", "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa" },
                    { new Guid("d4812a5f-54dc-4ab6-8e0a-7018744f2722"), "This is a testMessage6", "4b40d3d6-bd20-43f2-a5b8-082009c13682" },
                    { new Guid("d64d8d14-2e2f-4044-995e-3c28a66b1d0e"), "This is a testMessage4", "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa" }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "FlightId", "SeatAmount", "UserId" },
                values: new object[,]
                {
                    { new Guid("5b36ccee-3b31-49cb-ab02-3e021779dac4"), new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6"), 1, "51d08f0c-0c50-4e47-8e8b-55f7125019bc" },
                    { new Guid("6ae64930-1ba7-4933-96cf-d89fdd4353bd"), new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6"), 3, "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa" },
                    { new Guid("79a77425-9216-40c3-b6c9-39d1dc097909"), new Guid("6809c4c1-3714-4037-8d3d-cf06b3b6555c"), 5, "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa" },
                    { new Guid("bb207b88-d785-4e2b-820d-0c68f27739f5"), new Guid("138d28a4-5cb2-4792-8aaa-2357310cfcc4"), 3, "51d08f0c-0c50-4e47-8e8b-55f7125019bc" },
                    { new Guid("d58210ef-c2b6-4413-934c-2ae16eab260e"), new Guid("97550023-4dd5-425b-8285-420b3bd52ad1"), 3, "4b40d3d6-bd20-43f2-a5b8-082009c13682" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_FlightLog_FlightId",
                table: "FlightLog",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_FromId",
                table: "Flights",
                column: "FromId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_HelicopterId",
                table: "Flights",
                column: "HelicopterId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_PilotId",
                table: "Flights",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_Flights_ToId",
                table: "Flights",
                column: "ToId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_FlightId",
                table: "Reservations",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_UserId",
                table: "UserMessages",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "FlightLog");

            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "UserMessages");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "Airports");

            migrationBuilder.DropTable(
                name: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "Helicopters");
        }
    }
}
