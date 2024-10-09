using System.Collections.Generic;
using UnityEngine;
using TMPro; // Necessário para usar TextMeshPro

public class TextoLocalizador : MonoBehaviour
{
    [Header("Configurações")]
    public string chaveTexto; // Chave pública para buscar o texto correspondente
    public TextMeshProUGUI textoDisplay; // Referência ao componente TextMeshProUGUI (opcional)

    private void Start()
    {
        // Se não for atribuído manualmente, tenta buscar o próprio TextMeshProUGUI no GameObject
        if (textoDisplay == null)
        {
            textoDisplay = GetComponent<TextMeshProUGUI>();
        }

        // Verifica se o componente TextMeshProUGUI foi encontrado
        if (textoDisplay == null)
        {
            Debug.LogError("TextMeshProUGUI não encontrado no objeto. Atribua manualmente ou adicione o componente.");
            return;
        }

        // Busca o texto correspondente utilizando a chave
        string textoCorrespondente = GerenciadorDeLocalizacao.Instancia.ObterTextoLocalizado(chaveTexto);

        // Atualiza o texto do componente TextMeshProUGUI com o texto localizado
        textoDisplay.text = textoCorrespondente;
    }
}