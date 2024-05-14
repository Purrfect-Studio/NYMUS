using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogoControlador : MonoBehaviour
{
    [Header("GameObject do Dialogo")]
    public GameObject dialogoObj; //objeto do dialogo
    [Header("Imagem")]
    public Image fotoPersonagem;
    [Header("Texto do Dialogo")]
    public Text textoFala;
    [Header("Nome do personagem")]
    public Text nomePersonagem;

    [Header("Configuracoes")]
    public float velocidadeDigitacao; // Velocidade que vai aparecer os dígitos
    private string[] sentencas;
    private int index;

    [SerializeField] private UnityEvent travarMovimentacao;
    [SerializeField] private UnityEvent liberarMovimentacao;

    public void Fala(Sprite foto, string[] texto, string nomedoPersonagem)
    {
        dialogoObj.SetActive(true);
        fotoPersonagem.sprite = foto;
        sentencas = texto;
        nomePersonagem.text = nomedoPersonagem;
        travarMovimentacao.Invoke();
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
        Console.Write("passou sentenca");
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
                liberarMovimentacao.Invoke();
            }
        }
    }
}
