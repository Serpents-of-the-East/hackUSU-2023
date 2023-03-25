using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TravelHandler : MonoBehaviour
{

    public enum LastPointTouched
    {
        city_to_forest,
        forest_to_city
    }

    [Header("Forest to City")]
    public Vector3 forestToCityLocation;
    public Vector3 forestToCityCameraLocation;

    [Header("City to Forest")]
    public Vector3 cityToForestLocation;
    public Vector3 cityToForestCameraLocation;

    public LastPointTouched lastPointTouched;

    public string currentOverworldArea;
    public bool loadFromCombat = false;
    public Vector3 loadBackPlayerPosition;
    public Vector3 loadBackCameraPosition;

    private void OnLevelWasLoaded(int level)
    {
        string sceneName = SceneManager.GetActiveScene().name;
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

        if (loadFromCombat)
        {
            player.transform.position = loadBackPlayerPosition;
            mainCamera.transform.position = loadBackCameraPosition;
            loadFromCombat = false;
        }
        else
        {
            if (sceneName == "Village_Day" && lastPointTouched == LastPointTouched.forest_to_city)
            {
                currentOverworldArea = sceneName;
                player.transform.position = forestToCityLocation;
                mainCamera.transform.position = forestToCityCameraLocation;
            }
            if (sceneName == "ForestPath_Day" && lastPointTouched == LastPointTouched.city_to_forest)
            {
                currentOverworldArea = sceneName;
                player.transform.position = cityToForestLocation;
                mainCamera.transform.position = cityToForestCameraLocation;
            }
        }


    }

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("TravelHandler");

        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }
}
