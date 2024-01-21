using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIMessageTrigger.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class Alterandoentities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Qrcode",
                table: "MessageTrigger",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Qrcode",
                table: "MessageTrigger");
        }
    }
}
