using Microsoft.EntityFrameworkCore.Migrations;

namespace Airport.DAL.Migrations
{
    public partial class FixingFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Planes_PlaneId",
                table: "Departures");

            migrationBuilder.RenameColumn(
                name: "Expierence",
                table: "Pilots",
                newName: "Experience");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Planes_PlaneId",
                table: "Departures",
                column: "PlaneId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures");

            migrationBuilder.DropForeignKey(
                name: "FK_Departures_Planes_PlaneId",
                table: "Departures");

            migrationBuilder.RenameColumn(
                name: "Experience",
                table: "Pilots",
                newName: "Expierence");

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Crews_CrewId",
                table: "Departures",
                column: "CrewId",
                principalTable: "Crews",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Flights_FlightId",
                table: "Departures",
                column: "FlightId",
                principalTable: "Flights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Departures_Planes_PlaneId",
                table: "Departures",
                column: "PlaneId",
                principalTable: "Planes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
