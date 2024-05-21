using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glitchs : MonoBehaviour
{
    // Referência ao SpriteRenderer do objeto
    private SpriteRenderer spriteRenderer;

    // Referência ao Transform do objeto
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

    // Método para flipar o eixo X
    public void FlipX()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
    }

    // Método para flipar o eixo Y
    public void FlipY()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.flipY = !spriteRenderer.flipY;
        }
    }

    // Método para mudar a cor do sprite para uma aleatória
    public void MudarCorAleatoria()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.color = new Color(Random.value, Random.value, Random.value);
        }
    }

    // Método para mudar a posição do objeto para um lugar próximo
    public void MudarPosicaoAleatoria()
    {
        if (objetoTransform != null)
        {
            Vector3 novaPosicao = objetoTransform.position + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0); 
            objetoTransform.position = novaPosicao;
        }
    }

    public void Piscar()
    {
        StartCoroutine("piscar");
    }

    IEnumerator piscar()
    {
        for (float i = 0f; i < 1f; i += 0.1f)
        {
            spriteRenderer.enabled = false; //desativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
            spriteRenderer.enabled = true; // ativa o sprite
            yield return new WaitForSeconds(0.1f); //espera 0.1 segundos
        }
    }
}
