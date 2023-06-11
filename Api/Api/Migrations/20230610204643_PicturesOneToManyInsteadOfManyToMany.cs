using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyInventory.Api.Migrations
{
    /// <inheritdoc />
    public partial class PicturesOneToManyInsteadOfManyToMany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ItemId",
                table: "Items");

            migrationBuilder.DropTable(
                name: "ItemPicture");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Items",
                newName: "ParentItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ItemId",
                table: "Items",
                newName: "IX_Items_ParentItemId");

            migrationBuilder.AddColumn<int>(
                name: "ItemId",
                table: "Pictures",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ParentItemId",
                table: "Items",
                column: "ParentItemId",
                principalTable: "Items",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pictures_Items_ItemId",
                table: "Pictures",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Items_Items_ParentItemId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Pictures_Items_ItemId",
                table: "Pictures");

            migrationBuilder.DropIndex(
                name: "IX_Pictures_ItemId",
                table: "Pictures");

            migrationBuilder.DropColumn(
                name: "ItemId",
                table: "Pictures");

            migrationBuilder.RenameColumn(
                name: "ParentItemId",
                table: "Items",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_ParentItemId",
                table: "Items",
                newName: "IX_Items_ItemId");

            migrationBuilder.CreateTable(
                name: "ItemPicture",
                columns: table => new
                {
                    ItemsId = table.Column<int>(type: "int", nullable: false),
                    PictureId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemPicture", x => new { x.ItemsId, x.PictureId });
                    table.ForeignKey(
                        name: "FK_ItemPicture_Items_ItemsId",
                        column: x => x.ItemsId,
                        principalTable: "Items",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemPicture_Pictures_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Pictures",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemPicture_PictureId",
                table: "ItemPicture",
                column: "PictureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Items_ItemId",
                table: "Items",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id");
        }
    }
}
