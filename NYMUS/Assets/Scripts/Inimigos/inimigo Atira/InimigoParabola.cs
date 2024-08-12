using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoParabola : MonoBehaviour
{
    private InimigoPatrulha inimigoPatrulha;
    private AtirarEmVariosPontos atirarEmVariosPontos;
    private ProcurarJogador procurarJogador;
    private InimigoAtiraControlador InimigoAtiraControlador;
    // Start is called before the first frame update
    void Start()
    {
        inimigoPatrulha = GetComponent<InimigoPatrulha>();
        atirarEmVariosPontos = GetComponent<AtirarEmVariosPontos>();
        procurarJogador = GetComponent<ProcurarJogador>();
        InimigoAtiraControlador = GetComponent<InimigoAtiraControlador>();

        InimigoAtiraControlador.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        alterarDirecaoDoTiro();
        atirarNaDirecaoDoJogador();
    }

    void alterarDirecaoDoTiro()
    {
        if (inimigoPatrulha.direcao == 1 && atirarEmVariosPontos.velocidadeTirox < 0)
        {
            atirarEmVariosPontos.velocidadeTirox *= -1;
        }
        if (inimigoPatrulha.direcao == -1 && atirarEmVariosPontos.velocidadeTirox > 0)
        {
            atirarEmVariosPontos.velocidadeTirox *= -1;
        }
    }

    void atirarNaDirecaoDoJogador()
    {
        if (procurarJogador.procurarJogador())
        {
            if(transform.position.x - procurarJogador.jogador.transform.position.x < 0 && inimigoPatrulha.direcao == 1 || transform.position.x - procurarJogador.jogador.transform.position.x > 0 && inimigoPatrulha.direcao == -1)
            {
                InimigoAtiraControlador.enabled = true;
            }
            else
            {
                InimigoAtiraControlador.enabled = false;
            }
        }
    }
}
