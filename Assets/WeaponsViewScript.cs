using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponsViewScript : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Weapon> weapons;
    // Start is called before the first frame update
    void Start()
    {
        this.weapons = playerInventory.weapons;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
