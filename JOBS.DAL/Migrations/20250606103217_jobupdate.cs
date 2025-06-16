using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JOBS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class jobupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PredictionId",
                table: "Jobs",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PredictionId",
                table: "Jobs");
        }
    }
}
