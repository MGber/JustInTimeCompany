using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JustInTimeCompany.Migrations
{
    public partial class modifiedseed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "319a3971-483c-4ed9-a0cc-2bec19a07b06",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c67635db-5fe0-4dc9-a541-83a29c75c08b", "AQAAAAEAACcQAAAAEMUR2gpDgnyOpH2iNDeVX2H/5/MBEagR805PGoLuLHu5Bt0TpDvprQa1IB0rZFpOuA==", "8acc1ab1-5c5f-433c-91fe-72bceaf25962" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ac33a35-d3d6-4a04-a79b-aded19a205db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4a2fe6a2-c4aa-48f6-9189-444bb2163c98", "AQAAAAEAACcQAAAAEH6uK1ofa6oOHE93YtEVagCNQNsTsvP88trmCS0rwKDshxpD/mz7jUHlnDDMnzeLgA==", "d007a34e-d070-4a4d-a96c-e6cff53cc7ec" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b40d3d6-bd20-43f2-a5b8-082009c13682",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "31d42741-a33e-4167-a307-7a7002371e36", "AQAAAAEAACcQAAAAELKJT/nSsskhtMATaRjSOjtKBZVD7MMc913LLVSAS0UN//mtxHY/xz7zuI+7br0srw==", "56a8beac-f8e5-4042-980a-0a0ea555c276" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51d08f0c-0c50-4e47-8e8b-55f7125019bc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17c5742a-f6eb-4868-8ea9-913df64f11cc", "AQAAAAEAACcQAAAAEIr9Ak4TUPmiENghfrySE47V7D3e0gR8NzrY+/nxMsueE946WXmBesj7jOzAwKodMg==", "541533c3-05a6-4129-bc17-087f652c6f3b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "821f98b0-ff1e-458a-b15b-a9ea85750e45",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a2d1d1e9-596f-4007-b0ae-a2e2afd2e921", "AQAAAAEAACcQAAAAEFB5EFJai/Fv9XNlW0jJYFapeLZ+7QoPEYLcuxw9wbGfP7hQ11gCc028VmfgmzpoWA==", "d8a128df-365b-4669-9f07-7b7fdd894132" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b7a77c91-dedc-432c-8a54-c2f28010814d", "AQAAAAEAACcQAAAAEE6yUWIMK2hm6Sw651J/44DbjsgMufrpkvWAv59Bc86LEmhq2RxU/J14N4SHWz1Vpw==", "6cc88c4d-f17d-4202-a3e5-3ac2afd67560" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efb6c53f-48d0-4981-a3c7-ef714fa2b508",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e47b2f78-59d6-4085-a587-0abf906c6205", "AQAAAAEAACcQAAAAENp8wFk/2GlLW0YpqaT4dCnXweO07Z6Ih3Whm2doL5Fo3CEK3xUSiF0glZMKxsBLlw==", "d3645e5e-7ba9-47ae-937d-61f901f02e2d" });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 9, 11, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 11, 18, 23, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("97550023-4dd5-425b-8285-420b3bd52ad1"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 9, 10, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 10, 18, 23, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("c7bdc7a7-8a10-46e2-8058-3f9b645662c6"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 9, 12, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 9, 12, 18, 23, 0, 0, DateTimeKind.Unspecified) });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "319a3971-483c-4ed9-a0cc-2bec19a07b06",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccd6a6ca-ed23-485b-a40d-5e32e0020211", "AQAAAAEAACcQAAAAEDh9Q61uFWiPuIFN/Ii5iNLuXp+IC8E/vkcQCYcPNsGb82C1kIL41cBBhZYzb4WveQ==", "c5070b7e-970f-443e-a1fd-9dead3642823" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3ac33a35-d3d6-4a04-a79b-aded19a205db",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6808c6c2-881e-44bd-806f-0ed6c4bf19c2", "AQAAAAEAACcQAAAAEJj9o5t9kTav7VC8tFTDYov43JTH0mAFGTbzGYEi4mA2+YUnEom0NBe9Jv5nfEEK6w==", "dae3e5ad-5650-442e-a6ae-2d360ed07267" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "4b40d3d6-bd20-43f2-a5b8-082009c13682",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "c5ddbe69-9e0e-43ba-89d5-cade6888c492", "AQAAAAEAACcQAAAAEP2PZpq7VGgsq1xM9KVUtesjwzBrRdN0hwhnak7Jv788fKeduYXY7k2pzY4u3lZB5A==", "661cb6f2-434f-496f-8916-db09ac22d651" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "51d08f0c-0c50-4e47-8e8b-55f7125019bc",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "6d804c13-17d5-4375-a6f9-c7e6d21040b9", "AQAAAAEAACcQAAAAENVemRP5y37PloG0ltwC5sWiRVjYJDX9DtUr80xn6OCn7HodvpiR0DmFRRF0OxXjZA==", "28892ef9-c5d7-4087-977c-5c4c5b7d75fa" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "821f98b0-ff1e-458a-b15b-a9ea85750e45",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "821d99eb-2a0e-4f59-b020-ce6c47ad515e", "AQAAAAEAACcQAAAAEEhtx6uwEb6Q0we6drGtoEV35gtjtFHxWLZUUOcm5MZmsNnRyioZJ3wgPSMGwdoXcQ==", "4f5bc9fb-f05f-4284-a76e-c4e41b16ab82" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "bb76dfd3-10d3-4224-b4b3-712aafa7ccaa",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2a098ed6-6532-4200-b5ae-7b68007d908a", "AQAAAAEAACcQAAAAEAv8OI+L0vcDi1cdvOyvcHEMlQb1mk5roSitQZCiI0nw5Lv6Ex4May5FgQuPBhdyRg==", "41d68c2c-e6c2-4cab-ba42-51fd9047f37b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "efb6c53f-48d0-4981-a3c7-ef714fa2b508",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "830b388c-79e2-4f4a-85d0-b779c4ed860c", "AQAAAAEAACcQAAAAEN4N7eJqAxByqlieirXpwNcHK0Shg170vQZf1bzbTwOqF83anvLYx8i97776naAgwg==", "0e2b0c0c-a0d9-4eaf-a6a7-9047ed75d0d8" });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("44198f59-813b-4a96-a930-84f7b77a4eb6"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 8, 11, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 11, 18, 23, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("97550023-4dd5-425b-8285-420b3bd52ad1"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 8, 10, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 10, 18, 23, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: new Guid("c7bdc7a7-8a10-46e2-8058-3f9b645662c6"),
                columns: new[] { "ScheduledArrival", "ScheduledDeparture" },
                values: new object[] { new DateTime(2022, 8, 12, 20, 23, 0, 0, DateTimeKind.Unspecified), new DateTime(2022, 8, 12, 18, 23, 0, 0, DateTimeKind.Unspecified) });
        }
    }
}
