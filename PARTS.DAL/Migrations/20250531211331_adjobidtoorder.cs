using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PARTS.DAL.Migrations
{
    /// <inheritdoc />
    public partial class adjobidtoorder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "JobId",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "JobId",
                table: "Orders");
        }
    }
}
