using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KingdomAdventure.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EnemyNPCs",
                columns: table => new
                {
                    EnemyNPCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Boss = table.Column<bool>(type: "bit", nullable: false),
                    EnemyNPCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    CurrentLP = table.Column<double>(type: "float", nullable: false),
                    FullLP = table.Column<double>(type: "float", nullable: false),
                    AtkMelee = table.Column<double>(type: "float", nullable: true),
                    AtkPierce = table.Column<double>(type: "float", nullable: true),
                    AtkMagic = table.Column<double>(type: "float", nullable: true),
                    DefMelee = table.Column<double>(type: "float", nullable: true),
                    DefPierce = table.Column<double>(type: "float", nullable: true),
                    DefMagic = table.Column<double>(type: "float", nullable: true),
                    Crit = table.Column<double>(type: "float", nullable: true),
                    CritDmg = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EnemyNPCs", x => x.EnemyNPCID);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryID);
                });

            migrationBuilder.CreateTable(
                name: "UpgradeItem",
                columns: table => new
                {
                    UpgradeItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerGold = table.Column<double>(type: "float", nullable: true),
                    Experience = table.Column<double>(type: "float", nullable: true),
                    LP = table.Column<double>(type: "float", nullable: true),
                    FullLP = table.Column<double>(type: "float", nullable: true),
                    AtkMelee = table.Column<double>(type: "float", nullable: true),
                    AtkPierce = table.Column<double>(type: "float", nullable: true),
                    AtkMagic = table.Column<double>(type: "float", nullable: true),
                    DefMelee = table.Column<double>(type: "float", nullable: true),
                    DefPierce = table.Column<double>(type: "float", nullable: true),
                    DefMagic = table.Column<double>(type: "float", nullable: true),
                    Str = table.Column<double>(type: "float", nullable: true),
                    End = table.Column<double>(type: "float", nullable: true),
                    Dex = table.Column<double>(type: "float", nullable: true),
                    Int = table.Column<double>(type: "float", nullable: true),
                    Crit = table.Column<double>(type: "float", nullable: true),
                    CritDmg = table.Column<double>(type: "float", nullable: true),
                    ItemType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UpgradeItem", x => x.UpgradeItemID);
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerGold = table.Column<double>(type: "float", nullable: true),
                    Experience = table.Column<double>(type: "float", nullable: true),
                    LP = table.Column<double>(type: "float", nullable: true),
                    FullLP = table.Column<double>(type: "float", nullable: true),
                    AtkMelee = table.Column<double>(type: "float", nullable: true),
                    AtkPierce = table.Column<double>(type: "float", nullable: true),
                    AtkMagic = table.Column<double>(type: "float", nullable: true),
                    DefMelee = table.Column<double>(type: "float", nullable: true),
                    DefPierce = table.Column<double>(type: "float", nullable: true),
                    DefMagic = table.Column<double>(type: "float", nullable: true),
                    Str = table.Column<double>(type: "float", nullable: true),
                    End = table.Column<double>(type: "float", nullable: true),
                    Dex = table.Column<double>(type: "float", nullable: true),
                    Int = table.Column<double>(type: "float", nullable: true),
                    Crit = table.Column<double>(type: "float", nullable: true),
                    CritDmg = table.Column<double>(type: "float", nullable: true),
                    UpgradeItem1UpgradeItemID = table.Column<int>(type: "int", nullable: true),
                    UpgradeItem2UpgradeItemID = table.Column<int>(type: "int", nullable: true),
                    UpgradeItem3UpgradeItemID = table.Column<int>(type: "int", nullable: true),
                    UpgradeItem4UpgradeItemID = table.Column<int>(type: "int", nullable: true),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: true),
                    ArmorType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Items_UpgradeItem_UpgradeItem1UpgradeItemID",
                        column: x => x.UpgradeItem1UpgradeItemID,
                        principalTable: "UpgradeItem",
                        principalColumn: "UpgradeItemID");
                    table.ForeignKey(
                        name: "FK_Items_UpgradeItem_UpgradeItem2UpgradeItemID",
                        column: x => x.UpgradeItem2UpgradeItemID,
                        principalTable: "UpgradeItem",
                        principalColumn: "UpgradeItemID");
                    table.ForeignKey(
                        name: "FK_Items_UpgradeItem_UpgradeItem3UpgradeItemID",
                        column: x => x.UpgradeItem3UpgradeItemID,
                        principalTable: "UpgradeItem",
                        principalColumn: "UpgradeItemID");
                    table.ForeignKey(
                        name: "FK_Items_UpgradeItem_UpgradeItem4UpgradeItemID",
                        column: x => x.UpgradeItem4UpgradeItemID,
                        principalTable: "UpgradeItem",
                        principalColumn: "UpgradeItemID");
                });

            migrationBuilder.CreateTable(
                name: "Player",
                columns: table => new
                {
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PlayerGold = table.Column<double>(type: "float", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    Experience = table.Column<double>(type: "float", nullable: false),
                    CurrentLP = table.Column<double>(type: "float", nullable: false),
                    FullLP = table.Column<double>(type: "float", nullable: false),
                    AtkMelee = table.Column<double>(type: "float", nullable: false),
                    AtkPierce = table.Column<double>(type: "float", nullable: false),
                    AtkMagic = table.Column<double>(type: "float", nullable: false),
                    DefMelee = table.Column<double>(type: "float", nullable: false),
                    DefPierce = table.Column<double>(type: "float", nullable: false),
                    DefMagic = table.Column<double>(type: "float", nullable: false),
                    Str = table.Column<double>(type: "float", nullable: false),
                    End = table.Column<double>(type: "float", nullable: false),
                    Dex = table.Column<double>(type: "float", nullable: false),
                    Int = table.Column<double>(type: "float", nullable: false),
                    Crit = table.Column<double>(type: "float", nullable: false),
                    CritDmg = table.Column<double>(type: "float", nullable: false),
                    MainhandItemID = table.Column<int>(type: "int", nullable: true),
                    OffhandItemID = table.Column<int>(type: "int", nullable: true),
                    HeadItemID = table.Column<int>(type: "int", nullable: true),
                    ShoulderItemID = table.Column<int>(type: "int", nullable: true),
                    ChestItemID = table.Column<int>(type: "int", nullable: true),
                    TrousersItemID = table.Column<int>(type: "int", nullable: true),
                    FeetItemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Player", x => x.PlayerID);
                    table.ForeignKey(
                        name: "FK_Player_Items_ChestItemID",
                        column: x => x.ChestItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_FeetItemID",
                        column: x => x.FeetItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_HeadItemID",
                        column: x => x.HeadItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_MainhandItemID",
                        column: x => x.MainhandItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_OffhandItemID",
                        column: x => x.OffhandItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_ShoulderItemID",
                        column: x => x.ShoulderItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Items_TrousersItemID",
                        column: x => x.TrousersItemID,
                        principalTable: "Items",
                        principalColumn: "ItemID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeItem1UpgradeItemID",
                table: "Items",
                column: "UpgradeItem1UpgradeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeItem2UpgradeItemID",
                table: "Items",
                column: "UpgradeItem2UpgradeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeItem3UpgradeItemID",
                table: "Items",
                column: "UpgradeItem3UpgradeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Items_UpgradeItem4UpgradeItemID",
                table: "Items",
                column: "UpgradeItem4UpgradeItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ChestItemID",
                table: "Player",
                column: "ChestItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_FeetItemID",
                table: "Player",
                column: "FeetItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_HeadItemID",
                table: "Player",
                column: "HeadItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_MainhandItemID",
                table: "Player",
                column: "MainhandItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_OffhandItemID",
                table: "Player",
                column: "OffhandItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_ShoulderItemID",
                table: "Player",
                column: "ShoulderItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TrousersItemID",
                table: "Player",
                column: "TrousersItemID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnemyNPCs");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "UpgradeItem");
        }
    }
}
