using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogoControlador : MonoBehaviour
{
    [Header("Components")]
    public GameObject dialogoObj; //objeto do dialogo
    public Image fotoPersonagem;
    public Text textoFala;
    public Text nomePersonagem;

    [Header("Settings")]
    public float velocidadeDigitacao; // Velocidade que vai aparecer os dígitos
    private string[] sentencas;
    private int index;

    public void Fala(Sprite foto, string[] texto, string nomedoPersonagem)
    {
        dialogoObj.SetActive(true);
        fotoPersonagem.sprite = foto;
        sentencas = texto;
        nomePersonagem.text = nomedoPersonagem;
        PlayerControlador.podeMover = false;
        VidaJogador.invulneravel = true;
        StartCoroutine(EscreverSentenca());
    }
    
    IEnumerator EscreverSentenca()
    {
        foreach (char letra in sentencas[index].ToCharArray())
        {
            textoFala.text += letra; //Somando uma letra no string da fala por vez
            yield return new WaitForSeconds(velocidadeDigitacao);
        }
    }

    public void PassarSentenca()
    {
        if (textoFala.text == sentencas[index])
        {
            if (index < sentencas.Length-1) // Verifica se ainda tem dialogos
            {
                index++;
                textoFala.text = ""; //limpa a caixa de texto para o prox texto
                StartCoroutine(EscreverSentenca());
            }
            else //acabou os textos
            {
                textoFala.text = "";
                index = 0;
                dialogoObj.SetActive(false);
                PlayerControlador.podeMover = true;
                VidaJogador.invulneravel = false;
            }
        }
    }
}
