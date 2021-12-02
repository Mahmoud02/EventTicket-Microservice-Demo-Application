using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventTicket.Services.EventCatalog.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.CategoryId);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    Artist = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "CategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), "Concerts" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { new Guid("bf3f3002-7e53-441e-8b76-f6280be284aa"), "Soccer" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "CategoryId", "Name" },
                values: new object[] { new Guid("fe98f549-e790-4e9f-aa16-18c2292a2ee9"), "Conferences" });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("ee272f8b-6096-4cb6-8625-bb4bb2d89e8b"), "Tamer Hosny", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2022, 5, 30, 19, 23, 16, 476, DateTimeKind.Local).AddTicks(6094), "Tamer Hosny will be performing  at Must Theater and at the Football Yard of Misr University for science and technology", "https://www.ticketsmarche.com/mm/TAMER%20SLIDER12.jpg", "Tamer Hosny Live", 65 });

            migrationBuilder.InsertData(
                table: "Events",
                columns: new[] { "EventId", "Artist", "CategoryId", "Date", "Description", "ImageUrl", "Name", "Price" },
                values: new object[] { new Guid("3448d5a4-0f72-4dd7-bf15-c14a46b26c00"), "Omar Khairat", new Guid("b0788d2f-8003-43c1-92a4-edc76a7c5dde"), new DateTime(2022, 8, 30, 19, 23, 16, 477, DateTimeKind.Local).AddTicks(7225), "Event House proudly presents the legendary music composer Omar Khairat at The Marquee Theater for an exquisite musical experience that will enchant our audience in an exclusive concertConcert by: Cairo Festival CityOrganized by: Event House", "https://www.ticketsmarche.com/mm/okdec2021slider.jpg", "Omar Khairat Live!", 85 });

            migrationBuilder.CreateIndex(
                name: "IX_Events_CategoryId",
                table: "Events",
                column: "CategoryId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
