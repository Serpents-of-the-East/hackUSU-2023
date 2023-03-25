using System.Collections;
using System.Collections.Generic;
using TMPro;
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


    public int currentSelectedOpponent = 0;
    public List<GameObject> opponents = new List<GameObject>();
    public List<GameObject> allies = new List<GameObject>();
    public int selectedPlayer = 0;
    public bool choosingOpponent = false;
    private Vector3 nav;
    private int selectedMove = -1;

    public List<int> selectedMoves = new();
    public List<int> targetedOpponent = new();
    public List<bool> disabled = new();



    public List<int> orderOfTurns = new();
    private bool isAnimating = false;
    private int animatedTurn = -1;
    int yourTurns = 0;
    private int currentTurn = 0;

    private bool alreadyUpdated = false;
    private bool opponentAttacking = false;
    public bool victorious = false;

    // Start is called before the first frame update
    void Start()
    {
        lastSelectedScreen = overview;
    }

    // Update is called once per frame
    void Update()
    {
        if (!victorious)
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

                if (opponents.Count == 0)
                {
                    victorious = true;
                }
            }
        }
        else
        {
            Debug.Log("Victory!");
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


        // Set the move names
        switch(selectedPlayer)
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
        opponents[0].GetComponent<SelectedScript>().isSelected = true;
        selectedMove = id - 1;
        selectedMoves.Add(id);

        switch(selectedPlayer)
        {
            case 0:
                inventoryAndStats.blackMageStats.currentSP -= inventoryAndStats.blackMageMoves[selectedMove].spCost;
                break;
            case 1:
                inventoryAndStats.whiteMageStats.currentSP -= inventoryAndStats.whiteMageMoves[selectedMove].spCost;

                break;
            case 2:
                inventoryAndStats.warriorStats.currentSP -= inventoryAndStats.warriorMoves[selectedMove].spCost;
                break;
            case 3:
                inventoryAndStats.archerStats.currentSP -= inventoryAndStats.archerMoves[selectedMove].spCost;
                break;
            default:
                break;
        }



        eventSystem.SetActive(false);
        choosingOpponent = true;
        selectedPlayer++;
        if (selectedPlayer > 3)
        {
            selectedPlayer = 0;
        }
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
            targetedOpponent.Add(Random.Range(0, 3));
        }
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


        foreach (var i in  order)
        {
            orderOfTurns.Add(i.Key);
        }

    }

    public void ProceedAttacking()
    {
        Debug.Log(currentTurn);
        Debug.Log("Order of Turns: " + orderOfTurns[currentTurn]);
        Debug.Log(targetedOpponent[orderOfTurns[currentTurn]]);
        Debug.Log(opponents[targetedOpponent[orderOfTurns[currentTurn]]]);
        Debug.Log("Opponent Count: " + opponents.Count);
        if (opponents.Count == 0)
        {
            victorious = true;
            return;
        }

        if (orderOfTurns[currentTurn] > opponents.Count + allies.Count)
        {
            yourTurns = 0;
            eventSystem.SetActive(true);
            overview.SetActive(true);
            orderOfTurns.Clear();
            selectedMoves.Clear();
            targetedOpponent.Clear();
            currentTurn = 0;
            alreadyUpdated = false;

            opponents.ForEach(e => Debug.Log(e.GetComponentInChildren<EnemyScript>().gameObject.name));

            opponents.RemoveAll(e => e.GetComponentInChildren<EnemyScript>().isDead);
        }


        if (!opponentAttacking && orderOfTurns[currentTurn] < 4 && !isAnimating)
        {
            animatedTurn = currentTurn;
            allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().StartAnimation(false);
            isAnimating = true;
        }
        else if (orderOfTurns[currentTurn] - allies.Count > 0 && opponents[orderOfTurns[currentTurn] - allies.Count].GetComponentInChildren<EnemyScript>().isDead)
        {
            currentTurn++;
        }
        else if (targetedOpponent[orderOfTurns[currentTurn]] < targetedOpponent.Count && opponents[targetedOpponent[orderOfTurns[currentTurn]]] != null && !opponentAttacking && orderOfTurns[currentTurn] < 4 && animatedTurn != -1 && !(allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().isMagic || allies[orderOfTurns[animatedTurn]].GetComponent<AnimationHandler>().isPhysical))
        {
            float damage = 0;
            bool isPhysical = false;

            switch (orderOfTurns[currentTurn])
            {
                case 0:
                    damage = inventoryAndStats.blackMageMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.blackMageMoves[selectedMove].elemental;
                    break;
                case 1:
                    damage = inventoryAndStats.whiteMageMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.whiteMageMoves[selectedMove].elemental;
                    break;
                case 2:
                    damage = inventoryAndStats.warriorMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.warriorMoves[selectedMove].elemental;
                    break;
                case 3:
                    damage = inventoryAndStats.archerMoves[selectedMove].damage;
                    isPhysical = !inventoryAndStats.archerMoves[selectedMove].elemental;
                    break;
                default:
                    break;
            }

            if (opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>() != null && opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>().gameObject.activeSelf)
            {

                opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>().TakeDamage(inventoryAndStats.DealDamage(damage * 1000, isPhysical, orderOfTurns[currentTurn]), isPhysical);

                if (opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>().isDead)
                {
                    opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>().gameObject.SetActive(false);
                }

            }

            /*            if (opponents[targetedOpponent[orderOfTurns[animatedTurn]]] != null && )
                        {
                            Debug.Log("It's dead");
                            //opponents.Remove(opponents[targetedOpponent[orderOfTurns[animatedTurn]]]);
                        }*/


            isAnimating = false;
            currentTurn++;
        }
        else if (orderOfTurns[currentTurn] >= 4 && !isAnimating && (opponents[targetedOpponent[orderOfTurns[animatedTurn]]].GetComponentInChildren<EnemyScript>()?.gameObject?.activeSelf ?? false))
        {
            animatedTurn = currentTurn;
            opponentAttacking = true;
            opponents[orderOfTurns[animatedTurn] - allies.Count].GetComponentInChildren<EnemyScript>().StartAnimation();
            isAnimating = true;
        }
        else if (opponentAttacking && isAnimating && !opponents[orderOfTurns[animatedTurn] - allies.Count].GetComponentInChildren<EnemyScript>().isAttacking)
        {
            opponentAttacking = false;
            opponents[orderOfTurns[animatedTurn] - allies.Count].GetComponentInChildren<EnemyScript>().isAttacking = false;


            inventoryAndStats.TakeDamage(Random.Range(0, 50), Random.Range(0, 2) == 0, (targetedOpponent[orderOfTurns[animatedTurn] - allies.Count]));

            isAnimating = false;
            currentTurn++;
            animatedTurn = -1;
        }
        else
        {
            currentTurn++;
        }

        if (currentTurn > allies.Count + opponents.Count - 1)
        {
            yourTurns = 0;
            eventSystem.SetActive(true);
            overview.SetActive(true);
            orderOfTurns.Clear();
            selectedMoves.Clear();
            targetedOpponent.Clear();
            currentTurn = 0;
            alreadyUpdated = false;

            opponents.RemoveAll((e => e.GetComponentInChildren<EnemyScript>().isDead));

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
            this.targetedOpponent.Add(currentSelectedOpponent);
            Debug.Log("Currently Selected: " + currentSelectedOpponent);
            Debug.Log("Currently opponet: " + opponents[currentSelectedOpponent]);

            eventSystem.SetActive(true);
            choosingOpponent = false;
            overview.SetActive(true);
            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            yourTurns++;
        }
        if (inputValue.isPressed && !eventSystem.activeSelf && yourTurns == 3)
        {
            this.selectedMoves.Add(selectedMove);
            this.targetedOpponent.Add(currentSelectedOpponent);

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

            if (currentSelectedOpponent < opponents.Count)
            {
                opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = false;
            }
            else
            {
                currentSelectedOpponent = opponents.Count - 1;
            }

            if (opponents.Count == 1)
            {
                currentSelectedOpponent = 0;
            }
            else
            {
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
            }
           

            opponents[currentSelectedOpponent].GetComponent<SelectedScript>().isSelected = true;
        }
    }


}
