using System.Collections;
using System.Collections.Generic;
using TMPro;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;
using UnityEngine.SceneManagement;

public class UITitleHandler : MonoBehaviour {

    public TMP_InputField nameInput;

    public void LoadMainScene() {
        DataManager.Instance.playingUserName = nameInput.text;
        DataManager.Instance.LoadUserRanking();
        SceneManager.LoadScene("main");
    }

    public void QuitGame() {
#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();  
#endif
    }
}
