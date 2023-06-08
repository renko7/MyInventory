using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventory.Api.Migrations
{
    /// <inheritdoc />
    public partial class GuidPublicId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "PublicId",
                table: "Items",
                type: "uniqueidentifier",
                nullable: false,
                defaultValueSql: "newsequentialid()");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PublicId",
                table: "Items");
        }
    }
}
