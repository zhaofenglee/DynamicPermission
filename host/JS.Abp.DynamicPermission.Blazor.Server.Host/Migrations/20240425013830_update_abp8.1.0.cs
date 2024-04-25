using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace JS.Abp.DynamicPermission.Blazor.Server.Host.Migrations
{
    /// <inheritdoc />
    public partial class update_abp810 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "NormalizedName",
                table: "AbpTenants",
                type: "nvarchar(64)",
                maxLength: 64,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants",
                column: "NormalizedName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_AbpTenants_NormalizedName",
                table: "AbpTenants");

            migrationBuilder.DropColumn(
                name: "NormalizedName",
                table: "AbpTenants");
        }
    }
}
