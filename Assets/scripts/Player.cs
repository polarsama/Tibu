using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody Rgb;
    public float movementSpeed = 0;
    public float rotationSpeed = 0;
    public float jumpForce = 0;
    public float groundCheckDistance = 0.3f;
    public LayerMask groundMask;
    public Vector2 sensibility = new Vector2(0, 0);

    [SerializeField] private Camera mainCamera;
    private float xRotation = 0f;
    private bool isGrounded;

    void Start()
    {
        Rgb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            Debug.LogWarning("Main Camera no asignada, usando Camera.main");
        }

        if (Rgb != null)
        {
            Rgb.freezeRotation = true;
        }
    }

    void Update()
    {
        HandleCameraRotation();
        HandleMovement();
        CheckGrounded();
        HandleJump();
    }

    void HandleMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        // Obtener dirección relativa a la cámara
        Vector3 cameraForward = mainCamera.transform.forward;
        Vector3 cameraRight = mainCamera.transform.right;

        // Mantener el movimiento horizontal
        cameraForward.y = 0;
        cameraRight.y = 0;
        cameraForward.Normalize();
        cameraRight.Normalize();

        // Calcular dirección de movimiento
        Vector3 moveDirection = (cameraForward * z + cameraRight * x).normalized;

        if (moveDirection.magnitude >= 0.1f)
        {
            // Mover el personaje
            Vector3 movement = moveDirection * movementSpeed * Time.deltaTime;
            transform.position += movement;

            // Rotar el personaje hacia la dirección del movimiento
            Quaternion targetRotation = Quaternion.LookRotation(moveDirection);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void HandleCameraRotation()
    {
        float mouseX = Input.GetAxis("Mouse X") * sensibility.x;
        float mouseY = Input.GetAxis("Mouse Y") * sensibility.y;

        // Rotar la cámara verticalmente
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -80f, 80f);
        mainCamera.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        // Rotar el personaje horizontalmente (esto también rotará la cámara ya que es hija del personaje)
        transform.Rotate(Vector3.up * mouseX);
    }

    void CheckGrounded()
    {
        isGrounded = Physics.Raycast(transform.position + Vector3.up * 0.1f, Vector3.down, groundCheckDistance + 0.1f, groundMask);
    }

    void HandleJump()
    {
        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            Rgb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    void OnDrawGizmos()
    {
        // Visualizar el raycast de detección de suelo
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position + Vector3.up * 0.1f, transform.position + Vector3.up * 0.1f + Vector3.down * (groundCheckDistance + 0.1f));
    }
}