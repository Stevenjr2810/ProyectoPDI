using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance;
    public GameObject instructionsPanel;
    public GameObject gameOverPanel;

    void Awake()
    {
        Instance = this;
    }

    public void ShowInstructions()
    {
        instructionsPanel.SetActive(true);
    }

    public void HideInstructions()
    {
        instructionsPanel.SetActive(false);
    }

    public void ShowGameOverPanel()
    {
        gameOverPanel.SetActive(true);
    }

    public void HideGameOverPanel()
    {
        gameOverPanel.SetActive(false);
    }
}

