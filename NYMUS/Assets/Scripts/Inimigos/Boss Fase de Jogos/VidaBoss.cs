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
    [SerializeField] public static bool podeMover;

    [Header("GameObject do inimigo")]
    public GameObject inimigo;

    [Header("RigidBody")]
    public Rigidbody2D rb;   // rb = rigidbody

    [Header("Animator")]
    public Animator animacao;

    [Header("jogador")]
    public GameObject jogador;

    [Header("Sprite")]
    [SerializeField] private SpriteRenderer sprite;



    // Start is called before the first frame update
    void Start()
    {
        podeMover = true;
        vidaAtual = vidaMaxima; // define a vida atual como a vida maxima
        jogador = GameObject.FindGameObjectWithTag("Jogador");
        if (animacao != null)
        {
            animacao = GetComponent<Animator>();
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void tomarDano(float dano)
    {
        Debug.Log("Tomei dano" + dano);
        vidaAtual -= dano;
        StartCoroutine("Piscar");
        if (vidaAtual <= 0)
        {
            StartCoroutine("morreu");
        }
    }

    IEnumerator Piscar()
    {
        for (float i = 0f; i < 0.2f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
    }

    IEnumerator morreu()
    {
        podeMover = false;
        yield return new WaitForSeconds(1f);
        Destroy(inimigo);
    }
}
