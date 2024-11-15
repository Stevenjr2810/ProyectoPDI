using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;                 // Velocidad de movimiento
    public float mouseSensitivity = 2f;      // Sensibilidad del mouse
    public float gravity = -9.81f;           // Gravedad aplicada al jugador

    private CharacterController controller;
    private float verticalRotation = 0f;
    private Vector3 velocity;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; // Bloquea el cursor al centro de la pantalla
    }

    void Update()
    {
        // Movimiento del jugador
        float moveForward = Input.GetAxis("Vertical") * speed;
        float moveSide = Input.GetAxis("Horizontal") * speed;
        
        Vector3 move = transform.right * moveSide + transform.forward * moveForward;
        controller.Move(move * Time.deltaTime);

        // Aplica la gravedad
        if (controller.isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Asegura que el jugador se mantenga en el suelo
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);

        // Rotación de la cámara en el eje Y (girar izquierda y derecha)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        transform.Rotate(Vector3.up * mouseX);

        // Rotación en el eje X para mirar arriba y abajo
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        Camera.main.transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);
    }
}
