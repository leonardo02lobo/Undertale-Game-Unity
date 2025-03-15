using UnityEngine;
using UnityEngine.UI;

public class Bala : MonoBehaviour
{
    public float velocidad;
    public int danio;

    private void Update()
    {
        transform.Translate(Vector2.right * velocidad * Time.deltaTime);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Enemigo")){
            //GetComponent<EnemigoControlador>().Tom(danio);
            Destroy(gameObject);
        }
    }   
}