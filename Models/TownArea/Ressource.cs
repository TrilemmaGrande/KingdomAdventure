﻿namespace KingdomAdventure.Models.TownArea
{
    public enum ERessourceName
    {
        Food,
        Wood,
        Stone,
        Cloth,
        Iron,
        Silver,
        Gold,
        IronArmor,
        Sword,
        Leather,
        LeatherArmor,
        Bow,
        Robe,
        Wand
    }
    public class Ressource
    {
        public int RessourceID { get; set; }
        public string? RessourceName { get; set; }
        public ERessourceName ERessourceName { get; set; }
        public double RessourceValue { get; set; }
        public string Icon { get; set; }
    }
}