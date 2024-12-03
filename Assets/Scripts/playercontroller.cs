using UnityEngine;
using TMPro;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Movimiento del Jugador")]
    public float walkSpeed = 5f;          // Velocidad normal
    public float runSpeed = 10f;         // Velocidad al correr (Shift)
    public float jumpHeight = 2f;        // Altura del salto
    public float gravity = -9.81f;       // Gravedad
    public Transform groundCheck;        // Objeto que verifica si estamos en el suelo
    public float groundDistance = 0.4f;  // Distancia para verificar el suelo
    public LayerMask groundMask;         // Máscara para identificar el suelo

    [Header("Cámara y Rotación")]
    public Transform cameraTransform;    // Cámara para rotación con el mouse
    public float sensitivity = 1f;       // Sensibilidad del mouse

    [Header("Armas")]
    public Transform weaponHoldPoint;    // WeaponHoldPoint asignado desde el Inspector
    public Weapon currentWeapon;         // Arma actualmente equipada
    private Weapon weaponInRange;        // Arma en rango para recoger

    public TextMeshProUGUI messageText;  // Mensaje en pantalla

    private CharacterController controller;
    private Vector3 velocity;
    private bool isGrounded;
    private float xRotation = 0f;        // Rotación acumulada en el eje X (vertical)

    void Start()
    {
        controller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // Buscar la pistola inicial en la escena y equiparla
        Pistol pistol = FindObjectOfType<Pistol>();
        if (pistol != null)
        {
            EquipWeapon(pistol);
            messageText.text = "";
        }
        else
        {
            Debug.LogError("Pistola inicial no encontrada en la escena.");
        }
    }

    void Update()
    {
        // Manejar disparo y recarga
        if (Input.GetButtonDown("Fire1") && currentWeapon != null)
        {
            currentWeapon.Shoot();
        }

        if (Input.GetKeyDown(KeyCode.R) && currentWeapon != null)
        {
            currentWeapon.Reload();
        }

        // Recoger arma
        if (Input.GetKeyDown(KeyCode.E) && weaponInRange != null)
        {
            EquipWeapon(weaponInRange);
        }

        // Movimiento y salto
        HandleMovement();

        // Movimiento de la cámara con el mouse
        RotateCamera();
    }

    private void HandleMovement()
    {
        // Verificar si estamos en el suelo
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; // Reseteamos la velocidad vertical al estar en el suelo
        }

        // Movimiento horizontal basado en la dirección de la cámara
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        // Calcula la dirección de movimiento relativa a la cámara
        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        // Asegúrate de que las direcciones no tengan componente vertical
        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        // Calcular el movimiento basado en la entrada del jugador
        Vector3 move = forward * z + right * x;

        // Cambiar velocidad si presionamos Shift
        float speed = Input.GetKey(KeyCode.LeftShift) ? runSpeed : walkSpeed;

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

    public void EquipWeapon(Weapon newWeapon)
    {
        if (newWeapon != null)
        {
            // Si ya hay un arma equipada, devolverla al suelo
            if (currentWeapon != null)
            {
                currentWeapon.transform.SetParent(null);
                currentWeapon.gameObject.AddComponent<Rigidbody>(); // Agregar Rigidbody para caer al suelo
                currentWeapon.GetComponent<Collider>().enabled = true; // Reactivar el collider
                currentWeapon = null;
            }

            // Equipar la nueva arma
            currentWeapon = newWeapon;
            Collider[] weaponColliders = newWeapon.GetComponents<Collider>();
            foreach (Collider collider in weaponColliders)
            {
                collider.enabled = false; // Desactivar colliders
            }
            Rigidbody weaponRigidbody = newWeapon.GetComponent<Rigidbody>();
            if (weaponRigidbody != null)
            {
                Destroy(weaponRigidbody); // Eliminar Rigidbody
            }
            newWeapon.transform.SetParent(weaponHoldPoint);
            newWeapon.transform.localPosition = Vector3.zero;
            newWeapon.transform.localRotation = Quaternion.identity;
            newWeapon.transform.localScale = Vector3.one;

            

            Debug.Log($"Arma equipada: {currentWeapon.weaponName}");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Detectar si el objeto es un arma
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null)
        {
            weaponInRange = weapon; // Arma en rango
            if (messageText != null)
            {
                messageText.text = $"Presiona E para recoger";
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Si salimos del rango, limpiar la referencia
        Weapon weapon = other.GetComponent<Weapon>();
        if (weapon != null && weapon == weaponInRange)
        {
            weaponInRange = null;
            if (messageText != null)
            {
                messageText.text = "";
            }
        }
    }
}
