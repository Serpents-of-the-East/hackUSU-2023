using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectedScript : MonoBehaviour
{
    public SpriteRenderer arrow;
    public bool isSelected;
    public MenuController menuController;
    // Start is called before the first frame update
    void Start()
    {
        arrow.enabled = false;

    }

    // Update is called once per frame
    void Update()
    { 
        arrow.enabled = isSelected;


    }
}
