using UnityEngine;

public class AudioTest : MonoBehaviour
{
    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource no encontrado en el objeto.");
        }
    }
}
