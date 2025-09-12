using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TooliRent.DAL.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Tools_ToolId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_ToolId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Tools");

            migrationBuilder.DropColumn(
                name: "IsPickedUp",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "IsReturned",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "ToolId",
                table: "Bookings",
                newName: "Status");

            migrationBuilder.CreateTable(
                name: "ToolItems",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ToolId = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ToolItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ToolItems_Tools_ToolId",
                        column: x => x.ToolId,
                        principalTable: "Tools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BookingToolItem",
                columns: table => new
                {
                    BookingId = table.Column<int>(type: "int", nullable: false),
                    ToolItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookingToolItem", x => new { x.BookingId, x.ToolItemId });
                    table.ForeignKey(
                        name: "FK_BookingToolItem_Bookings_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookingToolItem_ToolItems_ToolItemId",
                        column: x => x.ToolItemId,
                        principalTable: "ToolItems",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookingToolItem_ToolItemId",
                table: "BookingToolItem",
                column: "ToolItemId");

            migrationBuilder.CreateIndex(
                name: "IX_ToolItems_ToolId",
                table: "ToolItems",
                column: "ToolId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookingToolItem");

            migrationBuilder.DropTable(
                name: "ToolItems");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Bookings",
                newName: "ToolId");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Tools",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "IsPickedUp",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsReturned",
                table: "Bookings",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_ToolId",
                table: "Bookings",
                column: "ToolId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Tools_ToolId",
                table: "Bookings",
                column: "ToolId",
                principalTable: "Tools",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
