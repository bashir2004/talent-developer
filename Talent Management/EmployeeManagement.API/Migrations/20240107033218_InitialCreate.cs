using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManagement.API.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Employees",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SkillSet = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Hobbies = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "NEWID()"),
                    FirstName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Salt = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    UpdatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true, defaultValueSql: "'00000000-0000-0000-0000-000000000000'"),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true, defaultValueSql: "getdate()"),
                    ExpiresAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Employees",
                columns: new[] { "Id", "CreatedAt", "Email", "Hobbies", "PhoneNumber", "SkillSet", "UpdatedAt", "UpdatedBy", "Username" },
                values: new object[] { new Guid("531fd700-37c7-47d6-9055-8474f62be903"), new DateTime(2024, 1, 7, 11, 32, 18, 164, DateTimeKind.Local).AddTicks(8996), "someone@example.com", "PLaying Games", "+60182458049", "AspNet, Angular, React", new DateTime(2024, 1, 7, 11, 32, 18, 164, DateTimeKind.Local).AddTicks(8997), new Guid("00000000-0000-0000-0000-000000000000"), "JohnDoe" });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "CreatedAt", "CreatedBy", "Email", "ExpiresAt", "FirstName", "LastName", "Password", "Salt" },
                values: new object[,]
                {
                    { new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"), new DateTime(2024, 1, 7, 11, 32, 18, 164, DateTimeKind.Local).AddTicks(8907), new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"), "faisal@gmail.com", null, "Faisal", "Shahzad", "Faisal@123", "LKDJ03230" },
                    { new Guid("cfae8354-c7f7-4bd7-af31-43fa991b078e"), new DateTime(2024, 1, 7, 11, 32, 18, 164, DateTimeKind.Local).AddTicks(8918), new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"), "tuan@gmail.com", null, "Le", "Tuan", "Faisal@123", "LKDJ03230" },
                    { new Guid("d0413c86-36c6-486b-982d-b13fa76b90b9"), new DateTime(2024, 1, 7, 11, 32, 18, 164, DateTimeKind.Local).AddTicks(8921), new Guid("a61c0906-b308-4f6e-b860-28f4e3ee8713"), "marc@gmail.com", null, "Marc", "Josha", "Faisal@123", "LKDJ03230" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
