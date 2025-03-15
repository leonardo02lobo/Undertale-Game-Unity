using System.Collections;
using UnityEngine;

public class EnemigoControlador : MonoBehaviour
{

    public Transform player;
    public float detectionRadius = 5.0f;
    public float speed = 2.0f;
    public float fuerzaRebote = 2f;
    public int vidaEnemigo = 3;
    public Puntaje puntaje;
    public float puntajeDisparo;
    public float puntajeEmbestida;

    private Rigidbody2D rb;
    private Vector2 movement;
    private bool enMovimiento;
    private bool RecibirDanio;
    private bool jugadorVivo;
    public bool muerto;
    private Animator Animator;
    public int numeroMuertes;
    private bool patrullando = true; // Indica si el enemigo está patrullando
    private int direccionPatrullaje = 1; // Dirección del patrullaje (1 = derecha, -1 = izquierda)

    private float posicion2X = -11.88f;
    private float posicion1X = 9.55f;
    public float posicion1Y = 2.86f;
    public float coordenadaYInicioPatrullaje = 0;

    void Start()
    {
        jugadorVivo = true;
        rb = GetComponent<Rigidbody2D>();
        Animator = GetComponent<Animator>();
    }

    void Update()
    {

        if(player.position.y >= coordenadaYInicioPatrullaje)
        {
            patrullando = true;
        }

        if (transform.position.y < 0)
        {
            Vector2 direccionDanio = new Vector2(gameObject.transform.position.x, 0);
            RecibeDanio(direccionDanio, 1);
            return;
        }
        if (jugadorVivo && !muerto)
        {
            Movimiento();
        }
        Animator.SetBool("enMovimiento", enMovimiento);
    }

    public void Movimiento()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < detectionRadius)
        {
            // Si el jugador está dentro del radio de detección, persíguelo
            Vector2 direction = (player.position - transform.position).normalized;

            if (direction.x < 0)
            {
                transform.localScale = new Vector3(1, 1, 1); // Voltea hacia la izquierda
            }
            else if (direction.x > 0)
            {
                transform.localScale = new Vector3(-1, 1, 1); // Voltea hacia la derecha
            }

            movement = new Vector2(direction.x, 0);
            enMovimiento = true;
            patrullando = false; // Desactiva el patrullaje mientras persigue al jugador
        }
        else
        {
            // Si el jugador no está en el radio de detección, activa el patrullaje
            patrullando = true; // Reactiva el patrullaje

            if (patrullando)
            {
                // Mueve al enemigo hacia la dirección actual del patrullaje
                movement = new Vector2(direccionPatrullaje, 0);

                // Cambia la escala del enemigo según la dirección del patrullaje
                if (direccionPatrullaje > 0)
                {
                    transform.localScale = new Vector3(-1, 1, 1); // Voltea hacia la derecha
                }
                else
                {
                    transform.localScale = new Vector3(1, 1, 1); // Voltea hacia la izquierda
                }

                enMovimiento = true;

                // Verifica si el enemigo ha llegado a uno de los puntos de patrullaje
                if ((direccionPatrullaje > 0 && transform.position.x >= posicion1X) ||
                    (direccionPatrullaje < 0 && transform.position.x <= posicion2X))
                {
                    // Cambia la dirección del patrullaje
                    direccionPatrullaje *= -1;
                }
            }
            else
            {
                // Si no está patrullando ni persiguiendo al jugador, detén el movimiento
                movement = Vector2.zero;
                enMovimiento = false;
            }
        }

        // Aplica el movimiento al Rigidbody2D
        if (!RecibirDanio)
        {
            rb.MovePosition(rb.position + movement * speed * Time.deltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position,detectionRadius);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Vector2 direccionDanio = new Vector2(transform.position.x, 0);
            MovimientoPersonaje jugador = collision.gameObject.GetComponent<MovimientoPersonaje>();
            Debug.Log("embestida");
            puntaje.SumarPuntos(puntajeEmbestida);
            jugador.RecibeDanio(direccionDanio, 1);
            jugadorVivo = !jugador.muerto;
            if (!jugadorVivo)
            {
                enMovimiento = false;
                patrullando = true; // Activa el patrullaje cuando el jugador muere
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Embestida") || collision.CompareTag("BalaEmbestida"))
        {
            Vector2 direccionDanio = new Vector2(collision.gameObject.transform.position.x, 0);
            Debug.Log("Disparo");
            puntaje.SumarPuntos(puntajeDisparo);
            RecibeDanio(direccionDanio, 1);
        }

        if (collision.CompareTag("BalaEmbestida"))
        {
            Destroy(collision.gameObject);
        }
    }

    public void RecibeDanio(Vector2 direccion, int danio)
    {
        if (!RecibirDanio)
        {
            RecibirDanio = true;
            vidaEnemigo -= danio;
            if (vidaEnemigo <= 0)
            {
                enMovimiento = false;
                int random = Random.Range(0, 100);
                if (random % 2 == 0)
                {
                    transform.position = new Vector3(posicion1X, posicion1Y, 0f);
                }
                else
                {
                    transform.position = new Vector3(posicion2X, posicion1Y, 0f);
                }
                numeroMuertes--;
                RecibirDanio = false;
                vidaEnemigo = 3;
                if (numeroMuertes == 0)
                {
                    Destroy(gameObject);
                    muerto = true;
                }
            }
            else
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 1).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
                StartCoroutine(DesactivaDanio());
            }

        }
    }

    IEnumerator DesactivaDanio()
    {
        yield return new WaitForSeconds(0.4f);
        RecibirDanio = false;
    }
}