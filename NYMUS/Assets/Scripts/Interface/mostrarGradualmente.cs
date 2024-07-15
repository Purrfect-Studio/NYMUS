using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class mostrarGradualmente : MonoBehaviour
{
    public Image barraDeVida;
    public float duracao = 2f; // Duração do efeito de preenchimento
    private Color corOriginal;

    void Start()
    {
        // Salva a cor original da barra de vida
        corOriginal = barraDeVida.color;
        // Define a barra de vida como invisível
        barraDeVida.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, 0f);
        barraDeVida.fillAmount = 0f;
    }

    public void MostrarBarraDeVida()
    {
        StartCoroutine(PreencherBarraGradualmente());
    }

    private IEnumerator PreencherBarraGradualmente()
    {
        float tempoInicial = Time.time;
        while (Time.time < tempoInicial + duracao)
        {
            float t = (Time.time - tempoInicial) / duracao;
            barraDeVida.fillAmount = Mathf.Lerp(0f, 1f, t);
            // Gradualmente torna a barra visível
            barraDeVida.color = new Color(corOriginal.r, corOriginal.g, corOriginal.b, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }
        barraDeVida.fillAmount = 1f;
        barraDeVida.color = corOriginal;
    }
}
