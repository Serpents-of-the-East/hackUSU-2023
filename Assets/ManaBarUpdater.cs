using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ManaBarUpdater : MonoBehaviour
{

    public GameObject[] playerManaImages;

    public float[] maxMana;
    public float[] currentMana;

    private float[] originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        //originalWidth = playerHealthImages[0].rectTransform.rect.width;
        RectTransform rt = (RectTransform)playerManaImages[0].transform;
        originalWidth = new float[] { rt.rect.width, rt.rect.width, rt.rect.width, rt.rect.width };

        // Initialize health to max health in the beginning
        for (int i = 0; i < maxMana.Length; i++)
        {
            currentMana[i] = maxMana[i];
        }
    }

    // Should be an array of damage taken for each player 
    // ex. [0, 0, 25, 5]
    void RemoveMana(int[] manaUsed)
    {
        for (int i = 0; i < manaUsed.Length; i++)
        {
            if (currentMana[i] <= 0)
            {
                // THEY DIED
                continue;
            }
            float amountDamage = manaUsed[i];
            currentMana[i] -= amountDamage;
            RectTransform rt = (RectTransform)playerManaImages[i].transform;
            rt.sizeDelta = new Vector2((currentMana[i] / maxMana[i]) * originalWidth[i], rt.sizeDelta.y);
            rt.transform.position = new Vector2(rt.transform.position.x - 7 * amountDamage / 8, rt.transform.position.y);
        }
    }

        //RemoveMana(new int[4] { 10, 25, 10, 15 }); This is how you remove some mana
}
