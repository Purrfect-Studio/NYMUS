using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtiraTesteControlador : MonoBehaviour
{
    // Componentes necess�rios e vari�veis para controlar o comportamento de atirar do inimigo
    [Header("Script Atirar")]
    public TiroTeste atirar; // Componente que lida com a l�gica de atirar
    public ProcurarJogador procurarJogador;
    [Header("Jogador")]
    public Transform jogador; // Refer�ncia para o transform do jogador
    // Configura��es do tiro
    [Header("Configuracoes do Tiro")]
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
        // Adiciona um listener para o evento de encontrar jogador
        //procurarJogador.JogadorEncontrado.AddListener(Atirar);
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

        // Verifica a dire��o do jogador e atualiza a dire��o do inimigo
        if(procurarJogador.procurarJogador()==true)
        {
            if (jogador.position.x < transform.position.x && olhandoEsquerda == false)
            {
                olhandoEsquerda = true;
                transform.Rotate(0f, 180f, 0f);
                atirar.velocidadeTiro *= -1;
            }
            if (jogador.position.x > transform.position.x && olhandoEsquerda == true)
            {
                olhandoEsquerda = false;
                transform.Rotate(0f, 180f, 0f);
                atirar.velocidadeTiro *= -1;
            }
        }
    }
}
