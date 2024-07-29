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

    [Header("Para Cutscene")]
    public bool acabouDialogo = false;

    // Flag para verificar se uma fala est� sendo escrita no momento
    private bool escrevendoFala;
    // Vari�vel para armazenar a corrotina ativa
    private Coroutine corrotinaEscrever;

    [SerializeField] private UnityEvent travarMovimentacao; // Evento para travar a movimenta��o do personagem
    [SerializeField] private UnityEvent liberarMovimentacao; // Evento para liberar a movimenta��o do personagem

    public void IniciarDialogo(List<Fala> falas)
    {
        // Inicializa o di�logo e bloqueia a movimenta��o do personagem
        dialogoObj.SetActive(true);
        this.falas = falas;
        index = 0;
        acabouDialogo = false;
        travarMovimentacao.Invoke();
        corrotinaEscrever = StartCoroutine(EscreverFala());
    }

    private void AtualizarInterface(Fala fala)
    {
        // Atualiza a interface com a imagem, nome e texto do personagem
        fotoPersonagem.sprite = fala.imagemPersonagem;
        nomePersonagem.text = fala.nomePersonagem;
        textoFala.text = "";
    }

    IEnumerator EscreverFala()
    {
        // Atualiza a interface e escreve a fala
        escrevendoFala = true;
        AtualizarInterface(falas[index]);

        foreach (char letra in falas[index].texto.ToCharArray())
        {
            textoFala.text += letra;
            yield return new WaitForSeconds(velocidadeDigitacao);
        }

        escrevendoFala = false;
    }

    public void PassarFala()
    {
        if (escrevendoFala)
        {
            CompletarFala();
        }
        else if (index < falas.Count - 1)
        {
            ProximaFala();
        }
        else
        {
            FinalizarDialogo();
        }
    }

    private void CompletarFala()
    {
        // Completa a fala atual instantaneamente
        StopCoroutine(corrotinaEscrever);
        textoFala.text = falas[index].texto;
        escrevendoFala = false;
    }

    private void ProximaFala()
    {
        // Passa para a pr�xima fala
        index++;
        corrotinaEscrever = StartCoroutine(EscreverFala());
    }

    public void PularDialogo()
    {
        // Pula todo o di�logo e mostra a �ltima fala
        if (corrotinaEscrever != null)
        {
            StopCoroutine(corrotinaEscrever);
        }

        index = falas.Count - 1;
        AtualizarInterface(falas[index]);
        textoFala.text = falas[index].texto;

        acabouDialogo = true;
        StartCoroutine(EsperarEFinalizar());
    }

    private void FinalizarDialogo()
    {
        // Limpa o di�logo e libera a movimenta��o do personagem
        acabouDialogo = true;
        dialogoObj.SetActive(false);
        liberarMovimentacao.Invoke();
    }

    private IEnumerator EsperarEFinalizar()
    {
        // Espera um curto per�odo antes de finalizar o di�logo
        yield return new WaitForSeconds(0.1f);
        FinalizarDialogo();
    }
}
