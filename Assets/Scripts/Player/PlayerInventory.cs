using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Item
{
    public int id;
    public string name;
    public string description;
    public int count;
}

public class Level
{
    public int level;
    public int maxLevel;
    public List<int> xpRequirements;
    public int currentXp;
    public bool isAtMaxLevel;
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

public class Quest
{
    public int id;
    public string Name { get; set; }
    public string Description { get; set; }
    public string CompletionDescription { get; set; }
    public string KeyItem { get; set; }
    public bool Required { get; set; } // Optional False
    public bool Completed { get; set; }
}


public class Stats
{
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
}


public class CurrentInventory
{
    public Weapon weapon;
    public Armor chestPiece;
    public Armor headPiece;
    public Armor legPiece;
}



public class PlayerInventory : MonoBehaviour
{
    public List<Quest> quests;
    public List<Armor> armors;
    public List<Weapon> weapons;
    public List<Item> items;
    public Level level;
    public CurrentInventory currentInventory;
    public Stats stats;

    // Start is called before the first frame update
    void Start()
    {
        quests = new List<Quest>();
        armors = new List<Armor>();
        weapons = new List<Weapon>();
        items = new List<Item>();
        level = new Level();
        currentInventory = new CurrentInventory();
        stats = new Stats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

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

    public void RemoveItem(Item item)
    {
        Item inventoryItem = items[item.id];
        
        if (inventoryItem != null)
        {
            if (inventoryItem.count <= item.count)
            {
                items.Remove(inventoryItem);
            }
            else
            {
                inventoryItem.count -= item.count;
            }
        }
    }

    public void AddItem(Item item)
    {
        Item inventoryItem = items[item.id];
        
        if (inventoryItem != null)
        {
             inventoryItem.count += item.count;          
        }
        else
        {
            items.Add(item);
        }
    }

    public void AddWeapon(Weapon weapon)
    {
        weapons.Add(weapon);
    }

    public void RemoveWeapon(Weapon weapon)
    {
        weapons.Remove(weapon);
    }

    public void AddArmor(Armor armor)
    {
        armors.Add(armor);
    }

    public void RemoveArmor(Armor armor)
    {
        armors.Remove(armor);
    }

    public void AddQuest(Quest quest)
    {
        quests.Add(quest);
    }

    public void CompleteQuest(Quest quest)
    {
        quests[quest.id].Completed = true;
    }

    public void EquipWeapon(Weapon weapon)
    {
        weapons.Add(currentInventory.weapon);
        currentInventory.weapon = weapon;
        weapons.Remove(weapon);
    }

    public void EquipChestPiece(Armor chestPiece)
    {
        armors.Add(currentInventory.chestPiece);
        currentInventory.chestPiece = chestPiece;
        armors.Remove(chestPiece);
    }

    public void EquipHeadGear(Armor headGear)
    {
        armors.Add(currentInventory.headPiece);
        currentInventory.headPiece = headGear;
        armors.Remove(headGear);
    }

    public void EquipLegGear(Armor legPiece)
    {
        armors.Add(currentInventory.legPiece);
        currentInventory.legPiece = legPiece;
        armors.Remove(legPiece);
    }
}
