using UnityEngine;
using UnityEngine.UI;

public class InteractablePainting : MonoBehaviour
{
    public GameObject interactionText; // Texto que indica "Presiona E para interactuar"
    public GameObject infoCanvas; // Canvas que muestra la información de la obra
    public CollectionManager collectionManager; // Referencia al CollectionManager para registrar la recolección

    private bool isPlayerNearby = false;
    private bool isCollected = false; // Control para evitar múltiples recolecciones

    void Start()
    {
        interactionText.SetActive(false); // Ocultamos el texto al inicio
        infoCanvas.SetActive(false); // Ocultamos el canvas de información al inicio
    }

    void Update()
    {
        // Si el jugador está cerca y presiona la tecla E
        if (isPlayerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ShowInfo();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Si el jugador entra en el área de interacción
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
            interactionText.SetActive(true); // Muestra el mensaje de interacción
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si el jugador sale del área de interacción
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
            interactionText.SetActive(false); // Oculta el mensaje de interacción
        }
    }

    private void ShowInfo()
    {
        // Pausa el juego y muestra el Canvas con la información de la obra
        Time.timeScale = 0; // Pausa el juego
        infoCanvas.SetActive(true); // Muestra el Canvas de información
        interactionText.SetActive(false); // Oculta el mensaje de interacción mientras está en el Canvas

        // Muestra el cursor y desbloquea el control de la cámara
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void ResumeGame()
    {
        // Reanuda el juego y oculta el Canvas de información
        Time.timeScale = 1; // Reanuda el tiempo
        infoCanvas.SetActive(false); // Oculta el Canvas de información

        // Oculta el cursor y bloquea el control de la cámara en primera persona
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Registrar la recolección si aún no ha sido recolectada
        if (!isCollected)
        {
            isCollected = true; // Marca la obra como recolectada
            collectionManager.CollectPainting(); // Notifica al CollectionManager
        }
    }
}
