using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirVinyl.DataAccess.Sqlite.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "People",
                columns: table => new
                {
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    FirstName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DateOfBirth = table.Column<DateTimeOffset>(type: "TEXT", nullable: false),
                    Gender = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberOfRecordsOnWishList = table.Column<int>(type: "INTEGER", nullable: false),
                    AmountOfCashToSpend = table.Column<decimal>(type: "decimal(8,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_People", x => x.PersonId);
                });

            migrationBuilder.CreateTable(
                name: "PressingDetails",
                columns: table => new
                {
                    PressingDetailId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Grams = table.Column<int>(type: "INTEGER", nullable: false),
                    Inches = table.Column<int>(type: "INTEGER", nullable: false),
                    Description = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PressingDetails", x => x.PressingDetailId);
                });

            migrationBuilder.CreateTable(
                name: "RecordStores",
                columns: table => new
                {
                    RecordStoreId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    StoreAddress_Street = table.Column<string>(type: "TEXT", maxLength: 200, nullable: true),
                    StoreAddress_City = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    StoreAddress_PostalCode = table.Column<string>(type: "TEXT", maxLength: 10, nullable: true),
                    StoreAddress_Country = table.Column<string>(type: "TEXT", maxLength: 100, nullable: true),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    Specialization = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RecordStores", x => x.RecordStoreId);
                });

            migrationBuilder.CreateTable(
                name: "VinylRecords",
                columns: table => new
                {
                    VinylRecordId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    Artist = table.Column<string>(type: "TEXT", maxLength: 150, nullable: false),
                    CatalogNumber = table.Column<string>(type: "TEXT", maxLength: 50, nullable: true),
                    Year = table.Column<int>(type: "INTEGER", nullable: true),
                    PressingDetailId = table.Column<int>(type: "INTEGER", nullable: false),
                    PersonId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VinylRecords", x => x.VinylRecordId);
                    table.ForeignKey(
                        name: "FK_VinylRecords_People_PersonId",
                        column: x => x.PersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_VinylRecords_PressingDetails_PressingDetailId",
                        column: x => x.PressingDetailId,
                        principalTable: "PressingDetails",
                        principalColumn: "PressingDetailId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Rating",
                columns: table => new
                {
                    RatingId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Value = table.Column<int>(type: "INTEGER", nullable: false),
                    RatedByPersonId = table.Column<int>(type: "INTEGER", nullable: false),
                    RecordStoreId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rating", x => x.RatingId);
                    table.ForeignKey(
                        name: "FK_Rating_People_RatedByPersonId",
                        column: x => x.RatedByPersonId,
                        principalTable: "People",
                        principalColumn: "PersonId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Rating_RecordStores_RecordStoreId",
                        column: x => x.RecordStoreId,
                        principalTable: "RecordStores",
                        principalColumn: "RecordStoreId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 1, 300m, new DateTimeOffset(new DateTime(1981, 5, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), "kevin@kevindockx.com", "Kevin", 1, "Dockx", 10 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 2, 2000m, new DateTimeOffset(new DateTime(1986, 3, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), "sven@someemailprovider.com", "Sven", 1, "Vercauteren", 34 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 3, 100m, new DateTimeOffset(new DateTime(1977, 12, 27, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), "nele@someemailprovider.com", "Nele", 0, "Verheyen", 120 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 4, 2500m, new DateTimeOffset(new DateTime(1983, 5, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), "nils@someemailprovider.com", "Nils", 1, "Missorten", 23 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 5, 90m, new DateTimeOffset(new DateTime(1981, 10, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), "tim@someemailprovider.com", "Tim", 1, "Van den Broeck", 19 });

            migrationBuilder.InsertData(
                table: "People",
                columns: new[] { "PersonId", "AmountOfCashToSpend", "DateOfBirth", "Email", "FirstName", "Gender", "LastName", "NumberOfRecordsOnWishList" },
                values: new object[] { 6, 200m, new DateTimeOffset(new DateTime(1981, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 9, 0, 0, 0)), null, "Kenneth", 1, "Mills", 98 });

            migrationBuilder.InsertData(
                table: "PressingDetails",
                columns: new[] { "PressingDetailId", "Description", "Grams", "Inches" },
                values: new object[] { 1, "Audiophile LP", 180, 12 });

            migrationBuilder.InsertData(
                table: "PressingDetails",
                columns: new[] { "PressingDetailId", "Description", "Grams", "Inches" },
                values: new object[] { 2, "Regular LP", 140, 12 });

            migrationBuilder.InsertData(
                table: "PressingDetails",
                columns: new[] { "PressingDetailId", "Description", "Grams", "Inches" },
                values: new object[] { 3, "Audiophile Single", 50, 7 });

            migrationBuilder.InsertData(
                table: "PressingDetails",
                columns: new[] { "PressingDetailId", "Description", "Grams", "Inches" },
                values: new object[] { 4, "Regular Single", 40, 7 });

            migrationBuilder.InsertData(
                table: "RecordStores",
                columns: new[] { "RecordStoreId", "Discriminator", "Name", "Specialization", "Tags", "StoreAddress_City", "StoreAddress_Country", "StoreAddress_PostalCode", "StoreAddress_Street" },
                values: new object[] { 2, "SpecializedRecordStore", "Indie Records, Inc", "Indie", "[\"Rock\",\"Indie\",\"Alternative\"]", "Antwerp", "Belgium", "2000", "1, Main Street" });

            migrationBuilder.InsertData(
                table: "RecordStores",
                columns: new[] { "RecordStoreId", "Discriminator", "Name", "Specialization", "Tags", "StoreAddress_City", "StoreAddress_Country", "StoreAddress_PostalCode", "StoreAddress_Street" },
                values: new object[] { 3, "SpecializedRecordStore", "Rock Records, Inc", "Rock", "[\"Rock\",\"Pop\"]", "Antwerp", "Belgium", "2000", "5, Big Street" });

            migrationBuilder.InsertData(
                table: "RecordStores",
                columns: new[] { "RecordStoreId", "Discriminator", "Name", "Tags", "StoreAddress_City", "StoreAddress_Country", "StoreAddress_PostalCode", "StoreAddress_Street" },
                values: new object[] { 1, "RecordStore", "All Your Music Needs", "[\"Rock\",\"Pop\",\"Indie\",\"Alternative\"]", "Antwerp", "Belgium", "2000", "25, Fluffy Road" });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 1, 1, 1, 4 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 4, 1, 2, 5 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 2, 2, 1, 4 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 5, 2, 2, 4 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 7, 2, 3, 4 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 3, 3, 1, 4 });

            migrationBuilder.InsertData(
                table: "Rating",
                columns: new[] { "RatingId", "RatedByPersonId", "RecordStoreId", "Value" },
                values: new object[] { 6, 3, 3, 5 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 13, "Anne Clarke", "TII/339", 5, 3, "Our Darkness", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 11, "Justin Bieber", "OOP/098", 4, 3, "Baby", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 6, "Leonard Cohen", "PPP/783", 1, 3, "Suzanne", 1967 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 14, "Dead Kennedys", "DKE/864", 5, 2, "Give Me Convenience or Give Me Death", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 12, "The Prodigy", "NBE/864", 4, 2, "Music for the Jilted Generation", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 10, "The Dandy Warhols", "TDW/516", 3, 2, "Thirteen Tales From Urban Bohemia", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 9, "Cher", "CHE/190", 2, 2, "Closer to the Truth", 2013 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 2, "Arctic Monkeys", "EUI/111", 1, 2, "AM", 2013 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 3, "Beatles", "DEI/113", 1, 2, "The White Album", 1968 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 15, "Sisters of Mercy", "IIE/824", 5, 4, "Temple of Love", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 8, "Nirvana", "ABC/111", 2, 1, "Nevermind", 1991 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 7, "Marvin Gaye", "MVG/445", 1, 1, "What's Going On", null });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 5, "Nirvana", "DPI/123", 1, 1, "Bleach", 1989 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 1, "Nirvana", "ABC/111", 1, 1, "Nevermind", 1991 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 4, "Beatles", "DPI/123", 1, 2, "Sergeant Pepper's Lonely Hearts Club Band", 1967 });

            migrationBuilder.InsertData(
                table: "VinylRecords",
                columns: new[] { "VinylRecordId", "Artist", "CatalogNumber", "PersonId", "PressingDetailId", "Title", "Year" },
                values: new object[] { 16, "Abba", "TDW/516", 6, 4, "Gimme Gimme Gimme", null });

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RatedByPersonId",
                table: "Rating",
                column: "RatedByPersonId");

            migrationBuilder.CreateIndex(
                name: "IX_Rating_RecordStoreId",
                table: "Rating",
                column: "RecordStoreId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_PersonId",
                table: "VinylRecords",
                column: "PersonId");

            migrationBuilder.CreateIndex(
                name: "IX_VinylRecords_PressingDetailId",
                table: "VinylRecords",
                column: "PressingDetailId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rating");

            migrationBuilder.DropTable(
                name: "VinylRecords");

            migrationBuilder.DropTable(
                name: "RecordStores");

            migrationBuilder.DropTable(
                name: "People");

            migrationBuilder.DropTable(
                name: "PressingDetails");
        }
    }
}
