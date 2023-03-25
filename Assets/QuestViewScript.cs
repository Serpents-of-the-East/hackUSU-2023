using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class QuestViewScript : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Quest> quests;
    // Start is called before the first frame update
    void Start()
    {
        quests = playerInventory.quests;
        DrawQuests();
    }


    void DrawQuests()
    {
        Debug.Log("This was called");
        Button button = gameObject.AddComponent(typeof(Button)) as Button; // WRONG THIS IS ADDING TO THIS COMPONENT I WANT IT TO ADD TO A CHILD
        

    }

    // Update is called once per frame
    void Update()
    {
    }
}
