using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using TMPro;
using UnityEngine;

public class QuestManagement : MonoBehaviour
{
    public class QuestStep
    {
        public string questName;
        public int currentStep;
        public string keyItem;
        public string[] allSteps;
    }

    public Dictionary<string, QuestStep> currentQuests;
    public List<string> keyItems;

    private void Start()
    {
        currentQuests = new Dictionary<string, QuestStep>();
        keyItems = new List<string>();
    }

    public void StartQuest(QuestStep quest)
    {
        Debug.Log("Starting a new quest: " + quest.questName);
        Debug.Log("Look for the: " + quest.keyItem);
        currentQuests[quest.questName.Trim()] = quest;

        if (keyItems.Contains(quest.keyItem.Trim()))
        {
            Debug.Log("Already have the item");
            // already have the item
            ProgressQuest(quest.questName.Trim());
        }
    }

    public void AddKeyItem(string itemTag)
    {
        keyItems.Add(itemTag.Trim());
        string questToProgress = null;
        foreach (var quest in currentQuests)
        {
            Debug.Log(quest.Value.keyItem);
            if (quest.Value.keyItem.Trim() == itemTag.Trim())
            {
                Debug.Log("Found matching key item");
                questToProgress = quest.Value.questName;
            }
        }
        if (questToProgress != null)
        {
            ProgressQuest(questToProgress);
        }
    }

    public void ProgressQuest(string questName)
    {
        if (currentQuests[questName.Trim()].currentStep < currentQuests[questName.Trim()].allSteps.Length)
        {
            Debug.Log("Completed a quest!");
            currentQuests[questName.Trim()].currentStep += 1;

            GameObject obj = GameObject.FindGameObjectWithTag("QuestTitle");
            Debug.Log(obj.name);
            TMP_Text textMeshPro = obj.GetComponent<TMP_Text>();
            Debug.Log(textMeshPro);
            textMeshPro.text = questName.Trim();

            GameObject titleCard = GameObject.FindGameObjectWithTag("QuestCompletionTitleCard");
            titleCard.GetComponent<Animator>().SetTrigger("Start");

        }
    }
}
