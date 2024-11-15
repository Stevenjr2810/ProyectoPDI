using UnityEngine;
using UnityEngine.UI;

public class CollectionManager : MonoBehaviour
{
    public int totalPaintings = 3; // Número total de obras en el escenario
    private int collectedPaintings = 0; // Contador de obras recolectadas
    public Text collectionText; // UI para mostrar la cantidad de obras recolectadas
    public GameObject victoryPanel; // Panel de victoria para mostrar cuando el jugador complete el escenario

    void Start()
    {
        UpdateCollectionText();
        victoryPanel.SetActive(false); // Asegurarse de que el panel de victoria esté oculto al inicio
    }

    public void CollectPainting()
    {
        collectedPaintings++;
        UpdateCollectionText();

        if (collectedPaintings >= totalPaintings)
        {
            Win();
        }
    }

    void UpdateCollectionText()
    {
        collectionText.text = $"Obras: {collectedPaintings}/{totalPaintings}";
    }

    void Win()
    {
        // Muestra el panel de victoria y pausa el juego
        victoryPanel.SetActive(true);
        Time.timeScale = 0;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
}
