using UnityEngine;

public class CanvasController : MonoBehaviour
{
    // Lista de todos los Canvas que quieres controlar
    public GameObject[] canvasArray;

    // Método para activar el Canvas correspondiente y desactivar los demás
    public void ShowCanvas(int index)
    {
        for (int i = 0; i < canvasArray.Length; i++)
        {
            if (i == index)
            {
                canvasArray[i].SetActive(true);  // Activa el Canvas seleccionado
            }
            else
            {
                canvasArray[i].SetActive(false); // Desactiva los demás
            }
        }
    }

    // Método para activar el Canvas según la clasificación
    public void ShowCanvasByClassification(int classificationIndex)
    {
        for (int i = 0; i < canvasArray.Length; i++)
        {
            canvasArray[i].SetActive(i == classificationIndex);
        }
    }
}

