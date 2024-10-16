using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosCamera : MonoBehaviour
{
    [Header("Configura��es")]
    public Camera cameraprincipal;
    private float tamanhoNormal;

    private bool estaComZoom = false;
    public float tamanhoFinal = 10f; // Depois tentar colocar esse valor como par�metro no m�todo
    public float delay = 3; // Depois tentar colocar esse valor como par�metro no m�todo
    // Start is called before the first frame update
    void Start()
    {
        tamanhoNormal = cameraprincipal.orthographicSize;
    }

    public void DarZoom()
    {     
        StartCoroutine(MudarAoLongoDoTempo(tamanhoNormal/1.6f, delay));
        estaComZoom = true;
    }
    public void DarZoomOut()
    {
        StartCoroutine(MudarAoLongoDoTempo(tamanhoNormal* 2f, delay));
        estaComZoom = false;
    }
    public void TirarZoom()
    {
        StartCoroutine(MudarAoLongoDoTempo(tamanhoNormal, delay));
        estaComZoom = false;
    }

    public void MudarTamanho()
    {        
        if (estaComZoom == false)
        {
            StartCoroutine(MudarAoLongoDoTempo(tamanhoFinal, delay));
            estaComZoom = true; 
        }
        else
        {
            StartCoroutine(MudarAoLongoDoTempo(tamanhoNormal, delay));
            estaComZoom = false;
        }
    }

    public IEnumerator MudarAoLongoDoTempo(float tamanhoFinal, float delay)
    {
        float tamanhoInicial = cameraprincipal.orthographicSize;
        float tempoInicial = Time.time;

        while (Time.time < tempoInicial + delay)
        {
            float t = (Time.time - tempoInicial) / delay;
            cameraprincipal.orthographicSize = Mathf.Lerp(tamanhoInicial, tamanhoFinal, t);
            yield return null;
        }
        cameraprincipal.orthographicSize = tamanhoFinal;
    }
}




