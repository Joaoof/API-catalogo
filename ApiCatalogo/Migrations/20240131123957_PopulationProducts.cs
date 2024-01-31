using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ApiCatalogo.Migrations
{
    /// <inheritdoc />
    public partial class PopulationProducts : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Insert into Products(Name, Description, Price, ImageUrl, Stock, DataRegister, CategoryId) Values ('Coca-Cola Diet', 'Refrigerante de Coca 350ml', 5.45, 'cocacola.jpg', 50, now(), 1)");

            migrationBuilder.Sql("Insert into Products(Name, Description, Price, ImageUrl, Stock, DataRegister, CategoryId) Values ('Teste','Teste',5.45,'teste.jpg',50,now(),2)");

            migrationBuilder.Sql("Insert into Products(Name, Description, Price, ImageUrl, Stock, DataRegister, CategoryId) Values ('Teste','Teste',5.45,'teste.jpg',50,now(),3)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("Delete from Products");
        }
    }
}
