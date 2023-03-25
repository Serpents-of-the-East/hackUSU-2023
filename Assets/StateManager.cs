using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StateManager : MonoBehaviour
{
    // Start is called before the first frame update
    public bool isBattle;
    public bool isPaused;
    public bool isPlaying;

    public PlayerInventory playerInventory;

    public void OnPause(InputValue value)
    {
    }
    void Start()
    {
        playerInventory = GetComponent<PlayerInventory>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
