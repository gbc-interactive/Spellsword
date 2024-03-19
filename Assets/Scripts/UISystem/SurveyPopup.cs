using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurveyPopup : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://forms.gle/kdqNMye5NCMVtqBu9");
        SceneManager.LoadScene(0);
    }
}
