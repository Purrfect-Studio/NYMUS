using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")]
public class PlayerData : ScriptableObject
{
    [Header("Vida")]
    public float vidaMaxima;
    public float vidaAtual;
    public float vidaAnteriorL3h;

    [Header("Escudo")]
    public float escudoMaximo;
    public float escudoPermanente;

    [Header("Knockback")]
    public float forcaKnockbackX;
    public float forcaKnockbackY;

    [Header("Andar")]
    public float velocidade;

    [Header("Pulo")]
    public float alturaPulo; // altura maxima do pulo
    public float tempoAteAlturaMaximaPulo; //tempo para alcancar a altura maxima
    public float tempoBufferInputPulo; //buffer de input antes de chegar no chao
    [HideInInspector] public float forcaPulo; // forca do pulo
    [Range(0.01f, 0.5f)] public float coyoteTime; //pode pular depois de sair do chao
    public float multiplicadorGravidadeCortarPulo; //multiplicador de quando o jogador soltar o botao de pulo enquanto esta pulando


    [Header("Gravidade")]
    public float multiplicadorGravidadeCaindo; //multiplicador de gravidade quando o jogador estiver caindo
    public float velocidadeMaximaCaindo; //velocidade maxima de queda
    [HideInInspector] public float gravityStrength;
    [HideInInspector] public float gravityScale;

    [Header("Pulo duplo")]
    public bool possuiPuloDuplo;

    [Header("Energia")]
    public float energiaMaxima;
    public float velocidadeRegeneracaoDeEnergia;

    [Header("Dash")]
    public bool possuiDash;
    public float forcaDashX;
    public float forcaDashY;
    public float energiaNecessariaParaDash;
    public float tempoMaximoDash;
    public float cooldownDash;

    [Header("Ataque Melee")]
    public float dano;

    [Header("Ataque Ranged")]
    public bool possuiAtaqueRanged;
    public float velocidadeAtaqueRanged;
    public float duracaoAtaqueRanged;

    [Header("Escada")]
    public float velocidadeEscada;

    public void OnValidate()
    {
        vidaAtual = vidaMaxima;

        //pulo 
        gravityStrength = -(2 * alturaPulo) / (tempoAteAlturaMaximaPulo * tempoAteAlturaMaximaPulo);
        //gravityScale = gravityStrength / Physics2D.gravity.y;

        forcaPulo = Mathf.Abs(gravityStrength) * tempoAteAlturaMaximaPulo;
    }
}
