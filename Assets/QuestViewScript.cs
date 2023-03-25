using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class QuestViewScript : MonoBehaviour
{
    public PlayerInventory playerInventory;
    public List<Quest> quests;
    // Start is called before the first frame update
    void Start()
    {
        quests = playerInventory.quests;
    }


    void DrawQuests()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }
}
