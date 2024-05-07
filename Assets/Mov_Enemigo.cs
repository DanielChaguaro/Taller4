using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Enemigo : MonoBehaviour
{
    public float moveSpeed = 3f; // Velocidad de movimiento del enemigo
    public float moveDistance = 5f; // Distancia total que el enemigo se moverá de izquierda a derecha
    private Vector3 originalPosition; // Posición inicial del enemigo
    private float moveDirection = 1f; // Dirección de movimiento (1 para derecha, -1 para izquierda)

    void Start()
    {
        originalPosition = transform.position; // Guardar la posición inicial del enemigo
    }

    void Update()
    {
        // Calcular la nueva posición del enemigo
        Vector3 targetPosition = originalPosition + Vector3.right * moveDirection * moveDistance;
        
        // Mover al enemigo hacia la nueva posición
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

        // Si el enemigo llega al límite derecho o izquierdo, cambiar la dirección de movimiento
        if (Vector3.Distance(transform.position, targetPosition) <= 0.01f)
        {
            moveDirection *= -1; // Invertir la dirección de movimiento
        }
    }
}
