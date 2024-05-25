using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogoControlador : MonoBehaviour
{
    [Header("GameObject do Dialogo")]
    public GameObject dialogoObj; // Objeto do di�logo na interface
    [Header("Imagem")]
    public Image fotoPersonagem; // Imagem do personagem que est� falando
    [Header("Texto do Dialogo")]
    public Text textoFala; // Texto do di�logo
    [Header("Nome do personagem")]
    public Text nomePersonagem; // Nome do personagem que est� falando

    [Header("Configuracoes")]
    public float velocidadeDigitacao; // Velocidade que as letras do texto aparecem
    private string[] sentencas; // Array de senten�as que comp�em o di�logo
    private int index; // �ndice da senten�a atual

    // Flag para verificar se uma senten�a est� sendo escrita no momento
    private bool escrevendoSentenca;
    // Vari�vel para armazenar a corrotina ativa
    private Coroutine corrotinaEscrever;

    [SerializeField] private UnityEvent travarMovimentacao; // Evento para travar a movimenta��o do personagem
    [SerializeField] private UnityEvent liberarMovimentacao; // Evento para liberar a movimenta��o do personagem

    public void Fala(Sprite foto, string[] texto, string nomedoPersonagem)
    {
        // Ativa o objeto de di�logo na interface
        dialogoObj.SetActive(true);
        // Define a imagem do personagem
        fotoPersonagem.sprite = foto;
        // Define as senten�as do di�logo
        sentencas = texto;
        // Define o nome do personagem
        nomePersonagem.text = nomedoPersonagem;
        // Invoca o evento para travar a movimenta��o
        travarMovimentacao.Invoke();
        // Inicia a corrotina para escrever a senten�a
        index = 0; // Certifica-se que o �ndice comece do zero
        corrotinaEscrever = StartCoroutine(EscreverSentenca());
    }

    IEnumerator EscreverSentenca()
    {
        // Seta a flag para indicar que uma senten�a est� sendo escrita
        escrevendoSentenca = true;
        // Limpa o texto atual
        textoFala.text = "";

        // Itera sobre cada letra da senten�a atual e a adiciona ao texto de fala
        foreach (char letra in sentencas[index].ToCharArray())
        {
            textoFala.text += letra; // Adiciona uma letra ao texto
            yield return new WaitForSeconds(velocidadeDigitacao); // Aguarda um intervalo de tempo antes de adicionar a pr�xima letra
        }

        // Seta a flag para indicar que a senten�a terminou de ser escrita
        escrevendoSentenca = false;
    }

    public void PassarSentenca()
    {
        Console.Write("passou sentenca");
        // Verifica se uma senten�a est� sendo escrita
        if (escrevendoSentenca)
        {
            // Para a corrotina de escrita
            StopCoroutine(corrotinaEscrever);
            // Completa imediatamente a senten�a atual
            textoFala.text = sentencas[index];
            // Seta a flag para indicar que a senten�a terminou de ser escrita
            escrevendoSentenca = false;
        }
        // Verifica se o texto atual � igual � senten�a atual
        else if (textoFala.text == sentencas[index])
        {
            // Verifica se ainda existem senten�as a serem mostradas
            if (index < sentencas.Length - 1)
            {
                // Incrementa o �ndice para a pr�xima senten�a
                index++;
                // Inicia a corrotina para escrever a pr�xima senten�a
                corrotinaEscrever = StartCoroutine(EscreverSentenca());
            }
            else // Todas as senten�as foram mostradas
            {
                // Limpa o texto de fala
                textoFala.text = "";
                // Reseta o �ndice
                index = 0;
                // Desativa o objeto de di�logo
                dialogoObj.SetActive(false);
                // Invoca o evento para liberar a movimenta��o
                liberarMovimentacao.Invoke();
            }
        }
    }
}
