using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Blog.Migrations
{
    public partial class test444 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntity_BlogPosts_BlogPostId",
                table: "FileEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity");

            migrationBuilder.RenameTable(
                name: "FileEntity",
                newName: "FileEntities");

            migrationBuilder.RenameIndex(
                name: "IX_FileEntity_BlogPostId",
                table: "FileEntities",
                newName: "IX_FileEntities_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileEntities",
                table: "FileEntities",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntities_BlogPosts_BlogPostId",
                table: "FileEntities",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FileEntities_BlogPosts_BlogPostId",
                table: "FileEntities");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FileEntities",
                table: "FileEntities");

            migrationBuilder.RenameTable(
                name: "FileEntities",
                newName: "FileEntity");

            migrationBuilder.RenameIndex(
                name: "IX_FileEntities_BlogPostId",
                table: "FileEntity",
                newName: "IX_FileEntity_BlogPostId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FileEntity",
                table: "FileEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_FileEntity_BlogPosts_BlogPostId",
                table: "FileEntity",
                column: "BlogPostId",
                principalTable: "BlogPosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
