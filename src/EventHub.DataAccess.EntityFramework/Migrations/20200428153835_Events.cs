using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace EventHub.DataAccess.EntityFramework.Migrations
{
    public partial class Events : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByUserId = table.Column<string>(nullable: true),
                    Name = table.Column<string>(maxLength: 200, nullable: false),
                    Description = table.Column<string>(maxLength: 600, nullable: true),
                    StartDate = table.Column<DateTimeOffset>(nullable: false),
                    EndDate = table.Column<DateTimeOffset>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Events_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "EventMembers",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByUserId = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    IsAdmin = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventMembers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EventMembers_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventMembers_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EventMembers_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_EventMembers_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Messages",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: false),
                    CreatedByUserId = table.Column<string>(nullable: true),
                    ModifiedAt = table.Column<DateTimeOffset>(nullable: true),
                    ModifiedByUserId = table.Column<string>(nullable: true),
                    EventId = table.Column<Guid>(nullable: false),
                    Text = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Messages", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_CreatedByUserId",
                        column: x => x.CreatedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Messages_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Messages_AspNetUsers_ModifiedByUserId",
                        column: x => x.ModifiedByUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventMembers_CreatedByUserId",
                table: "EventMembers",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMembers_ModifiedByUserId",
                table: "EventMembers",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMembers_UserId",
                table: "EventMembers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_EventMembers_EventId_UserId",
                table: "EventMembers",
                columns: new[] { "EventId", "UserId" },
                unique: true,
                filter: "[UserId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Events_CreatedByUserId",
                table: "Events",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_ModifiedByUserId",
                table: "Events",
                column: "ModifiedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_CreatedByUserId",
                table: "Messages",
                column: "CreatedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_EventId",
                table: "Messages",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Messages_ModifiedByUserId",
                table: "Messages",
                column: "ModifiedByUserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EventMembers");

            migrationBuilder.DropTable(
                name: "Messages");

            migrationBuilder.DropTable(
                name: "Events");
        }
    }
}
