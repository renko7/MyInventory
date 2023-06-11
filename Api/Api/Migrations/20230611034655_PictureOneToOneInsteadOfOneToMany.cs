using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventory.Api.Migrations
{
    /// <inheritdoc />
    public partial class PictureOneToOneInsteadOfOneToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures",
                column: "ItemId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures");

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures",
                column: "ItemId");
        }
    }
}
