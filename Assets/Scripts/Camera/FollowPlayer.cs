using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    public GameObject player;
    Vector3 offset;
    private Transform cameraPosition;
    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = GetComponent<Transform>();
        offset = cameraPosition.position - player.transform.position;
        Debug.Log(offset);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = player.transform.position + offset;
    }
}
