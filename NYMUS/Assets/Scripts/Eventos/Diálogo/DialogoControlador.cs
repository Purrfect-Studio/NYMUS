using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class DialogoControlador : MonoBehaviour
{
    [Header("GameObject do Dialogo")]
    public GameObject dialogoObj; // Objeto do diálogo na interface
    [Header("Imagem")]
    public Image fotoPersonagem; // Imagem do personagem que está falando
    [Header("Texto do Dialogo")]
    public Text textoFala; // Texto do diálogo
    [Header("Nome do personagem")]
    public Text nomePersonagem; // Nome do personagem que está falando

    [Header("Configuracoes")]
    public float velocidadeDigitacao; // Velocidade que as letras do texto aparecem
    private string[] sentencas; // Array de sentenças que compõem o diálogo
    private int index; // Índice da sentença atual

    // Flag para verificar se uma sentença está sendo escrita no momento
    private bool escrevendoSentenca;
    // Variável para armazenar a corrotina ativa
    private Coroutine corrotinaEscrever;

    [SerializeField] private UnityEvent travarMovimentacao; // Evento para travar a movimentação do personagem
    [SerializeField] private UnityEvent liberarMovimentacao; // Evento para liberar a movimentação do personagem

    public void Fala(Sprite foto, string[] texto, string nomedoPersonagem)
    {
        // Ativa o objeto de diálogo na interface
        dialogoObj.SetActive(true);
        // Define a imagem do personagem
        fotoPersonagem.sprite = foto;
        // Define as sentenças do diálogo
        sentencas = texto;
        // Define o nome do personagem
        nomePersonagem.text = nomedoPersonagem;
        // Invoca o evento para travar a movimentação
        travarMovimentacao.Invoke();
        // Inicia a corrotina para escrever a sentença
        index = 0; // Certifica-se que o índice comece do zero
        corrotinaEscrever = StartCoroutine(EscreverSentenca());
    }

    IEnumerator EscreverSentenca()
    {
        // Seta a flag para indicar que uma sentença está sendo escrita
        escrevendoSentenca = true;
        // Limpa o texto atual
        textoFala.text = "";

        // Itera sobre cada letra da sentença atual e a adiciona ao texto de fala
        foreach (char letra in sentencas[index].ToCharArray())
        {
            textoFala.text += letra; // Adiciona uma letra ao texto
            yield return new WaitForSeconds(velocidadeDigitacao); // Aguarda um intervalo de tempo antes de adicionar a próxima letra
        }

        // Seta a flag para indicar que a sentença terminou de ser escrita
        escrevendoSentenca = false;
    }

    public void PassarSentenca()
    {
        Console.Write("passou sentenca");
        // Verifica se uma sentença está sendo escrita
        if (escrevendoSentenca)
        {
            // Para a corrotina de escrita
            StopCoroutine(corrotinaEscrever);
            // Completa imediatamente a sentença atual
            textoFala.text = sentencas[index];
            // Seta a flag para indicar que a sentença terminou de ser escrita
            escrevendoSentenca = false;
        }
        // Verifica se o texto atual é igual à sentença atual
        else if (textoFala.text == sentencas[index])
        {
            // Verifica se ainda existem sentenças a serem mostradas
            if (index < sentencas.Length - 1)
            {
                // Incrementa o índice para a próxima sentença
                index++;
                // Inicia a corrotina para escrever a próxima sentença
                corrotinaEscrever = StartCoroutine(EscreverSentenca());
            }
            else // Todas as sentenças foram mostradas
            {
                // Limpa o texto de fala
                textoFala.text = "";
                // Reseta o índice
                index = 0;
                // Desativa o objeto de diálogo
                dialogoObj.SetActive(false);
                // Invoca o evento para liberar a movimentação
                liberarMovimentacao.Invoke();
            }
        }
    }
}
