using UnityEngine;
using Unity.Barracuda;
using System.Collections.Generic;

public class EscenarioClassifier : MonoBehaviour
{
    public NNModel modeloONNX;
    public Texture2D imagenEntrada;
    private IWorker worker;
    private string inputLayerName = "input";
    private readonly string[] clases = { "Pirámides", "Castillo", "Mar" };

    void Start()
    {
        Model model = ModelLoader.Load(modeloONNX);
        worker = WorkerFactory.CreateWorker(WorkerFactory.Type.ComputePrecompiled, model);
    }

    public int ClasificarImagen(Texture2D imagen)
    {
        try
        {
            if (imagen == null)
            {
                Debug.LogError("La imagen de entrada es nula");
                return -1;
            }

            // Crear y ejecutar el tensor
            using (Tensor entradaTensor = PreprocesarImagen(imagen))
            {
                // Ejecutar la inferencia con el tensor
                worker.Execute(entradaTensor);

                // Obtener el resultado
                using (Tensor salidaTensor = worker.PeekOutput())
                {
                    return ArgMax(salidaTensor);
                }
            }
        }
        catch (System.Exception e)
        {
            Debug.LogError($"Error al clasificar imagen: {e.Message}\n{e.StackTrace}");
            return -1;
        }
    }

    public string ObtenerNombreClase(int indiceClase)
    {
        if (indiceClase >= 0 && indiceClase < clases.Length)
            return clases[indiceClase];
        return "Clase desconocida";
    }
        private Tensor PreprocesarImagen(Texture2D imagen)
        {
            int targetWidth = 128;
            int targetHeight = 128;

            // Redimensionar la imagen
            Texture2D imagenRedimensionada = ResizeTexture(imagen, targetWidth, targetHeight);
            Color32[] pixeles = imagenRedimensionada.GetPixels32();

            // Crear array para los datos del tensor en formato NHWC (batch, height, width, channels)
            float[] datos = new float[targetHeight * targetWidth * 3];

            // Llenar el array con los valores de los canales RGB normalizados
            for (int y = 0; y < targetHeight; y++)
            {
                for (int x = 0; x < targetWidth; x++)
                {
                    int pixelIndex = y * targetWidth + x;
                    Color32 pixel = pixeles[pixelIndex];

                    // Asignar los valores en el formato NHWC
                    datos[(y * targetWidth + x) * 3 + 0] = pixel.r / 255.0f; // Canal R
                    datos[(y * targetWidth + x) * 3 + 1] = pixel.g / 255.0f; // Canal G
                    datos[(y * targetWidth + x) * 3 + 2] = pixel.b / 255.0f; // Canal B
                }
            }

            // Crear el tensor en formato NHWC
            var tensor = new Tensor(new TensorShape(1, targetHeight, targetWidth, 3), datos);

            // Verificar las primeras entradas del tensor para depuración
            Debug.Log($"Valores del tensor: R={datos[0]}, G={datos[1]}, B={datos[2]}");
            Debug.Log($"Tensor creado: batch={tensor.shape.batch}, height={tensor.shape.height}, width={tensor.shape.width}, channels={tensor.shape.channels}");

            // Limpiar la textura temporal
            if (imagenRedimensionada != imagen)
            {
                Object.Destroy(imagenRedimensionada);
            }

            return tensor;
        }




    private Texture2D ResizeTexture(Texture2D source, int targetWidth, int targetHeight)
    {
        // Si la imagen ya tiene el tamaño correcto, la devolvemos directamente
        if (source.width == targetWidth && source.height == targetHeight)
            return source;

        RenderTexture rt = RenderTexture.GetTemporary(targetWidth, targetHeight);
        rt.filterMode = FilterMode.Bilinear;

        // Hacer el blit para redimensionar
        Graphics.Blit(source, rt);

        // Crear la nueva textura
        Texture2D result = new Texture2D(targetWidth, targetHeight);

        // Guardar el RenderTexture activo actual
        RenderTexture prevActive = RenderTexture.active;
        RenderTexture.active = rt;

        // Leer los píxeles
        result.ReadPixels(new Rect(0, 0, targetWidth, targetHeight), 0, 0);
        result.Apply();

        // Restaurar el RenderTexture activo
        RenderTexture.active = prevActive;
        RenderTexture.ReleaseTemporary(rt);

        return result;
    }

    private int ArgMax(Tensor tensor)
    {
        if (tensor.length == 0)
        {
            Debug.LogError("El tensor está vacío");
            return -1;
        }

        float maxValue = float.MinValue;
        int maxIndex = 0;

        for (int i = 0; i < tensor.length; i++)
        {
            float value = tensor[i];
            if (value > maxValue)
            {
                maxValue = value;
                maxIndex = i;
            }
        }

        return maxIndex;
    }

    void OnDestroy()
    {
        if (worker != null)
        {
            worker.Dispose();
            worker = null;
        }
    }

    // Método de depuración para verificar las dimensiones del tensor
    private void LogTensorInfo(Tensor tensor, string nombre)
    {
        Debug.Log($"Tensor {nombre}: " +
                  $"batch={tensor.batch}, " +
                  $"height={tensor.height}, " +
                  $"width={tensor.width}, " +
                  $"channels={tensor.channels}, " +
                  $"length={tensor.length}");
    }
}