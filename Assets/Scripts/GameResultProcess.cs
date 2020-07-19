using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameResultProcess : MonoBehaviour
{
    public TextMeshProUGUI winLoseText;
    
    private CanvasGroup gameResultCanvasGroup;
    private CanvasGroup[] allCanvasGroups;

    private void Start()
    {
        gameResultCanvasGroup = GetComponent<CanvasGroup>();
        allCanvasGroups = FindObjectsOfType<CanvasGroup>();
    }

    public void ProcessWin()
    {
        ProcessWinLose(true);
    }

    public void ProcessLose()
    {
        ProcessWinLose(false);
    }

    private void ProcessWinLose(bool isWin=false)
    {
        foreach(CanvasGroup canvasGroup in allCanvasGroups)
            if(canvasGroup != gameResultCanvasGroup){
                Utility.SetCanvasGroupEnabled(canvasGroup, false);
                Debug.Log(canvasGroup.name);
            }
        
        Utility.SetCanvasGroupEnabled(gameResultCanvasGroup, true);
        winLoseText.text = "You " + (isWin ? "won!" : "lose!");
    }

}
