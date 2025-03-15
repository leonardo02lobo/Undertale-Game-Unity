using Assets.Scripts.Models;
using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

public class MovimientoPersonaje : MonoBehaviour
{

    private float movimiento = 5f;

    public float fuerzaSalto = 10f;
    public int vida = 3;
    public float fuerzaRebote = 10f;
    public float longitudRaycast = 0.1f;
    public LayerMask capaSuelo;
    public GameObject canvasMuerte;
    public GameObject canvasGanador;
    public Puntaje PuntajeJuego;
    public GameObject pozo1;
    public GameObject pozo2;

    private bool ensuelo;
    private bool RecibirDanio;
    private bool atacando;
    public bool muerto;
    private float alturaMax = 7f;
    private Rigidbody2D rb;

    public Animator animator;
    float pisos = 14.4451f;
    public EnemigoControlador enemigo1;
    public EnemigoControlador enemigo2;
    public EnemigoControlador enemigo11;
    public EnemigoControlador enemigo21;
    public EnemigoFinalControlador enemigoFinal;
    private bool nivelPasado1 = false;
    private bool nivelPasado2 = false;
    private int NumeroDeTeletransportaciones = 0;
    private bool JuegoPasado = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        if (transform.position.y < 0)
        {
            muerto = true;

            canvasMuerte.SetActive(true);
            transform.position = new Vector3(0f, 0f, transform.position.z);
            return;
        }
        //revisar al enemigo 1
        if (((enemigo1.muerto && enemigo11.muerto) && !nivelPasado1))
        {
            pozo1.SetActive(true);
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (transform.position.x >= pozo1.transform.position.x  && transform.position.y >= pozo1.transform.position.y)
            {
                nivelPasado1 = true;
                alturaMax = 20f;
                fuerzaRebote += 2;
                rb.bodyType = RigidbodyType2D.Dynamic;
                transform.position = new Vector3(transform.position.x, 17f, transform.position.y);
            }

            //if (transform.position.y > pisos)
            //{
            //    nivelPasado1 = true; 
            //    pisos = 26f;
            //    fuerzaRebote += 2;
            //    rb.bodyType = RigidbodyType2D.Dynamic;
            //}
        }

        //Revisar al enemigo 2
        if (((enemigo2.muerto && enemigo21.muerto) && !nivelPasado2))
        {
            pozo2.SetActive(true);
            rb.bodyType = RigidbodyType2D.Kinematic;

            if (transform.position.x >= pozo2.transform.position.x && transform.position.y >= pozo2.transform.position.y)
            {
                nivelPasado2 = true;
                rb.bodyType = RigidbodyType2D.Dynamic;
                transform.position = new Vector3(transform.position.x, 26f, transform.position.y);
            }
            //if (transform.position.y > pisos)
            //{
            //    nivelPasado2 = true;
            //    pisos = 26f;
            //    rb.bodyType = RigidbodyType2D.Dynamic;
            //}
        }
        //Muerte Final
        if (enemigoFinal.muerto)
        {
            canvasGanador.SetActive(true);
            Datos datos;
            try
            {
                datos = new Datos()
                {
                    puntaje = (int)PuntajeJuego.getPuntaje(),
                    nombre =  ControladorNombre.Instance.GetNombre()
                };
            }catch(NullReferenceException e)
            {
                datos = new Datos()
                {
                    puntaje = (int)PuntajeJuego.getPuntaje(),
                    nombre = string.Empty
                };
            }
            
            if (!JuegoPasado)
            {
                GuardarDatos(datos);
            }
            Destroy(enemigoFinal);
        }

        //Cambiar de posicion en la parte final
        if(Input.GetKeyDown(KeyCode.K) && NumeroDeTeletransportaciones <= 3 && transform.position.y > 26)
        {
            NumeroDeTeletransportaciones++;
            if(transform.position.x > 0)
            {
                transform.position = new Vector3(-12f, 26.98f, 0f);
            }
            else
            {
                transform.position = new Vector3(8.81f,26.98f,0f);
            }
        }

        if (!muerto)
        {
            Movimiento();
            RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, longitudRaycast, capaSuelo);
            ensuelo = hit.collider != null;

            if (ensuelo && Input.GetKeyDown(KeyCode.Space) && !RecibirDanio)
            {
                rb.AddForce(new Vector2(0f, fuerzaSalto), ForceMode2D.Impulse);
            }
            if (Input.GetKeyDown(KeyCode.F) && !atacando && ensuelo)
            {
                Atacando();
            }
        }
        
        animator.SetBool("enSuelo", ensuelo);
        animator.SetBool("Danio", RecibirDanio);
        animator.SetBool("Ataque", atacando);
        animator.SetBool("muerto", muerto);
    }

    public void Movimiento()
    {
        float movimientoX = Input.GetAxis("Horizontal") * Time.deltaTime * movimiento;
        float movimientoY = Input.GetAxis("Vertical") * Time.deltaTime * movimiento;

        // Permitir el movimiento hacia abajo incluso cuando el personaje esté en la altura máxima
        if (transform.position.y >= alturaMax && movimientoY > 0)
        {
            movimientoY = 0;
        }

        animator.SetFloat("mover", movimientoX * movimiento);
        animator.SetFloat("moverAtras", movimientoY * movimiento);

        if (movimientoX < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (movimientoX > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }


        Vector3 posicion = transform.position;

        if (!RecibirDanio)
            transform.position = new Vector3(movimientoX + posicion.x, movimientoY + posicion.y, posicion.z);
    }


    public void RecibeDanio(Vector2 direccion, int danio)
    {
        if (!RecibirDanio)
        {
            RecibirDanio = true;
            vida -= danio;
            if(vida <= 0)
            {
                muerto = true;
                canvasMuerte.SetActive(true);
            }
            if (!muerto)
            {
                Vector2 rebote = new Vector2(transform.position.x - direccion.x, 0.5f).normalized;
                rb.AddForce(rebote * fuerzaRebote, ForceMode2D.Impulse);
            }
            
        }
    }

    public void DesactivarDanio()
    {
        RecibirDanio = false;
        rb.linearVelocity = Vector2.zero;
    }

    public void Atacando()
    {
        atacando = true;
    }

    public void DesactivaAtaque()
    {
        atacando = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(transform.position, transform.position + Vector3.down * longitudRaycast);
    }
    public void GuardarDatos(Datos nuevoDato)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "Datos.json");
        DatosList datosList;

        if (File.Exists(filePath))
        {
            // Leer el JSON existente y deserializarlo directamente a DatosList
            string json = File.ReadAllText(filePath);
            datosList = JsonUtility.FromJson<DatosList>(json);
        }
        else
        {
            // Si el archivo no existe, crear una nueva lista vacía
            datosList = new DatosList();
            datosList.datos = new List<Datos>();
        }

        // Agregar el nuevo dato a la lista
        datosList.datos.Add(nuevoDato);

        // Serializar y guardar el JSON actualizado
        string jsonActualizado = JsonUtility.ToJson(datosList, true); // El 'true' formatea el JSON para mejor legibilidad
        File.WriteAllText(filePath, jsonActualizado);
        JuegoPasado = true;
    }
}
