using Unity.VisualScripting;
using UnityEngine;

public class DisparoEnemigo : MonoBehaviour
{
    public Transform controladorDisparo;
    public float distanciaLineaBase = 5f; // Distancia base de la l�nea
    public LayerMask capaJugador;
    public bool jugadorRango;
    public float tiempoEntreDisparos;
    public float tiempoDisparo;
    public GameObject balaEnemigo;
    public GameObject jugador;
    private float siguienteDisparo;

    private float distanciaLinea; // Distancia real de la l�nea ajustada por la escala

    private void Start()
    {
        // Calcula la distancia de la l�nea basada en la escala del objeto al inicio
        ActualizarDistanciaLinea();
    }

    
    private void Update()
    {
        // Actualizar distancia de la l�nea
        ActualizarDistanciaLinea();

        // Detectar al jugador con el raycast
        jugadorRango = Physics2D.Raycast(controladorDisparo.position, transform.right, distanciaLinea, capaJugador);

        if (jugadorRango && Time.time >= siguienteDisparo)
        {
            Disparar();
            siguienteDisparo = Time.time + tiempoEntreDisparos; // Programar el siguiente disparo
        }
    }

    private void Disparar()
    {
        // Calcular la direcci�n del disparo basada en la escala del enemigo
        Vector2 direccionDisparo = transform.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Calcular la rotaci�n basada en la direcci�n
        float angulo = Mathf.Atan2(direccionDisparo.y, direccionDisparo.x) * Mathf.Rad2Deg;

        // Instanciar la bala con la rotaci�n calculada
        Instantiate(balaEnemigo, controladorDisparo.position, Quaternion.Euler(0, 0, angulo));
    }

    private void OnDrawGizmos()
    {
        // Dibuja la l�nea con la distancia ajustada
        Gizmos.color = Color.red;
        Gizmos.DrawLine(controladorDisparo.position, controladorDisparo.position + transform.right * distanciaLinea);
    }

    private void ActualizarDistanciaLinea()
    {
        // Ajusta la distancia de la l�nea multiplicando la escala local del objeto
        distanciaLinea = distanciaLineaBase * transform.localScale.x;
    }
}