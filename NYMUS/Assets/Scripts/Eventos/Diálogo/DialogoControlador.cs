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
    private List<Fala> falas; // Lista de falas que comp�em o di�logo
    private int index; // �ndice da fala atual

    // Flag para verificar se uma fala est� sendo escrita no momento
    private bool escrevendoFala;
    // Vari�vel para armazenar a corrotina ativa
    private Coroutine corrotinaEscrever;

    [SerializeField] private UnityEvent travarMovimentacao; // Evento para travar a movimenta��o do personagem
    [SerializeField] private UnityEvent liberarMovimentacao; // Evento para liberar a movimenta��o do personagem

    public void IniciarDialogo(List<Fala> falas)
    {
        // Ativa o objeto de di�logo na interface
        dialogoObj.SetActive(true);
        // Define as falas do di�logo
        this.falas = falas;
        // Invoca o evento para travar a movimenta��o
        travarMovimentacao.Invoke();
        // Inicia a corrotina para escrever a fala
        index = 0; // Certifica-se que o �ndice comece do zero
        corrotinaEscrever = StartCoroutine(EscreverFala());
    }

    IEnumerator EscreverFala()
    {
        // Seta a flag para indicar que uma fala est� sendo escrita
        escrevendoFala = true;
        // Limpa o texto atual
        textoFala.text = "";

        // Define a imagem e o nome do personagem
        fotoPersonagem.sprite = falas[index].imagemPersonagem;
        nomePersonagem.text = falas[index].nomePersonagem;

        // Itera sobre cada letra da fala atual e a adiciona ao texto de fala
        foreach (char letra in falas[index].texto.ToCharArray())
        {
            textoFala.text += letra; // Adiciona uma letra ao texto
            yield return new WaitForSeconds(velocidadeDigitacao); // Aguarda um intervalo de tempo antes de adicionar a pr�xima letra
        }

        // Seta a flag para indicar que a fala terminou de ser escrita
        escrevendoFala = false;
    }

    public void PassarFala()
    {
        // Verifica se uma fala est� sendo escrita
        if (escrevendoFala)
        {
            // Para a corrotina de escrita
            StopCoroutine(corrotinaEscrever);
            // Completa imediatamente a fala atual
            textoFala.text = falas[index].texto;
            // Seta a flag para indicar que a fala terminou de ser escrita
            escrevendoFala = false;
        }
        // Verifica se o texto atual � igual � fala atual
        else if (textoFala.text == falas[index].texto)
        {
            // Verifica se ainda existem falas a serem mostradas
            if (index < falas.Count - 1)
            {
                // Incrementa o �ndice para a pr�xima fala
                index++;
                // Inicia a corrotina para escrever a pr�xima fala
                corrotinaEscrever = StartCoroutine(EscreverFala());
            }
            else // Todas as falas foram mostradas
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
