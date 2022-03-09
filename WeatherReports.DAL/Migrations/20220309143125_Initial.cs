using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeatherReports.DAL.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Weather",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Data = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Temperature = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Humidity = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Td = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AtmosphericPressure = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindDirection = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WindSpeed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Cloudiness = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    H = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    VV = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    WeatherPhenomena = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Weather", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Weather");
        }
    }
}
