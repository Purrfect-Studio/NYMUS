using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtiraControlador : MonoBehaviour
{
    [Header("Script Atirar")]
    public AtirarEmVariosPontos atirar; // Componente que lida com a lógica de atirar

    [Header("Jogador")]
    private GameObject jogador; // Referência para o transform do jogador

    [Header("Script Procurar Jogador")]
    public bool procuraJogador;
    public ProcurarJogador procurarJogador;

    [Header("Configurações do Tiro")]
    public int municao; // Quantidade de tiros antes de recarregar
    public float intervaloTiro; // Intervalo entre os tiros
    public float tempoRecarga; // Tempo de recarga depois de atirar
    public bool olhandoEsquerda; // Direção para onde o inimigo está olhando

    // Variáveis de controle
    private int quantidadeTiros; // Quantidade atual de tiros disponíveis
    private float contadorIntervaloTiro; // Contador para o intervalo entre os tiros

    void Start()
    {
        // Inicialização das variáveis
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
        // Lógica para atirar
        if (quantidadeTiros == 0)
        {
            // Se não há mais munição, inicia a recarga
            contadorIntervaloTiro = tempoRecarga;
            quantidadeTiros = municao;
        }
        else
        {
            // Caso contrário, atira e decrementa a quantidade de tiros
            contadorIntervaloTiro = intervaloTiro;
            quantidadeTiros -= 1;
            atirar.AtirarProjétil();
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

        // Verifica a direção do jogador e atualiza a direção do inimigo se procuraJogador for true
        if (procuraJogador)
        {
            if (procurarJogador != null && procurarJogador.procurarJogador()) // Verifica se as variáveis desnecessárias 
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
