using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaTriggerWatch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("ToForest"))
        {
            GameObject travelObj = GameObject.FindGameObjectWithTag("TravelHandler");
            TravelHandler travelHandler = travelObj.GetComponent<TravelHandler>();
            travelHandler.lastPointTouched = TravelHandler.LastPointTouched.city_to_forest;
            SceneManager.LoadScene("ForestPath_Day");
        }

        if (other.gameObject.CompareTag("ToTown"))
        {
            GameObject travelObj = GameObject.FindGameObjectWithTag("TravelHandler");
            TravelHandler travelHandler = travelObj.GetComponent<TravelHandler>();
            travelHandler.lastPointTouched = TravelHandler.LastPointTouched.forest_to_city;
            SceneManager.LoadScene("Village_Day");
        }
    }
}
