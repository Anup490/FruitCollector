using UnityEngine;
using UnityEngine.UIElements;

class HUD
{
    Label scoreUI;
    Label timerUI;
    Label healthUI;
    Label gameoverUI;
    Label messageUI;

    public HUD(VisualElement ui)
    {
        if (ui != null)
        {
            scoreUI = ui.Query<Label>("ScoreValue").First();
            timerUI = ui.Query<Label>("TimerValue").First();
            healthUI = ui.Query<Label>("HealthValue").First();
            gameoverUI = ui.Query<Label>("GameOverLabel").First();
            messageUI = ui.Query<Label>("MessageValue").First();
            if (healthUI != null)
            {
                IStyle healthUIStyle = healthUI.style;
                healthUIStyle.minWidth = 100;
            }
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

    public void UpdateHealth(int health)
    {
        if (health < 0) return;
        if (healthUI != null)
        {
            healthUI.text = health.ToString();
            IStyle healthUIStyle = healthUI.style;
            healthUIStyle.minWidth = health;
            if (health == 0)
                healthUIStyle.backgroundColor = new Color(1.0f, 1.0f, 1.0f, 0.0f);
            else if (health < 50)
                healthUIStyle.backgroundColor = Color.red;
        }    
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
