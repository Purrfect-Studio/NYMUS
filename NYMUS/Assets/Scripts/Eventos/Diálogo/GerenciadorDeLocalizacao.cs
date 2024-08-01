using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class GerenciadorDeLocalizacao : MonoBehaviour
{
    public static GerenciadorDeLocalizacao Instancia { get; private set; }

    private Dictionary<string, string> textosLocalizados;
    public string idiomaAtual = "en";

    private void Awake()
    {
        if (Instancia == null)
        {
            Instancia = this;
            DontDestroyOnLoad(gameObject);
            CarregarTextosLocalizados();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void CarregarTextosLocalizados()
    {
        if (textosLocalizados == null)
        {
            textosLocalizados = new Dictionary<string, string>();
        }
        else
        {
            textosLocalizados.Clear(); // Limpa o dicionário antes de carregar novos textos
        }

        string caminho = Path.Combine(Application.streamingAssetsPath, $"{idiomaAtual}_textos.json");
        if (File.Exists(caminho))
        {
            string conteudoJson = File.ReadAllText(caminho);

            Debug.Log("Conteúdo JSON: " + conteudoJson); // Log para verificar o conteúdo do JSON

            try
            {
                var dados = JsonUtility.FromJson<TextosJson>(conteudoJson);

                if (dados != null && dados.textos != null)
                {
                    foreach (var item in dados.textos)
                    {
                        textosLocalizados[item.chave] = item.texto;
                    }

                    // Notifica outros componentes que o idioma foi alterado
                    NotificarAlteracaoDeIdioma();
                }
                else
                {
                    Debug.LogError("Falha ao desserializar o JSON.");
                }
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Erro ao desserializar o JSON: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError($"Arquivo de localização não encontrado: {caminho}");
        }
    }

    public string ObterTextoLocalizado(string chave)
    {
        if (textosLocalizados == null)
        {
            Debug.LogError("TextosLocalizados não foi inicializado.");
            return null;
        }

        if (textosLocalizados.TryGetValue(chave, out string texto))
        {
            return texto;
        }

        Debug.LogWarning($"Texto não encontrado para a chave: {chave}");
        return null;
    }

    public void DefinirIdioma(string idioma)
    {
        idiomaAtual = idioma;
        CarregarTextosLocalizados();
    }

    private void NotificarAlteracaoDeIdioma()
    {
        // Envia um evento ou chama um método para atualizar os textos na UI
        // Exemplo: Mensagens para atualizar a UI
        /*MensagemParaAtualizarUI[] componentesParaAtualizar = FindObjectsOfType<MensagemParaAtualizarUI>();
        foreach (var componente in componentesParaAtualizar)
        {
            componente.AtualizarTexto();
        }*/
    }
}

[System.Serializable]
public class TextoItem
{
    public string chave;
    public string texto;
}

[System.Serializable]
public class TextosJson
{
    public TextoItem[] textos;
}
