using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{

    public GameObject[] playerHealthImages;

    public InventoryAndStats inventoryAndStats;

    public float[] maxHealth;
    public float[] currentHealth;

    private float[] originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        //originalWidth = playerHealthImages[0].rectTransform.rect.width;
        RectTransform rt = (RectTransform)playerHealthImages[0].transform;
        originalWidth = new float[]{ rt.rect.width, rt.rect.width, rt.rect.width, rt.rect.width };

        // Initialize health to max health in the beginning
        for (int i = 0; i < maxHealth.Length; i++)
        {
            currentHealth[i] = maxHealth[i];
        }
        inventoryAndStats = FindObjectOfType<InventoryAndStats>();

        maxHealth[0] = inventoryAndStats.blackMageStats.maxHealth;
        maxHealth[1] = inventoryAndStats.whiteMageStats.maxHealth;
        maxHealth[2] = inventoryAndStats.warriorStats.maxHealth;
        maxHealth[3] = inventoryAndStats.archerStats.maxHealth;

        currentHealth[0] = inventoryAndStats.blackMageStats.currentHealth;
        currentHealth[1] = inventoryAndStats.whiteMageStats.currentHealth;
        currentHealth[2] = inventoryAndStats.warriorStats.currentHealth;
        currentHealth[3] = inventoryAndStats.archerStats.currentHealth;

    }

    // Should be an array of damage taken for each player 
    // ex. [0, 0, 25, 5]
    void Damage(int[] damageTaken)
    {
        for (int i = 0; i < damageTaken.Length; i++)
        {
            if (currentHealth[i] <= 0)
            {
                // THEY DIED
                continue;
            }
            float amountDamage = damageTaken[i];
            currentHealth[i] -= amountDamage;
            RectTransform rt = (RectTransform)playerHealthImages[i].transform;
            rt.sizeDelta = new Vector2((currentHealth[i] / maxHealth[i]) * originalWidth[i], rt.sizeDelta.y);
            rt.transform.position = new Vector2(rt.transform.position.x - 7*amountDamage/8, rt.transform.position.y);
        }
    }

    private void Update()
    {
        float[] oldHealth = currentHealth;

        currentHealth[0] = inventoryAndStats.blackMageStats.currentHealth;
        currentHealth[1] = inventoryAndStats.whiteMageStats.currentHealth;
        currentHealth[2] = inventoryAndStats.warriorStats.currentHealth;
        currentHealth[3] = inventoryAndStats.archerStats.currentHealth;

        for (int i = 0; i < playerHealthImages.Length; i++)
        {
            float amountDamage = oldHealth[i] - currentHealth[i];
            RectTransform rt = (RectTransform)playerHealthImages[i].transform;
            rt.sizeDelta = new Vector2((currentHealth[i] / maxHealth[i]) * originalWidth[i], rt.sizeDelta.y);
            rt.transform.position = new Vector2(rt.transform.position.x - amountDamage / 2, rt.transform.position.y);
        }
    }

    //        Damage(new int[4] {0, 5, 25, 45 }); This is how you use the Damage function


}
