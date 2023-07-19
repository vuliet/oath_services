using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Auth.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpenIddictEntityFrameworkCoreApplications",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ClientId = table.Column<string>(type: "longtext", nullable: true),
                    ClientSecret = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "longtext", nullable: true),
                    ConsentType = table.Column<string>(type: "longtext", nullable: true),
                    DisplayName = table.Column<string>(type: "longtext", nullable: true),
                    DisplayNames = table.Column<string>(type: "longtext", nullable: true),
                    Permissions = table.Column<string>(type: "longtext", nullable: true),
                    PostLogoutRedirectUris = table.Column<string>(type: "longtext", nullable: true),
                    Properties = table.Column<string>(type: "longtext", nullable: true),
                    RedirectUris = table.Column<string>(type: "longtext", nullable: true),
                    Requirements = table.Column<string>(type: "longtext", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictEntityFrameworkCoreApplications", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpenIddictEntityFrameworkCoreScopes",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ConcurrencyToken = table.Column<string>(type: "longtext", nullable: true),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Descriptions = table.Column<string>(type: "longtext", nullable: true),
                    DisplayName = table.Column<string>(type: "longtext", nullable: true),
                    DisplayNames = table.Column<string>(type: "longtext", nullable: true),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    Properties = table.Column<string>(type: "longtext", nullable: true),
                    Resources = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictEntityFrameworkCoreScopes", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedName = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    IsDeleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedUserName = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "longtext", nullable: true),
                    NormalizedEmail = table.Column<string>(type: "longtext", nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    PasswordHash = table.Column<string>(type: "longtext", nullable: true),
                    SecurityStamp = table.Column<string>(type: "longtext", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumber = table.Column<string>(type: "longtext", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserTokens",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    TokenName = table.Column<string>(type: "longtext", nullable: true),
                    TokenValue = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<Guid>(type: "char(36)", nullable: false),
                    LoginProvider = table.Column<string>(type: "longtext", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpenIddictEntityFrameworkCoreAuthorizations",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ApplicationId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "longtext", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Properties = table.Column<string>(type: "longtext", nullable: true),
                    Scopes = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: true),
                    Subject = table.Column<string>(type: "longtext", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictEntityFrameworkCoreAuthorizations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictEntityFrameworkCoreAuthorizations_OpenIddictEntity~",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictEntityFrameworkCoreApplications",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true),
                    Value = table.Column<string>(type: "longtext", nullable: true),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserClaims",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    Type = table.Column<string>(type: "longtext", nullable: true),
                    Value = table.Column<string>(type: "longtext", nullable: true),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UserRoles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserId = table.Column<string>(type: "varchar(255)", nullable: true),
                    RoleId = table.Column<string>(type: "varchar(255)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    CreatedBy = table.Column<string>(type: "longtext", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    LastModifiedBy = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_UserRoles_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OpenIddictEntityFrameworkCoreTokens",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false),
                    ApplicationId = table.Column<string>(type: "varchar(255)", nullable: true),
                    AuthorizationId = table.Column<string>(type: "varchar(255)", nullable: true),
                    ConcurrencyToken = table.Column<string>(type: "longtext", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ExpirationDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    Payload = table.Column<string>(type: "longtext", nullable: true),
                    Properties = table.Column<string>(type: "longtext", nullable: true),
                    RedemptionDate = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ReferenceId = table.Column<string>(type: "longtext", nullable: true),
                    Status = table.Column<string>(type: "longtext", nullable: true),
                    Subject = table.Column<string>(type: "longtext", nullable: true),
                    Type = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenIddictEntityFrameworkCoreTokens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenIddictEntityFrameworkCoreTokens_OpenIddictEntityFramewor~",
                        column: x => x.ApplicationId,
                        principalTable: "OpenIddictEntityFrameworkCoreApplications",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_OpenIddictEntityFrameworkCoreTokens_OpenIddictEntityFramewo~1",
                        column: x => x.AuthorizationId,
                        principalTable: "OpenIddictEntityFrameworkCoreAuthorizations",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictEntityFrameworkCoreAuthorizations_ApplicationId",
                table: "OpenIddictEntityFrameworkCoreAuthorizations",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictEntityFrameworkCoreTokens_ApplicationId",
                table: "OpenIddictEntityFrameworkCoreTokens",
                column: "ApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_OpenIddictEntityFrameworkCoreTokens_AuthorizationId",
                table: "OpenIddictEntityFrameworkCoreTokens",
                column: "AuthorizationId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_UserId",
                table: "UserRoles",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OpenIddictEntityFrameworkCoreScopes");

            migrationBuilder.DropTable(
                name: "OpenIddictEntityFrameworkCoreTokens");

            migrationBuilder.DropTable(
                name: "RoleClaims");

            migrationBuilder.DropTable(
                name: "UserClaims");

            migrationBuilder.DropTable(
                name: "UserRoles");

            migrationBuilder.DropTable(
                name: "UserTokens");

            migrationBuilder.DropTable(
                name: "OpenIddictEntityFrameworkCoreAuthorizations");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "OpenIddictEntityFrameworkCoreApplications");
        }
    }
}
