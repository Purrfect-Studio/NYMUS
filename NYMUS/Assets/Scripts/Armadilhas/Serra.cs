using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Serra : MonoBehaviour
{
    private float eixoZ;
    public GameObject[] pontosDeMovimento;
    private int indexPontos = 0;
    private Vector2 novaPosicao;
    public float velocidade;
    private float velocidadeGiro = 1000;

    public bool pararPorUmTempo = false;
    public float tempoParado;
    // Start is called before the first frame update
    void Start()
    {
        novaPosicao = pontosDeMovimento[indexPontos].transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        giroDaSerra();
        moverSerra();
    }

    private void giroDaSerra()
    {
        eixoZ += Time.deltaTime * velocidadeGiro;
        transform.rotation = Quaternion.Euler(0, 0, eixoZ);
    }

    private void moverSerra()
    {
        if(transform.position == pontosDeMovimento[indexPontos].transform.position)
        {
            indexPontos++;
            if(indexPontos >= pontosDeMovimento.Length)
            {
                indexPontos = 0;
            }
            if (!pararPorUmTempo)
            {
                novaPosicao = pontosDeMovimento[indexPontos].transform.position;
            }
            else
            {
                StartCoroutine("PararPorUmTempo");
            }
            
        }

        transform.position = Vector2.MoveTowards(transform.position, novaPosicao, velocidade * Time.deltaTime);
    }

    IEnumerator PararPorUmTempo()
    {
        yield return new WaitForSeconds(tempoParado);
        novaPosicao = pontosDeMovimento[indexPontos].transform.position;
    }
}
