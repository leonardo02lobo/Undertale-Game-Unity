using UnityEngine;
using TMPro;

public class Puntaje : MonoBehaviour
{
    private float puntaje;
    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        textMesh.text = "Puntaje: " + puntaje.ToString("0");
    }

    public void SumarPuntos(float puntosExtras)
    {
        puntaje += puntosExtras;
    }

    public float getPuntaje()
    {
        return puntaje;
    }
}
