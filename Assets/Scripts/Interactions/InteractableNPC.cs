using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using System;



public class InteractableNPC : MonoBehaviour
{
    public GameObject dialogueTexture;
    public bool completedTalkingTo;
    public bool interactable;
    public bool interactedWith;
    public TextAsset dialogue;
    public TextAsset questTxtFile;
    public Canvas canvasDialogueInGame;
    public TMP_Text textDialogueInGame;
    public int lineOfText = 0;
    public string currentLine;
    public bool firstInteraction = true;
    public int linesInText;
    public string[] lines;
    public string[] questLines;
    public Quest quest;
    public AudioClip[] audioClips;
    public AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        canvasDialogueInGame.gameObject.SetActive(false);
        if (interactable && dialogue != null)
        {
            lines = dialogue.text.Split("\n"[0]);
            currentLine = lines[0];
            linesInText = lines.Length;

            quest = new Quest();

            questLines = questTxtFile.text.Split("\n"[0]);

            quest.id = int.Parse(questLines[0]);
            quest.Name = questLines[1];
            quest.Description = questLines[2];
            quest.Required = bool.Parse(questLines[3]);
            quest.Completed = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (interactedWith && firstInteraction)
        {
            dialogueTexture.SetActive(false);
            canvasDialogueInGame.gameObject.SetActive(true);
            textDialogueInGame.text = currentLine;
            audioSource.clip = audioClips[lineOfText];
            audioSource.Play();
              
            firstInteraction = false;
            interactedWith = false;

        } 
        else if (interactedWith && !completedTalkingTo)
        {
            lineOfText += 1;

            if (lineOfText < linesInText)
            {
                currentLine = lines[lineOfText];
                textDialogueInGame.text = currentLine;
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                    audioSource.clip = audioClips[lineOfText];
                    audioSource.Play();
                }
                else if (audioSource != null)
                {
                    audioSource.clip = audioClips[lineOfText];
                    audioSource.Play();
                }
            }
            else
            {
                if (audioSource != null && audioSource.isPlaying)
                {
                    audioSource.Stop();
                }
                completedTalkingTo = true;
                interactable = false;
                canvasDialogueInGame.gameObject.SetActive(false);
            }

            interactedWith = false;
        }
    }
}
