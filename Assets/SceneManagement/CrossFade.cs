using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CrossFade : MonoBehaviour
{
    public Animator crossFade;

    IEnumerator LoadNext(string scenename)
    {
        crossFade.SetTrigger("Start");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(scenename);

    }

    public void LoadScene(string scenename)
    {
        StartCoroutine(LoadNext(scenename));
    }
}
