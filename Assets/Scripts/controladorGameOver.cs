using UnityEngine;
using UnityEngine.SceneManagement;

public class controladorGameOver : MonoBehaviour
{
    public void Reiniciar()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Salir()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 2);
    }
}
