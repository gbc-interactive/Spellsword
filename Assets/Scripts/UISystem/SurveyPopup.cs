using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurveyPopup : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("http://google.com/");
        SceneManager.LoadScene(0);
    }
}
