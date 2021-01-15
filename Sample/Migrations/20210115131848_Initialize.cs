using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Sample.Migrations
{
    public partial class Initialize : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Artists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Biography = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Artists", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Genres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Genres", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<TimeSpan>(type: "time", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AlbumTracks",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    AlbumId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    TrackId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AlbumTracks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AlbumTracks_Products_AlbumId",
                        column: x => x.AlbumId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AlbumTracks_Products_TrackId",
                        column: x => x.TrackId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "GenreItems",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenreItems", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenreItems_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenreItems_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_GenreItems_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Images",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ImageSize = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Images_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Images_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LocalizationNames",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Title = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    LanguageType = table.Column<int>(type: "int", nullable: false),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    GenreId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocalizationNames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocalizationNames_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalizationNames_Genres_GenreId",
                        column: x => x.GenreId,
                        principalTable: "Genres",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LocalizationNames_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Lyrics",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Text = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateOfRegistration = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifyDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lyrics", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lyrics_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductArtists",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductArtists", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductArtists_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductArtists_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Qualities",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    FileType = table.Column<int>(type: "int", nullable: false),
                    Size = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    QualityType = table.Column<int>(type: "int", nullable: false),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualities_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWSEQUENTIALID()"),
                    Text = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Discriminator = table.Column<int>(type: "int", nullable: false),
                    ArtistId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tags_Artists_ArtistId",
                        column: x => x.ArtistId,
                        principalTable: "Artists",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Tags_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DateOfRegistration", "LastModifyDate" },
                values: new object[,]
                {
                    { new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), new DateTime(2020, 6, 6, 15, 47, 23, 527, DateTimeKind.Local).AddTicks(1746), new DateTime(2021, 1, 16, 13, 39, 11, 780, DateTimeKind.Local).AddTicks(9934) },
                    { new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), new DateTime(2021, 1, 13, 18, 42, 22, 40, DateTimeKind.Local).AddTicks(4844), new DateTime(2021, 1, 15, 18, 33, 20, 128, DateTimeKind.Local).AddTicks(8166) },
                    { new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), new DateTime(2020, 7, 5, 15, 32, 29, 850, DateTimeKind.Local).AddTicks(951), new DateTime(2021, 1, 16, 8, 47, 16, 64, DateTimeKind.Local).AddTicks(1385) },
                    { new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), new DateTime(2021, 1, 13, 17, 28, 42, 428, DateTimeKind.Local).AddTicks(2352), new DateTime(2021, 1, 16, 3, 43, 22, 137, DateTimeKind.Local).AddTicks(3057) },
                    { new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), new DateTime(2020, 10, 29, 7, 37, 26, 694, DateTimeKind.Local).AddTicks(4378), new DateTime(2021, 1, 16, 6, 14, 46, 734, DateTimeKind.Local).AddTicks(1116) },
                    { new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), new DateTime(2020, 12, 8, 15, 49, 16, 431, DateTimeKind.Local).AddTicks(306), new DateTime(2021, 1, 15, 19, 53, 40, 576, DateTimeKind.Local).AddTicks(3411) },
                    { new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), new DateTime(2020, 11, 9, 12, 14, 33, 900, DateTimeKind.Local).AddTicks(5604), new DateTime(2021, 1, 15, 23, 56, 0, 717, DateTimeKind.Local).AddTicks(7802) },
                    { new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), new DateTime(2020, 6, 2, 3, 20, 19, 71, DateTimeKind.Local).AddTicks(389), new DateTime(2021, 1, 16, 3, 48, 2, 290, DateTimeKind.Local).AddTicks(5623) },
                    { new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), new DateTime(2020, 7, 24, 12, 27, 12, 834, DateTimeKind.Local).AddTicks(4813), new DateTime(2021, 1, 15, 23, 12, 3, 904, DateTimeKind.Local).AddTicks(3984) },
                    { new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), new DateTime(2020, 4, 3, 22, 58, 5, 398, DateTimeKind.Local).AddTicks(8349), new DateTime(2021, 1, 16, 15, 17, 15, 946, DateTimeKind.Local).AddTicks(766) },
                    { new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), new DateTime(2020, 1, 16, 20, 1, 59, 710, DateTimeKind.Local).AddTicks(702), new DateTime(2021, 1, 16, 4, 52, 49, 177, DateTimeKind.Local).AddTicks(1798) },
                    { new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), new DateTime(2020, 9, 1, 17, 32, 39, 36, DateTimeKind.Local).AddTicks(9069), new DateTime(2021, 1, 16, 15, 34, 13, 349, DateTimeKind.Local).AddTicks(2498) },
                    { new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), new DateTime(2020, 11, 7, 4, 2, 39, 484, DateTimeKind.Local).AddTicks(8547), new DateTime(2021, 1, 16, 7, 59, 51, 714, DateTimeKind.Local).AddTicks(9851) },
                    { new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), new DateTime(2020, 7, 20, 23, 33, 0, 464, DateTimeKind.Local).AddTicks(6612), new DateTime(2021, 1, 15, 18, 27, 17, 4, DateTimeKind.Local).AddTicks(9353) },
                    { new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), new DateTime(2020, 12, 9, 22, 3, 27, 473, DateTimeKind.Local).AddTicks(2880), new DateTime(2021, 1, 16, 5, 28, 32, 859, DateTimeKind.Local).AddTicks(8683) },
                    { new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), new DateTime(2020, 7, 13, 1, 2, 26, 454, DateTimeKind.Local).AddTicks(1185), new DateTime(2021, 1, 16, 0, 16, 39, 779, DateTimeKind.Local).AddTicks(9167) },
                    { new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), new DateTime(2020, 5, 21, 21, 14, 42, 584, DateTimeKind.Local).AddTicks(3378), new DateTime(2021, 1, 15, 17, 26, 49, 553, DateTimeKind.Local).AddTicks(606) },
                    { new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), new DateTime(2020, 3, 12, 0, 36, 16, 751, DateTimeKind.Local).AddTicks(8018), new DateTime(2021, 1, 16, 15, 10, 24, 502, DateTimeKind.Local).AddTicks(4478) },
                    { new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), new DateTime(2020, 12, 5, 8, 22, 17, 29, DateTimeKind.Local).AddTicks(3354), new DateTime(2021, 1, 16, 7, 5, 57, 759, DateTimeKind.Local).AddTicks(6003) },
                    { new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), new DateTime(2020, 10, 28, 5, 8, 33, 571, DateTimeKind.Local).AddTicks(9043), new DateTime(2021, 1, 15, 20, 16, 35, 111, DateTimeKind.Local).AddTicks(2382) },
                    { new Guid("e492b852-576c-43a7-bb80-35665d88c099"), new DateTime(2020, 5, 27, 5, 54, 26, 442, DateTimeKind.Local).AddTicks(9244), new DateTime(2021, 1, 15, 18, 3, 10, 320, DateTimeKind.Local).AddTicks(652) },
                    { new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), new DateTime(2020, 4, 9, 9, 26, 7, 717, DateTimeKind.Local).AddTicks(1121), new DateTime(2021, 1, 16, 4, 13, 47, 677, DateTimeKind.Local).AddTicks(1167) },
                    { new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), new DateTime(2020, 6, 18, 20, 18, 29, 421, DateTimeKind.Local).AddTicks(535), new DateTime(2021, 1, 16, 13, 59, 56, 461, DateTimeKind.Local).AddTicks(4583) },
                    { new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), new DateTime(2020, 9, 29, 1, 34, 33, 815, DateTimeKind.Local).AddTicks(1756), new DateTime(2021, 1, 15, 21, 8, 32, 787, DateTimeKind.Local).AddTicks(1852) },
                    { new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), new DateTime(2020, 12, 10, 6, 31, 35, 303, DateTimeKind.Local).AddTicks(9534), new DateTime(2021, 1, 15, 22, 57, 22, 875, DateTimeKind.Local).AddTicks(4754) },
                    { new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), new DateTime(2020, 10, 25, 0, 11, 55, 900, DateTimeKind.Local).AddTicks(6310), new DateTime(2021, 1, 15, 22, 31, 17, 733, DateTimeKind.Local).AddTicks(6789) },
                    { new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), new DateTime(2020, 4, 9, 22, 32, 47, 425, DateTimeKind.Local).AddTicks(14), new DateTime(2021, 1, 16, 15, 22, 28, 275, DateTimeKind.Local).AddTicks(4914) },
                    { new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), new DateTime(2020, 7, 7, 13, 18, 53, 171, DateTimeKind.Local).AddTicks(1860), new DateTime(2021, 1, 16, 2, 53, 52, 113, DateTimeKind.Local).AddTicks(8819) },
                    { new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), new DateTime(2020, 10, 27, 23, 0, 24, 258, DateTimeKind.Local).AddTicks(1143), new DateTime(2021, 1, 16, 8, 29, 31, 632, DateTimeKind.Local).AddTicks(7765) },
                    { new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), new DateTime(2020, 3, 1, 16, 44, 10, 119, DateTimeKind.Local).AddTicks(8246), new DateTime(2021, 1, 15, 22, 45, 22, 49, DateTimeKind.Local).AddTicks(7377) },
                    { new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), new DateTime(2020, 9, 22, 4, 3, 39, 474, DateTimeKind.Local).AddTicks(1433), new DateTime(2021, 1, 15, 17, 8, 3, 249, DateTimeKind.Local).AddTicks(423) },
                    { new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), new DateTime(2020, 7, 7, 17, 10, 48, 772, DateTimeKind.Local).AddTicks(9183), new DateTime(2021, 1, 16, 2, 21, 32, 930, DateTimeKind.Local).AddTicks(7931) },
                    { new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), new DateTime(2020, 12, 26, 17, 14, 29, 260, DateTimeKind.Local).AddTicks(7565), new DateTime(2021, 1, 16, 15, 20, 55, 729, DateTimeKind.Local).AddTicks(1109) },
                    { new Guid("548d7014-9e08-405d-b228-4acdea77762f"), new DateTime(2020, 3, 30, 20, 25, 31, 942, DateTimeKind.Local).AddTicks(5662), new DateTime(2021, 1, 16, 0, 45, 37, 690, DateTimeKind.Local).AddTicks(9348) },
                    { new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), new DateTime(2020, 2, 21, 13, 0, 24, 449, DateTimeKind.Local).AddTicks(854), new DateTime(2021, 1, 16, 1, 55, 42, 172, DateTimeKind.Local).AddTicks(5087) },
                    { new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), new DateTime(2020, 3, 24, 21, 11, 56, 637, DateTimeKind.Local).AddTicks(9968), new DateTime(2021, 1, 16, 5, 11, 58, 765, DateTimeKind.Local).AddTicks(4495) },
                    { new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), new DateTime(2020, 9, 17, 17, 29, 16, 40, DateTimeKind.Local).AddTicks(2580), new DateTime(2021, 1, 16, 1, 38, 54, 110, DateTimeKind.Local).AddTicks(6782) },
                    { new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), new DateTime(2020, 4, 27, 17, 43, 27, 131, DateTimeKind.Local).AddTicks(8430), new DateTime(2021, 1, 15, 20, 49, 19, 626, DateTimeKind.Local).AddTicks(5547) },
                    { new Guid("7644f565-d186-4827-aff7-198891d67559"), new DateTime(2020, 4, 20, 14, 32, 47, 172, DateTimeKind.Local).AddTicks(1500), new DateTime(2021, 1, 15, 21, 2, 45, 8, DateTimeKind.Local).AddTicks(4953) },
                    { new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), new DateTime(2020, 7, 30, 9, 15, 51, 18, DateTimeKind.Local).AddTicks(3324), new DateTime(2021, 1, 16, 13, 9, 54, 102, DateTimeKind.Local).AddTicks(7593) },
                    { new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), new DateTime(2020, 5, 25, 23, 13, 4, 236, DateTimeKind.Local).AddTicks(1385), new DateTime(2021, 1, 16, 14, 38, 3, 442, DateTimeKind.Local).AddTicks(4140) },
                    { new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), new DateTime(2020, 2, 20, 14, 28, 20, 323, DateTimeKind.Local).AddTicks(8101), new DateTime(2021, 1, 16, 14, 50, 26, 813, DateTimeKind.Local).AddTicks(6362) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DateOfRegistration", "LastModifyDate" },
                values: new object[,]
                {
                    { new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), new DateTime(2020, 3, 25, 3, 8, 19, 375, DateTimeKind.Local).AddTicks(1786), new DateTime(2021, 1, 16, 11, 40, 1, 962, DateTimeKind.Local).AddTicks(8522) },
                    { new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), new DateTime(2021, 1, 12, 7, 46, 33, 356, DateTimeKind.Local).AddTicks(8026), new DateTime(2021, 1, 15, 21, 21, 39, 23, DateTimeKind.Local).AddTicks(2994) },
                    { new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), new DateTime(2021, 1, 2, 18, 6, 4, 106, DateTimeKind.Local).AddTicks(1568), new DateTime(2021, 1, 16, 8, 18, 7, 974, DateTimeKind.Local).AddTicks(9459) },
                    { new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), new DateTime(2020, 2, 21, 14, 38, 21, 983, DateTimeKind.Local).AddTicks(4669), new DateTime(2021, 1, 15, 19, 28, 57, 749, DateTimeKind.Local).AddTicks(3112) },
                    { new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), new DateTime(2020, 5, 2, 2, 34, 33, 647, DateTimeKind.Local).AddTicks(4368), new DateTime(2021, 1, 15, 20, 54, 23, 259, DateTimeKind.Local).AddTicks(8690) },
                    { new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), new DateTime(2020, 11, 17, 7, 51, 15, 226, DateTimeKind.Local).AddTicks(6613), new DateTime(2021, 1, 15, 22, 49, 29, 260, DateTimeKind.Local).AddTicks(3916) },
                    { new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), new DateTime(2020, 10, 21, 15, 25, 41, 854, DateTimeKind.Local).AddTicks(6967), new DateTime(2021, 1, 16, 12, 56, 4, 772, DateTimeKind.Local).AddTicks(1739) },
                    { new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), new DateTime(2020, 11, 26, 21, 47, 10, 764, DateTimeKind.Local).AddTicks(1858), new DateTime(2021, 1, 16, 1, 16, 45, 728, DateTimeKind.Local).AddTicks(3681) },
                    { new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), new DateTime(2020, 1, 15, 19, 59, 39, 381, DateTimeKind.Local).AddTicks(8025), new DateTime(2021, 1, 16, 1, 57, 49, 798, DateTimeKind.Local).AddTicks(7718) },
                    { new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), new DateTime(2021, 1, 6, 6, 7, 39, 943, DateTimeKind.Local).AddTicks(3692), new DateTime(2021, 1, 16, 6, 16, 5, 13, DateTimeKind.Local).AddTicks(6912) },
                    { new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), new DateTime(2020, 9, 5, 15, 55, 24, 554, DateTimeKind.Local).AddTicks(3770), new DateTime(2021, 1, 16, 1, 8, 32, 105, DateTimeKind.Local).AddTicks(6973) },
                    { new Guid("93f27cb5-7595-45b2-b804-07b088320016"), new DateTime(2020, 12, 12, 6, 7, 36, 474, DateTimeKind.Local).AddTicks(888), new DateTime(2021, 1, 16, 10, 52, 50, 20, DateTimeKind.Local).AddTicks(4948) },
                    { new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), new DateTime(2020, 9, 13, 10, 45, 37, 593, DateTimeKind.Local).AddTicks(8710), new DateTime(2021, 1, 16, 4, 13, 35, 949, DateTimeKind.Local).AddTicks(8411) },
                    { new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), new DateTime(2020, 3, 9, 16, 24, 13, 108, DateTimeKind.Local).AddTicks(7408), new DateTime(2021, 1, 15, 22, 57, 17, 169, DateTimeKind.Local).AddTicks(403) },
                    { new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), new DateTime(2020, 4, 8, 0, 24, 31, 339, DateTimeKind.Local).AddTicks(748), new DateTime(2021, 1, 16, 3, 29, 13, 684, DateTimeKind.Local).AddTicks(8003) },
                    { new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), new DateTime(2020, 3, 18, 20, 27, 33, 81, DateTimeKind.Local).AddTicks(4484), new DateTime(2021, 1, 16, 11, 51, 26, 674, DateTimeKind.Local).AddTicks(9810) },
                    { new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), new DateTime(2020, 4, 13, 16, 8, 52, 275, DateTimeKind.Local).AddTicks(6880), new DateTime(2021, 1, 16, 3, 17, 24, 484, DateTimeKind.Local).AddTicks(5675) },
                    { new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), new DateTime(2020, 3, 21, 14, 5, 1, 347, DateTimeKind.Local).AddTicks(9067), new DateTime(2021, 1, 16, 14, 10, 14, 770, DateTimeKind.Local).AddTicks(4267) },
                    { new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), new DateTime(2020, 12, 25, 16, 37, 44, 670, DateTimeKind.Local).AddTicks(8524), new DateTime(2021, 1, 16, 15, 18, 21, 927, DateTimeKind.Local).AddTicks(8227) },
                    { new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), new DateTime(2020, 6, 24, 6, 55, 8, 227, DateTimeKind.Local).AddTicks(1140), new DateTime(2021, 1, 16, 4, 57, 31, 359, DateTimeKind.Local).AddTicks(3392) },
                    { new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), new DateTime(2020, 11, 16, 7, 10, 9, 51, DateTimeKind.Local).AddTicks(2312), new DateTime(2021, 1, 16, 3, 16, 22, 576, DateTimeKind.Local).AddTicks(7475) },
                    { new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), new DateTime(2020, 8, 1, 5, 55, 1, 827, DateTimeKind.Local).AddTicks(9280), new DateTime(2021, 1, 16, 15, 12, 4, 400, DateTimeKind.Local).AddTicks(6646) },
                    { new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), new DateTime(2020, 11, 18, 19, 6, 21, 74, DateTimeKind.Local).AddTicks(6500), new DateTime(2021, 1, 16, 3, 9, 31, 50, DateTimeKind.Local).AddTicks(1735) },
                    { new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), new DateTime(2021, 1, 3, 12, 30, 0, 923, DateTimeKind.Local).AddTicks(9101), new DateTime(2021, 1, 16, 1, 32, 26, 526, DateTimeKind.Local).AddTicks(7461) },
                    { new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), new DateTime(2020, 8, 16, 16, 3, 44, 918, DateTimeKind.Local).AddTicks(2786), new DateTime(2021, 1, 15, 17, 2, 17, 939, DateTimeKind.Local).AddTicks(9331) },
                    { new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), new DateTime(2020, 2, 4, 18, 39, 28, 152, DateTimeKind.Local).AddTicks(5328), new DateTime(2021, 1, 15, 21, 37, 5, 968, DateTimeKind.Local).AddTicks(1377) },
                    { new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), new DateTime(2020, 10, 22, 0, 1, 12, 147, DateTimeKind.Local).AddTicks(8367), new DateTime(2021, 1, 16, 6, 19, 21, 288, DateTimeKind.Local).AddTicks(6536) },
                    { new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), new DateTime(2020, 7, 29, 13, 10, 12, 940, DateTimeKind.Local).AddTicks(6130), new DateTime(2021, 1, 15, 21, 14, 44, 504, DateTimeKind.Local).AddTicks(7515) },
                    { new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), new DateTime(2020, 3, 26, 9, 36, 43, 763, DateTimeKind.Local).AddTicks(450), new DateTime(2021, 1, 16, 0, 35, 58, 140, DateTimeKind.Local).AddTicks(1763) },
                    { new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), new DateTime(2020, 6, 21, 6, 44, 53, 78, DateTimeKind.Local).AddTicks(8955), new DateTime(2021, 1, 16, 16, 13, 7, 233, DateTimeKind.Local).AddTicks(5879) },
                    { new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), new DateTime(2020, 2, 8, 6, 38, 42, 111, DateTimeKind.Local).AddTicks(7069), new DateTime(2021, 1, 16, 4, 5, 54, 903, DateTimeKind.Local).AddTicks(6820) },
                    { new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), new DateTime(2020, 7, 12, 16, 14, 49, 964, DateTimeKind.Local).AddTicks(3893), new DateTime(2021, 1, 15, 17, 35, 22, 567, DateTimeKind.Local).AddTicks(4256) },
                    { new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), new DateTime(2020, 2, 27, 0, 2, 22, 379, DateTimeKind.Local).AddTicks(2307), new DateTime(2021, 1, 16, 14, 6, 48, 67, DateTimeKind.Local).AddTicks(7437) },
                    { new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), new DateTime(2020, 3, 5, 18, 35, 50, 327, DateTimeKind.Local).AddTicks(4267), new DateTime(2021, 1, 16, 10, 33, 53, 286, DateTimeKind.Local).AddTicks(2764) },
                    { new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), new DateTime(2020, 10, 4, 8, 34, 53, 115, DateTimeKind.Local).AddTicks(7443), new DateTime(2021, 1, 15, 17, 49, 39, 650, DateTimeKind.Local).AddTicks(9442) },
                    { new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), new DateTime(2020, 10, 4, 7, 7, 33, 337, DateTimeKind.Local).AddTicks(3307), new DateTime(2021, 1, 15, 22, 47, 23, 459, DateTimeKind.Local).AddTicks(8426) },
                    { new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), new DateTime(2020, 9, 5, 6, 43, 58, 286, DateTimeKind.Local).AddTicks(1190), new DateTime(2021, 1, 15, 20, 11, 38, 803, DateTimeKind.Local).AddTicks(1931) },
                    { new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), new DateTime(2020, 6, 27, 10, 33, 1, 208, DateTimeKind.Local).AddTicks(4251), new DateTime(2021, 1, 16, 11, 56, 38, 7, DateTimeKind.Local).AddTicks(3892) },
                    { new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), new DateTime(2020, 11, 23, 15, 57, 43, 736, DateTimeKind.Local).AddTicks(2538), new DateTime(2021, 1, 15, 19, 9, 22, 716, DateTimeKind.Local).AddTicks(98) },
                    { new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), new DateTime(2020, 9, 8, 15, 7, 43, 577, DateTimeKind.Local).AddTicks(6262), new DateTime(2021, 1, 16, 7, 12, 38, 546, DateTimeKind.Local).AddTicks(7297) },
                    { new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), new DateTime(2020, 12, 15, 4, 44, 15, 76, DateTimeKind.Local).AddTicks(4565), new DateTime(2021, 1, 15, 21, 29, 20, 742, DateTimeKind.Local).AddTicks(5235) },
                    { new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), new DateTime(2020, 3, 23, 3, 21, 1, 973, DateTimeKind.Local).AddTicks(5766), new DateTime(2021, 1, 16, 12, 9, 52, 841, DateTimeKind.Local).AddTicks(3329) }
                });

            migrationBuilder.InsertData(
                table: "Genres",
                columns: new[] { "Id", "DateOfRegistration", "LastModifyDate" },
                values: new object[,]
                {
                    { new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), new DateTime(2020, 8, 10, 18, 7, 48, 239, DateTimeKind.Local).AddTicks(3183), new DateTime(2021, 1, 16, 15, 17, 1, 622, DateTimeKind.Local).AddTicks(3787) },
                    { new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), new DateTime(2020, 4, 7, 5, 8, 53, 243, DateTimeKind.Local).AddTicks(2669), new DateTime(2021, 1, 15, 20, 58, 57, 685, DateTimeKind.Local).AddTicks(364) },
                    { new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), new DateTime(2020, 8, 25, 16, 52, 17, 424, DateTimeKind.Local).AddTicks(3670), new DateTime(2021, 1, 15, 22, 26, 0, 399, DateTimeKind.Local).AddTicks(7540) },
                    { new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), new DateTime(2020, 9, 11, 2, 34, 12, 446, DateTimeKind.Local).AddTicks(5144), new DateTime(2021, 1, 16, 2, 44, 6, 314, DateTimeKind.Local).AddTicks(4899) },
                    { new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), new DateTime(2020, 6, 10, 1, 13, 41, 513, DateTimeKind.Local).AddTicks(6522), new DateTime(2021, 1, 16, 6, 55, 32, 621, DateTimeKind.Local).AddTicks(8235) },
                    { new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), new DateTime(2020, 8, 7, 7, 0, 27, 258, DateTimeKind.Local).AddTicks(5006), new DateTime(2021, 1, 16, 12, 59, 19, 428, DateTimeKind.Local).AddTicks(420) },
                    { new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), new DateTime(2020, 4, 25, 16, 25, 59, 726, DateTimeKind.Local).AddTicks(1384), new DateTime(2021, 1, 16, 10, 36, 45, 422, DateTimeKind.Local).AddTicks(4338) },
                    { new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), new DateTime(2020, 7, 11, 17, 23, 24, 122, DateTimeKind.Local).AddTicks(2181), new DateTime(2021, 1, 16, 13, 43, 42, 350, DateTimeKind.Local).AddTicks(3730) },
                    { new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), new DateTime(2020, 5, 16, 18, 20, 53, 719, DateTimeKind.Local).AddTicks(9832), new DateTime(2021, 1, 16, 2, 42, 33, 51, DateTimeKind.Local).AddTicks(4384) },
                    { new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), new DateTime(2021, 1, 3, 20, 55, 33, 831, DateTimeKind.Local).AddTicks(2070), new DateTime(2021, 1, 16, 12, 10, 40, 862, DateTimeKind.Local).AddTicks(3939) },
                    { new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), new DateTime(2020, 3, 24, 9, 17, 16, 721, DateTimeKind.Local).AddTicks(1721), new DateTime(2021, 1, 16, 8, 7, 28, 507, DateTimeKind.Local).AddTicks(4713) },
                    { new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), new DateTime(2020, 1, 31, 13, 17, 16, 132, DateTimeKind.Local).AddTicks(9237), new DateTime(2021, 1, 16, 3, 53, 11, 93, DateTimeKind.Local).AddTicks(3836) },
                    { new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), new DateTime(2020, 11, 1, 18, 16, 49, 601, DateTimeKind.Local).AddTicks(5630), new DateTime(2021, 1, 15, 19, 6, 55, 36, DateTimeKind.Local).AddTicks(3953) },
                    { new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), new DateTime(2020, 10, 19, 19, 9, 14, 73, DateTimeKind.Local).AddTicks(2461), new DateTime(2021, 1, 16, 5, 47, 19, 126, DateTimeKind.Local).AddTicks(3441) },
                    { new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), new DateTime(2020, 7, 6, 20, 20, 59, 891, DateTimeKind.Local).AddTicks(8437), new DateTime(2021, 1, 16, 3, 31, 12, 168, DateTimeKind.Local).AddTicks(452) },
                    { new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), new DateTime(2020, 7, 19, 6, 41, 46, 784, DateTimeKind.Local).AddTicks(2735), new DateTime(2021, 1, 16, 2, 0, 26, 777, DateTimeKind.Local).AddTicks(6311) }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("1c2c5367-7386-4a11-8c75-e81112d0ae6f"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/723.jpg" },
                    { new Guid("7fa731d4-40f4-4fb8-aa98-cb21a56e2ab6"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1187.jpg" },
                    { new Guid("e879d4a1-b95a-452a-8255-facef2331b10"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/965.jpg" },
                    { new Guid("5d9a3f40-4b3f-4812-aed3-2e9633bebb11"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1112.jpg" },
                    { new Guid("c6a05b54-c8d1-4fc4-a8ef-53b177c57dbf"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/608.jpg" },
                    { new Guid("91edd982-597f-4a90-be6c-8ea8b75dcb4d"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/57.jpg" },
                    { new Guid("09df9036-c2c5-4606-be68-fd05f35889a8"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/96.jpg" },
                    { new Guid("dc92c142-731f-44f8-b1b6-91f3d12d5242"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1006.jpg" },
                    { new Guid("93690cc2-6a36-430c-ae05-c2365ebf4a10"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1163.jpg" },
                    { new Guid("354793f0-f856-4f0f-a6b4-887bcdf8a7f1"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/160.jpg" },
                    { new Guid("a3156d99-c1c9-4ab4-8f7e-79caa1464421"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/919.jpg" },
                    { new Guid("df8f6b24-7c89-4f11-9fe7-5214a94ae9c3"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/688.jpg" },
                    { new Guid("ca15dad4-74e5-4be8-b59f-63cef2215946"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/45.jpg" },
                    { new Guid("7fdb7fef-a9b6-4496-ab83-a62a40020cf6"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1113.jpg" },
                    { new Guid("ebad4982-84b4-43ac-9448-8310056ba02b"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/745.jpg" },
                    { new Guid("843e4664-e4d2-4542-b505-25bd440f5da1"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/972.jpg" },
                    { new Guid("87b094ae-8572-4f39-9f72-1778ee57a58c"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/378.jpg" },
                    { new Guid("d5d85eeb-466e-4ff1-b971-dd6007b09596"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/259.jpg" },
                    { new Guid("0070199a-84fd-49c5-aa00-eee599379c50"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/510.jpg" },
                    { new Guid("6f4881ad-67b3-4954-9e04-61ec2de20c4a"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/933.jpg" },
                    { new Guid("65067bd3-f32f-4c6d-ad2e-677c9ba5d90d"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/352.jpg" },
                    { new Guid("d53f2601-a9da-4818-888b-9133f025c165"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1143.jpg" },
                    { new Guid("5e1a50dd-3bf8-400a-9666-3e5f7cbb3827"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1142.jpg" },
                    { new Guid("c82f071b-a11f-4660-9fae-e7946fb55314"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/103.jpg" },
                    { new Guid("3cfff6a7-3927-4028-a5d1-71ae1ad58920"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/924.jpg" },
                    { new Guid("ec9108f7-1d38-4975-b9e5-4ca92b2142a7"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1223.jpg" },
                    { new Guid("1425af6a-6bbb-4460-9dfd-2c7653c4556e"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/324.jpg" },
                    { new Guid("5a7a047a-19d5-46a3-9fc6-b28a82af71b8"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/726.jpg" },
                    { new Guid("19e185c6-ecd5-4a0c-a8cb-964c3d1f8779"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1035.jpg" },
                    { new Guid("d9a9200f-24fa-4e5c-a876-aff868bdd508"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/592.jpg" },
                    { new Guid("2834bcb3-5ce1-426b-b942-d75d2f5632e1"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/681.jpg" },
                    { new Guid("5e4a4ad3-09d3-4bab-ae50-e6ee1e99366a"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/730.jpg" },
                    { new Guid("77fffbb2-59dc-420e-bccf-b5dc7ca99a1f"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/116.jpg" },
                    { new Guid("ffc05182-e9e9-4ab8-9f96-a62d38c68196"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/821.jpg" },
                    { new Guid("4be3a096-f80a-4cd7-bdce-e22c68b8220c"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/641.jpg" },
                    { new Guid("287cd184-1542-40c9-936b-ab842bb89d63"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/697.jpg" },
                    { new Guid("1e7a3352-d8af-49b2-911c-dfdbfaf59b1a"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/833.jpg" },
                    { new Guid("50758b8c-2dbb-4347-92bb-7100f0343a1f"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/701.jpg" },
                    { new Guid("9a10040d-6a09-44d5-aeaf-78a442042d82"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/129.jpg" },
                    { new Guid("86bffb5b-6632-4e37-ac7e-c78ee1b3f20b"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1228.jpg" },
                    { new Guid("7e3d36a3-9ebd-4333-91e0-00de0542a4d5"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/109.jpg" },
                    { new Guid("daec3a6a-85a5-4fc7-9c81-07951f9633e7"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/672.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("ee850973-7475-4f4c-be9d-10148972cd59"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/458.jpg" },
                    { new Guid("70af7ce6-487a-4db2-9ff4-8bcb23d1522b"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/484.jpg" },
                    { new Guid("b78e06e5-9d25-4dcd-b58a-765086574939"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/134.jpg" },
                    { new Guid("b7e2e771-d9bc-46cb-98cb-0de4b5cd8db0"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/893.jpg" },
                    { new Guid("aaece752-f31c-4fa2-b9a5-fe5869084bdd"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/898.jpg" },
                    { new Guid("41f18a98-e4a8-4e61-a089-bd861dda4022"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/561.jpg" },
                    { new Guid("d2088702-929f-4e27-b075-714bce61e4b8"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/150.jpg" },
                    { new Guid("d85ff66c-d9dd-4991-a73c-2f5e746e4136"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/420.jpg" },
                    { new Guid("43af9367-0b5d-4621-bea1-39d870698bfb"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/986.jpg" },
                    { new Guid("eca07202-fc55-49f9-aa93-b7b407571ce8"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/927.jpg" },
                    { new Guid("5a80f558-de7b-478a-8b3f-8f60a20e534a"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/127.jpg" },
                    { new Guid("9972f1b0-747a-4325-afac-90a41aaf110f"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/947.jpg" },
                    { new Guid("f357e0e7-e919-452c-8856-ddf50019a3fc"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1057.jpg" },
                    { new Guid("5892143e-be33-4276-bf69-a1efa655aa44"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1213.jpg" },
                    { new Guid("8847ea37-b99d-4bdd-aa2d-b3055794c049"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/802.jpg" },
                    { new Guid("966323c5-9865-45d5-a997-5afcaf4809f9"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/453.jpg" },
                    { new Guid("64b4595a-cfe3-4b8a-b0d6-e88d0696f87d"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/344.jpg" },
                    { new Guid("180af0ea-626e-4532-be28-53d394db2e33"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/282.jpg" },
                    { new Guid("57f31604-e2ff-40b4-a6d3-ff879acad1a0"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/204.jpg" },
                    { new Guid("58b70f2c-99ae-4166-b2ab-23d849cc2869"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/114.jpg" },
                    { new Guid("610a18e5-90e4-40d3-b4aa-d980d4d68cb5"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/930.jpg" },
                    { new Guid("82cdc493-e529-41f9-a6a8-4fde680f185b"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1054.jpg" },
                    { new Guid("137abc66-f46e-437e-8e66-045d42666d30"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/22.jpg" },
                    { new Guid("e4c88e3c-243f-4c4d-bdf1-831c876e9b58"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/583.jpg" },
                    { new Guid("990e0c78-260e-4adb-b51d-948c9644ede7"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/989.jpg" },
                    { new Guid("4c948192-d784-48fe-b64a-09d17d5d609c"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/532.jpg" },
                    { new Guid("1c559ea5-4242-4b94-88dc-2f02f04b7e01"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/538.jpg" },
                    { new Guid("fa6158d6-0714-46c4-be5a-d2c4ffca9480"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/675.jpg" },
                    { new Guid("31bca01a-1b28-4ea1-9c40-485cdb1ef101"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/916.jpg" },
                    { new Guid("f47f9174-ee4a-4930-9543-80b972c35337"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1163.jpg" },
                    { new Guid("f4dd0ac0-ce30-48a1-9e82-7ed9a662be8a"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/971.jpg" },
                    { new Guid("0495715a-edf6-43c5-b302-b78fa90cb4b5"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/950.jpg" },
                    { new Guid("06189e04-f0dc-4420-bd94-e26efea265a1"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/823.jpg" },
                    { new Guid("448c8a56-e4d7-4cf9-af0d-1d01f18df9f6"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1070.jpg" },
                    { new Guid("445b0a50-2d6c-4ec1-a70b-ae180be55956"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/236.jpg" },
                    { new Guid("7c61b778-9f31-46f7-a604-17a01ef7327e"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1199.jpg" },
                    { new Guid("ed83e2d2-96e3-43de-9bdf-35b661916736"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1171.jpg" },
                    { new Guid("e1f0d701-5cd8-46db-a4da-fd8fa0c60c61"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/734.jpg" },
                    { new Guid("6b93465d-567a-4bfb-a18d-d04102a19c4d"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/734.jpg" },
                    { new Guid("12e0e00d-94ce-41d8-8539-d288542172b8"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1242.jpg" },
                    { new Guid("472d2f12-09d5-4cbf-8787-cb9366a1b39f"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1049.jpg" },
                    { new Guid("4c02f286-3e84-44a1-b3be-3d1905e2b1b3"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1121.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("0f72ca02-5f3a-4b1a-9490-cf9db4faf088"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/403.jpg" },
                    { new Guid("e8774bef-8943-4448-8f53-4181b7282b3e"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/203.jpg" },
                    { new Guid("ffb5fba0-c218-48cb-b640-05ae490ef94b"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/294.jpg" },
                    { new Guid("44a6b9a9-c181-4c9e-9293-27eaf643e3e4"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/515.jpg" },
                    { new Guid("cf62e772-b78b-4a5c-9666-621fb2191049"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/590.jpg" },
                    { new Guid("06e8e6d6-d338-4a80-9c3f-ade8e1ee2cb7"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/809.jpg" },
                    { new Guid("8d7634d8-a345-4edc-a829-3526de31a599"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/33.jpg" },
                    { new Guid("6cfd7969-17d1-48aa-85b9-cc4267551147"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/12.jpg" },
                    { new Guid("85b72edf-dd33-4611-a603-a9fc0a4c05e8"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/521.jpg" },
                    { new Guid("1931bad6-db5c-42c7-aa00-9d2b465a48b6"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1172.jpg" },
                    { new Guid("bd35b1f0-227e-4ecb-a38c-829a6cdc153a"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/525.jpg" },
                    { new Guid("b3faf711-122f-44d7-8942-9e7459fec7b1"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1108.jpg" },
                    { new Guid("4b24e178-8627-4e8f-9736-4a2d63498560"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/152.jpg" },
                    { new Guid("11d83e94-dac6-4024-94c8-c690b1761bb8"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1195.jpg" },
                    { new Guid("5b1b7b1d-fe89-4798-9b46-40b3df0c7453"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/549.jpg" },
                    { new Guid("9a31fcd8-0ab2-48b6-a776-b56f1c20e39c"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/873.jpg" },
                    { new Guid("776e6e67-de2d-4b32-95f5-ed829c492bdd"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/218.jpg" },
                    { new Guid("1f8ac13c-0e21-4361-ada6-2bd1b71538aa"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/133.jpg" },
                    { new Guid("7abded16-e016-4811-931a-b0880bd3c7e2"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/125.jpg" },
                    { new Guid("ef272dcc-0b7a-452c-a34d-572b2bb1f335"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/958.jpg" },
                    { new Guid("1850ee85-e68c-4985-b16e-480d758316c2"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1172.jpg" },
                    { new Guid("237f4b89-894d-430b-9e26-36d1632e0a9f"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/371.jpg" },
                    { new Guid("9b28745c-ae4b-4487-8203-2c790232c492"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/655.jpg" },
                    { new Guid("36c18ae4-a272-417e-9ca9-209f85f9406b"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/820.jpg" },
                    { new Guid("3f8e8aec-84b5-4b0d-86ca-aecb88610921"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/624.jpg" },
                    { new Guid("ba5d1a7c-a270-4a4c-b3cb-2dcc3ab883e1"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/95.jpg" },
                    { new Guid("79df1011-5e2e-4568-8e83-6cb404aebbcf"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/330.jpg" },
                    { new Guid("cb252c8d-9f56-48db-9162-5f85a5a8b126"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/129.jpg" },
                    { new Guid("ea7221a6-e536-446f-a63c-f52e709bf653"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1158.jpg" },
                    { new Guid("b3846cbc-d849-4a18-bc51-8be57de82188"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/551.jpg" },
                    { new Guid("adb66b1c-e4f2-4c56-9865-c5010f5647c1"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/559.jpg" },
                    { new Guid("2d88cad5-9c0f-4123-a940-88b25b6eca63"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1054.jpg" },
                    { new Guid("771a2170-47ab-4088-a119-db9fad518cbf"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/90.jpg" },
                    { new Guid("d3fa9b5a-af4e-415c-81c7-0e00e4451632"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/51.jpg" },
                    { new Guid("bd51a813-e3f4-4e21-9459-b081fb76f2ac"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/221.jpg" },
                    { new Guid("5449ba25-f040-44fc-bafa-2eecd64aa042"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/494.jpg" },
                    { new Guid("9a5ec80e-0d49-4a6c-92b2-c60fa545f6ec"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/20.jpg" },
                    { new Guid("42d8c58f-c757-4da0-bbe5-3c3c29478a2a"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/777.jpg" },
                    { new Guid("c6dec06e-b163-4e21-8ae1-a189e08f240b"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/351.jpg" },
                    { new Guid("6cb7d74b-e63b-411a-a18c-394c307a2a90"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/456.jpg" },
                    { new Guid("88992bda-3318-46cb-b28b-ae03ed381ff8"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/57.jpg" },
                    { new Guid("d40649ff-1ee6-45e0-91d7-509b387db3df"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/808.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("ca033a5b-f165-4949-aef6-15a98413e52c"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/870.jpg" },
                    { new Guid("6957448e-3c06-4107-addd-1f7f3361d6d2"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1042.jpg" },
                    { new Guid("c7899d41-f59f-4320-9d7c-f5ec20c3415e"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/782.jpg" },
                    { new Guid("93c17adc-660a-43c0-9581-83b5656be54f"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/489.jpg" },
                    { new Guid("2ef8f0b3-3305-414a-ba16-7078966e8fb4"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/93.jpg" },
                    { new Guid("7db34d80-137d-4434-b429-1cee310a9f21"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/852.jpg" },
                    { new Guid("7066c0cf-0c02-4a14-8d13-c6c21889653b"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/896.jpg" },
                    { new Guid("2a8fdfeb-a882-4b7c-9254-87e5cfecef66"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/852.jpg" },
                    { new Guid("1d6b3fea-5b42-4ffd-817e-4187c3131c89"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/432.jpg" },
                    { new Guid("bd3462a2-479b-4cbe-9a81-43ef965a7c1c"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1226.jpg" },
                    { new Guid("68598641-65c7-4375-87b8-be2a770dd3bd"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/244.jpg" },
                    { new Guid("babc00a0-e0e2-43c1-9d44-86c1b7b12d18"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1229.jpg" },
                    { new Guid("9ddff1de-01c6-474f-af7e-3666ed870ade"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1037.jpg" },
                    { new Guid("a5df7101-f366-4096-907a-6e41a6f0d9f5"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/904.jpg" },
                    { new Guid("22612c04-e667-4df4-861a-53ecc1369385"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/926.jpg" },
                    { new Guid("be032c58-1c8b-4e05-a98d-00564c7e5cda"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/284.jpg" },
                    { new Guid("e70467bb-10c0-42d4-a3d1-6ca277041a8c"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/73.jpg" },
                    { new Guid("84172717-642f-4fb0-bf02-75b952781b88"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/836.jpg" },
                    { new Guid("357fb80b-1083-4bad-bc3b-79a137fe02cd"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1171.jpg" },
                    { new Guid("e66441e6-8cf2-4200-93e6-89b767164676"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/553.jpg" },
                    { new Guid("5a816a68-67f0-42e3-911f-a7fe85fa0d7a"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/159.jpg" },
                    { new Guid("1ea25386-79cd-4ca8-89ad-3fedeb10443b"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/622.jpg" },
                    { new Guid("89983192-6c5a-4847-abc5-5b798ee677a7"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1240.jpg" },
                    { new Guid("6a4ee8ce-d0d7-469a-b81f-0fc1f84b8955"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/181.jpg" },
                    { new Guid("40c032d5-ebb4-4a4c-a4f0-bfcdb033d782"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1186.jpg" },
                    { new Guid("996d85d3-e604-483b-84bd-afa85b9a145e"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/479.jpg" },
                    { new Guid("55ed649a-a50f-4e5b-a74a-60ad5a954376"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/820.jpg" },
                    { new Guid("2cdbefe3-5987-441f-8f30-556c600fda8a"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/627.jpg" },
                    { new Guid("1d7598ae-9c02-4cbd-b9fd-e4cfb01eebb3"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/991.jpg" },
                    { new Guid("8d031135-6986-4d86-8669-24b537684018"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/708.jpg" },
                    { new Guid("4b1c6887-2a46-4b34-aec9-72fb6c227249"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/929.jpg" },
                    { new Guid("8bf5e280-2f41-43ff-8b67-b2c361730171"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/877.jpg" },
                    { new Guid("8a165ca4-0572-40a3-8a42-d392adbf845e"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/908.jpg" },
                    { new Guid("fc1cdf80-52a8-46c2-a1af-cfc04b1bd2e9"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1202.jpg" },
                    { new Guid("e6a7698b-f0fe-4739-b118-33873ccf53cb"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/242.jpg" },
                    { new Guid("82818b0c-81f7-48ec-b5cc-1936e6c82e87"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/133.jpg" },
                    { new Guid("a82c93b7-0e30-49ed-a38a-58cea4f65163"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/930.jpg" },
                    { new Guid("743835eb-7291-4e6c-b7a3-e79b8b3f411a"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/192.jpg" },
                    { new Guid("38c1daae-7bdd-433e-9d65-574d167be64b"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/508.jpg" },
                    { new Guid("03c35e04-538f-47b0-8a53-e50878929a4f"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/633.jpg" },
                    { new Guid("ce13294e-79de-4159-8e5d-75acb7f4b6a7"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/683.jpg" },
                    { new Guid("afc9644d-e9bc-4db8-bc0c-f8f77fc00964"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/408.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("6eab5548-6d52-4981-863d-56221a356ed4"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/95.jpg" },
                    { new Guid("f73c151b-dd77-4131-8d8d-fae62866552d"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1147.jpg" },
                    { new Guid("91a2cd6e-0a04-4154-b422-0fee12d7c7df"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/91.jpg" },
                    { new Guid("b2f5cb15-fa36-45e9-84ba-5e1f26c7f810"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/510.jpg" },
                    { new Guid("2ea0a779-752b-4034-93a5-01b93fe408da"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/341.jpg" },
                    { new Guid("87bb05e3-3942-4bab-a15f-1c950a3c789a"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/58.jpg" },
                    { new Guid("7d722ac2-a4b2-41d1-9977-a6e207f9e3f3"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/58.jpg" },
                    { new Guid("1d090c93-90ac-4897-94bf-63f7d07b5b62"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/513.jpg" },
                    { new Guid("4c2cabbc-ef31-4c0a-8b84-7c226b1063a2"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/441.jpg" },
                    { new Guid("e28d7bba-e733-43ee-8364-7ec8b889ae84"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/148.jpg" },
                    { new Guid("b57518b6-ba18-458b-8771-80e374468af3"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/632.jpg" },
                    { new Guid("e5627876-e7e2-4a9b-b0dd-cf885056afa1"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/651.jpg" },
                    { new Guid("5a6b8f0b-1029-4000-99b6-0623ff16220a"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/279.jpg" },
                    { new Guid("d650603a-02cd-4049-8d9b-fe47f85aec62"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/649.jpg" },
                    { new Guid("f9bd82e0-18be-4cd9-ae7f-23afad841046"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/263.jpg" },
                    { new Guid("b220fe2c-fad5-4e99-b70b-004d091e5942"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/902.jpg" },
                    { new Guid("c5f77e56-5640-45ed-877f-5bdb9ac76f28"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/448.jpg" },
                    { new Guid("6f33ab83-d00e-46fb-bcb2-27c017e7c6cb"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/814.jpg" },
                    { new Guid("82a15dbb-ab3c-4ff9-996b-402750cf2011"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/28.jpg" },
                    { new Guid("9de04b1c-8ca5-4b63-97ea-40a7ab3b412c"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/508.jpg" },
                    { new Guid("42aa7537-4d6a-414e-af98-a743d30851ae"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/775.jpg" },
                    { new Guid("be238ea6-4771-41d8-9f9d-581f44ac9cd4"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/465.jpg" },
                    { new Guid("14051791-7c3d-49fa-988f-04d1bb80c90a"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/295.jpg" },
                    { new Guid("7124b83a-3383-4f5b-bd92-865073ca8492"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/781.jpg" },
                    { new Guid("e3d8d467-2924-43d7-8aab-221136a4de28"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/952.jpg" },
                    { new Guid("33ad8c53-5f60-49e0-8ac9-54073c3aa6e9"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/699.jpg" },
                    { new Guid("fd698a47-b84b-4b1f-8306-ea114c3e6c72"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/125.jpg" },
                    { new Guid("730f65bd-a5ab-4a21-80c3-20484eb75e4a"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/70.jpg" },
                    { new Guid("484400e3-c3c5-46ac-887b-fb097d901511"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/441.jpg" },
                    { new Guid("b7567b19-5003-4722-9700-2a63c4997d7a"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/884.jpg" },
                    { new Guid("01b5bfd5-c232-4963-9294-a5194a9dfd5e"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/377.jpg" },
                    { new Guid("4b5552b6-db06-4ab9-b4b2-f299172a4d5a"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/996.jpg" },
                    { new Guid("6b7f728d-8cf9-4e80-83ac-7677f140a643"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/911.jpg" },
                    { new Guid("7c6344e7-1b6b-4046-817c-be52b5d65cd4"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/177.jpg" },
                    { new Guid("ddf7686e-7ade-4cf0-bee0-c4ac54dc946f"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/875.jpg" },
                    { new Guid("93329b8b-c0e8-444e-87ee-e9ba2bdc0974"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1028.jpg" },
                    { new Guid("490b826b-64a7-4fea-b3f6-c83ad8e5ec9e"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/962.jpg" },
                    { new Guid("ff857f4a-fac7-4f08-adb8-e141107acce7"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/623.jpg" },
                    { new Guid("99c56b94-b4a4-452b-89ef-e6a9e207af27"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/458.jpg" },
                    { new Guid("752a9501-df82-4096-8aa8-1ccff4a025cd"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/408.jpg" },
                    { new Guid("ca6a344a-542c-4935-a8b9-fe86198ee68d"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/317.jpg" },
                    { new Guid("1aba7212-da93-4794-840b-248a5c6450c6"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/705.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("805e2e21-27f3-4755-b796-0ccb95ae3b0c"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1181.jpg" },
                    { new Guid("66f025e8-b139-48fc-b083-050c5d9ef22e"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/421.jpg" },
                    { new Guid("433c0d98-1817-47f1-be0d-28a9fa3153d6"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1168.jpg" },
                    { new Guid("7c8cfb9a-caf1-40d6-827a-e7c3c5bcf08e"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/80.jpg" },
                    { new Guid("46ceca8e-0b53-4e27-b87b-a2d6e78582ed"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/801.jpg" },
                    { new Guid("f8c38947-d592-4199-880a-a0bef1e47356"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/235.jpg" },
                    { new Guid("52ac35d5-50a5-4bd3-85d1-fad61e1de5b6"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/548.jpg" },
                    { new Guid("c6d6217e-613f-42fc-a1cd-be3beaa26063"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/798.jpg" },
                    { new Guid("c6b96550-92b8-46cc-bac3-ebb0584180c5"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1094.jpg" },
                    { new Guid("21b3fac8-f4e5-489f-ac3a-959e28627654"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/139.jpg" },
                    { new Guid("277ff4d9-8c62-4c95-9194-768e6ad24c83"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/760.jpg" },
                    { new Guid("6255edf6-6e3c-4c68-91e3-9aaaaab0440f"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/791.jpg" },
                    { new Guid("9e947147-5fbf-43fa-930a-9f8da0b107e3"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1059.jpg" },
                    { new Guid("cf36c997-79fb-4b62-98c5-5d8939ad9a4c"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/513.jpg" },
                    { new Guid("1e9d2cd2-c7e4-4a0f-ae24-2764b15feb56"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1020.jpg" },
                    { new Guid("5c25e3d7-a5ad-488e-95f2-23bce0982375"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/572.jpg" },
                    { new Guid("67dd7a32-3be2-4b29-ad3b-97d3e7cb7204"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/333.jpg" },
                    { new Guid("827505bf-9b40-47a7-8ea9-4483b4be38ec"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1027.jpg" },
                    { new Guid("56125ed0-3016-4dcc-97e9-197bb30fd3b5"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/29.jpg" },
                    { new Guid("e8911df9-f745-4bd2-ac04-1ee3a6fbeca5"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/813.jpg" },
                    { new Guid("8957c71d-a4f0-43f4-8cd8-b891dbc846e1"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/157.jpg" },
                    { new Guid("3c75d410-9e5c-49d9-b6af-bd04e5088022"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/785.jpg" },
                    { new Guid("db15d5aa-bc68-4e88-933d-c12116b87519"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/720.jpg" },
                    { new Guid("cb8b0339-04e0-45d2-87cb-5b9f6fee40d5"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/477.jpg" },
                    { new Guid("a73c90db-9cef-4c86-b91c-d5f7a897def2"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/665.jpg" },
                    { new Guid("7690f4c1-d5bd-4f4f-9d4d-bdd073d04295"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/182.jpg" },
                    { new Guid("71d31b97-cdd2-4c4a-adf6-3e0c4400985b"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/891.jpg" },
                    { new Guid("91182264-51cb-48c7-a1e1-ef0df7d235b7"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/396.jpg" },
                    { new Guid("e6f9e743-5977-4a06-b74f-a3b56352288f"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/855.jpg" },
                    { new Guid("9a51a784-ddbc-4050-890d-e6b8484645f3"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/100.jpg" },
                    { new Guid("c29ff8bb-9742-4139-bc95-3999d6638070"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/651.jpg" },
                    { new Guid("cd6dc00c-c74d-4230-ad67-a90a54fb94f2"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/858.jpg" },
                    { new Guid("8334b630-1056-4222-876a-7eb17f1c3f4f"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1074.jpg" },
                    { new Guid("ae6cf6f0-547b-46a2-aabf-fb24085ecb7a"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/204.jpg" },
                    { new Guid("e921b9a0-e3d3-4e51-9b66-a054f332a0c6"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/283.jpg" },
                    { new Guid("7016346e-0027-4430-b813-840beebde08f"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/623.jpg" },
                    { new Guid("657eca24-6a35-4e8c-8a1a-5055445c47ec"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/507.jpg" },
                    { new Guid("200e28d8-640d-4142-bc08-8b89470a8aae"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/716.jpg" },
                    { new Guid("bd8f48a9-2dff-4f73-8197-1f02dab1e7a6"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/394.jpg" },
                    { new Guid("3511a1b6-909f-4808-8610-88b46a722c6e"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/153.jpg" },
                    { new Guid("d017c220-7c8e-4e8b-8b3d-18eeeecc3717"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1072.jpg" },
                    { new Guid("9bc26840-ae73-4025-bca5-83152e9ebd56"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/712.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("64a172b5-266b-43eb-b820-43a21066d3c3"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/164.jpg" },
                    { new Guid("68005c6f-d842-4a6e-843f-8daa376d9094"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/503.jpg" },
                    { new Guid("ebb29d88-c59f-4603-85cf-78f5cbfd78c7"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/462.jpg" },
                    { new Guid("681119d0-1f10-4047-ab06-5b8c16a1d474"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/237.jpg" },
                    { new Guid("efc7d6e3-16f8-42b4-a5ec-5bf5030b2a7d"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/700.jpg" },
                    { new Guid("d78c72b0-f39a-4f69-a1c7-30994111d94f"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/84.jpg" },
                    { new Guid("40a2f44f-38f5-4fca-bef1-2a78870947fb"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1194.jpg" },
                    { new Guid("03e7778e-3e6f-4a53-8650-1cd6b7940fe9"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/220.jpg" },
                    { new Guid("77890b37-6368-404c-b314-fc65f9d16333"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/801.jpg" },
                    { new Guid("320ed17d-b6ce-4666-bb08-ac39e5344048"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1139.jpg" },
                    { new Guid("0aa38c7c-1152-4d26-8427-388a4674c47a"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/593.jpg" },
                    { new Guid("76dc7f7b-1d0b-48d3-9b98-a244f986f917"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/949.jpg" },
                    { new Guid("10e4f707-780e-43ce-8c8a-3afa6861552b"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1138.jpg" },
                    { new Guid("326465c5-a535-41fd-99bd-ea8364b73896"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/261.jpg" },
                    { new Guid("254dc7c6-ee50-41a3-abdb-83b96f0e02a9"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/255.jpg" },
                    { new Guid("7ccd0d81-d9ed-4b4a-b14f-5da80b37c6c1"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/449.jpg" },
                    { new Guid("db7c6f38-9049-4213-a42c-8e7a7fc9df18"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1126.jpg" },
                    { new Guid("d1861ecf-899c-4d55-bdf4-390117a3e69b"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/771.jpg" },
                    { new Guid("b41fd07d-7b7f-486f-a149-65f1c49fce2b"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/527.jpg" },
                    { new Guid("38ea1673-d56c-485b-8bb4-378258dbecbb"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/331.jpg" },
                    { new Guid("a6969c28-f358-4259-a6d1-453dc195d74a"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/215.jpg" },
                    { new Guid("5f2263b7-7fc3-4865-90a0-1e740bf69d64"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/535.jpg" },
                    { new Guid("f5227e4e-7e75-44c5-bc81-b7167376bcd9"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/677.jpg" },
                    { new Guid("e195efe6-15c6-481c-8a89-76a5f10e47fe"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/619.jpg" },
                    { new Guid("90b2a95e-f1d1-40ac-9198-f28e254e113b"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1059.jpg" },
                    { new Guid("5783b8ea-afd4-4d04-9d03-0319fbe30617"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/464.jpg" },
                    { new Guid("a24c15ba-a0ca-42a0-93a4-36fc537249f5"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/38.jpg" },
                    { new Guid("1c7949a4-7345-4aeb-bacd-a05edf5dd245"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/199.jpg" },
                    { new Guid("be2514f8-c53c-426d-a953-adb61e2a6cbd"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/579.jpg" },
                    { new Guid("7441c263-0816-42cd-9598-263f2c3a2bf0"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/298.jpg" },
                    { new Guid("312cb942-3157-45e7-8744-1f7edb87981a"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/534.jpg" },
                    { new Guid("2d7c3525-30d9-4a54-b294-0ecf3b74f3d3"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/740.jpg" },
                    { new Guid("759366a5-9cf5-42a6-a126-d8635af9fc86"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/391.jpg" },
                    { new Guid("f937a9a2-04dc-4a11-8645-5359df8b8b85"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/549.jpg" },
                    { new Guid("32fdffed-a8a6-4368-9125-477b3f147b6c"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/827.jpg" },
                    { new Guid("3f042aa1-a16b-4049-9741-ee22b8d7df8b"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/806.jpg" },
                    { new Guid("065a7938-477c-407b-a7bc-3dd7ea2fb2f6"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1103.jpg" },
                    { new Guid("5d22b8f4-98d3-49b9-bcdf-8eb2320be6a5"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/611.jpg" },
                    { new Guid("a093bf3d-282d-4df8-a14f-2882f30a9e8b"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/21.jpg" },
                    { new Guid("501a76ab-141e-4088-b6fc-9dc2ad111749"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/819.jpg" },
                    { new Guid("b897e185-4bb3-4ac7-aee6-36934e5f6562"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/41.jpg" },
                    { new Guid("30605a3d-2cff-4778-a1d2-2f1d99f4ed2d"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/368.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("5e7c2499-6a2e-41e9-8cec-1217e374e4d4"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/243.jpg" },
                    { new Guid("a8b9097d-0315-482c-ac5a-a73e112a0e4e"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1188.jpg" },
                    { new Guid("32cf5ef4-0619-4ffc-9ce2-15111047e1e5"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/930.jpg" },
                    { new Guid("82fd6003-f659-4052-bfad-e01ce5f6e01f"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/955.jpg" },
                    { new Guid("b3258878-6588-4f89-b114-3461578e0b1f"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/626.jpg" },
                    { new Guid("87688abe-52bd-4ac1-b13d-26e40d3edfaf"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/878.jpg" },
                    { new Guid("35511423-6b41-488f-bd6c-64554c53d636"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/753.jpg" },
                    { new Guid("d3782a7a-4f1c-4321-8e2c-f5f18dd3fb8a"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/60.jpg" },
                    { new Guid("12dd0f7c-e80c-402a-8586-6e1a8c303636"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/98.jpg" },
                    { new Guid("d7ed2dce-472e-4a51-ac6b-7da9219feb27"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/849.jpg" },
                    { new Guid("aac9aad1-966c-465c-b03b-a67cff59f7da"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/705.jpg" },
                    { new Guid("270ebbb5-5f9d-403c-aa2f-34ebc04c397f"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/408.jpg" },
                    { new Guid("7e450ce1-4897-46a5-b33d-bc4e9ed5fe5f"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/551.jpg" },
                    { new Guid("a00adfa8-af2a-4928-99c4-30dd74cc420a"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/282.jpg" },
                    { new Guid("21ad9ce5-54bd-4bf1-a96d-9005ae545cbc"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/713.jpg" },
                    { new Guid("56db4b6b-64d2-45f2-ae77-e31c57adf9b0"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/541.jpg" },
                    { new Guid("b2ade231-7a68-4894-b19f-29622377f751"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/746.jpg" },
                    { new Guid("353c897f-2e2d-4b19-90b5-1845f0b5ec89"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/244.jpg" },
                    { new Guid("fb6f001d-50cc-4e7c-bda6-d20cedc385b2"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/809.jpg" },
                    { new Guid("fb546ff0-7f02-424e-8e43-8f40e09b02b1"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/229.jpg" },
                    { new Guid("c53b5da9-fa6f-4a2d-a71f-3a320eb227de"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1080.jpg" },
                    { new Guid("c9be0e78-a912-489b-9923-4b530170937f"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/508.jpg" },
                    { new Guid("65752683-6132-41d7-a87e-8de87f844f2d"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/540.jpg" },
                    { new Guid("3d62df8d-e1e1-4731-b570-11a41a290f45"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1248.jpg" },
                    { new Guid("bed76ca7-07a2-4852-a57c-479fa2e77d5f"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/44.jpg" },
                    { new Guid("e5c22571-c404-4abc-8f19-cf5de35e584a"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/794.jpg" },
                    { new Guid("03197912-bb97-40c9-8d7c-14b1569e22da"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1145.jpg" },
                    { new Guid("d712ade5-9034-442d-9986-fbe48f19cf75"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1170.jpg" },
                    { new Guid("afd5fffb-b8c4-45af-998e-bb0d90265fa9"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1233.jpg" },
                    { new Guid("db715d9a-dec6-400d-8b80-76539e62784d"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/928.jpg" },
                    { new Guid("24985e6c-bc53-4899-957e-9468545b16b1"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1124.jpg" },
                    { new Guid("eede48d9-aaff-4ba6-a49d-1c30b2f65ac6"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/793.jpg" },
                    { new Guid("19a93796-603a-4d72-9360-586d19f03060"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/536.jpg" },
                    { new Guid("8f6c6a62-f145-448c-a886-790c12467ac4"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/57.jpg" },
                    { new Guid("0a8ca56b-f1b6-4108-b3dc-2e4047c5cfe8"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1030.jpg" },
                    { new Guid("fd54027a-a238-4ee6-a86a-ad43f8853a0e"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1208.jpg" },
                    { new Guid("2d7b5f42-dcd6-4813-9d7e-62471f595a58"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1115.jpg" },
                    { new Guid("954c83c5-a7b3-4865-9ae9-fe0ef169fb86"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/618.jpg" },
                    { new Guid("f9ce5cca-a568-4bbc-b2be-e9800df94f18"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1093.jpg" },
                    { new Guid("db52927a-6a37-4c58-9802-b89767a3a9b6"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/579.jpg" },
                    { new Guid("8f5776e5-6606-46a1-a18a-86103043a6cc"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/368.jpg" },
                    { new Guid("551fad43-022b-4f11-b430-b549e4ea8ac0"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/930.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("feff44a9-51fd-4540-be0a-2713b67f661b"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/71.jpg" },
                    { new Guid("c12ba6c2-5da5-4edb-a28d-0a2fb4867798"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/534.jpg" },
                    { new Guid("01fccc2b-1a5c-48b6-9575-a03c03157f36"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/540.jpg" },
                    { new Guid("73ec8e35-8b10-43aa-854d-007f786fef8c"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/256.jpg" },
                    { new Guid("1ec67d3a-75ea-4274-a39f-cbf862727722"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/934.jpg" },
                    { new Guid("8d940d63-cfd1-4de9-8ef9-939a1d118f94"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1184.jpg" },
                    { new Guid("c8a44de2-352a-4517-8b06-734002efecd0"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/504.jpg" },
                    { new Guid("ffc5926f-611e-4ec5-a6a3-d6ce16ce462c"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/871.jpg" },
                    { new Guid("cd4fa784-f3a2-4f46-9e20-556144116e0e"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/369.jpg" },
                    { new Guid("7ae061ea-230d-4d66-b9b1-0941e2c70ab7"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1222.jpg" },
                    { new Guid("b8c97946-b0db-48a3-9fcd-a2dfb80e0d4b"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1116.jpg" },
                    { new Guid("4a06e3f9-2620-4fa3-a1be-55b69839e5c8"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/725.jpg" },
                    { new Guid("f2823fb4-a1ee-4ec6-8ff4-a6ae658d8172"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/880.jpg" },
                    { new Guid("00564296-cd9d-40f4-92ae-e496747031fc"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/568.jpg" },
                    { new Guid("217b9caf-3631-4e3a-bcdc-a46150ff8d00"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1123.jpg" },
                    { new Guid("79ce5d9a-c648-4816-aa11-54e677b66c84"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/91.jpg" },
                    { new Guid("f16e384c-6fa9-4841-8282-c122513ca057"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1185.jpg" },
                    { new Guid("464f7a2e-5560-401f-85da-340d426d6cda"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/960.jpg" },
                    { new Guid("79dc2802-d0d1-4bdf-a07e-2dd31fbe54e0"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/386.jpg" },
                    { new Guid("9911cc38-634e-4176-9330-9c7d6d657cb6"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/194.jpg" },
                    { new Guid("f266257f-d114-4859-8a64-6fd07020d4e2"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/500.jpg" },
                    { new Guid("b246787c-f0b5-4d59-b688-815809601da8"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/752.jpg" },
                    { new Guid("3f9a3aa6-eac7-4cfb-bc59-e4490c267829"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1143.jpg" },
                    { new Guid("991e506e-325a-4b17-bf51-65e7a6821e8f"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/0.jpg" },
                    { new Guid("11a50739-e35e-4e4c-b383-b3b2f2e52e0a"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/867.jpg" },
                    { new Guid("173ddfd6-5473-4400-909a-8ab860842133"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1091.jpg" },
                    { new Guid("3aa48258-9d88-4574-b554-533bb2dd9225"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/137.jpg" },
                    { new Guid("e73854ee-053f-4ec2-947b-bc55cb7f3b56"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1074.jpg" },
                    { new Guid("b8ae44f6-8456-4209-8ec0-275daf0894f7"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1156.jpg" },
                    { new Guid("67dd5473-d221-4709-b9c0-d73b75cab03f"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/723.jpg" },
                    { new Guid("ca9317fb-9532-42bc-97c3-e35f492b8c66"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/116.jpg" },
                    { new Guid("c7bdd080-3724-47f6-b453-93a4a4faaa94"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/524.jpg" },
                    { new Guid("98517c3d-7449-4f78-a365-6e0f184c77aa"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/16.jpg" },
                    { new Guid("622c4ce7-196a-4ed2-99a2-300a9499b2f7"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/269.jpg" },
                    { new Guid("7e650cba-3dcd-43aa-9e19-82b21d95cb71"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/167.jpg" },
                    { new Guid("672e6a6c-b15d-44e1-9556-812ad4f53c0c"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1009.jpg" },
                    { new Guid("864f213d-2591-4461-831b-19a717cc8075"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/352.jpg" },
                    { new Guid("07288ec3-5d76-4b22-900e-78e9e5bf9b0d"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1019.jpg" },
                    { new Guid("46695c95-839e-4dc9-93e6-adc37500db9e"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/605.jpg" },
                    { new Guid("10ba6486-c01d-4c32-8074-752c3b1795e7"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 5, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/608.jpg" },
                    { new Guid("0854ff0a-b4e5-40df-b5ae-faeaf83c988a"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/607.jpg" },
                    { new Guid("ef1c5662-da02-4343-8256-e5a96bf09613"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/645.jpg" }
                });

            migrationBuilder.InsertData(
                table: "Images",
                columns: new[] { "Id", "Discriminator", "GenreId", "ImageSize", "Name" },
                values: new object[,]
                {
                    { new Guid("8b30d248-8efb-4d32-b049-9b31e9361564"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/344.jpg" },
                    { new Guid("e50b2191-95b8-464b-b465-6a1e33a756bb"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/560.jpg" },
                    { new Guid("e852bc35-1f77-418e-a31c-c1be69c76466"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/249.jpg" },
                    { new Guid("71858208-ba6b-44ed-a722-0adff178e912"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1232.jpg" },
                    { new Guid("b0fc18a3-bb8e-48cb-a4bc-327997f5a4bb"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/930.jpg" },
                    { new Guid("51ec2884-5a9d-4c57-a02c-e763304df68a"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1241.jpg" },
                    { new Guid("db1ffd14-2c96-4c68-aee2-bf3eb711048c"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1127.jpg" },
                    { new Guid("2e6df040-29ac-4256-a11f-f37d5e0c08bb"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/267.jpg" },
                    { new Guid("1f986926-bf96-4d8c-b48e-8d45e4afa233"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/113.jpg" },
                    { new Guid("7287509e-ee16-4a86-8df6-f78a733ad6cb"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/988.jpg" },
                    { new Guid("8b061034-16ae-45b4-acd0-455ff4ba7b2a"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/286.jpg" },
                    { new Guid("92c30baf-03ff-4da3-8c69-304e3662a6d6"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/524.jpg" },
                    { new Guid("11f4ab65-789b-40e4-b5cf-ed274667a81c"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/864.jpg" },
                    { new Guid("1e3629a3-8f48-480b-b5e1-437df36b1b14"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 3, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1102.jpg" },
                    { new Guid("8b11bfac-abb4-4cbc-8593-04c76bf36e48"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/36.jpg" },
                    { new Guid("04b1d234-d48a-43b0-be03-3d12ba9d88cb"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/779.jpg" },
                    { new Guid("3e795ece-b1a2-4f24-ab40-691c04381608"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 0, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/230.jpg" },
                    { new Guid("a3c0ffde-7336-4405-9ca9-ee103027a055"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/666.jpg" },
                    { new Guid("df7947bb-3e13-4159-a558-c70d6ee106fa"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 2, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/812.jpg" },
                    { new Guid("75de3e33-56c4-4f74-845b-48999e341a7b"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1051.jpg" },
                    { new Guid("54dd828e-0d76-4b9c-8cb2-6ed0dec040f4"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 1, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/1045.jpg" },
                    { new Guid("6a2e23db-d69a-4582-a59b-a857df665019"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 4, "https://cloudflare-ipfs.com/ipfs/Qmd3W5DuhgHirLHGVixi6V76LhCkZUz6pnFt5AJBiyvHye/avatar/672.jpg" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("811363ed-da27-4e4f-a595-eb9229834836"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 2, "Folk" },
                    { new Guid("f04c7a72-ea1d-412c-af4f-682207e5dfd4"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 0, "Reggae" },
                    { new Guid("1940199c-ed53-4cd2-b93d-af0b31723192"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 1, "World" },
                    { new Guid("b43f9244-4981-4fbe-931d-daa81a8003ba"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 1, "Hip Hop" },
                    { new Guid("f06a85d6-2ff4-403d-90b9-1028ef8f33a7"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 1, "Non Music" },
                    { new Guid("2e27c55e-2e8d-4258-ad26-d195c2ba6b06"), 2, new Guid("7e26eeb4-ba54-4991-bb0d-1397466d5852"), 0, "Rap" },
                    { new Guid("49bd08a5-b36e-4a45-be94-d337c8a9ddfe"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 1, "Latin" },
                    { new Guid("d86dc9ed-ec2a-46a1-bbe8-cef5df774d83"), 2, new Guid("ad1c2e73-8f21-4392-8fb6-74660d296489"), 1, "Non Music" },
                    { new Guid("ba159964-a01c-41d6-b7bc-62b7e82f71f9"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 1, "Non Music" },
                    { new Guid("04b342bf-d422-4e90-a97c-ab836006ea8f"), 2, new Guid("2b0c3f5f-a219-4d7b-93c5-7d83f9c57ae7"), 1, "Non Music" },
                    { new Guid("6d1ad8c4-8a23-484d-9553-57382068fcff"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 0, "Folk" },
                    { new Guid("88a5d8d5-cf92-4562-9733-547e80b02d4d"), 2, new Guid("c54b6b98-3616-42ba-b618-ebf8fdea7369"), 1, "World" },
                    { new Guid("641d1470-55a8-4cf1-9cb7-a35cde13020e"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 2, "Folk" },
                    { new Guid("9b5efdbd-b9b1-4f14-a94b-6dd105d22ab1"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 2, "Electronic" },
                    { new Guid("eac71640-46dd-4eac-a444-84f9c29d121a"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 0, "Funk" },
                    { new Guid("a47c3dcf-2f39-46c4-9948-e22fae4c4ac6"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 2, "Blues" },
                    { new Guid("2ca39cde-9378-4ade-ac70-48e39d7329cd"), 2, new Guid("a4dc5831-6662-4486-942c-b1a8bcbb9d82"), 1, "Reggae" },
                    { new Guid("423a17f2-fd72-49e9-9aa1-1d8068934927"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 2, "Soul" },
                    { new Guid("ed84e6a4-e885-40db-bd53-a527b7b19577"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 1, "Hip Hop" },
                    { new Guid("638cb11b-f726-4dc4-b8f7-baf33518a6bf"), 2, new Guid("db8b6a7f-2533-41b1-ab50-8ab04f3df1cd"), 1, "World" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("2359dced-b670-438e-9aa6-2e922435a1ad"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 2, "Stage And Screen" },
                    { new Guid("327dab45-afc2-4143-90fa-5a41ae9933c1"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 1, "Jazz" },
                    { new Guid("762688e3-31c5-4c0e-9677-50676a6a3965"), 2, new Guid("548d7014-9e08-405d-b228-4acdea77762f"), 0, "Soul" },
                    { new Guid("6abeeb8e-89a6-4058-b9f4-fd7d278e54ca"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 2, "Folk" },
                    { new Guid("8ab42c60-3e71-46fd-93d6-94825a427316"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 0, "Folk" },
                    { new Guid("18ff581e-af40-48af-a27f-67c0f4470b0f"), 2, new Guid("5310f4a1-7a41-4ce3-8a10-2641be4825bd"), 2, "Jazz" },
                    { new Guid("74840d56-4f7e-4d49-8491-bf9725dc5540"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 0, "Funk" },
                    { new Guid("c61cad51-929e-40fe-969f-1d6700027ade"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 0, "Soul" },
                    { new Guid("3b95482a-db73-47c0-a936-5ef9460b99ca"), 2, new Guid("d21272e4-a74b-4099-9b34-d6a7009650fe"), 1, "Metal" },
                    { new Guid("e9f71672-04fc-4d12-b34a-47f8fb681ee0"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 1, "Funk" },
                    { new Guid("28073626-e2ab-4023-9a42-ce22123665f2"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 2, "Electronic" },
                    { new Guid("f3ec375c-e41e-401b-a912-7cf9a78cb77f"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 1, "Country" },
                    { new Guid("c988e705-ae83-45f9-a962-d226e6c315a4"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 1, "Rock" },
                    { new Guid("5c6718bb-0d8c-44e3-b497-52808089613b"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 2, "Pop" },
                    { new Guid("26fa37d5-2f7a-4704-b520-080e95d6b530"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 1, "Country" },
                    { new Guid("593b46b1-0971-4f8d-8c2d-003f11416677"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 1, "Pop" },
                    { new Guid("d388d5ba-795b-42e2-a7aa-eabf86592f77"), 2, new Guid("b8e4c54c-8c7c-4dfd-b8b3-695a5a7bd37a"), 0, "Rock" },
                    { new Guid("cf74f955-ea7a-46fd-b59c-c8bf2a6b829f"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 1, "World" },
                    { new Guid("6979b06e-0e1f-44e6-83f5-87f57d1713dc"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 1, "Non Music" },
                    { new Guid("85bf33f5-7635-43bd-9741-f1d0da00cccb"), 2, new Guid("5ab5a9e3-a5ee-4d85-bdc7-7062fc34a07a"), 0, "Metal" },
                    { new Guid("4d619755-0d51-4286-af09-d5b7b0eb0b96"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 0, "Rap" },
                    { new Guid("42f50759-bb1a-45dd-a8c8-4dd2566c2744"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 1, "Blues" },
                    { new Guid("77a3d86f-43be-43ca-878b-557702ee9b15"), 2, new Guid("e8dc868a-f3bf-4c04-90bc-7b30cf716e4c"), 0, "Hip Hop" },
                    { new Guid("26ba4749-87ab-4077-a5d4-5431e50f1c6e"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 1, "Electronic" },
                    { new Guid("dae356c8-6f82-447b-b26e-72749f275138"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 0, "Soul" },
                    { new Guid("39f3462a-b670-4215-8108-a5a63bd0d567"), 2, new Guid("a6292371-5599-4efc-8dfa-6576393dec23"), 1, "Funk" },
                    { new Guid("fe7f69df-be67-4b2b-adc4-6f42d4cc6fc8"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 0, "Electronic" },
                    { new Guid("a6753b53-b9f9-48c8-b15c-0c423bce8d4e"), 2, new Guid("4e3aacca-f4a8-4525-a838-a10c97682e08"), 2, "Country" },
                    { new Guid("4c57dc64-b434-4323-86eb-03f26a91af76"), 2, new Guid("b402f6c6-407b-4a2a-b87a-e894efd40163"), 0, "Folk" },
                    { new Guid("37c8b65f-9f26-4c19-ad34-af459a0fe596"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 0, "Funk" },
                    { new Guid("d285ab32-928e-4629-a853-80c5a925ef68"), 2, new Guid("72b13796-7ef0-4785-9cab-d759ddf89471"), 1, "Blues" },
                    { new Guid("5f2c4483-964c-4cf2-8808-31181ae57565"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 2, "Jazz" },
                    { new Guid("f1bd4962-7654-4bbc-a146-9ebf5e533b39"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 0, "Country" },
                    { new Guid("229ad193-ac51-40b1-9cbc-d0438c6c3456"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 2, "Folk" },
                    { new Guid("6d33d23b-e653-4e64-b728-cab94be7c3fb"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 2, "Stage And Screen" },
                    { new Guid("8aace5b8-ac2a-4fdb-8aad-a6f8c3ec1016"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 2, "Latin" },
                    { new Guid("c9141689-e3a0-4e2a-a362-81cc3e7c0a4b"), 2, new Guid("c09e01cb-c0bf-49be-a4c6-3e0ad45177b8"), 0, "Reggae" },
                    { new Guid("70780a83-a23e-4818-81fe-88d22c2491ac"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 1, "Reggae" },
                    { new Guid("6b003d42-33e0-4e1e-a5dd-3a1a5032ad56"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 0, "Classical" },
                    { new Guid("0e588640-0d6b-4d3d-ad79-8337736ab818"), 2, new Guid("c9b8b1cd-8a51-4b22-bd6b-7e93a52ce909"), 2, "Stage And Screen" },
                    { new Guid("6fd1eb47-d538-49bc-aa7c-793c9cbd9e66"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 2, "Jazz" },
                    { new Guid("d585aef3-a43c-4c2b-b7c8-ccaacc6e6268"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 0, "Country" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("bb9dd61c-41f2-46cf-8b23-b9a687610e2d"), 2, new Guid("64ebe34b-4f9e-4b67-874b-e9e0f4c4925a"), 0, "Funk" },
                    { new Guid("c01fef6a-cf12-47db-909b-6abd804378d6"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "Metal" },
                    { new Guid("f2466c34-ddce-4087-9542-c6730a907cb8"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "World" },
                    { new Guid("df09fb0a-bd2a-454c-aa0e-28d0a7212e99"), 2, new Guid("da35517f-af20-4fbd-b9e9-b3e5a0f86456"), 2, "Electronic" },
                    { new Guid("97efa6e0-02af-405f-b088-60991bc0264c"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 2, "Non Music" },
                    { new Guid("d5292421-88ea-4444-9d5e-6aea4943695a"), 2, new Guid("d0b76ca4-f681-4fc0-a039-6a394b08be8f"), 1, "Hip Hop" },
                    { new Guid("d5e66c0d-1add-4a3b-8559-c58278007e74"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 1, "Rap" },
                    { new Guid("1ab13cd0-1c2b-499b-b4dc-34eb0e8e44cb"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 2, "Non Music" },
                    { new Guid("83d66a18-ecd3-4cb6-8cd9-4aab4a5a546c"), 2, new Guid("5ea715eb-a924-47f5-bc21-5ebf0a0268ab"), 2, "Rap" },
                    { new Guid("504c3a48-1454-4339-b821-4817ef75f150"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 2, "Folk" },
                    { new Guid("a4b72461-1c9a-46e0-aeb4-a4357e3ea14e"), 2, new Guid("34fa3ff5-eb6c-49d0-a00c-5092dfdcb07f"), 1, "Metal" },
                    { new Guid("acff5eaf-ebf5-49c5-9749-9c80f97440ee"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 2, "Jazz" },
                    { new Guid("74efaf4d-0592-4b79-b790-db428f4342a4"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 0, "Folk" },
                    { new Guid("251e0037-50ce-455f-a322-1b1336b152e4"), 2, new Guid("7644f565-d186-4827-aff7-198891d67559"), 1, "Stage And Screen" },
                    { new Guid("4fbcc649-6638-41bc-af27-e7bc70035427"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 0, "Reggae" },
                    { new Guid("206dd7ea-7290-41f1-b1b0-605aada30f96"), 2, new Guid("d0aa135d-4c8d-4fe2-a8f0-cc08f8da7a7a"), 2, "Latin" },
                    { new Guid("13abafa8-b4ac-4185-a04d-352473d13cbc"), 2, new Guid("5dffca28-85f9-47bd-b1a8-aeb2a6c11cbb"), 2, "Classical" },
                    { new Guid("777a4bdf-bf7d-4685-9d4c-9201f72ff15e"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 0, "Stage And Screen" },
                    { new Guid("339e5664-82c4-4a5a-90ac-2a994a346e7a"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 1, "Jazz" },
                    { new Guid("5b04461d-825a-480d-a1e2-0af53b17afd6"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 1, "Pop" },
                    { new Guid("01a416a2-230f-4165-a30b-df2b3e961474"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 2, "Metal" },
                    { new Guid("0f6ef7fe-7549-4e0f-95a4-8483a64b3126"), 2, new Guid("4f22412f-afb4-4180-aa39-81b694e33f86"), 0, "Classical" },
                    { new Guid("82bd54a7-7ead-463c-98bc-dcc0cf31e36f"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 0, "Soul" },
                    { new Guid("d7ba580f-828e-4b85-b5ac-c1dc94599ae8"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 0, "Electronic" },
                    { new Guid("17a2ea79-994d-44fc-a66b-c54379f46e64"), 2, new Guid("37ece1c8-e6d0-4282-ba2c-9b7f8b5dea9b"), 0, "Pop" },
                    { new Guid("1dd322f8-8b9f-48cd-aed7-1ce6a2c7b5e6"), 2, new Guid("960c4db6-a9ab-4d9c-bf44-5d9e33254825"), 2, "Funk" },
                    { new Guid("115ef618-f584-46d1-b53f-02a9db5c4291"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 0, "Latin" },
                    { new Guid("c73d1ee1-6fc1-4fb7-9f83-db1cd8426d1d"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 0, "Folk" },
                    { new Guid("0021ed4a-fff4-44d3-b625-28a4a0957781"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 0, "Soul" },
                    { new Guid("95e15108-c84e-4287-917c-b3dfb9c4df7a"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 1, "Classical" },
                    { new Guid("badd5840-80cd-4d73-ba39-822532c06f67"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 1, "Metal" },
                    { new Guid("19aa7108-e3e4-4de9-bb75-00a4d89e70dc"), 2, new Guid("897fdac8-0d46-4c04-ad98-1ca179e055c6"), 1, "Rock" },
                    { new Guid("84e68950-0e53-4ce0-90e7-9f462664b499"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 0, "Reggae" },
                    { new Guid("22815098-b478-4bc3-ae94-b844522e4e0e"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 1, "Reggae" },
                    { new Guid("cfca71d2-e2e9-4b84-af55-346a1fa4bb68"), 2, new Guid("bd22e70b-4814-4bcf-9383-77d30bc97b2e"), 1, "Stage And Screen" },
                    { new Guid("ce88f345-9940-4c1f-bead-35dcdc040d3d"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 0, "Stage And Screen" },
                    { new Guid("a508b3e0-d538-4fc4-8682-2bef6dbad3e0"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 1, "Jazz" },
                    { new Guid("dd63082a-c2bf-42fc-8801-b501f7706d60"), 2, new Guid("638d8efd-cfa9-43ca-b45c-dadb8a1a40d9"), 1, "Folk" },
                    { new Guid("7116e9f8-984c-48eb-bf31-ef44b45de7c2"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 0, "Jazz" },
                    { new Guid("5d1995a3-7804-49fe-bdb1-943ef4c4b7bd"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 1, "World" },
                    { new Guid("6ffc806d-650e-4c94-86c3-8ff88686fd53"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 0, "Classical" },
                    { new Guid("d6faa33b-8f88-4d72-9183-e2c96da2c594"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 2, "Latin" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("dec7d741-7b61-4c99-92ba-81b1b66dca1b"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 1, "World" },
                    { new Guid("8dc42b03-1f43-43c5-a284-9c25c4be8949"), 2, new Guid("6ae8c8ed-ec49-4ba5-b8cb-0bf9d8404355"), 1, "Rap" },
                    { new Guid("6e7a36a5-f475-40a5-ada3-d5b9b6514279"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 0, "Folk" },
                    { new Guid("4fcbb7ce-0c9c-44cf-a7c9-cc7f8c0e2570"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 1, "Non Music" },
                    { new Guid("f4bef84d-e99f-4782-81a6-9bad925e1b92"), 2, new Guid("93f27cb5-7595-45b2-b804-07b088320016"), 1, "Soul" },
                    { new Guid("fbba102b-b74d-41c7-b25e-deaaf0ee4016"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 2, "Pop" },
                    { new Guid("61646c2e-139a-4fc3-a867-9c46dd6cc83e"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 2, "Pop" },
                    { new Guid("1c44e562-351a-45c6-bc00-1d9b530a8625"), 2, new Guid("9ad6c035-5c0f-4089-b129-0dd2b14ce170"), 2, "Non Music" },
                    { new Guid("3cf044d0-a256-463f-b1f2-23d890fe39f1"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 0, "Non Music" },
                    { new Guid("6e5ab92b-e5dd-4a11-a3cf-7c0b8846f6a5"), 2, new Guid("ea2bd4b2-fc59-4f88-ad49-fcdc1b4d10d6"), 2, "Rap" },
                    { new Guid("d5fc2434-36d0-40e3-902f-256d57629298"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 0, "Soul" },
                    { new Guid("48866d59-449a-403e-91e6-efb2c8856a7e"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 1, "Rock" },
                    { new Guid("7b248682-479d-4859-a4f6-85019f2f91af"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 0, "Reggae" },
                    { new Guid("87f91944-8cfe-4f3b-87a3-98043f591314"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 2, "World" },
                    { new Guid("664bf89b-926b-4fd7-8ebf-812b670f8fd5"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 0, "Jazz" },
                    { new Guid("499f7caf-2240-4609-b91b-d76a0e6e0be9"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 0, "Blues" },
                    { new Guid("a780fc9e-f307-4b38-b99c-b62fca385bfd"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 2, "Hip Hop" },
                    { new Guid("6926064c-3170-4d7b-ae65-67f30cb6893d"), 2, new Guid("148ea126-6039-468d-924e-7f09bee9f7aa"), 2, "Classical" },
                    { new Guid("0db6bda6-a877-4ac6-9d42-af94485a7cb6"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 0, "Soul" },
                    { new Guid("68142e37-bedf-4694-89fa-cca0d38429ba"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 2, "Electronic" },
                    { new Guid("f59d010e-4cd9-4f35-9144-1e25278e23ff"), 2, new Guid("6bf1cc6f-baf2-45e1-a500-480aaa5e9f30"), 2, "Folk" },
                    { new Guid("8f60cf02-4fd3-4604-9879-545fabcdb192"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 0, "Metal" },
                    { new Guid("20a9f307-5567-48f4-bc47-12fc4123fb82"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 1, "Rap" },
                    { new Guid("117c492b-7bc2-4645-a799-97a154ad5cf2"), 2, new Guid("331b2837-be80-4dd3-8690-3722b54f2c3f"), 2, "Electronic" },
                    { new Guid("401317ab-18aa-481b-9339-371475d7f13d"), 2, new Guid("0b287dab-3600-4dc9-9def-47ee1a104c62"), 1, "Metal" },
                    { new Guid("4cc52671-3b02-4fff-886e-1bc1e1dbfeb9"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 1, "Jazz" },
                    { new Guid("3d038ebe-d99c-4d75-b422-1b3817f76b76"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 0, "Classical" },
                    { new Guid("cd645bd9-e734-4106-86b0-b2a4fe52ec78"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 0, "Electronic" },
                    { new Guid("6af251fd-ad0e-4999-965b-7fb697be3242"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 1, "Metal" },
                    { new Guid("33d6edb0-258c-4bd3-895a-9273aea0e683"), 2, new Guid("5ef121ec-a2f0-4c3a-bc94-af2220f889a3"), 1, "Pop" },
                    { new Guid("3a7993d7-1c04-437d-aa3c-5d9f6c8c583c"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 2, "Blues" },
                    { new Guid("64014773-2497-410a-9d00-82b84cd35da5"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 1, "Metal" },
                    { new Guid("ec8b7602-1f3d-42eb-8d2f-bf542e55411f"), 2, new Guid("5f650ffb-93ed-49eb-aaa6-2d777793a688"), 2, "Stage And Screen" },
                    { new Guid("4b0a6072-439a-4f05-8ca2-a8040c8cc7e4"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 1, "Jazz" },
                    { new Guid("01f7627e-870c-4f4b-a81d-94150a90015e"), 2, new Guid("43b013c6-8a97-4e8d-8bd3-eb5de5613472"), 0, "Latin" },
                    { new Guid("f6f46654-1397-4f93-a132-a9e942bca81e"), 2, new Guid("3ba0cc08-6c69-4f18-af32-d6432da081ef"), 0, "Pop" },
                    { new Guid("314227a8-b8b2-479c-bfc0-b154363c149b"), 2, new Guid("5c807ece-29a8-4f92-b8a3-2317e19fe081"), 2, "Country" },
                    { new Guid("6419e09a-833c-467d-8bf5-8acc4d8f4249"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 2, "Soul" },
                    { new Guid("7cf39ea5-41c2-47f4-8f84-b5deb3ba130c"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 2, "Classical" },
                    { new Guid("5a5e224b-d0a8-4304-8973-61bef764b0d9"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 2, "Rock" },
                    { new Guid("70ed6fc6-56f8-4e5c-986a-4815e571318a"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 2, "Stage And Screen" },
                    { new Guid("2c91ffa8-d8b5-448f-97f3-cb2899234ac7"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 2, "World" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("fa5b5747-efa9-4308-8e75-6b629f43fcde"), 2, new Guid("b7858809-f639-4b9c-bc2f-41f96ffe4195"), 2, "Reggae" },
                    { new Guid("b32df076-033d-4921-bf07-0722c11525be"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 0, "Latin" },
                    { new Guid("b6bff0f3-3e87-4bdb-94dc-1032f6a6f9c3"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 1, "Classical" },
                    { new Guid("76249902-f4c4-4005-8a6f-c97b3bd9b031"), 2, new Guid("19fd77ae-50aa-4699-9eff-162704de3a41"), 1, "Non Music" },
                    { new Guid("a8df3537-11e9-4cfd-8b53-19f8b9db17df"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 0, "Country" },
                    { new Guid("6b86ebc5-281a-4815-ac83-0eed8c0fd10f"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 1, "Classical" },
                    { new Guid("b2ccba51-dd82-4c03-bb3f-83fd3ef23419"), 2, new Guid("9def0d6b-2f8f-4aa0-93af-2fbc6659876b"), 2, "Country" },
                    { new Guid("eb7feb8c-dfeb-4a28-8d1b-9406508282d9"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 2, "Pop" },
                    { new Guid("a6a9657a-9b2b-4bce-980e-845e21c65a58"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 0, "Metal" },
                    { new Guid("50d4d3f7-dfc5-4905-8c42-1b1ac87f5041"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 2, "Reggae" },
                    { new Guid("2fa232b1-1c79-4e16-a0c8-347ede0b60ab"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 1, "Reggae" },
                    { new Guid("6616614d-f8d2-4868-b721-e10e7564e0aa"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 0, "Electronic" },
                    { new Guid("8a3994c3-d9f3-42e1-a26f-be9f245ee4a9"), 2, new Guid("2ec0a42f-3831-49d2-9ade-eff59c622bfb"), 0, "Soul" },
                    { new Guid("58673121-211e-40ce-9c7f-35c4b1d5cec5"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 2, "Funk" },
                    { new Guid("2efcabdc-5b49-4609-955d-1827095c2519"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 2, "Soul" },
                    { new Guid("64ba803b-5da9-4531-9e3e-bd1709b18c91"), 2, new Guid("3c25f2da-522b-4281-beae-82091dcdfa81"), 1, "Stage And Screen" },
                    { new Guid("f28df3cb-f447-47dd-8230-7cad224dbbd3"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 0, "Pop" },
                    { new Guid("8650c60d-cfaf-4633-8c7f-f95013417802"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 1, "Hip Hop" },
                    { new Guid("22abdd10-4b2b-48f3-bf4c-e5d5b920bace"), 2, new Guid("5c99a7cf-d891-4619-a93d-61dfa8835760"), 2, "Funk" },
                    { new Guid("751278c2-d5dd-4c25-9dc0-deb86f183ffa"), 2, new Guid("e46f45fa-0678-438a-846a-3757bf1fd017"), 0, "Pop" },
                    { new Guid("38cca4c7-2496-41c4-919d-062d9286268e"), 2, new Guid("3c8e31da-7ea7-4758-87b4-816438d98405"), 1, "Classical" },
                    { new Guid("41e2a31d-3edc-4777-a940-2a29320c25f2"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 0, "Funk" },
                    { new Guid("ea60de1d-cf94-4b45-8efa-cf3cf60541fd"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 0, "Jazz" },
                    { new Guid("ad69edd4-84c5-4686-80c3-0fbeca1b45ec"), 2, new Guid("18de8547-9891-4db0-ad0f-afe431fc3f6c"), 2, "Soul" },
                    { new Guid("c1f5be4f-5fc7-450f-bddd-fe976138cdd1"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 2, "Blues" },
                    { new Guid("ccd08d5c-6a36-43bb-a212-d46e47442819"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 1, "Blues" },
                    { new Guid("ea608110-1cd3-4fb8-a674-1d68eaa79e87"), 2, new Guid("80168403-1b0c-40e6-a52c-c2f425f88eb7"), 0, "Pop" },
                    { new Guid("6277bc8e-c1c4-4abc-b814-e14ba0bfe92e"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 2, "Non Music" },
                    { new Guid("5d221b35-d7a7-43f8-a51b-b9204de595b5"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 1, "World" },
                    { new Guid("25fa7842-0707-413f-9c2b-a4cfaef86b92"), 2, new Guid("4fbd7943-1d7e-4359-8627-ded1c4aa76cf"), 0, "World" },
                    { new Guid("6b76a712-fc8c-4053-8a6d-077c05070123"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 2, "Classical" },
                    { new Guid("c26f76ef-c5ba-49c8-980d-7a6e118775d4"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 1, "Electronic" },
                    { new Guid("08f3f8cf-7b72-4ac1-86eb-24f5be5a6e79"), 2, new Guid("3e01e8a7-373f-4264-a20b-0641e319025c"), 1, "Classical" },
                    { new Guid("3a74ad05-3b5f-43bd-871d-f1d3f3deab0b"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 0, "Country" },
                    { new Guid("d1542784-069d-4ea9-b2d3-71ff256168dc"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 2, "Latin" },
                    { new Guid("af46c8fb-8391-4722-a58e-8710d86f904e"), 2, new Guid("4ce84e3f-f95d-4b3a-a664-3620f9391bfa"), 2, "Non Music" },
                    { new Guid("900997ff-02b5-459b-af8a-025a29d5703c"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 0, "Classical" },
                    { new Guid("b771f3f3-a632-42eb-b054-112c264d095f"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 0, "Soul" },
                    { new Guid("879ce222-140c-43fa-b357-9b071e8a0076"), 2, new Guid("99423eee-ae46-44ad-87b4-079cb7eeeb98"), 2, "Country" },
                    { new Guid("15b1794d-7a22-4822-a48c-b234320ed890"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 1, "Classical" },
                    { new Guid("0d9ac988-9f73-4200-912a-77051e777f49"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 1, "Metal" },
                    { new Guid("0d32fd1b-33f9-4eb9-894f-56cdfaf65a61"), 2, new Guid("c48aa9db-4e42-49ec-9c1f-84cd619e3197"), 0, "Stage And Screen" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("ee6d47a3-407e-47f1-87dd-f9a27615df53"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 1, "Folk" },
                    { new Guid("6f4fec5b-d1b3-4760-bdf7-6b3289df7c0f"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 2, "Soul" },
                    { new Guid("c61dcbfa-940f-4c41-ab7e-58b51f3048f0"), 2, new Guid("c332cd2e-ddeb-40c3-be07-6c7cbad78289"), 0, "Reggae" },
                    { new Guid("bf8d2186-c5c0-4887-baa3-2a896b373c2e"), 2, new Guid("c34b262c-478d-450f-aecc-f56573d30dfe"), 2, "Classical" },
                    { new Guid("9e40f9fc-edac-4444-942e-55e6210b68a2"), 2, new Guid("9c016245-e1a0-4b3c-8de8-56b40685f788"), 2, "Stage And Screen" },
                    { new Guid("ed0ec4be-ab13-4284-a675-9b9bad0b23d4"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 2, "Classical" },
                    { new Guid("d37a68b9-8b76-404f-9bba-d6dbde03563b"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 0, "Reggae" },
                    { new Guid("3aeaea6b-5239-4d6d-966d-7d3b741d0e8f"), 2, new Guid("fa360bbd-69e1-423e-8ede-1a236aa40892"), 0, "Metal" },
                    { new Guid("c0a1ce15-5eba-417e-a5fd-a1768839fbd6"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 2, "Blues" },
                    { new Guid("b393eb50-3bda-4b8c-bcc0-fa9427dc94b0"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 1, "Classical" },
                    { new Guid("3d5a9cc1-ddd7-4ed7-adae-80af5eb3cc25"), 2, new Guid("19d1fba7-fec6-48b9-9ed8-709d0645c8f6"), 2, "Non Music" },
                    { new Guid("bcf0f329-c3e1-4229-93ff-de1e5d5ed835"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 1, "Metal" },
                    { new Guid("46abca97-5515-484e-bc1c-e3b7b896e670"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 1, "Electronic" },
                    { new Guid("855c0068-092c-405f-a55f-640c875646f9"), 2, new Guid("a13accc3-8a7e-4300-b7bd-1c85af6b9dde"), 2, "Electronic" },
                    { new Guid("86f9e8f8-9e72-42a8-93f3-3097168b7654"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 2, "Blues" },
                    { new Guid("43d6ce7c-7cdb-462a-a141-4e24abbcb92b"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 0, "Rap" },
                    { new Guid("dcdd9779-4716-4ab6-a223-850cd5995632"), 2, new Guid("650b8a25-f434-4ea7-b752-bd2bba74651c"), 0, "Soul" },
                    { new Guid("e3d518bc-f459-4cb1-9e93-4451b01cfbe1"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 2, "Stage And Screen" },
                    { new Guid("9269ec73-be8d-4b83-9035-2a1e7cb1688e"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 0, "Metal" },
                    { new Guid("f016b7c3-f237-4c6e-9fbd-e0f895f9f8ae"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 1, "Rock" },
                    { new Guid("2fd18b49-b6f6-4c4a-b05d-f3a29002e308"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 0, "Rock" },
                    { new Guid("ef307292-094b-4016-be0e-b4d712578ece"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 0, "Country" },
                    { new Guid("e2c8a575-0f94-4a25-95a3-9defa534d8de"), 2, new Guid("8984e79b-3d7c-4cc9-9428-51f44d5fb733"), 2, "Stage And Screen" },
                    { new Guid("c2f807ab-f9dd-41df-9848-5aa7c97295d9"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 1, "Jazz" },
                    { new Guid("bef2df6b-adeb-4d2c-a6a0-9e57ab30633c"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 0, "Blues" },
                    { new Guid("48443e84-0555-430a-8473-330345a55978"), 2, new Guid("843b2682-6f2f-49bc-8742-4ffb68c5cc27"), 2, "Country" },
                    { new Guid("8c616055-f7e3-4383-ba73-9964e444e226"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 1, "Metal" },
                    { new Guid("c5b7d1dc-f5c1-428b-b683-bfcd30f1f3e2"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 0, "Latin" },
                    { new Guid("fa99729e-a34d-4a5a-a6ad-d72c98abfbb6"), 2, new Guid("e492b852-576c-43a7-bb80-35665d88c099"), 0, "Funk" },
                    { new Guid("432365b3-7d92-46d3-a8e7-a74d65c51249"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 0, "Folk" },
                    { new Guid("3f683283-9e0e-4772-a3f0-3d72c8ce58fa"), 2, new Guid("2f8b2539-2fd0-4eea-bf36-aef320520ebd"), 2, "Hip Hop" },
                    { new Guid("b97be165-0488-4e33-b228-c14bb17ef896"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 1, "Reggae" },
                    { new Guid("4e136deb-3ef8-4fe2-8fab-d049e489feb3"), 2, new Guid("6fe18726-1889-46cc-97de-1739c2b2c094"), 2, "Jazz" },
                    { new Guid("9ab28989-d000-4a8d-bf6a-ad474aeda0c6"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 0, "Hip Hop" },
                    { new Guid("71c79b9e-64ce-482f-ab6c-0faaa0308e6f"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 0, "Stage And Screen" },
                    { new Guid("c9a7fbbc-7333-44a1-be42-22d6f27669f2"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 1, "Folk" },
                    { new Guid("013c0b73-21d3-41ba-b4ed-485ed31a900c"), 2, new Guid("856c1e98-b7e1-4be8-8041-e0cea12b4247"), 2, "Reggae" },
                    { new Guid("ca6754d3-7cb7-472c-b210-f150663763c4"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 1, "Folk" },
                    { new Guid("f2f954f3-ec32-40c9-8976-f8795e0b49c3"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 1, "Rap" },
                    { new Guid("43a43f4d-0d47-4330-bc08-c0225e4d4fdd"), 2, new Guid("94160575-91e7-4118-81db-3c0b2ff321a7"), 0, "Funk" },
                    { new Guid("0b948866-5b60-498f-a837-41ec18da9632"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 0, "Non Music" },
                    { new Guid("77be5bfa-9228-4aef-b5c5-f219f852867a"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 1, "Latin" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("bafa91eb-a5ba-48d3-a41a-47a69e59b09c"), 2, new Guid("182b7d4c-8917-46ab-a05e-0bd70beaaaa3"), 0, "Blues" },
                    { new Guid("083059ac-5d58-46c2-b44d-f91f42d08852"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 0, "Folk" },
                    { new Guid("d596293f-91ef-4d27-b34f-3febe5a4a247"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 0, "Soul" },
                    { new Guid("2d904f9f-ff45-48c2-a440-b82523754953"), 2, new Guid("b6475979-f1cd-4f69-8105-e3a081ef5b15"), 1, "Rap" },
                    { new Guid("4ed23f4e-28cb-492e-9d9e-0eee434b338d"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 2, "Classical" },
                    { new Guid("acd61ff0-e8d3-459e-9880-42fea9f78b0f"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 1, "Metal" },
                    { new Guid("375ec5d4-8def-4584-92fb-87670eaf2c01"), 2, new Guid("f7bdf7e3-0123-4785-8c5e-e5848efaf4f7"), 1, "Classical" },
                    { new Guid("f871a31a-6f0f-4a52-ab34-dcc7a279d461"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 1, "Rap" },
                    { new Guid("20646148-0446-487e-9868-67f89889550a"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 1, "Pop" },
                    { new Guid("2cbe354b-ecfb-4a87-b210-da589451974b"), 2, new Guid("ec945e7b-b09c-4b71-b547-402d5f32718a"), 1, "Rap" },
                    { new Guid("920e2597-0779-4957-a97c-c770da415ee2"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 1, "Country" },
                    { new Guid("a8f0e0e7-4b9d-4030-aee3-7023f2573f6b"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 1, "Soul" },
                    { new Guid("d851623b-83f8-4436-b920-dbeeac9e7a1f"), 2, new Guid("c30025ad-4457-4d39-93ee-e5d092e0f420"), 2, "Blues" },
                    { new Guid("19ad0cdc-b096-4f82-84f4-126314e31709"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 2, "Blues" },
                    { new Guid("816a6ffe-2086-42ec-9e39-988fbbd012e5"), 2, new Guid("dbced78d-a1bd-45eb-bf14-e976de47e931"), 2, "Soul" },
                    { new Guid("6d1bd263-6f20-42d0-a7e5-bd73a77f139c"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 0, "Non Music" },
                    { new Guid("56e2137a-043c-4acd-a527-04b621822d84"), 2, new Guid("4b035928-4fdb-44c3-8755-edbb35696d24"), 2, "Electronic" },
                    { new Guid("01eb8740-39dd-4893-a289-4d4f6bbb1317"), 2, new Guid("5ac85168-7273-4f34-88e8-c2c1422ea830"), 0, "World" },
                    { new Guid("cf21708d-444a-4e2e-856f-910b7b8b1da3"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 0, "Pop" },
                    { new Guid("ee9e230d-f21e-467a-9167-117eafb5702b"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 0, "Rock" },
                    { new Guid("9004f493-a8d2-47ab-85d8-e6963a2c489f"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 2, "Soul" },
                    { new Guid("112db2aa-3cd7-426f-a337-50bb82047894"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 2, "Metal" },
                    { new Guid("3882efcc-e23a-48fb-a820-d67121f93b5c"), 2, new Guid("091d95ea-f872-41ad-9c4c-c12f5ac16899"), 2, "Reggae" },
                    { new Guid("bfc95ee7-67a1-49d6-a6f0-fac45cbc5dcb"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 2, "Stage And Screen" },
                    { new Guid("432d212d-8f44-47fc-ada6-a4d336490191"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 0, "Hip Hop" },
                    { new Guid("fe69e090-cc61-408d-9157-ea77717e1920"), 2, new Guid("580c74b0-74c0-479c-87bb-1fe8f77de0ef"), 0, "Country" },
                    { new Guid("8825aa2b-c50a-427d-bf03-a7883d3e7445"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 0, "Reggae" },
                    { new Guid("0108542b-48bb-4166-95f2-333896dc11e2"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 1, "Folk" },
                    { new Guid("6cbdcef2-fde4-4722-9af8-c155fb46af6a"), 2, new Guid("71addde8-77ee-46be-a306-d381c5e9d1c1"), 1, "World" },
                    { new Guid("ea03bf1e-c764-4d62-b39a-66f4fbbc0e44"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 0, "World" },
                    { new Guid("f0221939-cfbb-46b1-b740-3fa00c0a9b45"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 1, "Hip Hop" },
                    { new Guid("21391096-374a-4f74-a4a2-e0daebd00b83"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 0, "Non Music" },
                    { new Guid("4721a059-e1b9-4f4d-b629-0e0f7565f03d"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 1, "Funk" },
                    { new Guid("9541b524-886e-45bc-b3d5-082498805875"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 1, "Stage And Screen" },
                    { new Guid("b2de8ca9-0318-467f-99c1-6ad29de313e5"), 2, new Guid("30793799-6d6d-415f-ab58-964d9262f3ce"), 1, "Rock" },
                    { new Guid("af07fdc9-ddbb-4dd5-9986-9f9f2690ac7d"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 1, "Rap" },
                    { new Guid("38b87eb2-df01-4433-b92c-1d2979031ae8"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 2, "Rap" },
                    { new Guid("78ddc4b2-3970-4278-ad07-33bfcf062d96"), 2, new Guid("e0c23de5-f3a6-490e-86a9-f4fc66f81a3b"), 0, "Metal" },
                    { new Guid("ebd1b0b5-48df-4a4d-9177-d7124e187382"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 2, "Electronic" },
                    { new Guid("d4c2375b-838c-4b51-93ad-fd29f4ed2dcc"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 0, "Rap" },
                    { new Guid("e6ea87bd-5bc0-4fb2-892e-0bf65037c203"), 2, new Guid("b9ec6a2d-3e38-4cf2-bad5-c3a53879cf96"), 1, "Stage And Screen" },
                    { new Guid("9af120dd-410c-412a-ac21-87720e5e8307"), 2, new Guid("7b8ab7df-cf42-4e8f-9430-e392a5ca221f"), 0, "Soul" }
                });

            migrationBuilder.InsertData(
                table: "LocalizationNames",
                columns: new[] { "Id", "Discriminator", "GenreId", "LanguageType", "Title" },
                values: new object[,]
                {
                    { new Guid("50871993-d739-4429-a522-bfd1ea40cd8e"), 2, new Guid("ad4d546e-b943-47d2-bc26-e85babd4fbdf"), 1, "Blues" },
                    { new Guid("9107abf0-4c30-48b9-a64c-ae2ae9e1b06e"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 2, "Rock" },
                    { new Guid("cd4abad8-966a-46c0-9269-2c9f89d08a2d"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 0, "Funk" },
                    { new Guid("f7e9295a-7513-49d4-a88f-ae2d6602c48a"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 2, "Folk" },
                    { new Guid("0735f684-0cce-4b68-b484-6431eb6577be"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 1, "Classical" },
                    { new Guid("898b5d42-469c-4e57-bf2e-3001aaeb0560"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 2, "Blues" },
                    { new Guid("a5606ddc-6d1e-4596-8ac5-7d9e200b340e"), 2, new Guid("888c3836-6606-4d07-8a60-4c0e26399fed"), 2, "World" },
                    { new Guid("a9ac395e-42ff-40a3-88cb-6776a2a0a195"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 1, "Latin" },
                    { new Guid("437e71b6-f3da-476d-8f92-7cd048944af3"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 0, "Funk" },
                    { new Guid("a532e3b0-59f5-4ac4-8ec4-91057e11ac86"), 2, new Guid("19cccd89-e5be-4732-9990-9fbf9ffddfe1"), 0, "Stage And Screen" },
                    { new Guid("73453e31-93ec-4047-9509-f9e4d92f3f2a"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 0, "Jazz" },
                    { new Guid("a1d463c3-d939-477b-8af7-904c015e04bf"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 0, "Classical" },
                    { new Guid("334c1e2e-5cad-415e-86b2-3e64f79e73d6"), 2, new Guid("896eacd5-383a-4b20-beb7-dc124413d07b"), 2, "World" },
                    { new Guid("4f331037-d344-430a-bf44-5de62deea1e9"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 2, "Folk" },
                    { new Guid("03ca3e06-7d26-43eb-8449-baab7e115944"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 1, "Rock" },
                    { new Guid("abebe9d3-32d3-418c-b28f-d3d687f9840d"), 2, new Guid("32123a0b-c86f-4a21-9671-dd2a7d6ee843"), 1, "Classical" },
                    { new Guid("9c48cb5f-5d82-4c5d-b23d-464da8de060a"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 0, "Soul" },
                    { new Guid("d623a9ee-e51f-4746-8397-4558fea6a965"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 0, "Non Music" },
                    { new Guid("564a448b-ae4c-4f3c-b9b5-326c4754f054"), 2, new Guid("9b6a40a6-134f-4c43-995d-8821d5556f0f"), 0, "Soul" },
                    { new Guid("8697b3ac-7fc8-422e-9377-1414db736bd9"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 0, "Blues" },
                    { new Guid("f04c7383-c74b-459d-ac7a-d0aa20bb3997"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 1, "World" },
                    { new Guid("78dbbcca-2566-40bb-a6e3-bd255b524e7e"), 2, new Guid("ea65c248-6eaf-4a71-88d8-c0a9573bd472"), 0, "Country" },
                    { new Guid("72ddc9f9-a42c-45b9-a892-dad2693526aa"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 2, "Stage And Screen" },
                    { new Guid("5f01125e-9d4a-44f6-864d-7150c90ff31a"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 1, "Classical" },
                    { new Guid("93ac8883-b2bb-48c3-b5f6-ca19a9255935"), 2, new Guid("c55d2c0f-8e2f-444b-9c37-e351e813731e"), 1, "Metal" },
                    { new Guid("515f67c5-095c-40a4-bb74-866a93127078"), 2, new Guid("364b4479-25e5-46b3-b73c-21f4b6250b36"), 2, "Reggae" },
                    { new Guid("d007ec50-4590-44db-8b9e-d6a22bcb924e"), 2, new Guid("fff784a2-7a23-4863-84c3-f07a6f5c5397"), 0, "Soul" },
                    { new Guid("e4291bf2-565e-4289-b494-6ed196cef023"), 2, new Guid("3169c9c5-6be5-48d1-8170-4e406b46b9dd"), 0, "Classical" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AlbumTracks_AlbumId",
                table: "AlbumTracks",
                column: "AlbumId");

            migrationBuilder.CreateIndex(
                name: "IX_AlbumTracks_TrackId",
                table: "AlbumTracks",
                column: "TrackId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItems_ArtistId",
                table: "GenreItems",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItems_GenreId",
                table: "GenreItems",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_GenreItems_ProductId",
                table: "GenreItems",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ArtistId",
                table: "Images",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_GenreId",
                table: "Images",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ProductId",
                table: "Images",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationNames_ArtistId",
                table: "LocalizationNames",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationNames_GenreId",
                table: "LocalizationNames",
                column: "GenreId");

            migrationBuilder.CreateIndex(
                name: "IX_LocalizationNames_ProductId",
                table: "LocalizationNames",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Lyrics_ProductId",
                table: "Lyrics",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductArtists_ArtistId",
                table: "ProductArtists",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductArtists_ProductId",
                table: "ProductArtists",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualities_ProductId",
                table: "Qualities",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ArtistId",
                table: "Tags",
                column: "ArtistId");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_ProductId",
                table: "Tags",
                column: "ProductId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AlbumTracks");

            migrationBuilder.DropTable(
                name: "GenreItems");

            migrationBuilder.DropTable(
                name: "Images");

            migrationBuilder.DropTable(
                name: "LocalizationNames");

            migrationBuilder.DropTable(
                name: "Lyrics");

            migrationBuilder.DropTable(
                name: "ProductArtists");

            migrationBuilder.DropTable(
                name: "Qualities");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Genres");

            migrationBuilder.DropTable(
                name: "Artists");

            migrationBuilder.DropTable(
                name: "Products");
        }
    }
}
