using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ProductApplication.Migrations
{
    /// <inheritdoc />
    public partial class descmigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "productDescription",
                table: "Products",
                type: "varchar(700)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "productDescription",
                table: "Products");
        }
    }
}
