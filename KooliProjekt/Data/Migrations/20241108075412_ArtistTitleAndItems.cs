using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KooliProjekt.Data.Migrations
{
    /// <inheritdoc />
    public partial class ArtistTitleAndItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ArtistId",
                table: "Artist",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Artist",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Artist_ArtistId",
                table: "Artist",
                column: "ArtistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Artist_Artist_ArtistId",
                table: "Artist",
                column: "ArtistId",
                principalTable: "Artist",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Artist_Artist_ArtistId",
                table: "Artist");

            migrationBuilder.DropIndex(
                name: "IX_Artist_ArtistId",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "ArtistId",
                table: "Artist");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Artist");
        }
    }
}
