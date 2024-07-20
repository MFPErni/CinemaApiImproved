using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CinemaAPI_Improved.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DirectorList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Birthdate = table.Column<DateOnly>(type: "date", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GenreList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ViewersList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ContactNumber = table.Column<int>(type: "int", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ViewersList", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovieList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    DDetailsId = table.Column<int>(type: "int", nullable: false),
                    GDetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovieList_DirectorList_DDetailsId",
                        column: x => x.DDetailsId,
                        principalTable: "DirectorList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieList_GenreList_GDetailsId",
                        column: x => x.GDetailsId,
                        principalTable: "GenreList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TicketList",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PurchaseDate = table.Column<DateOnly>(type: "date", nullable: false),
                    MdetailsId = table.Column<int>(type: "int", nullable: false),
                    VdetailsId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TicketList_MovieList_MdetailsId",
                        column: x => x.MdetailsId,
                        principalTable: "MovieList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TicketList_ViewersList_VdetailsId",
                        column: x => x.VdetailsId,
                        principalTable: "ViewersList",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_DDetailsId",
                table: "MovieList",
                column: "DDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_MovieList_GDetailsId",
                table: "MovieList",
                column: "GDetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketList_MdetailsId",
                table: "TicketList",
                column: "MdetailsId");

            migrationBuilder.CreateIndex(
                name: "IX_TicketList_VdetailsId",
                table: "TicketList",
                column: "VdetailsId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TicketList");

            migrationBuilder.DropTable(
                name: "MovieList");

            migrationBuilder.DropTable(
                name: "ViewersList");

            migrationBuilder.DropTable(
                name: "DirectorList");

            migrationBuilder.DropTable(
                name: "GenreList");
        }
    }
}
