using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Move
{
    public string Name;
    public float damage;
    public float spCost;
    public bool hitsMultiple;
    public bool elemental;
}

public class InventoryAndStats : MonoBehaviour
{
    public CharacterStats whiteMageStats;
    public CharacterStats blackMageStats;
    public CharacterStats warriorStats;
    public CharacterStats archerStats;
    public List<Move> whiteMageMoves = new();
    public List<Move> blackMageMoves = new();
    public List<Move> warriorMoves = new();
    public List<Move> archerMoves = new();

    public TextAsset whiteMageTxt;
    public TextAsset blackMageTxt;
    public TextAsset warriorTxt;
    public TextAsset archerTxt;
    string[] lines;


    private void Awake()
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

        lines = whiteMageTxt.text.Split("\n"[0]);

        for (var i = 0; i < lines.Length; i+=5)
        {
            Move move = new Move();
            move.Name = lines[i + 0];
            move.damage = float.Parse(lines[i + 1]);
            move.spCost = float.Parse(lines[i +2]);
            move.hitsMultiple = bool.Parse(lines[i + 3]);
            move.elemental = bool.Parse(lines[i + 4]);
            whiteMageMoves.Add(move);
        }




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

        lines = blackMageTxt.text.Split("\n"[0]);

        for (var i = 0; i < lines.Length; i += 5)
        {
            Move move = new Move();
            move.Name = lines[i + 0];
            move.damage = float.Parse(lines[i + 1]);
            move.spCost = float.Parse(lines[i + 2]);
            move.hitsMultiple = bool.Parse(lines[i + 3]);
            move.elemental = bool.Parse(lines[i + 4]);
            blackMageMoves.Add(move);
        }

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

        lines = warriorTxt.text.Split("\n"[0]);

        for (var i = 0; i < lines.Length; i += 5)
        {
            Move move = new Move();
            move.Name = lines[i + 0];
            move.damage = float.Parse(lines[i + 1]);
            move.spCost = float.Parse(lines[i + 2]);
            move.hitsMultiple = bool.Parse(lines[i + 3]);
            move.elemental = bool.Parse(lines[i + 4]);
            warriorMoves.Add(move);
        }

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

        lines = archerTxt.text.Split("\n"[0]);

        for (var i = 0; i < lines.Length; i += 5)
        {
            Move move = new Move();
            move.Name = lines[i + 0];
            move.damage = float.Parse(lines[i + 1]);
            move.spCost = float.Parse(lines[i + 2]);
            move.hitsMultiple = bool.Parse(lines[i + 3]);
            move.elemental = bool.Parse(lines[i + 4]);
            archerMoves.Add(move);
        }
    }

    public void TakeDamage(float amount, bool isPhysical, int characterId)
    {
        switch (characterId)
        {
            case 0:
                if (isPhysical)
                {
                    blackMageStats.currentHealth -= amount - (amount * (blackMageStats.physicalDefense / 100));
                }
                else
                {
                    blackMageStats.currentHealth -= amount - (amount * (blackMageStats.elementalDefense / 100));
                }
                break;
            case 1:
                if (isPhysical)
                {
                    whiteMageStats.currentHealth -= amount - (amount * (whiteMageStats.physicalDefense / 100));
                }
                else
                {
                    whiteMageStats.currentHealth -= amount - (amount * (whiteMageStats.elementalDefense / 100));
                }
                break;
            case 2:
                if (isPhysical)
                {
                    warriorStats.currentHealth -= amount - (amount * (warriorStats.physicalDefense / 100));
                }
                else
                {
                    warriorStats.currentHealth -= amount - (amount * (warriorStats.elementalDefense / 100));
                }
                break;
            case 3:
                if (isPhysical)
                {
                    archerStats.currentHealth -= amount - (amount * (archerStats.physicalDefense / 100));
                }
                else
                {
                    archerStats.currentHealth -= amount - (amount * (archerStats.elementalDefense / 100));
                }
                break;
            default:
                break;
        }
    }

    public float DealDamage(float amount, bool isPhysical, int characterId)
    {
        switch (characterId)
        {
            case 0:
                if (isPhysical)
                {
                    return amount + amount * (blackMageStats.physicalAttack / 100);
                }
                else
                {
                    return amount + amount * (blackMageStats.elementalAttack / 100);
                }
            case 1:
                if (isPhysical)
                {
                    return amount + amount * (whiteMageStats.physicalAttack / 100);
                }
                else
                {
                    return amount + amount * (whiteMageStats.elementalAttack / 100);
                }
            case 2:
                if (isPhysical)
                {
                    return amount + amount * (warriorStats.physicalAttack / 100);
                }
                else
                {
                    return amount + amount * (warriorStats.elementalAttack / 100);
                }
            case 3:
                if (isPhysical)
                {
                    return amount + amount * (archerStats.physicalAttack / 100);
                }
                else
                {
                    return amount + amount * (archerStats.elementalAttack / 100);
                }
            default:
                return 0;
        }

    }

    public float GetSpeed(int characterId)
    {
        switch (characterId)
        {
            case 0:
                return Random.Range(blackMageStats.speed / 100 - 0.1f, blackMageStats.speed / 100 + 0.1f);
            case 1:
                return Random.Range(whiteMageStats.speed / 100 - 0.1f, whiteMageStats.speed / 100 + 0.1f);
            case 2:
                return Random.Range(warriorStats.speed / 100 - 0.1f, warriorStats.speed / 100 + 0.1f);
            case 3:
                return Random.Range(archerStats.speed / 100 - 0.1f, archerStats.speed / 100 + 0.1f);
            default:
                return 0;
        }
    }

}
