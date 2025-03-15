using UnityEngine;

public class CameraController : MonoBehaviour
{
    public Transform personaje;

    private float tamanioCamara;
    private float alturaPantalla;
    void Start()
    {
        tamanioCamara = Camera.main.orthographicSize;
        alturaPantalla = tamanioCamara * 2;
    }

    void Update()
    {
        ActualizarPosicionCamara();
    }

    void ActualizarPosicionCamara()
    {
        int pantallaPersonaje = (int)(personaje.position.y / alturaPantalla);
        float alturaCamara = (pantallaPersonaje * alturaPantalla) + tamanioCamara;

        transform.position = new Vector3(transform.position.x,alturaCamara,transform.position.z);
    }
}
