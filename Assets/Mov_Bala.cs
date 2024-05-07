using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mov_Bala : MonoBehaviour
{
    public float velocidad = 10f; // Velocidad de la bala
    public Vector3 direccion = Vector3.forward;
    // Start is called before the first frame update
    void Start()
    {
      
    }

    // Update is called once per frame
    void Update()
    {
        // Mover la bala hacia adelante
        transform.Translate(Vector3.forward * velocidad * Time.deltaTime);
    }
    void OnCollisionEnter(Collision collision)
    {
        // Verificar si la colisión es con el tanque 2
        if (collision.gameObject.CompareTag("Enemigos"))
        {
            // Destruir el tanque 2
            Destroy(collision.gameObject);
            // Destruir la bala
            Destroy(gameObject);
        }
        else
        {
            // Si la colisión no es con el tanque 2, solo destruir la bala
            Destroy(gameObject);
        }
    }
 }   
