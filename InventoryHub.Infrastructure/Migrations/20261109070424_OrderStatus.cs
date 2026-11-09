using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class OrderStatus : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Plain AlterColumn<string> would cast existing ints to their digit string
            // (e.g. 0 -> "0") instead of the enum name (e.g. "Pending"), corrupting
            // already-seeded rows. Remap values as part of the type change instead.
            migrationBuilder.Sql(@"
                ALTER TABLE ""Orders"" ALTER COLUMN ""Status"" TYPE text USING (
                    CASE ""Status""
                        WHEN 0 THEN 'Pending'
                        WHEN 1 THEN 'Fulfilled'
                        WHEN 2 THEN 'Cancelled'
                    END
                );");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Orders"" ALTER COLUMN ""Status"" TYPE integer USING (
                    CASE ""Status""
                        WHEN 'Pending' THEN 0
                        WHEN 'Fulfilled' THEN 1
                        WHEN 'Cancelled' THEN 2
                    END
                );");
        }
    }
}
