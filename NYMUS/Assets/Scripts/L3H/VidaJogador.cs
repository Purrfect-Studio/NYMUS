using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaJogador : MonoBehaviour
{
    [Header("PlayerData")]
    public PlayerData playerData;
    [Header("Vida")]
    public BarraDeVida barraDeVida;
    public static bool estaMorto;
    public string nomeDaFaseVoltada = "Menu";
    [HideInInspector] public bool tomeiDano;
    [HideInInspector] public bool podeReviver;

    [Header("Escudo")]
    public BarraDeEscudo barraDeEscudo;
    private float escudoAtual;
    private float sobredanoNoEscudo;

    [Header("Invulnerabilidade")]
    public static bool invulneravel;                //Liga e desliga a invulnerabilidade    

    [Header("Sprite")]
    private SpriteRenderer sprite; //Sprite do L3H
    private Color corRoxo;
    private Color corOriginal;

    [Header("PlayerControlador")]
    private PlayerControlador playerControlador;

    [Header("Knockback")]
    public static int knockbackParaDireita;         // Direcao do knockback

    [Header("RigidBody")]
    private Rigidbody2D rigidBody2D;                          // rb = rigidbody

    // Start é ativado no primeiro frame de cada fase
    void Start()
    {
        barraDeVida.definirVidaMaxima(playerData.vidaMaxima, playerData.vidaAtual);

        estaMorto = false;       
        invulneravel = false;   //Desativa a invulnerabilidade
        barraDeEscudo.definirEscudoMaximo(playerData.escudoMaximo);
        sprite = GetComponent<SpriteRenderer>();
        rigidBody2D = GetComponent<Rigidbody2D>();
        playerControlador = GetComponent<PlayerControlador>();

        corRoxo = new Color (r: (138 / 255f), g: (43 / 255f), b: (226 / 255f));
        corOriginal = sprite.color;
    }

    public void curar(float cura)
    {
        if (playerData.vidaAtual < playerData.vidaMaxima) // verifica se a vida atual [e menor que a vida maxima
        {
            playerData.vidaAtual += cura; // soma a cura na vida maxima
            if(playerData.vidaAtual > playerData.vidaMaxima) // verifica se a vida atual ficou maior que a vida maxima
            {
                playerData.vidaAtual = playerData.vidaMaxima; // define a vida atual como a vida maxima
            }
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
        }
    }

    public void receberEscudo(float valor)
    {
        if(escudoAtual+valor > playerData.escudoMaximo)
        {
            escudoAtual = playerData.escudoMaximo;
        }
        else
        {
            escudoAtual += valor;
        }
        barraDeEscudo.ajustarBarraDeEscudo(escudoAtual);
    }

    public void removerEscudo()
    {
        escudoAtual = 0;
        barraDeEscudo.ajustarBarraDeEscudo(escudoAtual);
    }

    public void receberRevive()
    {
        podeReviver = true;
    }

    public void removerRevive()
    {
        podeReviver = false;
    }

    public void envenenar(int veneno)
    {
        StartCoroutine(Veneno(veneno));
    }

    IEnumerator Veneno(int veneno)
    {
        for (int i = 0; i < veneno; i++)
        {
            playerData.vidaAtual -= 1;
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
            sprite.color = corRoxo;
            yield return new WaitForSeconds(0.2f);
            sprite.color = corOriginal;
            yield return new WaitForSeconds(0.8f);
        }
    }

    public void tomarDano(float danoTomado)
    {
        Debug.Log("Jogador tomou dano: " + danoTomado);
        tomeiDano = true;
        if(escudoAtual > 0)
        {
            if(escudoAtual - danoTomado < 0)
            {
                sobredanoNoEscudo = danoTomado - escudoAtual;
                escudoAtual = 0;
            }
            else
            {
                escudoAtual -= danoTomado;
            }
            barraDeEscudo.ajustarBarraDeEscudo(escudoAtual);
        }
        else
        {
            playerData.vidaAtual -= danoTomado;            // subtrai o dano recebido da vida atual
            PlayerControlador.estaPulando = false;
            Knockback();                        // chama o metodo de knockback
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
            StartCoroutine("PararMovimentacao");// chama a co-rotina "PararMovimentacao"
        }
        if (sobredanoNoEscudo > 0)
        {
            playerData.vidaAtual -= sobredanoNoEscudo;
            sobredanoNoEscudo = 0;
            PlayerControlador.estaPulando = false;
            Knockback();                        // chama o metodo de knockback
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
            StartCoroutine("PararMovimentacao");// chama a co-rotina "PararMovimentacao"
        }
        
        if (playerData.vidaAtual <= 0)
        {
            morrer(); // se a vida chegar a 0 chama o metodo de morrer
        }
        StartCoroutine("Invulnerabilidade");// chama a co-rotina "Invulnerabilidade"
    }

    void Knockback()
    {
        rigidBody2D.AddForce(new Vector2(playerData.forcaKnockbackX * -knockbackParaDireita, playerData.forcaKnockbackY), ForceMode2D.Impulse); // aplica uma forca na diagonal para empurrar o jogador para longe do inimigo
    }

    IEnumerator Invulnerabilidade()
    {
        invulneravel = true;                // ativa a invulnerabilidade
        //animacao de piscar
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false; //desativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
            sprite.enabled = true; // ativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
        }
        tomeiDano = false;
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
        if (!podeReviver)
        {
            playerControlador.TravarMovimentacao();
            PlayerControlador.podeMover = false; // desativa a movimentacao do jogador
            StartCoroutine("morreu"); // chama a co-rotina "morreu"
        }
        else
        {
            playerData.vidaAtual = playerData.vidaMaxima;
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
            removerRevive();
        }
    }

    IEnumerator morreu()
    {
        estaMorto = true;
        yield return new WaitForSeconds(1.5f); // espera 1.5 segundos
        playerControlador.LiberarMovimentacao();
        estaMorto = false;
        if (playerControlador.ultimoCheckpoint != null)
        {
            transform.position = new Vector2(playerControlador.ultimoCheckpoint.transform.position.x, playerControlador.ultimoCheckpoint.transform.position.y);
            playerData.vidaAtual = playerData.vidaMaxima;
            estaMorto = false;
            barraDeVida.ajustarBarraDeVida(playerData.vidaAtual);
        }
        else
        {
            SceneManager.LoadScene(nomeDaFaseVoltada); // volta pro lobby
        }
    }
}
