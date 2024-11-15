using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    // Método para cambiar a una escena según el nombre de la escena
    public void LoadSceneByName(string sceneName)
    {
        // Verifica que el nombre de la escena no esté vacío
        if (!string.IsNullOrEmpty(sceneName))
        {
            // Carga la escena especificada
            SceneManager.LoadScene(sceneName);
            Time.timeScale = 1;
        }
        else
        {
            Debug.LogWarning("El nombre de la escena está vacío. No se puede cargar la escena.");
        }
    }

    public void ExitGame()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();

    }
}
