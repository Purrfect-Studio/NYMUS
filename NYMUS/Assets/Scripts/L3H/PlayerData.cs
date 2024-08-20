using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Player Data")] //Create a new playerData object by right clicking in the Project Menu then Create/Player/Player Data and drag onto the player
public class PlayerData : ScriptableObject
{
    [Header("Vida")]
    public float vidaMaxima;
    [HideInInspector] public float vidaAtual;
    public float vidaAnteriorL3h;

    [Header("Escudo")]
    public float escudoMaximo;

    [Header("Knockback")]
    public float forcaKnockbackX;
    public float forcaKnockbackY;

    [Header("Andar")]
    public float velocidade;

    [Header("Pulo")]
    public float forcaPulo;

    [Header("Teste pulo")]
    public float jumpHeight;
    public float jumpTimeToApex;
    [HideInInspector] public float gravityStrength;
    [HideInInspector] public float gravityScale;
    [HideInInspector] public float force;
    [HideInInspector] public float jumpForce;

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
    public float alcanceAtaque;

    [Header("Ataque Ranged")]
    public bool possuiAtaqueRanged;
    public float velocidadeAtaqueRanged;
    public float duracaoAtaqueRanged;

    [Header("Escada")]
    public float velocidadeEscada;

    public void OnValidate()
    {
        vidaAtual = vidaMaxima;

        //teste pulo 
        gravityStrength = -(2 * jumpHeight) / (jumpTimeToApex * jumpTimeToApex);
        gravityScale = gravityStrength / Physics2D.gravity.y;
        jumpForce = Mathf.Abs(gravityStrength) * jumpTimeToApex;
        force = jumpForce;
    }
}
