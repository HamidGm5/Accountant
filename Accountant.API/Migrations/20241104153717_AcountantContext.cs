using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Accountant.API.Migrations
{
    /// <inheritdoc />
    public partial class AcountantContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "Users",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "PaymentTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "AdminId",
                table: "IncomeTransactions",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AdminAlias = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPassword = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminFirstname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminLastname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    NationalCardImgURL = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AdminPhoto = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AdminId",
                table: "Users",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_PaymentTransactions_AdminId",
                table: "PaymentTransactions",
                column: "AdminId");

            migrationBuilder.CreateIndex(
                name: "IX_IncomeTransactions_AdminId",
                table: "IncomeTransactions",
                column: "AdminId");

            migrationBuilder.AddForeignKey(
                name: "FK_IncomeTransactions_Admins_AdminId",
                table: "IncomeTransactions",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PaymentTransactions_Admins_AdminId",
                table: "PaymentTransactions",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users",
                column: "AdminId",
                principalTable: "Admins",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_IncomeTransactions_Admins_AdminId",
                table: "IncomeTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_PaymentTransactions_Admins_AdminId",
                table: "PaymentTransactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Admins_AdminId",
                table: "Users");

            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropIndex(
                name: "IX_Users_AdminId",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_PaymentTransactions_AdminId",
                table: "PaymentTransactions");

            migrationBuilder.DropIndex(
                name: "IX_IncomeTransactions_AdminId",
                table: "IncomeTransactions");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "PaymentTransactions");

            migrationBuilder.DropColumn(
                name: "AdminId",
                table: "IncomeTransactions");
        }
    }
}
