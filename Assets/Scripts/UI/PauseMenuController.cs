using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PauseMenuController : MonoBehaviour
{
/*    [Header("Overview Menu")]

    [Header("Quests Menu")]
    public GameObject questsMenu;
    public GameObject questsFirstSelected;*/


    /*[Header("Weapons Menu")]
    public GameObject weaponsMenu;
    public GameObject weaponsFirstSelected;

    [Header("Armor Menu")]
    public GameObject armorMenu;
    public GameObject armorFirstSelected;*/


    public GameObject overviewSelected;
    public GameObject itemsMenu;
    public GameObject eventSystem;

    public void MenuOpen(GameObject menu)
    {
        menu.SetActive(true);
    }

    public void SetSelectedInput(GameObject gameObject)
    {
        overviewSelected = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(gameObject);
    }

    public void UseItem()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewSelected);
        itemsMenu.SetActive(false);
    }

    public void MenuClose(GameObject menu)
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewSelected);
        menu.SetActive(false);
    }

    
}
