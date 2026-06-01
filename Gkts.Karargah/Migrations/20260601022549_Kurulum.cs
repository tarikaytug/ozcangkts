using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Gkts.Karargah.Migrations
{
    /// <inheritdoc />
    public partial class Kurulum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TaktikVeriler",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    BirlikAdi = table.Column<string>(type: "text", nullable: false),
                    Enlem = table.Column<double>(type: "double precision", nullable: false),
                    Boylam = table.Column<double>(type: "double precision", nullable: false),
                    Saglik = table.Column<int>(type: "integer", nullable: false),
                    KayitZamani = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TaktikVeriler", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TaktikVeriler");
        }
    }
}
