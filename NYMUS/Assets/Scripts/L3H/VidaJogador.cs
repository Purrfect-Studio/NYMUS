using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;                        // Vida maxima do L3H
    [SerializeField] private float vidaAtual;       // Vida atual do L3H

    [Header("Invulnerabilidade")]
    public static bool invulneravel;                //Liga e desliga a invulnerabilidade

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite; //Sprite do L3H

    [Header("Knockback")]
    public float forcaKnockbak;                     // Forca do knockback
    public static int knockbackParaDireita;         // Direcao do knockback

    [Header("RigidBody")]
    public Rigidbody2D rb;                          // rb = rigidbody

    [Header("Animator")]
    public Animator animacao;                       //Animator do L3H


    // Start é ativado no primeiro frame de cada fase
    void Start()
    {
        vidaAtual = vidaMaxima; //Coloca o L3h na vida maxima quando comeca a fase
        invulneravel = false;   //Desativa a invulnerabilidade
    }

    // Update é ativado a cada frame da fase
    void Update()
    {
        if (vidaAtual <= 0)
        {
            morrer(); // se a vida chegar a 0 chama o metodo de morrer
        }
    }

    void morrer()
    {
        PlayerControlador.podeMover = false; // desativa a movimentacao do jogador
        animacao.SetBool("estaMorto", true); // ativa a animacao de morte
        StartCoroutine("morreu"); // chama a co-rotina "morreu"
    }

    IEnumerator morreu()
    {
        yield return new WaitForSeconds(1.5f); // espera 1.5 segundos
        SceneManager.LoadScene("Lobby"); // volta pro lobby
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
        }
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;            // subtrai o dano recebido da vida atual
        invulneravel = true;                // ativa a invulnerabilidade
        StartCoroutine("Invulnerabilidade");// chama a co-rotina "Invulnerabilidade"
        Knockback();                        // chama o metodo de knockback
    }


    void Knockback()
    {
        rb.AddForce(new Vector2(10 * -knockbackParaDireita, 10), ForceMode2D.Impulse); // aplica uma forca na diagonal para empurrar o jogador para longe do inimigo
        StartCoroutine("Parar"); // chama a co-rotina "Parar"
    }

    IEnumerator Parar()
    {
        // Retira o controle do personagem
        PlayerControlador.podeMover = false;

        // comeca a animacao de tomar dano
        animacao.SetBool("tomarDano", true);

        yield return new WaitForSeconds(.5f);

        // Devolve o controle do personagem
        PlayerControlador.podeMover = true;

        // termina a animacao de tomar dano
        animacao.SetBool("tomarDano", false);
    }

    IEnumerator Invulnerabilidade()
    {
        //animacao de piscar
        for(float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false; //desativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
            sprite.enabled = true; // ativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
        }
        invulneravel = false; // desativa a invulnerabilidade
    }
}
