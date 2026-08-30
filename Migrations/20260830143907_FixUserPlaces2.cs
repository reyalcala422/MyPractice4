using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPractice4.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPlaces2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Places_PlacesId",
                table: "UserPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlaces",
                table: "UserPlaces");

            migrationBuilder.DropIndex(
                name: "IX_UserPlaces_PlacesId",
                table: "UserPlaces");

            migrationBuilder.DropColumn(
                name: "PlacesId",
                table: "UserPlaces");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "UserPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlaces",
                table: "UserPlaces",
                columns: new[] { "UserId", "PlaceId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlaces",
                table: "UserPlaces");

            migrationBuilder.AlterColumn<int>(
                name: "PlaceId",
                table: "UserPlaces",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PlacesId",
                table: "UserPlaces",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlaces",
                table: "UserPlaces",
                columns: new[] { "UserId", "PlacesId" });

            migrationBuilder.CreateIndex(
                name: "IX_UserPlaces_PlacesId",
                table: "UserPlaces",
                column: "PlacesId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaces_Places_PlacesId",
                table: "UserPlaces",
                column: "PlacesId",
                principalTable: "Places",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
