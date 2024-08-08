using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class irAteJogador : MonoBehaviour
{
    [Header("Script Procurar Jogador")]
    public ProcurarJogador procurarJogador;
    [Header("Jogador")]
    public GameObject jogador;

    public float velocidade;

    private void Start()
    {
        jogador = GameObject.FindGameObjectWithTag("Jogador");
    }
    // Update is called once per frame
    void Update()
    {
        if(procurarJogador.procurarJogador() == true)
        {
            irJogador();
        }
    }

    public void irJogador()
    {
        transform.position = Vector2.MoveTowards(transform.position, jogador.transform.position, velocidade * Time.deltaTime);
    }
}
