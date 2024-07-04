using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfeitosCamera : MonoBehaviour
{
    [Header("Movimentacao")]

    public Camera cameraprincipal;
    private float tamanhoNormal;

    int tamanhoFinal = 15;
    float delay = 3;
    // Start is called before the first frame update
    void Start()
    {
        tamanhoNormal = cameraprincipal.orthographicSize;
    }

    // Update is called once per frame


 
    public void MudarTamanho()
    {        
        StartCoroutine(MudarAoLongoDoTempo(tamanhoFinal, delay));
    }

    public IEnumerator MudarAoLongoDoTempo(int tamanhoFinal, float delay)
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
