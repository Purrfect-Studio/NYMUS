using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtiraControlador : MonoBehaviour
{
    [Header("Script Atirar")]
    public AtirarEmVariosPontos atirar; // Componente que lida com a l�gica de atirar

    [Header("Jogador")]
    private GameObject jogador; // Refer�ncia para o transform do jogador

    [Header("Script Procurar Jogador")]
    public bool procuraJogador;
    public ProcurarJogador procurarJogador;

    [Header("Configura��es do Tiro")]
    public int municao; // Quantidade de tiros antes de recarregar
    public float intervaloTiro; // Intervalo entre os tiros
    public float tempoRecarga; // Tempo de recarga depois de atirar
    public bool olhandoEsquerda; // Dire��o para onde o inimigo est� olhando

    // Vari�veis de controle
    private int quantidadeTiros; // Quantidade atual de tiros dispon�veis
    private float contadorIntervaloTiro; // Contador para o intervalo entre os tiros

    void Start()
    {
        // Inicializa��o das vari�veis
        contadorIntervaloTiro = intervaloTiro;
        quantidadeTiros = municao;
        if(procuraJogador)
        {
            procurarJogador = GetComponent<ProcurarJogador>();
        }
        atirar = GetComponent<AtirarEmVariosPontos>();
        jogador = GameObject.FindWithTag("Jogador");
    }

    void Atirar()
    {
        // L�gica para atirar
        if (quantidadeTiros == 0)
        {
            // Se n�o h� mais muni��o, inicia a recarga
            contadorIntervaloTiro = tempoRecarga;
            quantidadeTiros = municao;
        }
        else
        {
            // Caso contr�rio, atira e decrementa a quantidade de tiros
            contadorIntervaloTiro = intervaloTiro;
            quantidadeTiros -= 1;
            atirar.AtirarProj�til();
        }
    }

    void Update()
    {
        // Atualiza o intervalo entre os tiros
        if (contadorIntervaloTiro < 0)
        {
            Atirar();
        }
        else
        {
            contadorIntervaloTiro -= Time.deltaTime;
        }

        // Verifica a dire��o do jogador e atualiza a dire��o do inimigo se procuraJogador for true
        if (procuraJogador)
        {
            if (procurarJogador != null && procurarJogador.procurarJogador()) // Verifica se as vari�veis desnecess�rias 
            {
                if (jogador != null && jogador.transform.position.x < transform.position.x && !olhandoEsquerda)
                {
                    olhandoEsquerda = true;
                    transform.Rotate(0f, 180f, 0f);
                    atirar.velocidadeTiro *= -1;
                }
                else if (jogador != null && jogador.transform.position.x > transform.position.x && olhandoEsquerda)
                {
                    olhandoEsquerda = false;
                    transform.Rotate(0f, 180f, 0f);
                    atirar.velocidadeTiro *= -1;
                }
            }
        }
    }
}
