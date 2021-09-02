using Microsoft.EntityFrameworkCore.Migrations;

namespace Test1.Migrations
{
    public partial class tableBuyerChange6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ToOtherPhoneNo",
                table: "Buyers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ToOtherPhoneNo",
                table: "Buyers",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
