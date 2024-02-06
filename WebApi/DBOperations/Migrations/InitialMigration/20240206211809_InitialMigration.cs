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
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    GenreId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CUSTOMERGENRES", x => new { x.CustomerId, x.GenreId });
                    table.ForeignKey(
                        name: "FK_CUSTOMERGENRES_CUSTOMERS_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CUSTOMERGENRES_GENRES_GenreId",
                        column: x => x.GenreId,
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
                    GenreId = table.Column<int>(type: "int", nullable: false),
                    DirectorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIES", x => x.ID);
                    table.ForeignKey(
                        name: "FK_MOVIES_DIRECTORS_DirectorId",
                        column: x => x.DirectorId,
                        principalTable: "DIRECTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIES_GENRES_GenreId",
                        column: x => x.GenreId,
                        principalTable: "GENRES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ActorMovie",
                columns: table => new
                {
                    ActorsId = table.Column<int>(type: "int", nullable: false),
                    MoviesId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ActorMovie", x => new { x.ActorsId, x.MoviesId });
                    table.ForeignKey(
                        name: "FK_ActorMovie_ACTORS_ActorsId",
                        column: x => x.ActorsId,
                        principalTable: "ACTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ActorMovie_MOVIES_MoviesId",
                        column: x => x.MoviesId,
                        principalTable: "MOVIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MOVIEACTORS",
                columns: table => new
                {
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    ActorId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MOVIEACTORS", x => new { x.MovieId, x.ActorId });
                    table.ForeignKey(
                        name: "FK_MOVIEACTORS_ACTORS_ActorId",
                        column: x => x.ActorId,
                        principalTable: "ACTORS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MOVIEACTORS_MOVIES_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MOVIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PURCHASES",
                columns: table => new
                {
                    CustomerId = table.Column<int>(type: "int", nullable: false),
                    MovieId = table.Column<int>(type: "int", nullable: false),
                    PRICE = table.Column<double>(type: "float", nullable: false),
                    PURCHASETIME = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PURCHASES", x => new { x.CustomerId, x.MovieId });
                    table.ForeignKey(
                        name: "FK_PURCHASES_CUSTOMERS_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "CUSTOMERS",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PURCHASES_MOVIES_MovieId",
                        column: x => x.MovieId,
                        principalTable: "MOVIES",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ActorMovie_MoviesId",
                table: "ActorMovie",
                column: "MoviesId");

            migrationBuilder.CreateIndex(
                name: "IX_CUSTOMERGENRES_GenreId",
                table: "CUSTOMERGENRES",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIEACTORS_ActorId",
                table: "MOVIEACTORS",
                column: "ActorId");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_DirectorId",
                table: "MOVIES",
                column: "DirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_MOVIES_GenreId",
                table: "MOVIES",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_PURCHASES_MovieId",
                table: "PURCHASES",
                column: "MovieId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ActorMovie");

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
