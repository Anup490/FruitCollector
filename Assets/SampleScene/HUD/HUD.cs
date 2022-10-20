using UnityEngine;
using UnityEngine.UIElements;

class HUD
{
    Label scoreUI;
    Label timerUI;
    Label gameoverUI;
    Label messageUI;

    public HUD(VisualElement ui)
    {
        if (ui != null)
        {
            scoreUI = ui.Query<Label>("ScoreValue").First();
            timerUI = ui.Query<Label>("TimerValue").First();
            gameoverUI = ui.Query<Label>("GameOverLabel").First();
            messageUI = ui.Query<Label>("MessageValue").First();
            if (gameoverUI != null)
                gameoverUI.visible = false;
            if (messageUI != null)
                messageUI.visible = false;
        }
    }

    public void UpdateScore(int score)
    {
        if (scoreUI != null)
            scoreUI.text = score.ToString();
    }

    public void UpdateTimer(int timer)
    {
        if (timerUI != null)
            timerUI.text = timer.ToString();
    }

    public void ShowGameOver(string message)
    {
        if (gameoverUI != null)
            gameoverUI.visible = true;
        if (messageUI != null)
        {
            messageUI.visible = true;
            messageUI.text = message;
        }   
    }
}
