using UnityEngine;
using UnityEngine.SceneManagement;

public class menuPausa : MonoBehaviour
{

    [SerializeField] private GameObject botonPausa;
    [SerializeField] private GameObject menuDePausa;
    private bool juegoPausado = false;

    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            if (juegoPausado)
            {
                Reanudar();
            }
            else
            {
                Pausa();    
            }
        }
        if(Input.GetKey(KeyCode.Tab))
        {
            Salir();
        }
    }
    public void Pausa()
    {
        juegoPausado = true;
        Time.timeScale = 0f;
        botonPausa.SetActive(false);
        menuDePausa.SetActive(true);
    }

    public void Reanudar()
    {
        juegoPausado =false;
        Time.timeScale = 1.0f;
        botonPausa.SetActive(true);
        menuDePausa.SetActive(false);
    }

    public void Reiniciar() { 
        Time.timeScale = 1.0f; 
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        juegoPausado = false;
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
