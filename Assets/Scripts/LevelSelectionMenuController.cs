using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelSelectionMenuController : MonoBehaviour
{
    //LoadingScreen DontDestroyOnLoad

    public void SelectLevel(int levelNumber)
    {
        ChangeScene("Stage" + levelNumber);
    }

    public void BackToMainMenu()
    {
        ChangeScene("MainMenu");
    }

    private void ChangeScene(string sceneName)
    {
        LoadingScreen.instance.LoadScene(sceneName);
    }
}
