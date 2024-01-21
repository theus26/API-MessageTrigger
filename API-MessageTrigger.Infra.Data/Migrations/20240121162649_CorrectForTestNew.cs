using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace APIMessageTrigger.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class CorrectForTestNew : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "teste",
                table: "MessageTrigger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "teste",
                table: "MessageTrigger",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
