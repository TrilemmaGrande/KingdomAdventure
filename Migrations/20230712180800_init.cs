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
                name: "Building",
                columns: table => new
                {
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BuildingName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Building", x => x.BuildingID);
                });

            migrationBuilder.CreateTable(
                name: "EnemyNPC",
                columns: table => new
                {
                    EnemyNPCID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Boss = table.Column<bool>(type: "bit", nullable: false),
                    EnemyNPCName = table.Column<string>(type: "nvarchar(max)", nullable: false),
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
                    table.PrimaryKey("PK_EnemyNPC", x => x.EnemyNPCID);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ItemValue = table.Column<double>(type: "float", nullable: true),
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
                    UpgradeItemSlots = table.Column<int>(type: "int", nullable: true),
                    ItemType = table.Column<int>(type: "int", nullable: false),
                    WeaponType = table.Column<int>(type: "int", nullable: true),
                    ArmorType = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                });

            migrationBuilder.CreateTable(
                name: "Ressource",
                columns: table => new
                {
                    RessourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RessourceName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    RessourceValue = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ressource", x => x.RessourceID);
                });

            migrationBuilder.CreateTable(
                name: "Soldier",
                columns: table => new
                {
                    SoldierID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SoldierName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FullLP = table.Column<double>(type: "float", nullable: true),
                    AtkMelee = table.Column<double>(type: "float", nullable: true),
                    AtkPierce = table.Column<double>(type: "float", nullable: true),
                    AtkMagic = table.Column<double>(type: "float", nullable: true),
                    DefMelee = table.Column<double>(type: "float", nullable: true),
                    DefPierce = table.Column<double>(type: "float", nullable: true),
                    DefMagic = table.Column<double>(type: "float", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Soldier", x => x.SoldierID);
                });

            migrationBuilder.CreateTable(
                name: "Town",
                columns: table => new
                {
                    TownID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TownName = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Town", x => x.TownID);
                });

            migrationBuilder.CreateTable(
                name: "Upgrade",
                columns: table => new
                {
                    UpgradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UpgradeItemName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UpgradeItemValue = table.Column<double>(type: "float", nullable: true),
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
                    table.PrimaryKey("PK_Upgrade", x => x.UpgradeID);
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
                    TownID = table.Column<int>(type: "int", nullable: false),
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
                        name: "FK_Player_Item_ChestItemID",
                        column: x => x.ChestItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_FeetItemID",
                        column: x => x.FeetItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_HeadItemID",
                        column: x => x.HeadItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_MainhandItemID",
                        column: x => x.MainhandItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_OffhandItemID",
                        column: x => x.OffhandItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_ShoulderItemID",
                        column: x => x.ShoulderItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Item_TrousersItemID",
                        column: x => x.TrousersItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_Player_Town_TownID",
                        column: x => x.TownID,
                        principalTable: "Town",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownBuilding",
                columns: table => new
                {
                    TownBuildingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    Level = table.Column<int>(type: "int", nullable: false),
                    TownID = table.Column<int>(type: "int", nullable: false),
                    BuildingID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownBuilding", x => x.TownBuildingID);
                    table.ForeignKey(
                        name: "FK_TownBuilding_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownBuilding_Town_TownID",
                        column: x => x.TownID,
                        principalTable: "Town",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownRessource",
                columns: table => new
                {
                    TownRessourceID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TownID = table.Column<int>(type: "int", nullable: false),
                    RessourceID = table.Column<int>(type: "int", nullable: false),
                    BuildingID = table.Column<int>(type: "int", nullable: true),
                    BuildingID1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownRessource", x => x.TownRessourceID);
                    table.ForeignKey(
                        name: "FK_TownRessource_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID");
                    table.ForeignKey(
                        name: "FK_TownRessource_Building_BuildingID1",
                        column: x => x.BuildingID1,
                        principalTable: "Building",
                        principalColumn: "BuildingID");
                    table.ForeignKey(
                        name: "FK_TownRessource_Ressource_RessourceID",
                        column: x => x.RessourceID,
                        principalTable: "Ressource",
                        principalColumn: "RessourceID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownRessource_Town_TownID",
                        column: x => x.TownID,
                        principalTable: "Town",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownSoldierAttacking",
                columns: table => new
                {
                    TownSoldierAttackingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<double>(type: "float", nullable: true),
                    CurrentLP = table.Column<double>(type: "float", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TownID = table.Column<int>(type: "int", nullable: false),
                    SoldierID = table.Column<int>(type: "int", nullable: false),
                    BuildingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownSoldierAttacking", x => x.TownSoldierAttackingID);
                    table.ForeignKey(
                        name: "FK_TownSoldierAttacking_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID");
                    table.ForeignKey(
                        name: "FK_TownSoldierAttacking_Soldier_SoldierID",
                        column: x => x.SoldierID,
                        principalTable: "Soldier",
                        principalColumn: "SoldierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownSoldierAttacking_Town_TownID",
                        column: x => x.TownID,
                        principalTable: "Town",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TownSoldierDefending",
                columns: table => new
                {
                    TownSoldierDefendingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Level = table.Column<int>(type: "int", nullable: true),
                    Experience = table.Column<double>(type: "float", nullable: true),
                    CurrentLP = table.Column<double>(type: "float", nullable: true),
                    Amount = table.Column<int>(type: "int", nullable: false),
                    TownID = table.Column<int>(type: "int", nullable: false),
                    SoldierID = table.Column<int>(type: "int", nullable: false),
                    BuildingID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TownSoldierDefending", x => x.TownSoldierDefendingID);
                    table.ForeignKey(
                        name: "FK_TownSoldierDefending_Building_BuildingID",
                        column: x => x.BuildingID,
                        principalTable: "Building",
                        principalColumn: "BuildingID");
                    table.ForeignKey(
                        name: "FK_TownSoldierDefending_Soldier_SoldierID",
                        column: x => x.SoldierID,
                        principalTable: "Soldier",
                        principalColumn: "SoldierID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TownSoldierDefending_Town_TownID",
                        column: x => x.TownID,
                        principalTable: "Town",
                        principalColumn: "TownID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Inventory",
                columns: table => new
                {
                    InventoryID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlayerID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Inventory", x => x.InventoryID);
                    table.ForeignKey(
                        name: "FK_Inventory_Player_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Player",
                        principalColumn: "PlayerID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItem",
                columns: table => new
                {
                    InventoryItemID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItem", x => x.InventoryItemID);
                    table.ForeignKey(
                        name: "FK_InventoryItem_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItem_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryUpgrade",
                columns: table => new
                {
                    InventoryUpgradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryID = table.Column<int>(type: "int", nullable: false),
                    UpgradeID = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryUpgrade", x => x.InventoryUpgradeID);
                    table.ForeignKey(
                        name: "FK_InventoryUpgrade_Inventory_InventoryID",
                        column: x => x.InventoryID,
                        principalTable: "Inventory",
                        principalColumn: "InventoryID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryUpgrade_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID");
                    table.ForeignKey(
                        name: "FK_InventoryUpgrade_Upgrade_UpgradeID",
                        column: x => x.UpgradeID,
                        principalTable: "Upgrade",
                        principalColumn: "UpgradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "InventoryItemUpgrade",
                columns: table => new
                {
                    InventoryItemUpgradeID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InventoryItemID = table.Column<int>(type: "int", nullable: false),
                    UpgradeID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InventoryItemUpgrade", x => x.InventoryItemUpgradeID);
                    table.ForeignKey(
                        name: "FK_InventoryItemUpgrade_InventoryItem_InventoryItemID",
                        column: x => x.InventoryItemID,
                        principalTable: "InventoryItem",
                        principalColumn: "InventoryItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_InventoryItemUpgrade_Upgrade_UpgradeID",
                        column: x => x.UpgradeID,
                        principalTable: "Upgrade",
                        principalColumn: "UpgradeID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Inventory_PlayerID",
                table: "Inventory",
                column: "PlayerID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_InventoryID",
                table: "InventoryItem",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItem_ItemID",
                table: "InventoryItem",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemUpgrade_InventoryItemID",
                table: "InventoryItemUpgrade",
                column: "InventoryItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryItemUpgrade_UpgradeID",
                table: "InventoryItemUpgrade",
                column: "UpgradeID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUpgrade_InventoryID",
                table: "InventoryUpgrade",
                column: "InventoryID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUpgrade_ItemID",
                table: "InventoryUpgrade",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_InventoryUpgrade_UpgradeID",
                table: "InventoryUpgrade",
                column: "UpgradeID");

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
                name: "IX_Player_TownID",
                table: "Player",
                column: "TownID");

            migrationBuilder.CreateIndex(
                name: "IX_Player_TrousersItemID",
                table: "Player",
                column: "TrousersItemID");

            migrationBuilder.CreateIndex(
                name: "IX_TownBuilding_BuildingID",
                table: "TownBuilding",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_TownBuilding_TownID",
                table: "TownBuilding",
                column: "TownID");

            migrationBuilder.CreateIndex(
                name: "IX_TownRessource_BuildingID",
                table: "TownRessource",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_TownRessource_BuildingID1",
                table: "TownRessource",
                column: "BuildingID1");

            migrationBuilder.CreateIndex(
                name: "IX_TownRessource_RessourceID",
                table: "TownRessource",
                column: "RessourceID");

            migrationBuilder.CreateIndex(
                name: "IX_TownRessource_TownID",
                table: "TownRessource",
                column: "TownID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierAttacking_BuildingID",
                table: "TownSoldierAttacking",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierAttacking_SoldierID",
                table: "TownSoldierAttacking",
                column: "SoldierID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierAttacking_TownID",
                table: "TownSoldierAttacking",
                column: "TownID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierDefending_BuildingID",
                table: "TownSoldierDefending",
                column: "BuildingID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierDefending_SoldierID",
                table: "TownSoldierDefending",
                column: "SoldierID");

            migrationBuilder.CreateIndex(
                name: "IX_TownSoldierDefending_TownID",
                table: "TownSoldierDefending",
                column: "TownID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EnemyNPC");

            migrationBuilder.DropTable(
                name: "InventoryItemUpgrade");

            migrationBuilder.DropTable(
                name: "InventoryUpgrade");

            migrationBuilder.DropTable(
                name: "TownBuilding");

            migrationBuilder.DropTable(
                name: "TownRessource");

            migrationBuilder.DropTable(
                name: "TownSoldierAttacking");

            migrationBuilder.DropTable(
                name: "TownSoldierDefending");

            migrationBuilder.DropTable(
                name: "InventoryItem");

            migrationBuilder.DropTable(
                name: "Upgrade");

            migrationBuilder.DropTable(
                name: "Ressource");

            migrationBuilder.DropTable(
                name: "Building");

            migrationBuilder.DropTable(
                name: "Soldier");

            migrationBuilder.DropTable(
                name: "Inventory");

            migrationBuilder.DropTable(
                name: "Player");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Town");
        }
    }
}
