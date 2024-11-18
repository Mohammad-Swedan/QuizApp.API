using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForAndroid.DAL.Migrations
{
    /// <inheritdoc />
    public partial class editQuizLikeDisLike : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedDate",
                table: "QuizLikesDislikes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedDate",
                table: "QuizLikesDislikes");
        }
    }
}
