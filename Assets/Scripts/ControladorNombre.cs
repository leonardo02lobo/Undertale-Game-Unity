using UnityEngine;

public class ControladorNombre : MonoBehaviour
{
    public static ControladorNombre Instance;
    public string nombre;

    private void Awake()
    {
        if(ControladorNombre.Instance == null)
        {
            ControladorNombre.Instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetNombre(string nombre)
    {
        this.nombre = nombre;
    }

    public string GetNombre()
    {
        return nombre;
    }
}
