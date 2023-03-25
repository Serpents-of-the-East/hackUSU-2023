using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    [Header("Overview Menu")]
    public GameObject overviewFirstSelected;
    public GameObject overview;

    [Header("Attack Menu")]
    public GameObject AttackMenu;
    public GameObject attackFirstSelected;

    [Header("Items Menu")]
    public GameObject ItemsMenu;
    public GameObject itemsFirstSelected;


    public GameObject eventSystem;
    public GameObject lastSelectedScreen;

    public int currentSelectedOpponent = 0;
    public List<GameObject> opponents = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();

    public bool choosingOpponent = false;
    private Vector3 nav;
    private int selectedMove = -1;

    public List<int> selectedMoves = new();
    public List<int> targettedOpponent = new();

    public List<int> orderOfTurns = new();
    private bool isAnimating = false;
    private int animatedTurn = -1;
    int yourTurns = 0;
    private int currentTurn = 0;

    private bool alreadyUpdated = false;


    // Start is called before the first frame update
    void Start()
    {
        lastSelectedScreen = overview;
    }

    // Update is called once per frame
    void Update()
    {
        if (yourTurns == 4)
        {
            if (!alreadyUpdated)
            {
                DetermineOrder();

                //TODO: Update this with logic
                MonsterAttack();
                alreadyUpdated = true;
            }
            ProceedAttacking();

        }
    }



    public void ItemsMenuOpen()
    {
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(itemsFirstSelected);
        ItemsMenu.SetActive(true);
    }

    public void UseItem()
    {
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        ItemsMenu.SetActive(false);
    }

    public void ItemsMenuClose()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        ItemsMenu.SetActive(false);
    }

    public void AttackMenuOpen()
    {
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(attackFirstSelected);
        AttackMenu.SetActive(true);
    }

    public void Attack(int id)
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        AttackMenu.SetActive(false);
        overview.SetActive(false);
        opponents[0].GetComponent<SelectedScript>().isSelected = true;
        selectedMove = id;
        eventSystem.SetActive(false);
        choosingOpponent = true;
    }

    public void AttackMenuClose()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;

        AttackMenu.SetActive(false);
    }


    public void MonsterAttack()
    {
        for (int i = 0; i < opponents.Count; i++)
        {
            selectedMoves.Add(1);
            
        }
    }

    public void DetermineOrder()
    {
        for (int i = 0; i < opponents.Count + allies.Count; i++)
            orderOfTurns.Add(i);
    }
    
    public void ProceedAttacking()
    {
        if (orderOfTurns[currentTurn] < 4 && !isAnimating)
        {
            animatedTurn = currentTurn;
            allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().StartAnimation(false);
            isAnimating = true;
        }
        else if (animatedTurn != -1 && !(allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().isMagic || allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().isPhysical))
        {
            isAnimating = false;
            currentTurn++;
        }
        
        if (currentTurn > allies.Count - 1 + opponents.Count - 1)
        {
            yourTurns = 0;
            eventSystem.SetActive(true);
            overview.SetActive(true);
            orderOfTurns.Clear();
            selectedMoves.Clear();
            targettedOpponent.Clear();
            currentTurn = 0;
            alreadyUpdated = false;
        }
    }


    public void OnBack(InputValue inputValue)
    {
        if (inputValue.isPressed && eventSystem.activeSelf)
        {
            if (AttackMenu.activeSelf)
            {
                AttackMenu.SetActive(false);
            }
            else if(ItemsMenu.activeSelf)
            {
                ItemsMenu.SetActive(false);
            }
            lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        }
        // TODO: Let them go back to move selection
        else if (inputValue.isPressed && !eventSystem.activeSelf)
        {

        }
    }


    public void OnSubmit(InputValue inputValue)
    {
        if (inputValue.isPressed && !eventSystem.activeSelf && yourTurns < 3)
        {
            this.selectedMoves.Add(selectedMove);
            this.targettedOpponent.Add(currentSelectedOpponent);

            eventSystem.SetActive(true);
            choosingOpponent = false;
            overview.SetActive(true);
            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            yourTurns++;
        }
        if (inputValue.isPressed && !eventSystem.activeSelf && yourTurns == 3)
        {
            this.selectedMoves.Add(selectedMove);
            this.targettedOpponent.Add(currentSelectedOpponent);

            choosingOpponent = false;
            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            yourTurns++;
        }
    }


    public void OnSelectEnemy(InputValue inputValue)
    {
        if (!eventSystem.activeSelf)
        {
            nav = inputValue.Get<Vector2>();

            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;


            if (nav.y > 0)
            {

                currentSelectedOpponent -= 1;
                if (currentSelectedOpponent < 0)
                {
                    currentSelectedOpponent = opponents.Count - 1;
                }
            }
            else if (nav.y < 0)
            {
                currentSelectedOpponent += 1;
                if (currentSelectedOpponent > opponents.Count - 1)
                {
                    currentSelectedOpponent = 0;
                }
            }

            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = true;
        }
    }


}
