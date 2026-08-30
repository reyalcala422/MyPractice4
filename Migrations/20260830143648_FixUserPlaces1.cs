using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyPractice4.Migrations
{
    /// <inheritdoc />
    public partial class FixUserPlaces1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Users_PlaceId",
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

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserPlaces",
                table: "UserPlaces",
                columns: new[] { "UserId", "PlacesId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces",
                column: "PlaceId",
                principalTable: "Places",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserPlaces_Users_UserId",
                table: "UserPlaces",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Places_PlaceId",
                table: "UserPlaces");

            migrationBuilder.DropForeignKey(
                name: "FK_UserPlaces_Users_UserId",
                table: "UserPlaces");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserPlaces",
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
                name: "FK_UserPlaces_Users_PlaceId",
                table: "UserPlaces",
                column: "PlaceId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
