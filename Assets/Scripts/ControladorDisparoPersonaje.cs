using UnityEngine;

public class ControladorDisparoPersonaje : MonoBehaviour
{
    public Transform controladorDisparo;
    public float tiempoEntreDisparos;
    public Transform Personaje;
    public float tiempoDisparo;
    public GameObject balaJugador;
    public Puntaje Puntaje;

    private void Update()
    {
        if(Input.GetButtonDown("Fire1"))
        {
            if (Time.time > tiempoEntreDisparos + tiempoDisparo)
            { 
                Disparar();
                tiempoDisparo = Time.time;
            }
        }
        if (transform.position.y > 15)
        {
            tiempoEntreDisparos = 0.5f;
        }
        if (transform.position.y > 29.39)
        {
            tiempoEntreDisparos = 1f;
        }
    }

    private void Disparar()
    {
        // Determinar la dirección en la que el personaje está mirando
        Vector3 direccionDisparo = Personaje.localScale.x > 0 ? Vector2.right : Vector2.left;

        // Instanciar la bala
        GameObject bala = Instantiate(balaJugador, controladorDisparo.position, Quaternion.identity);

        // Ajustar la rotación de la bala en función de la dirección
        bala.transform.right = direccionDisparo;
    }
}
