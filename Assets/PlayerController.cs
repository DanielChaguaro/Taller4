using UnityEngine;

public class PlayerController3D : MonoBehaviour
{
    public float moveSpeed = 5f;    // Velocidad de movimiento del jugador
    public float jumpForce = 10f;   // Fuerza de salto del jugador
    private Rigidbody rb;
    private bool isGrounded;
    public float maxFallTime = 3f;
    private bool isBoosting = false;
    public float boostedMoveSpeed = 10f;
    public float boostDuration = 5f;
    private float fallTimer = 0f;
    private float originalMoveSpeed;
    private Vector3 originalGravity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalGravity = Physics.gravity;
        originalMoveSpeed = moveSpeed;
    }

    void Update()
    {
        // Movimiento horizontal
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector3 movement = new Vector3(horizontalInput, 0f, 0f) * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movement);
        
        // Saltar
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && isGrounded && !isBoosting)
        {
            isBoosting = true;
            moveSpeed *= 3; 
            Invoke("ResetSpeed", 5f); 
            isGrounded = false;
        }
        if (isGrounded)
        {
            fallTimer = 0f; // Reiniciar el temporizador si el jugador está en el suelo
        }
        else
        {
            fallTimer += Time.deltaTime; // Incrementar el temporizador si el jugador está en el aire
            if (fallTimer > maxFallTime)
            {
                MoveToPosition(new Vector3(-41f, 3f, 0f));
            }
        }
        // Modificar la gravedad cuando el jugador no esté en el suelo
        if (!isGrounded)
        {
            Physics.gravity = originalGravity * 2f; // Duplicar la gravedad
        }
        else
        {
            Physics.gravity = originalGravity; // Restaurar la gravedad original
        }
    }
    

    void OnCollisionEnter(Collision collision)
    {
        // Verificar si el jugador está en el suelo
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
        if (collision.gameObject.CompareTag("Limites"))
        {
            MoveToPosition(new Vector3(-41f, 3f, 0f));
        }
        if (collision.gameObject.CompareTag("Enemigos"))
        {
            MoveToPosition(new Vector3(-41f, 3f, 0f));
        }
    }
    void ResetSpeed()
    {
        isBoosting = false;
        moveSpeed = originalMoveSpeed;
    }
    void MoveToPosition(Vector3 newPosition)
    {
        transform.position = newPosition;
        rb.velocity = Vector3.zero; 
        isGrounded = true;
    }
}
