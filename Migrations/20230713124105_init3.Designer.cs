﻿// <auto-generated />
using System;
using KingdomAdventure.Models.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace KingdomAdventure.Migrations
{
    [DbContext(typeof(KingdomAdventureDBContext))]
    [Migration("20230713124105_init3")]
    partial class init3
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.8")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Building", b =>
                {
                    b.Property<int>("BuildingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingID"));

                    b.Property<string>("BuildingName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("Workplaces")
                        .HasColumnType("int");

                    b.HasKey("BuildingID");

                    b.ToTable("Building");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceConsuming", b =>
                {
                    b.Property<int>("BuildingRessourceConsumingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingRessourceConsumingID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<int>("RessourceID")
                        .HasColumnType("int");

                    b.HasKey("BuildingRessourceConsumingID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("RessourceID");

                    b.ToTable("BuildingRessourceConsuming");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceCost", b =>
                {
                    b.Property<int>("BuildingRessourceCostID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingRessourceCostID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<int>("RessourceID")
                        .HasColumnType("int");

                    b.HasKey("BuildingRessourceCostID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("RessourceID");

                    b.ToTable("BuildingRessourceCost");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceProducing", b =>
                {
                    b.Property<int>("BuildingRessourceProducingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingRessourceProducingID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<bool>("ProduceOnce")
                        .HasColumnType("bit");

                    b.Property<int>("RessourceID")
                        .HasColumnType("int");

                    b.HasKey("BuildingRessourceProducingID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("RessourceID");

                    b.ToTable("BuildingRessourceProducing");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingSoldierProducing", b =>
                {
                    b.Property<int>("BuildingSoldierProducingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("BuildingSoldierProducingID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<double?>("CurrentLP")
                        .HasColumnType("float");

                    b.Property<double?>("Experience")
                        .HasColumnType("float");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<int>("SoldierID")
                        .HasColumnType("int");

                    b.HasKey("BuildingSoldierProducingID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("SoldierID");

                    b.ToTable("BuildingSoldierProducing");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Ressource", b =>
                {
                    b.Property<int>("RessourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RessourceID"));

                    b.Property<string>("RessourceName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("RessourceValue")
                        .HasColumnType("float");

                    b.HasKey("RessourceID");

                    b.ToTable("Ressource");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Soldier", b =>
                {
                    b.Property<int>("SoldierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("SoldierID"));

                    b.Property<double?>("AtkMagic")
                        .HasColumnType("float");

                    b.Property<double?>("AtkMelee")
                        .HasColumnType("float");

                    b.Property<double?>("AtkPierce")
                        .HasColumnType("float");

                    b.Property<double?>("DefMagic")
                        .HasColumnType("float");

                    b.Property<double?>("DefMelee")
                        .HasColumnType("float");

                    b.Property<double?>("DefPierce")
                        .HasColumnType("float");

                    b.Property<double?>("FullLP")
                        .HasColumnType("float");

                    b.Property<string>("SoldierName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("SoldierID");

                    b.ToTable("Soldier");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Town", b =>
                {
                    b.Property<int>("TownID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TownID"));

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.Property<string>("TownName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("TownID");

                    b.HasIndex("PlayerID")
                        .IsUnique();

                    b.ToTable("Town");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownBuilding", b =>
                {
                    b.Property<int>("TownBuildingID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TownBuildingID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("BuildingID")
                        .HasColumnType("int");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int>("TownID")
                        .HasColumnType("int");

                    b.HasKey("TownBuildingID");

                    b.HasIndex("BuildingID");

                    b.HasIndex("TownID");

                    b.ToTable("TownBuilding");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownRessource", b =>
                {
                    b.Property<int>("TownRessourceID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TownRessourceID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<int>("RessourceID")
                        .HasColumnType("int");

                    b.Property<int>("TownID")
                        .HasColumnType("int");

                    b.HasKey("TownRessourceID");

                    b.HasIndex("RessourceID");

                    b.HasIndex("TownID");

                    b.ToTable("TownRessource");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownSoldier", b =>
                {
                    b.Property<int>("TownSoldierID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("TownSoldierID"));

                    b.Property<int>("Amount")
                        .HasColumnType("int");

                    b.Property<double?>("CurrentLP")
                        .HasColumnType("float");

                    b.Property<double?>("Experience")
                        .HasColumnType("float");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<int>("SoldierID")
                        .HasColumnType("int");

                    b.Property<int>("TownID")
                        .HasColumnType("int");

                    b.HasKey("TownSoldierID");

                    b.HasIndex("SoldierID");

                    b.HasIndex("TownID");

                    b.ToTable("TownSoldier");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.EnemyNPC", b =>
                {
                    b.Property<int>("EnemyNPCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("EnemyNPCID"));

                    b.Property<double?>("AtkMagic")
                        .HasColumnType("float");

                    b.Property<double?>("AtkMelee")
                        .HasColumnType("float");

                    b.Property<double?>("AtkPierce")
                        .HasColumnType("float");

                    b.Property<bool>("Boss")
                        .HasColumnType("bit");

                    b.Property<double?>("Crit")
                        .HasColumnType("float");

                    b.Property<double?>("CritDmg")
                        .HasColumnType("float");

                    b.Property<double?>("DefMagic")
                        .HasColumnType("float");

                    b.Property<double?>("DefMelee")
                        .HasColumnType("float");

                    b.Property<double?>("DefPierce")
                        .HasColumnType("float");

                    b.Property<string>("EnemyNPCName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<double>("FullLP")
                        .HasColumnType("float");

                    b.HasKey("EnemyNPCID");

                    b.ToTable("EnemyNPC");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Inventory", b =>
                {
                    b.Property<int>("InventoryID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryID"));

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.HasKey("InventoryID");

                    b.HasIndex("PlayerID")
                        .IsUnique();

                    b.ToTable("Inventory");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryItem", b =>
                {
                    b.Property<int>("InventoryItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryItemID"));

                    b.Property<int>("InventoryID")
                        .HasColumnType("int");

                    b.Property<int>("ItemID")
                        .HasColumnType("int");

                    b.HasKey("InventoryItemID");

                    b.HasIndex("InventoryID");

                    b.HasIndex("ItemID");

                    b.ToTable("InventoryItem");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryItemUpgrade", b =>
                {
                    b.Property<int>("InventoryItemUpgradeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryItemUpgradeID"));

                    b.Property<int>("InventoryItemID")
                        .HasColumnType("int");

                    b.Property<int>("UpgradeID")
                        .HasColumnType("int");

                    b.HasKey("InventoryItemUpgradeID");

                    b.HasIndex("InventoryItemID");

                    b.HasIndex("UpgradeID");

                    b.ToTable("InventoryItemUpgrade");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryUpgrade", b =>
                {
                    b.Property<int>("InventoryUpgradeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("InventoryUpgradeID"));

                    b.Property<int>("InventoryID")
                        .HasColumnType("int");

                    b.Property<int?>("ItemID")
                        .HasColumnType("int");

                    b.Property<int>("UpgradeID")
                        .HasColumnType("int");

                    b.HasKey("InventoryUpgradeID");

                    b.HasIndex("InventoryID");

                    b.HasIndex("ItemID");

                    b.HasIndex("UpgradeID");

                    b.ToTable("InventoryUpgrade");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Item", b =>
                {
                    b.Property<int>("ItemID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ItemID"));

                    b.Property<int?>("ArmorType")
                        .HasColumnType("int");

                    b.Property<double?>("AtkMagic")
                        .HasColumnType("float");

                    b.Property<double?>("AtkMelee")
                        .HasColumnType("float");

                    b.Property<double?>("AtkPierce")
                        .HasColumnType("float");

                    b.Property<double?>("Crit")
                        .HasColumnType("float");

                    b.Property<double?>("CritDmg")
                        .HasColumnType("float");

                    b.Property<double?>("DefMagic")
                        .HasColumnType("float");

                    b.Property<double?>("DefMelee")
                        .HasColumnType("float");

                    b.Property<double?>("DefPierce")
                        .HasColumnType("float");

                    b.Property<double?>("Dex")
                        .HasColumnType("float");

                    b.Property<double?>("End")
                        .HasColumnType("float");

                    b.Property<double?>("Experience")
                        .HasColumnType("float");

                    b.Property<double?>("FullLP")
                        .HasColumnType("float");

                    b.Property<double?>("Int")
                        .HasColumnType("float");

                    b.Property<string>("ItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<double?>("ItemValue")
                        .HasColumnType("float");

                    b.Property<double?>("LP")
                        .HasColumnType("float");

                    b.Property<double?>("PlayerGold")
                        .HasColumnType("float");

                    b.Property<double?>("Str")
                        .HasColumnType("float");

                    b.Property<int?>("UpgradeItemSlots")
                        .HasColumnType("int");

                    b.Property<int?>("WeaponType")
                        .HasColumnType("int");

                    b.HasKey("ItemID");

                    b.ToTable("Item");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Player", b =>
                {
                    b.Property<int>("PlayerID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerID"));

                    b.Property<double>("AtkMagic")
                        .HasColumnType("float");

                    b.Property<double>("AtkMelee")
                        .HasColumnType("float");

                    b.Property<double>("AtkPierce")
                        .HasColumnType("float");

                    b.Property<int?>("ChestItemID")
                        .HasColumnType("int");

                    b.Property<double>("Crit")
                        .HasColumnType("float");

                    b.Property<double>("CritDmg")
                        .HasColumnType("float");

                    b.Property<double>("CurrentLP")
                        .HasColumnType("float");

                    b.Property<double>("DefMagic")
                        .HasColumnType("float");

                    b.Property<double>("DefMelee")
                        .HasColumnType("float");

                    b.Property<double>("DefPierce")
                        .HasColumnType("float");

                    b.Property<double>("Dex")
                        .HasColumnType("float");

                    b.Property<double>("End")
                        .HasColumnType("float");

                    b.Property<double>("Experience")
                        .HasColumnType("float");

                    b.Property<int?>("FeetItemID")
                        .HasColumnType("int");

                    b.Property<double>("FullLP")
                        .HasColumnType("float");

                    b.Property<int?>("HeadItemID")
                        .HasColumnType("int");

                    b.Property<double>("Int")
                        .HasColumnType("float");

                    b.Property<int>("Level")
                        .HasColumnType("int");

                    b.Property<int?>("MainhandItemID")
                        .HasColumnType("int");

                    b.Property<int?>("OffhandItemID")
                        .HasColumnType("int");

                    b.Property<double>("PlayerGold")
                        .HasColumnType("float");

                    b.Property<string>("PlayerName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int?>("ShoulderItemID")
                        .HasColumnType("int");

                    b.Property<double>("Str")
                        .HasColumnType("float");

                    b.Property<int?>("TrousersItemID")
                        .HasColumnType("int");

                    b.HasKey("PlayerID");

                    b.HasIndex("ChestItemID");

                    b.HasIndex("FeetItemID");

                    b.HasIndex("HeadItemID");

                    b.HasIndex("MainhandItemID");

                    b.HasIndex("OffhandItemID");

                    b.HasIndex("ShoulderItemID");

                    b.HasIndex("TrousersItemID");

                    b.ToTable("Player");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.PlayerEnemyNPC", b =>
                {
                    b.Property<int>("PlayerEnemyNPCID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlayerEnemyNPCID"));

                    b.Property<double?>("CurrentLP")
                        .HasColumnType("float");

                    b.Property<int>("EnemyNPCID")
                        .HasColumnType("int");

                    b.Property<int?>("Level")
                        .HasColumnType("int");

                    b.Property<int>("PlayerID")
                        .HasColumnType("int");

                    b.HasKey("PlayerEnemyNPCID");

                    b.HasIndex("EnemyNPCID");

                    b.HasIndex("PlayerID");

                    b.ToTable("PlayerEnemyNPC");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Upgrade", b =>
                {
                    b.Property<int>("UpgradeID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UpgradeID"));

                    b.Property<double?>("AtkMagic")
                        .HasColumnType("float");

                    b.Property<double?>("AtkMelee")
                        .HasColumnType("float");

                    b.Property<double?>("AtkPierce")
                        .HasColumnType("float");

                    b.Property<double?>("Crit")
                        .HasColumnType("float");

                    b.Property<double?>("CritDmg")
                        .HasColumnType("float");

                    b.Property<double?>("DefMagic")
                        .HasColumnType("float");

                    b.Property<double?>("DefMelee")
                        .HasColumnType("float");

                    b.Property<double?>("DefPierce")
                        .HasColumnType("float");

                    b.Property<double?>("Dex")
                        .HasColumnType("float");

                    b.Property<double?>("End")
                        .HasColumnType("float");

                    b.Property<double?>("Experience")
                        .HasColumnType("float");

                    b.Property<double?>("FullLP")
                        .HasColumnType("float");

                    b.Property<double?>("Int")
                        .HasColumnType("float");

                    b.Property<int>("ItemType")
                        .HasColumnType("int");

                    b.Property<double?>("LP")
                        .HasColumnType("float");

                    b.Property<double?>("PlayerGold")
                        .HasColumnType("float");

                    b.Property<double?>("Str")
                        .HasColumnType("float");

                    b.Property<string>("UpgradeItemName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<double?>("UpgradeItemValue")
                        .HasColumnType("float");

                    b.HasKey("UpgradeID");

                    b.ToTable("Upgrade");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceConsuming", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Building", "Building")
                        .WithMany("ConsumingRessources")
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceCost", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Building", "Building")
                        .WithMany("BuildingRessourcesCosts")
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingRessourceProducing", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Building", "Building")
                        .WithMany("ProducingRessources")
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Ressource");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.BuildingSoldierProducing", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Building", "Building")
                        .WithMany("ProducingSoldiers")
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Soldier", "Soldier")
                        .WithMany()
                        .HasForeignKey("SoldierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Soldier");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Town", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.Player", "Player")
                        .WithOne("Town")
                        .HasForeignKey("KingdomAdventure.Models.TownArea.Town", "PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownBuilding", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Building", "Building")
                        .WithMany()
                        .HasForeignKey("BuildingID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Town", "Town")
                        .WithMany("TownBuildings")
                        .HasForeignKey("TownID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Building");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownRessource", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Ressource", "Ressource")
                        .WithMany()
                        .HasForeignKey("RessourceID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Town", "Town")
                        .WithMany("TownRessources")
                        .HasForeignKey("TownID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Ressource");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.TownSoldier", b =>
                {
                    b.HasOne("KingdomAdventure.Models.TownArea.Soldier", "Soldier")
                        .WithMany()
                        .HasForeignKey("SoldierID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.TownArea.Town", "Town")
                        .WithMany("TownSoldiers")
                        .HasForeignKey("TownID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Soldier");

                    b.Navigation("Town");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Inventory", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.Player", "Player")
                        .WithOne("Inventory")
                        .HasForeignKey("KingdomAdventure.Models.WorldArea.Inventory", "PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Player");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryItem", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.Inventory", "Inventory")
                        .WithMany("InventoryItems")
                        .HasForeignKey("InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Item")
                        .WithMany()
                        .HasForeignKey("ItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryItemUpgrade", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.InventoryItem", "InventoryItem")
                        .WithMany()
                        .HasForeignKey("InventoryItemID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.WorldArea.Upgrade", "Upgrade")
                        .WithMany()
                        .HasForeignKey("UpgradeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InventoryItem");

                    b.Navigation("Upgrade");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.InventoryUpgrade", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.Inventory", "Inventory")
                        .WithMany("InventoryUpgrades")
                        .HasForeignKey("InventoryID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", null)
                        .WithMany("UpgradeItems")
                        .HasForeignKey("ItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Upgrade", "Upgrade")
                        .WithMany()
                        .HasForeignKey("UpgradeID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Inventory");

                    b.Navigation("Upgrade");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Player", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Chest")
                        .WithMany()
                        .HasForeignKey("ChestItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Feet")
                        .WithMany()
                        .HasForeignKey("FeetItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Head")
                        .WithMany()
                        .HasForeignKey("HeadItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Mainhand")
                        .WithMany()
                        .HasForeignKey("MainhandItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Offhand")
                        .WithMany()
                        .HasForeignKey("OffhandItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Shoulder")
                        .WithMany()
                        .HasForeignKey("ShoulderItemID");

                    b.HasOne("KingdomAdventure.Models.WorldArea.Item", "Trousers")
                        .WithMany()
                        .HasForeignKey("TrousersItemID");

                    b.Navigation("Chest");

                    b.Navigation("Feet");

                    b.Navigation("Head");

                    b.Navigation("Mainhand");

                    b.Navigation("Offhand");

                    b.Navigation("Shoulder");

                    b.Navigation("Trousers");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.PlayerEnemyNPC", b =>
                {
                    b.HasOne("KingdomAdventure.Models.WorldArea.EnemyNPC", "EnemyNPC")
                        .WithMany()
                        .HasForeignKey("EnemyNPCID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("KingdomAdventure.Models.WorldArea.Player", "Player")
                        .WithMany()
                        .HasForeignKey("PlayerID")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EnemyNPC");

                    b.Navigation("Player");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Building", b =>
                {
                    b.Navigation("BuildingRessourcesCosts");

                    b.Navigation("ConsumingRessources");

                    b.Navigation("ProducingRessources");

                    b.Navigation("ProducingSoldiers");
                });

            modelBuilder.Entity("KingdomAdventure.Models.TownArea.Town", b =>
                {
                    b.Navigation("TownBuildings");

                    b.Navigation("TownRessources");

                    b.Navigation("TownSoldiers");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Inventory", b =>
                {
                    b.Navigation("InventoryItems");

                    b.Navigation("InventoryUpgrades");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Item", b =>
                {
                    b.Navigation("UpgradeItems");
                });

            modelBuilder.Entity("KingdomAdventure.Models.WorldArea.Player", b =>
                {
                    b.Navigation("Inventory")
                        .IsRequired();

                    b.Navigation("Town")
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
