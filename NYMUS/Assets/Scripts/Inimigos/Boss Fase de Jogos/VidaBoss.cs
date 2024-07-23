using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaBoss : MonoBehaviour
{
    [Header("Vida")]
    public float vidaMaxima;
    public float vidaAtual;
    public BarraDeVidaBoss barraDeVidaBoss;
    public static bool invulneravel = false;

    [Header("GameObject do inimigo")]
    public GameObject inimigo;

    [Header("Animator")]
    private Animator animacao;

    [Header("jogador")]
    private GameObject jogador;

    [Header("Sprite")]
    private SpriteRenderer sprite;


    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima

        animacao = GetComponent<Animator>();
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        
        sprite = GetComponent<SpriteRenderer>();

        barraDeVidaBoss.definirVidaMaxima(vidaMaxima);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tomarDano(float dano)
    {
        Debug.Log("Tomei dano" + dano);
        vidaAtual -= dano;
        barraDeVidaBoss.ajustarBarraDeVida(vidaAtual);
        StartCoroutine("Piscar");
        if (vidaAtual <= 0)
        {
            StartCoroutine("morreu");
        }
    }

    IEnumerator Piscar()
    {
        invulneravel = true;
        for (float i = 0f; i < 0.2f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;
    }

    IEnumerator morreu()
    {
        MovimentacaoBoss.podeMover = false;
        yield return new WaitForSeconds(1f);
        Destroy(inimigo);
    }
}
