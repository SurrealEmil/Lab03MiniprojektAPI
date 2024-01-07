using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Lab03MiniprojektAPI.Migrations
{
    /// <inheritdoc />
    public partial class RenamePeopleToPersons : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPerson_Persons_PeopleId",
                table: "InterestPerson");

            migrationBuilder.RenameColumn(
                name: "PeopleId",
                table: "InterestPerson",
                newName: "PersonsId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestPerson_PeopleId",
                table: "InterestPerson",
                newName: "IX_InterestPerson_PersonsId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPerson_Persons_PersonsId",
                table: "InterestPerson",
                column: "PersonsId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_InterestPerson_Persons_PersonsId",
                table: "InterestPerson");

            migrationBuilder.RenameColumn(
                name: "PersonsId",
                table: "InterestPerson",
                newName: "PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_InterestPerson_PersonsId",
                table: "InterestPerson",
                newName: "IX_InterestPerson_PeopleId");

            migrationBuilder.AddForeignKey(
                name: "FK_InterestPerson_Persons_PeopleId",
                table: "InterestPerson",
                column: "PeopleId",
                principalTable: "Persons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
