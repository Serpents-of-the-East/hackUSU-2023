using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorViewScript : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Armor> armor;


    // Start is called before the first frame update
    void Start()
    {
        this.armor = playerInventory.armors;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
