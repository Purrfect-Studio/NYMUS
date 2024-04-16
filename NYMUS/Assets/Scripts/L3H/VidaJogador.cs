using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VidaJogador : MonoBehaviour
{
    public static bool invulneravel;
    public float vidaMaxima;
    [SerializeField] private float vidaAtual;
    [SerializeField] private SpriteRenderer sprite;
    // Start is called before the first frame update
    void Start()
    {
        vidaAtual = vidaMaxima;
        invulneravel = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void tomarDano(float danoTomado)
    {
        vidaAtual -= danoTomado;
        invulneravel = true;
        StartCoroutine (invulnerabilidade());
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
