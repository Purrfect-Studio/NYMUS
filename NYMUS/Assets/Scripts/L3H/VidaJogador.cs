using System.Collections;
using System.Collections.Generic;
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
    [SerializeField] public static bool knockbackParaDireita;


    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        invulneravel = false;
    }

    private void FixedUpdate()
    {
        knockback();
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;
        invulneravel = true;
        contadorKnockback = tempoKnockback;
        StartCoroutine (invulnerabilidade());
    }

    void knockback()
    {
        if(contadorKnockback < 0)
        {
            PlayerControlador.podeMover = true;
        }
        else
        {
            PlayerControlador.podeMover = false;
            if (knockbackParaDireita == true)
            {
               // rb.AddForce()(1 * forcaKnockbak, rb.velocity.y);
            }
            if (knockbackParaDireita == false)
            {
                rb.velocity = new Vector2(-1 * forcaKnockbak, rb.velocity.y);
            }
        }
        contadorKnockback -= Time.deltaTime;
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
