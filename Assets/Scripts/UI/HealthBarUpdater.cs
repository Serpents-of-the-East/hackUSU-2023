using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUpdater : MonoBehaviour
{

    public PlayerInventory inventory;
    public Image[] playerHealthImages;

    public int maxHealth;
    public int currentHealth;

    private float originalWidth;

    // Start is called before the first frame update
    void Start()
    {
        originalWidth = playerHealthImages[0].rectTransform.rect.width;
    }

    // Update is called once per frame
    void Update()
    {
        playerHealthImages[0].fillAmount = currentHealth / (float)maxHealth;
    }
}
