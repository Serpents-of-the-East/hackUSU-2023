using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterStats
{
    public class Level
    {
        public int level = 0;
        public int maxLevel = 10;
        public List<int> xpRequirements = new List<int>() { 10, 20, 30, 40, 50, 60, 70, 80, 90 };
        public int currentXp = 0;
        public bool isAtMaxLevel = false;
    }

    public class Armor
    {
        public int id;
        public string name;
        public string description;
        public float physicalResist;
        public float elementalResist;
        public float speedModifier;
        public float value;
    }

    public class Weapon
    {
        public int id;
        public string name;
        public string description;
        public string type;
        public float speedModifier;
        public float critChance;
        public float baseDamage;
        public float value;
    }
    

    public enum CharacterName
    {
        archer,
        blackMage,
        whiteMage,
        warrior
    }

    public CharacterName characterName;
    public float currentHealth;
    public float maxHealth;
    public float currentSP;
    public float maxSP;
    public float physicalAttack;
    public float elementalAttack;
    public float physicalDefense;
    public float elementalDefense;
    public float accuracy;
    public float speed;
    public float critChance;
    public float evasiveness;
    public Level level;
    public Armor armor;
    public Weapon weapon;

    public List<string> statusEffects = new List<string>();

    public void LevelUp()
    {
        level.currentXp -= level.xpRequirements[level.level];
        level.level += 1;

        if (level.level == level.maxLevel)
        {
            level.currentXp = 0;
            level.isAtMaxLevel = true;
        }
        // TODO: Add in logic to bump up stats

    }
}
