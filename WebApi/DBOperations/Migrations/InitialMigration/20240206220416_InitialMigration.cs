using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WebApi.DBOperations.Migrations.InitialMigration
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ACTORS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ACTORS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "DIRECTORS",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SURNAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DIRECTORS", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "GENRES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GENRES", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "CUSTOMERGENRES",
                columns: table => new
                {
                    CUSTOMERID = table.Column<int>(type: "int", nullable: false),
                    GENREID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERGENRES", x => new { x.CUSTOMERID, x.GENREID });
                    table.ForeignKey(
                        name: "FK_CUSTOMERGENRES_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CUSTOMERGENRES_GENRES_GENREID",
                        column: x => x.GENREID,
                        principalTable: "GENRES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOVIES",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NAME = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    RELEASEDATE = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PRICE = table.Column<double>(type: "float", nullable: false),
                    GENREID = table.Column<int>(type: "int", nullable: false),
                    DIRECTORID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOVIES_DIRECTORS_DIRECTORID",
                        column: x => x.DIRECTORID,
                        principalTable: "DIRECTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIES_GENRES_GENREID",
                        column: x => x.GENREID,
                        principalTable: "GENRES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOVIEACTORS",
                columns: table => new
                {
                    MOVIEID = table.Column<int>(type: "int", nullable: false),
                    ACTORID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIEACTORS", x => new { x.MOVIEID, x.ACTORID });
                    table.ForeignKey(
                        name: "FK_MOVIEACTORS_ACTORS_ACTORID",
                        column: x => x.ACTORID,
                        principalTable: "ACTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIEACTORS_MOVIES_MOVIEID",
                        column: x => x.MOVIEID,
                        principalTable: "MOVIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASES",
                columns: table => new
                {
                    CUSTOMERID = table.Column<int>(type: "int", nullable: false),
                    MOVIEID = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<double>(type: "float", nullable: false),
                    PURCHASETIME = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASES", x => new { x.CUSTOMERID, x.MOVIEID });
                    table.ForeignKey(
                        name: "FK_PURCHASES_CUSTOMERS_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PURCHASES_MOVIES_MOVIEID",
                        column: x => x.MOVIEID,
                        principalTable: "MOVIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERGENRES_GENREID",
                table: "CUSTOMERGENRES",
                column: "GENREID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIEACTORS_ACTORID",
                table: "MOVIEACTORS",
                column: "ACTORID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_DIRECTORID",
                table: "MOVIES",
                column: "DIRECTORID");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_GENREID",
                table: "MOVIES",
                column: "GENREID");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASES_MOVIEID",
                table: "PURCHASES",
                column: "MOVIEID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CUSTOMERGENRES");

            migrationBuilder.DropTable(
                name: "MOVIEACTORS");

            migrationBuilder.DropTable(
                name: "PURCHASES");

            migrationBuilder.DropTable(
                name: "ACTORS");

            migrationBuilder.DropTable(
                name: "CUSTOMERS");

            migrationBuilder.DropTable(
                name: "MOVIES");

            migrationBuilder.DropTable(
                name: "DIRECTORS");

            migrationBuilder.DropTable(
                name: "GENRES");
        }
    }
}
