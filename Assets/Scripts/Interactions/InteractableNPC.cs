using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableNPC : MonoBehaviour
{
    public GameObject dialogueTexture;
    public bool completedTalkingTo;
    public bool interactable;
    public TextAsset dialogue;
    public int lineOfText = 0;
    public int linesInText;
    public string[] lines;

    // Start is called before the first frame update
    void Start()
    {
        if (interactable && dialogue != null)
        {
            lines = dialogue.text.Split("\n"[0]);
            linesInText = lines.Length;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (completedTalkingTo)
        {
            dialogueTexture.SetActive(false);
        }
    }
}
