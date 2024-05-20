using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitchs : MonoBehaviour
{
    
    // Refer�ncia ao SpriteRenderer do objeto
    private SpriteRenderer spriteRenderer;

    // Refer�ncia ao Transform do objeto
    private Transform objetoTransform;

    void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        if (objetoTransform == null)
        {
            objetoTransform = GetComponent<Transform>();
        }
    }


    // M�todo para flipar o eixo X
    public void FlipX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // M�todo para flipar o eixo Y
    public void FlipY()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    // M�todo para mudar a cor do sprite para uma aleat�ria
    public void MudarCorAleatoria()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // M�todo para mudar a posi��o do objeto para um lugar pr�ximo
    public void MudarPosicaoAleatoria()
    {
        if (objetoTransform != null)
        {
            Vector3 novaPosicao = objetoTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 
            objetoTransform.position = novaPosicao;
        }
    }
}
