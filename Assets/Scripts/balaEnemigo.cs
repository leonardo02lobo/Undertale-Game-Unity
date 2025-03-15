using UnityEngine;

public class balaEnemigo : MonoBehaviour
{
    public float velocidad;

    private void Start()
    {
        Destroy(gameObject, 5f);
    }

    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            MovimientoPersonaje jugador = collision.GetComponent<MovimientoPersonaje>();
            if (jugador != null)
            {
                jugador.RecibeDanio(Vector2.zero, 1);
                Destroy(gameObject);
            }
            
        }
    }
}

