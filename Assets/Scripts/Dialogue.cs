using UnityEngine;
using TMPro;
using System.Linq;
using System.Collections;
using UnityEngine.SceneManagement;

public class Dialogue : MonoBehaviour
{
    [SerializeField, TextArea(3, 10)] private string[] dialogos;
    [SerializeField] private TMP_Text texto;
    [SerializeField] private GameObject Boton;
    [SerializeField] private TMP_Text textoBoton;
    [SerializeField] private GameObject panel;
    [SerializeField] private GameObject textoNombre;
    [SerializeField] private GameObject inputNombre;
    [SerializeField] private GameObject textoDialogo;
    [SerializeField] private GameObject botonIniciarJuego;
    [SerializeField] private GameObject PanelTexto;
    private AudioSource audio;

    private bool didDialoguestart;
    private int indice = 0;
    private bool band = true;

    private void Start()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Fire1") && band)
        {
            StopAllCoroutines();
            texto.text = dialogos[indice];
            Boton.SetActive(true);
            audio.Pause();
        }
        if (indice == dialogos.Length && band)
        {
            textoNombre.SetActive(true);
            inputNombre.SetActive(true);
            textoDialogo.SetActive(false);
            Boton.SetActive(false);
            PanelTexto.SetActive(false);
            botonIniciarJuego.SetActive(true);
            audio.Pause();
            band = false;
            didDialoguestart = true;
        }
        if (!didDialoguestart)
        {
            Dialogo();
        }
    }

    private void Dialogo()
    {
        didDialoguestart = true;
        Boton.SetActive(false);
        StartCoroutine(showline());
    }

    private IEnumerator showline()
    {
        
        texto.text = string.Empty;

        foreach (char c in dialogos[indice])
        {
            texto.text += c;
            yield return new WaitForSeconds(0.1f);
        }
        Boton.SetActive(true);
        audio.Pause();
    }
    public void Incrementar()
    {
        indice++;
        didDialoguestart = false;
        audio.UnPause();
    }

    public void IniciarJuego()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
