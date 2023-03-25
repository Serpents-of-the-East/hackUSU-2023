using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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

    public InventoryAndStats inventoryAndStats;

    public GameObject eventSystem;
    public GameObject lastSelectedScreen;

    public TMP_Text Action1;
    public TMP_Text Action2;
    public TMP_Text Action3;
    public TMP_Text Action4;
    public TMP_Text Action5;
    public TMP_Text Action6;


    public List<GameObject> opponents = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();
    public int selectedPlayer = 0;
    public bool choosingOpponent = false;
    private Vector3 nav;

    private int selectedMove = -1; // locked in
    public int currentSelectedOpponent = 0;
    private int lockedInOpponentChoice = -1; // locked in

    public List<bool> disabled = new();

    public List<int> orderOfTurns = new();
    private int currentTurnIndex = 0;
    private bool isAnimating = false;

    private bool alreadyUpdated = false;
    public bool victorious = false;

    // Start is called before the first frame update
    void Start()
    {
        lastSelectedScreen = overview;
        disabled = new();
        for (int i = 0; i < allies.Count + opponents.Count; i++)
        {
            disabled.Add(false);
        }

        DetermineOrder();
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = allies.Count; i < disabled.Count; i++)
        {
            if (disabled[i])
            {
                opponents[i - allies.Count].transform.localScale = Vector3.zero;
            }
        }

        if (disabled.GetRange(4, disabled.Count - allies.Count).TrueForAll(e => e))
        {
            victorious = true;
        }
        else
        {
            if (currentTurnIndex >= opponents.Count + allies.Count)
            {
                DetermineOrder();

            }
            else
            {
                if (!isAnimating)
                {
                    // Go to next person's turn
                    HandleNextTurn();
                }
                else
                {
                    if (orderOfTurns[currentTurnIndex] < allies.Count)
                    {
                        // Check if player is in animation still
                        var animationHandler = allies[orderOfTurns[currentTurnIndex]].GetComponent<AnimationHandler>();
                        isAnimating = animationHandler.isPhysical || animationHandler.isMagic;
                    }
                    else
                    {
                        EnemyScript enemy = opponents[orderOfTurns[currentTurnIndex] - allies.Count].GetComponentInChildren<EnemyScript>();
                        isAnimating = enemy.isAttacking;
                    }

                }
            }
        }
    }

    private void HandleNextTurn()
    {
        while (disabled[currentTurnIndex])
        {
            currentTurnIndex++;
            if (currentTurnIndex >= disabled.Count)
            {
                currentTurnIndex = 0;
            }
        }

        if (orderOfTurns[currentTurnIndex] < allies.Count)
        {
            // Player turn
            PlayerTurn();
        }
        else
        {
            Debug.Log("Enemy Turn Should happen");
            EnemyScript enemy = opponents[orderOfTurns[currentTurnIndex] - allies.Count].GetComponentInChildren<EnemyScript>();
            if (!enemy.isAttacking)
            {
                EnemyTurn();
            }
            // Enemy turn
        }
    }

    private void PlayerTurn()
    {
        if (orderOfTurns[currentTurnIndex] > allies.Count - 1)
        {
            return;
        }

        if (selectedMove == -1)
        {
            eventSystem.SetActive(true);
            overview.SetActive(true);
        }
        else if (lockedInOpponentChoice != -1)
        {
            // Finally, start animation, take mana, and damage

            // Take mana
            float damage = 0;
            bool isPhysical = false;

            switch (orderOfTurns[currentTurnIndex])
            {
                case 0:
                    inventoryAndStats.blackMageStats.currentSP -= inventoryAndStats.blackMageMoves[selectedMove].spCost;
                    damage = inventoryAndStats.blackMageMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.blackMageMoves[selectedMove].elemental;
                    break;
                case 1:
                    inventoryAndStats.whiteMageStats.currentSP -= inventoryAndStats.whiteMageMoves[selectedMove].spCost;
                    damage = inventoryAndStats.whiteMageMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.whiteMageMoves[selectedMove].elemental;
                    break;
                case 2:
                    inventoryAndStats.warriorStats.currentSP -= inventoryAndStats.warriorMoves[selectedMove].spCost;
                    damage = inventoryAndStats.warriorMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.warriorMoves[selectedMove].elemental;
                    break;
                case 3:
                    inventoryAndStats.archerStats.currentSP -= inventoryAndStats.archerMoves[selectedMove].spCost;
                    damage = inventoryAndStats.archerMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.archerMoves[selectedMove].elemental;
                    break;
                default:
                    break;
            }

            // Dealt Damage
            opponents[lockedInOpponentChoice].GetComponentInChildren<EnemyScript>().TakeDamage(inventoryAndStats.DealDamage(damage, isPhysical, orderOfTurns[currentTurnIndex]), isPhysical);
            disabled[lockedInOpponentChoice + allies.Count] = opponents[lockedInOpponentChoice].GetComponentInChildren<EnemyScript>().isDead;


            // Start Animation
            isAnimating = true;
            allies[orderOfTurns[currentTurnIndex]].GetComponent<AnimationHandler>().StartAnimation(isPhysical);

            lockedInOpponentChoice = -1;
            selectedMove = -1;
            currentTurnIndex++;
        }
    }

    private void EnemyTurn()
    {
        isAnimating = true;
        inventoryAndStats.TakeDamage(Random.Range(0, 50), Random.Range(0, 2) == 0, Random.Range(0, allies.Count));

        EnemyScript enemy = opponents[orderOfTurns[currentTurnIndex] - allies.Count].GetComponentInChildren<EnemyScript>();

        disabled[0] = inventoryAndStats.blackMageStats.currentHealth <= 0;
        disabled[1] = inventoryAndStats.whiteMageStats.currentHealth <= 0;
        disabled[2] = inventoryAndStats.warriorStats.currentHealth <= 0;
        disabled[3] = inventoryAndStats.archerStats.currentHealth <= 0;

        enemy.StartAnimation();
        currentTurnIndex++;
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


        // Set the move names
        switch (selectedPlayer)
        {
            case 0:
                Action1.text = inventoryAndStats.blackMageMoves[0].Name;
                Action2.text = inventoryAndStats.blackMageMoves[1].Name;
                Action3.text = inventoryAndStats.blackMageMoves[2].Name;
                Action4.text = inventoryAndStats.blackMageMoves[3].Name;
                Action5.text = inventoryAndStats.blackMageMoves[4].Name;
                Action6.text = inventoryAndStats.blackMageMoves[5].Name;
                break;
            case 1:
                Action1.text = inventoryAndStats.whiteMageMoves[0].Name;
                Action2.text = inventoryAndStats.whiteMageMoves[1].Name;
                Action3.text = inventoryAndStats.whiteMageMoves[2].Name;
                Action4.text = inventoryAndStats.whiteMageMoves[3].Name;
                Action5.text = inventoryAndStats.whiteMageMoves[4].Name;
                Action6.text = inventoryAndStats.whiteMageMoves[5].Name;
                break;
            case 2:
                Action1.text = inventoryAndStats.warriorMoves[0].Name;
                Action2.text = inventoryAndStats.warriorMoves[1].Name;
                Action3.text = inventoryAndStats.warriorMoves[2].Name;
                Action4.text = inventoryAndStats.warriorMoves[3].Name;
                Action5.text = inventoryAndStats.warriorMoves[4].Name;
                Action6.text = inventoryAndStats.warriorMoves[5].Name;
                break;
            case 3:
                Action1.text = inventoryAndStats.archerMoves[0].Name;
                Action2.text = inventoryAndStats.archerMoves[1].Name;
                Action3.text = inventoryAndStats.archerMoves[2].Name;
                Action4.text = inventoryAndStats.archerMoves[3].Name;
                Action5.text = inventoryAndStats.archerMoves[4].Name;
                Action6.text = inventoryAndStats.archerMoves[5].Name;
                break;
            default:
                break;

        }

        AttackMenu.SetActive(true);
    }

    public void Attack(int id)
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        AttackMenu.SetActive(false);
        overview.SetActive(false);
        for (int i = allies.Count; i < disabled.Count; i++)
        {
            if (!disabled[i])
            {
                opponents[i - allies.Count].GetComponent<SelectedScript>().isSelected = true;
                break;
            }
        }
        selectedMove = id - 1;
        eventSystem.SetActive(false);
    }

    public void AttackMenuClose()
    {
        EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
        AttackMenu.SetActive(false);
    }

    public void DetermineOrder()
    {
        List<KeyValuePair<int, float>> order = new();
        orderOfTurns.Clear();
        for (int i = 0; i < opponents.Count + allies.Count; i++)
        {
            if (i < allies.Count)
            {
                order.Add(new KeyValuePair<int, float>(i, inventoryAndStats.GetSpeed(i)));
            }
            else
            {
                order.Add(new KeyValuePair<int, float>(i, opponents[i - allies.Count].GetComponentInChildren<EnemyScript>().GetSpeed()));
            }
        }

        order.Sort((pair1, pair2) => pair1.Value.CompareTo(pair2.Value));


        foreach (var i in order)
        {
            orderOfTurns.Add(i.Key);
        }
        currentTurnIndex = 0;
    }


    public void OnBack(InputValue inputValue)
    {
        if (inputValue.isPressed && eventSystem.activeSelf)
        {
            if (AttackMenu.activeSelf)
            {
                AttackMenu.SetActive(false);
            }
            else if (ItemsMenu.activeSelf)
            {
                ItemsMenu.SetActive(false);
            }
            lastSelectedScreen = EventSystem.current.currentSelectedGameObject;
            EventSystem.current.GetComponent<EventSystem>().SetSelectedGameObject(overviewFirstSelected);
        }
    }


    public void OnSubmit(InputValue inputValue)
    {
        if (selectedMove == -1 || currentTurnIndex == -1 || orderOfTurns[currentTurnIndex] >= allies.Count || isAnimating)
        {
            return;
        }

        // We have a move, and have now selected enemy
        if (inputValue.isPressed && selectedMove != -1)
        {
            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            lockedInOpponentChoice = currentSelectedOpponent;
        }
    }


    public void OnSelectEnemy(InputValue inputValue)
    {
        if (selectedMove != -1)
        {
            nav = inputValue.Get<Vector2>();

            if (currentSelectedOpponent != -1)
            {
                opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            }
            int direction = nav.y > 0 ? 1 : -1;
            currentSelectedOpponent += direction;
            if (currentSelectedOpponent < 0)
            {
                currentSelectedOpponent = opponents.Count - 1;
            }
            currentSelectedOpponent %= opponents.Count;
            while (disabled[allies.Count + currentSelectedOpponent])
            {
                currentSelectedOpponent += direction;
                if (currentSelectedOpponent < 0)
                {
                    currentSelectedOpponent = opponents.Count - 1;
                }
                currentSelectedOpponent %= opponents.Count;
            }

            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = true;
        }
    }


}