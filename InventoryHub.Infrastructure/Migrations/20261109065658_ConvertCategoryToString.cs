using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InventoryHub.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConvertCategoryToString : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Plain AlterColumn<string> would cast existing ints to their digit string
            // (e.g. 1 -> "1") instead of the enum name (e.g. "Laptop"), corrupting
            // already-seeded rows. Remap values as part of the type change instead.
            migrationBuilder.Sql(@"
                ALTER TABLE ""Products"" ALTER COLUMN ""Category"" TYPE text USING (
                    CASE ""Category""
                        WHEN 0 THEN 'Phone'
                        WHEN 1 THEN 'Laptop'
                        WHEN 2 THEN 'Console'
                        WHEN 3 THEN 'Headphones'
                        WHEN 4 THEN 'Tablet'
                        WHEN 5 THEN 'PC'
                        WHEN 6 THEN 'Other'
                    END
                );");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"
                ALTER TABLE ""Products"" ALTER COLUMN ""Category"" TYPE integer USING (
                    CASE ""Category""
                        WHEN 'Phone' THEN 0
                        WHEN 'Laptop' THEN 1
                        WHEN 'Console' THEN 2
                        WHEN 'Headphones' THEN 3
                        WHEN 'Tablet' THEN 4
                        WHEN 'PC' THEN 5
                        WHEN 'Other' THEN 6
                    END
                );");
        }
    }
}
