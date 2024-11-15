using UnityEngine;
using UnityEngine.UI;

public class InstructionsUI : MonoBehaviour
{
    public GameObject instructionsPanel; // Panel que contiene la imagen de instrucciones y el botón
    public GameTimer gameTimer;

    void Start()
    {
        // Al iniciar el juego, mostramos el panel de instrucciones y desbloqueamos el cursor
        instructionsPanel.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    // Método que se llama al hacer clic en el botón "Comenzar"
    public void Stari()
{
    instructionsPanel.SetActive(false); // Oculta el panel de instrucciones
    Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor para la vista en primera persona
    Cursor.visible = false;
    gameTimer.StartTimer();
}

}

