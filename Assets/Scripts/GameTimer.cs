using UnityEngine;
using UnityEngine.UI;

public class GameTimer : MonoBehaviour
{
    public float timeLimit = 300f; // 5 minutos en segundos
    public Text timerText; // Referencia al UI de texto del temporizador
    private bool gameEnded = false;
    private bool timerActive = false; // Controla si el temporizador está activo

    void Update()
    {
        if (!timerActive || gameEnded) return; // Si el temporizador no está activo o el juego ha terminado, no hace nada

        timeLimit -= Time.deltaTime;
        int minutes = Mathf.FloorToInt(timeLimit / 60);
        int seconds = Mathf.FloorToInt(timeLimit % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);

        if (timeLimit <= 0)
        {
            gameEnded = true;
            EndGame();
        }
    }

    public void StartTimer()
    {
        timerActive = true; // Activa el temporizador cuando se llama a este método
    }

    void EndGame()
    {
        timerText.text = "00:00";
        // Aquí puedes mostrar un mensaje de "Game Over" o activar un panel de fin de juego.
        UIManager.Instance.ShowGameOverPanel();
    }
}

