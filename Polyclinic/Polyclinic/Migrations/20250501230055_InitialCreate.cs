using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Polyclinic.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    IdDoctor = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: true),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Specialization = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Experience = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    WorkPhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    WorkSchedule = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    PersonalCabinet = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.IdDoctor);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Role = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.IdUser);
                });

            migrationBuilder.CreateTable(
                name: "DoctorSchedules",
                columns: table => new
                {
                    IdSchedule = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdDoctor = table.Column<int>(type: "INTEGER", nullable: true),
                    WorkingDay = table.Column<DateTime>(type: "TEXT", nullable: false),
                    StartTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DoctorSchedules", x => x.IdSchedule);
                    table.ForeignKey(
                        name: "FK_DoctorSchedules_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "IdDoctor",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Patients",
                columns: table => new
                {
                    IdPatient = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdUser = table.Column<int>(type: "INTEGER", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Address = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Gender = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "TEXT", nullable: false),
                    PhoneNumber = table.Column<string>(type: "TEXT", maxLength: 20, nullable: false),
                    Email = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    AccountActivated = table.Column<bool>(type: "INTEGER", nullable: false),
                    UserIdUser = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patients", x => x.IdPatient);
                    table.ForeignKey(
                        name: "FK_Patients_Users_IdUser",
                        column: x => x.IdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                    table.ForeignKey(
                        name: "FK_Patients_Users_UserIdUser",
                        column: x => x.UserIdUser,
                        principalTable: "Users",
                        principalColumn: "IdUser");
                });

            migrationBuilder.CreateTable(
                name: "AppointmentRequests",
                columns: table => new
                {
                    IdRequest = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPatient = table.Column<int>(type: "INTEGER", nullable: true),
                    IdDoctor = table.Column<int>(type: "INTEGER", nullable: true),
                    RequestDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    AppointmentDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Reason = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Status = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DoctorComment = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppointmentRequests", x => x.IdRequest);
                    table.ForeignKey(
                        name: "FK_AppointmentRequests_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "IdDoctor");
                    table.ForeignKey(
                        name: "FK_AppointmentRequests_Patients_IdPatient",
                        column: x => x.IdPatient,
                        principalTable: "Patients",
                        principalColumn: "IdPatient");
                });

            migrationBuilder.CreateTable(
                name: "MedicalCards",
                columns: table => new
                {
                    IdCard = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    IdPatient = table.Column<int>(type: "INTEGER", nullable: true),
                    IdDoctor = table.Column<int>(type: "INTEGER", nullable: true),
                    DiagnosisDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DiseaseName = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    Treatment = table.Column<string>(type: "TEXT", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MedicalCards", x => x.IdCard);
                    table.ForeignKey(
                        name: "FK_MedicalCards_Doctors_IdDoctor",
                        column: x => x.IdDoctor,
                        principalTable: "Doctors",
                        principalColumn: "IdDoctor");
                    table.ForeignKey(
                        name: "FK_MedicalCards_Patients_IdPatient",
                        column: x => x.IdPatient,
                        principalTable: "Patients",
                        principalColumn: "IdPatient");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_IdDoctor",
                table: "AppointmentRequests",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_AppointmentRequests_IdPatient",
                table: "AppointmentRequests",
                column: "IdPatient");

            migrationBuilder.CreateIndex(
                name: "IX_DoctorSchedules_IdDoctor",
                table: "DoctorSchedules",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCards_IdDoctor",
                table: "MedicalCards",
                column: "IdDoctor");

            migrationBuilder.CreateIndex(
                name: "IX_MedicalCards_IdPatient",
                table: "MedicalCards",
                column: "IdPatient");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_IdUser",
                table: "Patients",
                column: "IdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Patients_UserIdUser",
                table: "Patients",
                column: "UserIdUser");

            migrationBuilder.CreateIndex(
                name: "IX_Users_Username",
                table: "Users",
                column: "Username",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppointmentRequests");

            migrationBuilder.DropTable(
                name: "DoctorSchedules");

            migrationBuilder.DropTable(
                name: "MedicalCards");

            migrationBuilder.DropTable(
                name: "Doctors");

            migrationBuilder.DropTable(
                name: "Patients");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
