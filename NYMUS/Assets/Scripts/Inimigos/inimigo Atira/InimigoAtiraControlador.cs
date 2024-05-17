using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InimigoAtiraControlador : MonoBehaviour
{
    // Componentes necessários e variáveis para controlar o comportamento de atirar do inimigo
    [Header("Script Atirar")]
    public Atirar atirar; // Componente que lida com a lógica de atirar
    public ProcurarJogador procurarJogador;
    [Header("Jogador")]
    public Transform jogador; // Referência para o transform do jogador
    // Configurações do tiro
    [Header("Configuracoes do Tiro")]
    public int municao; // Quantidade de tiros antes de recarregar
    public float intervaloTiro; // Intervalo entre os tiros
    public float tempoRecarga; // Tempo de recarga depois de atirar
    public bool olhandoEsquerda; // Direção para onde o inimigo está olhando

    // Variáveis de controle
    private int quantidadeTiros; // Quantidade atual de tiros disponíveis
    private float contadorIntervaloTiro; // Contador para o intervalo entre os tiros

    [Header("Interacoes com o Jogador")]
    [SerializeField] private UnityEvent DanoCausado;
    public static bool acertouJogador;               // diz se o tiro acertou ou nao
    public float danoJogador;

    void Start()
    {
        // Inicialização das variáveis
        contadorIntervaloTiro = intervaloTiro;
        quantidadeTiros = municao;
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
        if (acertouJogador == true && VidaJogador.invulneravel == false)
        {
            if (transform.position.x <= jogador.transform.position.x)
            {
                VidaJogador.knockbackParaDireita = -1;
            }
            else
            {
                VidaJogador.knockbackParaDireita = 1;
            }
            DanoCausado.Invoke();
            acertouJogador = false;
        }
        // Atualiza o intervalo entre os tiros
        if (contadorIntervaloTiro < 0)
        {
            Atirar();
        }
        else
        {
            contadorIntervaloTiro -= Time.deltaTime;
        }

        // Verifica a direção do jogador e atualiza a direção do inimigo
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
