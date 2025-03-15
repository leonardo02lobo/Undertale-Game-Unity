using UnityEngine;

public class TiempoVida : MonoBehaviour
{
    [SerializeField] private float tiempovida;

    private void Start()
    {
        Destroy(gameObject, tiempovida);
    }
}