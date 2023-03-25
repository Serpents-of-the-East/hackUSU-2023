using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryAndStats : MonoBehaviour
{
    CharacterStats whiteMageStats;
    CharacterStats blackMageStats;
    CharacterStats warriorStats;
    CharacterStats archerStats;

    private void Start()
    {
        whiteMageStats = new CharacterStats()
        {
            level = new CharacterStats.Level(),
            characterName = CharacterStats.CharacterName.whiteMage,
            currentHealth = 100,
            maxHealth = 100,
            currentSP = 100,
            maxSP = 100,
            physicalAttack = 5,
            elementalAttack = 20,
            physicalDefense = 5,
            elementalDefense = 20,
            accuracy = 90,
            speed = 20,
            critChance = 20,
            evasiveness = 20,
        };
        blackMageStats = new CharacterStats()
        {
            level = new CharacterStats.Level(),
            characterName = CharacterStats.CharacterName.whiteMage,
            currentHealth = 100,
            maxHealth = 100,
            currentSP = 100,
            maxSP = 100,
            physicalAttack = 5,
            elementalAttack = 20,
            physicalDefense = 5,
            elementalDefense = 20,
            accuracy = 90,
            speed = 20,
            critChance = 20,
            evasiveness = 20,
        };
        warriorStats = new CharacterStats()
        {
            level = new CharacterStats.Level(),
            characterName = CharacterStats.CharacterName.whiteMage,
            currentHealth = 100,
            maxHealth = 100,
            currentSP = 100,
            maxSP = 100,
            physicalAttack = 20,
            elementalAttack = 5,
            physicalDefense = 20,
            elementalDefense = 5,
            accuracy = 90,
            speed = 20,
            critChance = 20,
            evasiveness = 20,
        };
        archerStats = new CharacterStats()
        {
            level = new CharacterStats.Level(),
            characterName = CharacterStats.CharacterName.whiteMage,
            currentHealth = 100,
            maxHealth = 100,
            currentSP = 100,
            maxSP = 100,
            physicalAttack = 10,
            elementalAttack = 10,
            physicalDefense = 10,
            elementalDefense = 10,
            accuracy = 90,
            speed = 40,
            critChance = 40,
            evasiveness = 40,
        };
    }
}
