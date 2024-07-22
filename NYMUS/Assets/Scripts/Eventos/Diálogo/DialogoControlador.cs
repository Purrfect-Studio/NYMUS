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
    private List<Fala> falas; // Lista de falas que compõem o diálogo
    private int index; // Índice da fala atual

    // Flag para verificar se uma fala está sendo escrita no momento
    private bool escrevendoFala;
    // Variável para armazenar a corrotina ativa
    private Coroutine corrotinaEscrever;

    [SerializeField] private UnityEvent travarMovimentacao; // Evento para travar a movimentação do personagem
    [SerializeField] private UnityEvent liberarMovimentacao; // Evento para liberar a movimentação do personagem

    public void IniciarDialogo(List<Fala> falas)
    {
        // Ativa o objeto de diálogo na interface
        dialogoObj.SetActive(true);
        // Define as falas do diálogo
        this.falas = falas;
        // Invoca o evento para travar a movimentação
        travarMovimentacao.Invoke();
        // Inicia a corrotina para escrever a fala
        index = 0; // Certifica-se que o índice comece do zero
        corrotinaEscrever = StartCoroutine(EscreverFala());
    }

    IEnumerator EscreverFala()
    {
        // Seta a flag para indicar que uma fala está sendo escrita
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
            yield return new WaitForSeconds(velocidadeDigitacao); // Aguarda um intervalo de tempo antes de adicionar a próxima letra
        }

        // Seta a flag para indicar que a fala terminou de ser escrita
        escrevendoFala = false;
    }

    public void PassarFala()
    {
        // Verifica se uma fala está sendo escrita
        if (escrevendoFala)
        {
            // Para a corrotina de escrita
            StopCoroutine(corrotinaEscrever);
            // Completa imediatamente a fala atual
            textoFala.text = falas[index].texto;
            // Seta a flag para indicar que a fala terminou de ser escrita
            escrevendoFala = false;
        }
        // Verifica se o texto atual é igual à fala atual
        else if (textoFala.text == falas[index].texto)
        {
            // Verifica se ainda existem falas a serem mostradas
            if (index < falas.Count - 1)
            {
                // Incrementa o índice para a próxima fala
                index++;
                // Inicia a corrotina para escrever a próxima fala
                corrotinaEscrever = StartCoroutine(EscreverFala());
            }
            else // Todas as falas foram mostradas
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
