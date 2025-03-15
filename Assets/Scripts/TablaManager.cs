using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.IO;
using Assets.Scripts.Models;
using System.Linq;

public class TablaManager : MonoBehaviour
{
    public GameObject content; // El objeto Content que contiene la tabla
    public GameObject celdaPrefab; // Prefab de la celda (Text o TextMeshPro)
    public GameObject celdaPrefab2;
    public Vector2 cellSize = new Vector2(100, 30); // Tamaño de cada celda
    public Vector2 spacing = new Vector2(10, 10); // Espacio entre celdas
    private float Y = 170.542f;

    private void Start()
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Datos.json");

        if (File.Exists(filePath))
        {
            // Leer y deserializar correctamente (sin envoltorio adicional)
            string json = File.ReadAllText(filePath);
            DatosList datosList = JsonUtility.FromJson<DatosList>(json);

            // Ordenar por puntaje descendente y tomar top 10
            var top10 = datosList.datos
                .OrderByDescending(d => d.puntaje)
                .Take(10)
                .ToList();

            // Crear filas de datos
            foreach (var datos in top10)
            {
                CrearCelda(celdaPrefab, datos.nombre, -125.572f, Y);
                CrearCelda(celdaPrefab2, datos.puntaje.ToString(), 190.63f, Y);
                Y -= 50;
            }
        }
        else
        {
            Debug.LogError("Archivo no encontrado: " + filePath);
        }
    }

    public void CrearCelda(GameObject celda, string texto, float x, float y)
    {
        GameObject nuevaCelda = Instantiate(celda, content.transform);
        TextMeshProUGUI textoCelda = nuevaCelda.GetComponent<TextMeshProUGUI>();
        textoCelda.text = texto;

        // Calcular la posición de la celda
        RectTransform rectTransform = nuevaCelda.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = new Vector2(x, y);
    }
}
