using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemsViewScript : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Item> items;

    // Start is called before the first frame update
    void Start()
    {
        items = playerInventory.items;
    }

    public void UseItem(Item item)
    {
        playerInventory.RemoveItem(item);
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
