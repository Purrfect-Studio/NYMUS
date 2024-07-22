using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class mostrarGradualmente : MonoBehaviour
{
    public Image background;
    //public Image fillArea;
    //public Image handleIcon;
    public float duracao = 2f; // Duração do efeito de preenchimento
    private Color corOriginalBackground;
    //private Color corOriginalFillArea;
    //private Color corOriginalHandleIcon;

    void Start()
    {
        // Salva a cor original da barra de vida
        corOriginalBackground = background.color;
        //corOriginalFillArea = fillArea.color;
        //corOriginalHandleIcon = handleIcon.color;

        // Define a barra de vida como invisível
        background.color = new Color(corOriginalBackground.r, corOriginalBackground.g, corOriginalBackground.b, 0f);
        background.fillAmount = 0f;

        //fillArea.color = new Color(corOriginalFillArea.r, corOriginalFillArea.g, corOriginalFillArea.b, 0f);
        //fillArea.fillAmount = 0f;
        
        //handleIcon.color = new Color(corOriginalHandleIcon.r, corOriginalHandleIcon.g, corOriginalHandleIcon.b, 0f);
        //handleIcon.fillAmount = 0f;
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
            background.fillAmount = Mathf.Lerp(0f, 1f, t);
            //fillArea.fillAmount = Mathf.Lerp(0f, 1f, t);
            //handleIcon.fillAmount = Mathf.Lerp(0f, 1f, t);
            // Gradualmente torna a barra visível
            background.color = new Color(corOriginalBackground.r, corOriginalBackground.g, corOriginalBackground.b, Mathf.Lerp(0f, 1f, t));
            //fillArea.color = new Color(corOriginalFillArea.r, corOriginalFillArea.g, corOriginalFillArea.b, Mathf.Lerp(0f, 1f, t));
            //handleIcon.color = new Color(corOriginalHandleIcon.r, corOriginalHandleIcon.g, corOriginalHandleIcon.b, Mathf.Lerp(0f, 1f, t));
            yield return null;
        }
        background.fillAmount = 1f;
        background.color = corOriginalBackground;

        //fillArea.fillAmount = 1f;
        //fillArea.color = corOriginalFillArea;
        
        //handleIcon.fillAmount = 1f;
        //handleIcon.color = corOriginalHandleIcon;
        }
    }
