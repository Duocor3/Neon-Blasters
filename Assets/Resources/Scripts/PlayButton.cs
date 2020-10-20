using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour {

    public GameObject popup;
    public GameObject popup2;

    public void SceneLoader()
    {
        SceneManager.LoadScene("Play Scene");
    }

    public void Help()
    {
        popup.SetActive(true);
    }

    public  void Credit()
    {
        popup2.SetActive(true);
    }

    public void Hide()
    {
        popup.SetActive(false);
        popup.SetActive(false);
    }
}
