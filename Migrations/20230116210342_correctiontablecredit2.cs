using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MsgFoundation.Migrations
{
    /// <inheritdoc />
    public partial class correctiontablecredit2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statusCredit",
                table: "Credits",
                newName: "StatusCredit");

            migrationBuilder.RenameColumn(
                name: "disbursed",
                table: "Credits",
                newName: "Disbursed");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "StatusCredit",
                table: "Credits",
                newName: "statusCredit");

            migrationBuilder.RenameColumn(
                name: "Disbursed",
                table: "Credits",
                newName: "disbursed");
        }
    }
}
