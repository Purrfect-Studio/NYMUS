using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public static bool invulneravel;
    public float vidaMaxima;
    [SerializeField] private float vidaAtual;
    [SerializeField] private SpriteRenderer sprite;

    public Rigidbody2D rb;   // rb = rigidbody
    public float forcaKnockbak;
    
    [SerializeField] public float contadorKnockback;
    [SerializeField] public float tempoKnockback;
    [SerializeField] public static int knockbackParaDireita;


    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        invulneravel = false;
    }

    private void FixedUpdate()
    {
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;
        invulneravel = true;
        Knockback();
        StartCoroutine("invulnerabilidade");
    }


    void Knockback()
    {
        rb.AddForce(new Vector2(10 * -knockbackParaDireita, 10), ForceMode2D.Impulse);
        StartCoroutine("Freeze");
    }

    IEnumerator Freeze()
    {
        // Retira o controle do personagem
        PlayerControlador.podeMover = false;

        yield return new WaitForSeconds(.5f);

        // Devolve o controle do personagem
        PlayerControlador.podeMover = true;
    }

    IEnumerator invulnerabilidade()
    {
        
        for(float i = 0f; i < 1f; i += 0.1f)
        {
            sprite.enabled = false;
            yield return new WaitForSeconds(0.1f);
            sprite.enabled = true;
            yield return new WaitForSeconds(0.1f);
        }
        invulneravel = false;
    }
}
