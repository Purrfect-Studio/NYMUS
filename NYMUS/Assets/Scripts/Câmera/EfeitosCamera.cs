using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosCamera : MonoBehaviour
{
    [Header("Configurações")]
    public Camera cameraprincipal;
    private float tamanhoNormal;

    private bool estaComZoom = false;
    float tamanhoFinal = 15f; // Depois tentar colocar esse valor como parâmetro no método
    float delay = 3; // Depois tentar colocar esse valor como parâmetro no método
    // Start is called before the first frame update
    void Start()
    {
        tamanhoNormal = cameraprincipal.orthographicSize;
    }

    // Update is called once per frame


 
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
