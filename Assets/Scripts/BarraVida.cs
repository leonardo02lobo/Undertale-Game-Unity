using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BarraVida : MonoBehaviour
{
    public Image rellenoBarraVida;
    private MovimientoPersonaje personaje;
    private float vidaMaxima;

    void Start()
    {
        personaje = GameObject.Find("personaje").GetComponent<MovimientoPersonaje>();
        vidaMaxima = personaje.vida;
    }

    void Update()
    {
        rellenoBarraVida.fillAmount = personaje.vida / vidaMaxima;
    }
}
