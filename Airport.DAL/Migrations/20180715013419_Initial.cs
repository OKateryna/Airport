using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Crews",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Crews", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Number = table.Column<string>(nullable: true),
                    PlaceDeparture = table.Column<string>(nullable: true),
                    TimeDeparture = table.Column<DateTime>(nullable: false),
                    Destination = table.Column<string>(nullable: true),
                    TimeDestination = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Pilots",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false),
                    Expierence = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pilots", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PlaneTypes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlaneModel = table.Column<string>(nullable: true),
                    PlacesAmount = table.Column<int>(nullable: false),
                    CarryingCapacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlaneTypes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Stewardesses",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    SecondName = table.Column<string>(nullable: true),
                    BirthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Stewardesses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tickets",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Price = table.Column<double>(nullable: false),
                    FlightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tickets", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tickets_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewPilot",
                columns: table => new
                {
                    CrewId = table.Column<int>(nullable: false),
                    PilotId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewPilot", x => new { x.CrewId, x.PilotId });
                    table.ForeignKey(
                        name: "FK_CrewPilot_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewPilot_Pilots_PilotId",
                        column: x => x.PilotId,
                        principalTable: "Pilots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Planes",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    PlaneName = table.Column<string>(nullable: true),
                    PlaneTypeId = table.Column<int>(nullable: false),
                    ManufectureDate = table.Column<DateTime>(nullable: false),
                    LifeSpan = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Planes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Planes_PlaneTypes_PlaneTypeId",
                        column: x => x.PlaneTypeId,
                        principalTable: "PlaneTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CrewStewardess",
                columns: table => new
                {
                    CrewId = table.Column<int>(nullable: false),
                    StewardessId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CrewStewardess", x => new { x.CrewId, x.StewardessId });
                    table.ForeignKey(
                        name: "FK_CrewStewardess_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CrewStewardess_Stewardesses_StewardessId",
                        column: x => x.StewardessId,
                        principalTable: "Stewardesses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departures",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    FlightId = table.Column<int>(nullable: false),
                    DepartureDate = table.Column<DateTime>(nullable: false),
                    CrewId = table.Column<int>(nullable: false),
                    PlaneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departures", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departures_Crews_CrewId",
                        column: x => x.CrewId,
                        principalTable: "Crews",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Departures_Planes_PlaneId",
                        column: x => x.PlaneId,
                        principalTable: "Planes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Crews",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { 1, "Alpha" },
                    { 2, "Bravo" },
                    { 3, "Apolo" },
                    { 4, "Delta" }
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "Destination", "Number", "PlaceDeparture", "TimeDeparture", "TimeDestination" },
                values: new object[,]
                {
                    { 1, "Paris", "12qwdf", "London", new DateTime(2016, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), new DateTime(2016, 12, 6, 0, 15, 0, 0, DateTimeKind.Unspecified) },
                    { 2, "Habana", "55abll", "Lviv", new DateTime(2017, 11, 23, 13, 10, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 11, 23, 23, 55, 0, 0, DateTimeKind.Unspecified) },
                    { 3, "London", "78qsco", "Habana", new DateTime(2018, 5, 11, 7, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2018, 5, 11, 16, 10, 0, 0, DateTimeKind.Unspecified) },
                    { 4, "Paris", "02fthn", "Lisbon", new DateTime(2017, 9, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2017, 9, 7, 10, 30, 0, 0, DateTimeKind.Unspecified) }
                });

            migrationBuilder.InsertData(
                table: "Pilots",
                columns: new[] { "Id", "BirthDate", "Expierence", "FirstName", "SecondName" },
                values: new object[,]
                {
                    { 4, new DateTime(1992, 11, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), 2, "Viktor", "Romaniuk" },
                    { 3, new DateTime(1960, 10, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), 31, "Taras", "Boiko" },
                    { 2, new DateTime(1987, 1, 19, 0, 0, 0, 0, DateTimeKind.Unspecified), 4, "Ihor", "Vitrenko" },
                    { 1, new DateTime(1980, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), 8, "Oleg", "Petrenko" }
                });

            migrationBuilder.InsertData(
                table: "PlaneTypes",
                columns: new[] { "Id", "CarryingCapacity", "PlacesAmount", "PlaneModel" },
                values: new object[,]
                {
                    { 1, 52800, 114, "777" },
                    { 2, 15000, 40, "A320" },
                    { 3, 30000, 300, "100" },
                    { 4, 47000, 80, "Ту-134" }
                });

            migrationBuilder.InsertData(
                table: "Stewardesses",
                columns: new[] { "Id", "BirthDate", "FirstName", "SecondName" },
                values: new object[,]
                {
                    { 1, new DateTime(1982, 6, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Olena", "Petrenko" },
                    { 2, new DateTime(1998, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Iryna", "Moroz" },
                    { 3, new DateTime(1993, 6, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "Larysa", "Kovalchuk" },
                    { 4, new DateTime(1989, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "Karina", "Voitovych" }
                });

            migrationBuilder.InsertData(
                table: "CrewPilot",
                columns: new[] { "CrewId", "PilotId" },
                values: new object[,]
                {
                    { 2, 1 },
                    { 4, 2 },
                    { 1, 4 },
                    { 3, 4 }
                });

            migrationBuilder.InsertData(
                table: "CrewStewardess",
                columns: new[] { "CrewId", "StewardessId" },
                values: new object[,]
                {
                    { 2, 4 },
                    { 1, 3 },
                    { 2, 2 },
                    { 3, 2 },
                    { 4, 2 },
                    { 1, 2 },
                    { 3, 4 },
                    { 3, 1 },
                    { 1, 1 },
                    { 2, 1 }
                });

            migrationBuilder.InsertData(
                table: "Planes",
                columns: new[] { "Id", "LifeSpan", "ManufectureDate", "PlaneName", "PlaneTypeId" },
                values: new object[,]
                {
                    { 1, 10, new DateTime(2009, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), "Sukhoi SuperJet", 3 },
                    { 2, 6, new DateTime(2018, 12, 5, 23, 15, 0, 0, DateTimeKind.Unspecified), "Airbus", 2 },
                    { 4, 20, new DateTime(2000, 9, 7, 8, 0, 0, 0, DateTimeKind.Unspecified), "Boeing", 1 },
                    { 3, 14, new DateTime(2016, 5, 11, 7, 30, 0, 0, DateTimeKind.Unspecified), "Tupolev", 4 }
                });

            migrationBuilder.InsertData(
                table: "Tickets",
                columns: new[] { "Id", "FlightId", "Price" },
                values: new object[,]
                {
                    { 3, 4, 222.2 },
                    { 4, 3, 100.0 },
                    { 2, 2, 212.0 },
                    { 1, 1, 112.0 }
                });

            migrationBuilder.InsertData(
                table: "Departures",
                columns: new[] { "Id", "CrewId", "DepartureDate", "FlightId", "PlaneId" },
                values: new object[,]
                {
                    { 2, 1, new DateTime(2017, 3, 23, 12, 0, 0, 0, DateTimeKind.Unspecified), 2, 4 },
                    { 4, 4, new DateTime(2017, 8, 5, 16, 45, 0, 0, DateTimeKind.Unspecified), 4, 2 },
                    { 3, 2, new DateTime(2017, 5, 11, 1, 45, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 1, 3, new DateTime(2017, 12, 9, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CrewPilot_CrewId",
                table: "CrewPilot",
                column: "CrewId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CrewPilot_PilotId",
                table: "CrewPilot",
                column: "PilotId");

            migrationBuilder.CreateIndex(
                name: "IX_CrewStewardess_StewardessId",
                table: "CrewStewardess",
                column: "StewardessId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_CrewId",
                table: "Departures",
                column: "CrewId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_FlightId",
                table: "Departures",
                column: "FlightId");

            migrationBuilder.CreateIndex(
                name: "IX_Departures_PlaneId",
                table: "Departures",
                column: "PlaneId");

            migrationBuilder.CreateIndex(
                name: "IX_Planes_PlaneTypeId",
                table: "Planes",
                column: "PlaneTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Tickets_FlightId",
                table: "Tickets",
                column: "FlightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CrewPilot");

            migrationBuilder.DropTable(
                name: "CrewStewardess");

            migrationBuilder.DropTable(
                name: "Departures");

            migrationBuilder.DropTable(
                name: "Tickets");

            migrationBuilder.DropTable(
                name: "Pilots");

            migrationBuilder.DropTable(
                name: "Stewardesses");

            migrationBuilder.DropTable(
                name: "Crews");

            migrationBuilder.DropTable(
                name: "Planes");

            migrationBuilder.DropTable(
                name: "Flights");

            migrationBuilder.DropTable(
                name: "PlaneTypes");
        }
    }
}
