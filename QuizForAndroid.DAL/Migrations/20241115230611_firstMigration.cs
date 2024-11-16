using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace QuizForAndroid.DAL.Migrations
{
    /// <inheritdoc />
    public partial class firstMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "QuestionTypeInfo",
                columns: table => new
                {
                    QuestionTypeID = table.Column<int>(type: "int", nullable: false),
                    TypeName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__7EDFF911433D551E", x => x.QuestionTypeID);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    RoleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Roles__8AFACE3A7393B002", x => x.RoleID);
                });

            migrationBuilder.CreateTable(
                name: "Universities",
                columns: table => new
                {
                    UniversityID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UniversityName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Universi__9F19E19C4193A149", x => x.UniversityID);
                });

            migrationBuilder.CreateTable(
                name: "Colleges",
                columns: table => new
                {
                    CollegeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CollegeName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Colleges__294095192E4C90E1", x => x.CollegeID);
                    table.ForeignKey(
                        name: "FK__Colleges__Univer__3E52440B",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID");
                });

            migrationBuilder.CreateTable(
                name: "Materials",
                columns: table => new
                {
                    MaterialID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaterialName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Material__C506131773962DC6", x => x.MaterialID);
                    table.ForeignKey(
                        name: "FK__Materials__Colle__4D94879B",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID");
                });

            migrationBuilder.CreateTable(
                name: "Specializations",
                columns: table => new
                {
                    SpecializationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SpecializationName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Speciali__5809D84F9753427C", x => x.SpecializationID);
                    table.ForeignKey(
                        name: "FK__Specializ__Colle__412EB0B6",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID");
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "varchar(50)", unicode: false, maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    UniversityID = table.Column<int>(type: "int", nullable: false),
                    CollegeID = table.Column<int>(type: "int", nullable: false),
                    SpecializationID = table.Column<int>(type: "int", nullable: false),
                    RoleID = table.Column<int>(type: "int", nullable: false, defaultValue: 1),
                    IsTrusted = table.Column<bool>(type: "bit", nullable: false),
                    IsDoctor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Users__1788CCACEEB9558A", x => x.UserID);
                    table.ForeignKey(
                        name: "FK__Users__CollegeID__48CFD27E",
                        column: x => x.CollegeID,
                        principalTable: "Colleges",
                        principalColumn: "CollegeID");
                    table.ForeignKey(
                        name: "FK__Users__RoleID__4AB81AF0",
                        column: x => x.RoleID,
                        principalTable: "Roles",
                        principalColumn: "RoleID");
                    table.ForeignKey(
                        name: "FK__Users__Specializ__49C3F6B7",
                        column: x => x.SpecializationID,
                        principalTable: "Specializations",
                        principalColumn: "SpecializationID");
                    table.ForeignKey(
                        name: "FK__Users__Universit__47DBAE45",
                        column: x => x.UniversityID,
                        principalTable: "Universities",
                        principalColumn: "UniversityID");
                });

            migrationBuilder.CreateTable(
                name: "Quizzes",
                columns: table => new
                {
                    QuizID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: false),
                    Description = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: true),
                    WriterID = table.Column<int>(type: "int", nullable: false),
                    MaterialID = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    IsDraft = table.Column<bool>(type: "bit", nullable: false),
                    IsTrusted = table.Column<bool>(type: "bit", nullable: false),
                    IsDoctor = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Quizzes__8B42AE6EDA3B402B", x => x.QuizID);
                    table.ForeignKey(
                        name: "FK__Quizzes__Materia__5441852A",
                        column: x => x.MaterialID,
                        principalTable: "Materials",
                        principalColumn: "MaterialID");
                    table.ForeignKey(
                        name: "FK__Quizzes__WriterI__534D60F1",
                        column: x => x.WriterID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "WriterApplications",
                columns: table => new
                {
                    ApplicationID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserID = table.Column<int>(type: "int", nullable: false),
                    FullName = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false),
                    ApplicationDate = table.Column<DateTime>(type: "datetime", nullable: false, defaultValueSql: "(getdate())"),
                    Status = table.Column<string>(type: "varchar(20)", unicode: false, maxLength: 20, nullable: false, defaultValue: "Pending"),
                    ReviewedBy = table.Column<int>(type: "int", nullable: true),
                    ReviewedDate = table.Column<DateTime>(type: "datetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__WriterAp__C93A4F79EF163886", x => x.ApplicationID);
                    table.ForeignKey(
                        name: "FK__WriterApp__Revie__787EE5A0",
                        column: x => x.ReviewedBy,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK__WriterApp__UserI__778AC167",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Drafts",
                columns: table => new
                {
                    DraftID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Drafts__3E93D63BE72B0711", x => x.DraftID);
                    table.ForeignKey(
                        name: "FK__Drafts__QuizID__656C112C",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID");
                });

            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    QuestionID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuizID = table.Column<int>(type: "int", nullable: false),
                    QuestionText = table.Column<string>(type: "varchar(1000)", unicode: false, maxLength: 1000, nullable: false),
                    QuestionType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Question__0DC06F8C543A4A48", x => x.QuestionID);
                    table.ForeignKey(
                        name: "FK__Questions__QuizI__59FA5E80",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID");
                });

            migrationBuilder.CreateTable(
                name: "QuizLikesDislikes",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false),
                    QuizID = table.Column<int>(type: "int", nullable: false),
                    IsLike = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__QuizLike__EF3CE64A82C55CFC", x => new { x.UserID, x.QuizID });
                    table.ForeignKey(
                        name: "FK__QuizLikes__QuizI__628FA481",
                        column: x => x.QuizID,
                        principalTable: "Quizzes",
                        principalColumn: "QuizID");
                    table.ForeignKey(
                        name: "FK__QuizLikes__UserI__619B8048",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateTable(
                name: "Choices",
                columns: table => new
                {
                    ChoiceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    QuestionID = table.Column<int>(type: "int", nullable: false),
                    ChoiceText = table.Column<string>(type: "varchar(500)", unicode: false, maxLength: 500, nullable: false),
                    IsCorrect = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Choices__76F51686BD01D378", x => x.ChoiceID);
                    table.ForeignKey(
                        name: "FK__Choices__Questio__5EBF139D",
                        column: x => x.QuestionID,
                        principalTable: "Questions",
                        principalColumn: "QuestionID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Choices_QuestionID",
                table: "Choices",
                column: "QuestionID");

            migrationBuilder.CreateIndex(
                name: "IX_Colleges_UniversityID",
                table: "Colleges",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "IX_Drafts_QuizID",
                table: "Drafts",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Materials_CollegeID",
                table: "Materials",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_Questions_QuizID",
                table: "Questions",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "UQ__Question__D4E7DFA8393065AB",
                table: "QuestionTypeInfo",
                column: "TypeName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_QuizLikesDislikes_QuizID",
                table: "QuizLikesDislikes",
                column: "QuizID");

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_MaterialID_IsDraft_CreatedDate",
                table: "Quizzes",
                columns: new[] { "MaterialID", "IsDraft", "CreatedDate" },
                descending: new[] { false, false, true });

            migrationBuilder.CreateIndex(
                name: "IX_Quizzes_WriterID",
                table: "Quizzes",
                column: "WriterID");

            migrationBuilder.CreateIndex(
                name: "UQ__Roles__8A2B61603D996F14",
                table: "Roles",
                column: "RoleName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Specializations_CollegeID",
                table: "Specializations",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "UQ__Universi__53F0B53C07D8FA45",
                table: "Universities",
                column: "UniversityName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_CollegeID",
                table: "Users",
                column: "CollegeID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_RoleID",
                table: "Users",
                column: "RoleID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_SpecializationID",
                table: "Users",
                column: "SpecializationID");

            migrationBuilder.CreateIndex(
                name: "IX_Users_UniversityID",
                table: "Users",
                column: "UniversityID");

            migrationBuilder.CreateIndex(
                name: "UQ__Users__A9D10534A35D02CE",
                table: "Users",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WriterApplications_ReviewedBy",
                table: "WriterApplications",
                column: "ReviewedBy");

            migrationBuilder.CreateIndex(
                name: "IX_WriterApplications_UserID",
                table: "WriterApplications",
                column: "UserID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Choices");

            migrationBuilder.DropTable(
                name: "Drafts");

            migrationBuilder.DropTable(
                name: "QuestionTypeInfo");

            migrationBuilder.DropTable(
                name: "QuizLikesDislikes");

            migrationBuilder.DropTable(
                name: "WriterApplications");

            migrationBuilder.DropTable(
                name: "Questions");

            migrationBuilder.DropTable(
                name: "Quizzes");

            migrationBuilder.DropTable(
                name: "Materials");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Specializations");

            migrationBuilder.DropTable(
                name: "Colleges");

            migrationBuilder.DropTable(
                name: "Universities");
        }
    }
}
