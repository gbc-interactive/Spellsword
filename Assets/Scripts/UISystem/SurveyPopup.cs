using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SurveyPopup : MonoBehaviour
{
    public void OpenURL()
    {
        Application.OpenURL("https://docs.google.com/forms/d/1WOHPHaMUPPAg0FT-29aq9pn9KIsbW5GoLJkd8GBpU8c/edit");
        SceneManager.LoadScene(0);
    }
}
