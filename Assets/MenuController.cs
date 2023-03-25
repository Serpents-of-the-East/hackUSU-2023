using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Overview Menu")]
    public GameObject overviewFirstSelected;

    [Header("Attack Menu")]
    public GameObject AttackMenu;
    public GameObject attackFirstSelected;

    [Header("Items Menu")]
    public GameObject ItemsMenu;
    public GameObject itemsFirstSelected;


    public GameObject eventSystem;

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
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(itemsFirstSelected);
        ItemsMenu.SetActive(true);
    }

    public void UseItem()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        ItemsMenu.SetActive(false);
    }

    public void ItemsMenuClose()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        ItemsMenu.SetActive(false);
    }

    public void AttackMenuOpen()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(attackFirstSelected);
        AttackMenu.SetActive(true);
    }

    public void Attack()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        AttackMenu.SetActive(false);
    }

    public void AttackMenuClose()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        AttackMenu.SetActive(false);
    }
}
