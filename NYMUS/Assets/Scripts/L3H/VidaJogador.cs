using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;                        // Vida maxima do L3H
    public float vidaAtual;       // Vida atual do L3H
    public BarraDeVida barraDeVida;

    [Header("Invulnerabilidade")]
    public static bool invulneravel;                //Liga e desliga a invulnerabilidade
    public bool tomeiDano;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite; //Sprite do L3H

    [Header("Knockback")]
    public float forcaKnockbackX;          // Forca do knockback
    public float forcaKnockbackY;          // Forca do knockback
    public static int knockbackParaDireita;         // Direcao do knockback

    [Header("RigidBody")]
    public Rigidbody2D rigidBody2D;                          // rb = rigidbody

    // Start é ativado no primeiro frame de cada fase
    void Start()
    {
        vidaAtual = vidaMaxima; //Coloca o L3h na vida maxima quando comeca a fase
        invulneravel = false;   //Desativa a invulnerabilidade
        tomeiDano = false;
        barraDeVida.definirVidaMaxima(vidaMaxima);
    }

    public void curar(float cura)
    {
        if (vidaAtual < vidaMaxima) // verifica se a vida atual [e menor que a vida maxima
        {
            vidaAtual += cura; // soma a cura na vida maxima
            if(vidaAtual > vidaMaxima) // verifica se a vida atual ficou maior que a vida maxima
            {
                vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
            }
            barraDeVida.ajustarBarraDeVida(vidaAtual);
        }
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;            // subtrai o dano recebido da vida atual
        if (vidaAtual <= 0)
        {
            morrer(); // se a vida chegar a 0 chama o metodo de morrer
        }
        PlayerControlador.estaPulando = false;
        Knockback();                        // chama o metodo de knockback
        barraDeVida.ajustarBarraDeVida(vidaAtual);
        StartCoroutine("Invulnerabilidade");// chama a co-rotina "Invulnerabilidade"
        StartCoroutine("PararMovimentacao");// chama a co-rotina "PararMovimentacao"
        //Debug.Log("Jogador tomou dano: " + danoTomado);
        //Debug.Log("Vida atual: " + vidaAtual);
    }

    void Knockback()
    {
        rigidBody2D.AddForce(new Vector2(forcaKnockbackX * -knockbackParaDireita, forcaKnockbackY), ForceMode2D.Impulse); // aplica uma forca na diagonal para empurrar o jogador para longe do inimigo
    }

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;                // ativa a invulnerabilidade
        tomeiDano = true;
        //animacao de piscar
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false; //desativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
            sprite.enabled = true; // ativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
        }
        tomeiDano = true;
        invulneravel = false; // desativa a invulnerabilidade
    }

    IEnumerator PararMovimentacao()
    {
        // Retira o controle do personagem
        PlayerControlador.podeMover = false;

        yield return new WaitForSeconds(.5f);

        // Devolve o controle do personagem
        PlayerControlador.podeMover = true;
    }

    void morrer()
    {
        PlayerControlador.podeMover = false; // desativa a movimentacao do jogador
        StartCoroutine("morreu"); // chama a co-rotina "morreu"
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1.5f); // espera 1.5 segundos
        SceneManager.LoadScene("Lobby"); // volta pro lobby
    }
}
