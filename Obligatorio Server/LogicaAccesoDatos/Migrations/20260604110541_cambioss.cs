using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LogicaAccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class cambioss : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Auditorias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "PrestamoId",
                table: "Auditorias",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias");

            migrationBuilder.DropForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias");

            migrationBuilder.AlterColumn<int>(
                name: "UsuarioId",
                table: "Auditorias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PrestamoId",
                table: "Auditorias",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Prestamos_PrestamoId",
                table: "Auditorias",
                column: "PrestamoId",
                principalTable: "Prestamos",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Auditorias_Usuarios_UsuarioId",
                table: "Auditorias",
                column: "UsuarioId",
                principalTable: "Usuarios",
                principalColumn: "Id");
        }
    }
}
