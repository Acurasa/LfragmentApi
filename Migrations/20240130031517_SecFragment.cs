using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LfragmentApi.Migrations
{
    /// <inheritdoc />
    public partial class SecFragment : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FragmentTag");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.AddColumn<List<string>>(
                name: "Tags",
                table: "Fragments",
                type: "text[]",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tags",
                table: "Fragments");

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Name = table.Column<string>(type: "text", nullable: false),
                    Color = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "FragmentTag",
                columns: table => new
                {
                    FragmentId = table.Column<Guid>(type: "uuid", nullable: false),
                    TagsName = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FragmentTag", x => new { x.FragmentId, x.TagsName });
                    table.ForeignKey(
                        name: "FK_FragmentTag_Fragments_FragmentId",
                        column: x => x.FragmentId,
                        principalTable: "Fragments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FragmentTag_Tags_TagsName",
                        column: x => x.TagsName,
                        principalTable: "Tags",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FragmentTag_TagsName",
                table: "FragmentTag",
                column: "TagsName");
        }
    }
}
