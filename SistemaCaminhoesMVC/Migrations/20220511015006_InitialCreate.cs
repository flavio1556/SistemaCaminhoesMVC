using Microsoft.EntityFrameworkCore.Migrations;

namespace SistemaCaminhoesMVC.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Model",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Model", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "veiculo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    YearManufacture = table.Column<int>(nullable: false),
                    ModelYear = table.Column<int>(nullable: false),
                    VehicleType = table.Column<int>(nullable: false),
                    Chassi = table.Column<string>(nullable: true),
                    ModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_veiculo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_veiculo_Model_ModelId",
                        column: x => x.ModelId,
                        principalTable: "Model",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_veiculo_ModelId",
                table: "veiculo",
                column: "ModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "veiculo");

            migrationBuilder.DropTable(
                name: "Model");
        }
    }
}
