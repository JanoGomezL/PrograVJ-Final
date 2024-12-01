using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float walkSpeed = 5f;          // Velocidad normal
    public float runSpeed = 10f;         // Velocidad al correr (Shift)
    public float jumpHeight = 2f;        // Altura del salto
    public float gravity = -9.81f;       // Gravedad
    public Transform groundCheck;        // Objeto que verifica si estamos en el suelo
    public float groundDistance = 0.4f;  // Distancia para verificar el suelo
    public LayerMask groundMask;         // Máscara para identificar el suelo
    public Transform cameraTransform;    // Cámara para rotación con el mouse
    public float sensitivity = 1f;       // Sensibilidad del mouse

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;        // Rotación acumulada en el eje X (vertical)

    void Start()
    {
        controller = GetComponent<CharacterController>();
        // Bloquear el cursor al inicio
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // Movimiento de la cámara con el mouse
        RotateCamera();

        // Verificar si estamos en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reseteamos la velocidad vertical al estar en el suelo
        }

        // Movimiento horizontal
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        // Cambiar velocidad si presionamos Shift
        float speed = Input.GetKey(KeyCode.LeftShift) ? walkSpeed : runSpeed;

        controller.Move(move * speed * Time.deltaTime);

        // Saltar
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }

        // Aplicar gravedad
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void RotateCamera()
    {
        // Obtén el movimiento del mouse
        float mouseX = Input.GetAxis("Mouse X") * sensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * sensitivity;

        // Acumular la rotación en el eje X y limitarla
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Limita rotación vertical entre -90 y 90 grados

        // Rotar la cámara en el eje X (vertical)
        cameraTransform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el cuerpo del jugador en el eje Y (horizontal)
        transform.Rotate(Vector3.up * mouseX);
    }
}
