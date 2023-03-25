using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public GameObject AttackMenu;
    public GameObject ItemsMenu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ItemsMenuOpen()
    {
        ItemsMenu.SetActive(true);
    }

    public void ItemsMenuClose()
    {
        ItemsMenu.SetActive(false);
    }

    public void AttackMenuOpen()
    {
        AttackMenu.SetActive(true);
    }

    public void AttackMenuClose()
    {
        AttackMenu.SetActive(false);
    }
}
