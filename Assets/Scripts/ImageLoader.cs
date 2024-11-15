using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using SFB;
using System.IO;

public class ImageLoader : MonoBehaviour
{
    public RawImage[] displayImages;  // Array de RawImages para mostrar la imagen en la UI
    public EscenarioClassifier classifier;  // Referencia al clasificador
    public CanvasController canvasController;  // Referencia al CanvasController

    // Método para abrir el explorador de archivos y seleccionar una imagen
    public void LoadImage()
    {
        var paths = StandaloneFileBrowser.OpenFilePanel("Seleccionar Imagen", "", "Imagenes (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg", false);

        if (paths.Length > 0 && !string.IsNullOrEmpty(paths[0]))
        {
            StartCoroutine(LoadAndDisplayImage(paths[0]));
        }
    }

    // Cargar la imagen desde la ruta y actualizar cada RawImage
    private IEnumerator LoadAndDisplayImage(string path)
    {
        byte[] imageData = File.ReadAllBytes(path);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(imageData);

        // Asigna la textura a cada RawImage para mostrarla
        foreach (RawImage image in displayImages)
        {
            image.texture = texture;
        }

        // Clasificar la imagen
        int result = classifier.ClasificarImagen(texture);
        string[] classes = { "Pirámides", "Castillo", "Mar" };

        Debug.Log("La imagen ha sido clasificada como: " + classes[result]);

        // Seleccionar el Canvas correspondiente según la clasificación
        switch (result)
        {
            case 1: // Castillo
                canvasController.ShowCanvas(3);  // Elemento 3 del Array
                break;
            case 2: // Mar
                canvasController.ShowCanvas(4);  // Elemento 4 del Array
                break;
            case 0: // Pirámides
                canvasController.ShowCanvas(5);  // Elemento 5 del Array
                break;
            default:
                Debug.LogWarning("Clasificación desconocida.");
                break;
        }

        yield return null;
    }
}

