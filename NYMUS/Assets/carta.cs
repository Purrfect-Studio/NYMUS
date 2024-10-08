using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class carta : MonoBehaviour
{
    public GameObject texto;
    public GameObject tagfeliz;
    public GameObject jogador;
    // Start is called before the first frame update
    void Start()
    {
        texto = GameObject.FindWithTag("SENHA SECRETA");   
        tagfeliz = GameObject.FindWithTag("tag19");
        jogador = GameObject.FindWithTag("Jogador");
    }

    public void metodo()
    {
        texto.GetComponent<TextMeshProUGUI>().text = "8241";
        tagfeliz.GetComponent<ContatoTrigger>().ativarSempre = true;
        tagfeliz.SetActive(true);
        jogador.GetComponent<VidaJogador>().curar(300);
    }

    // Update is called once per frame
    
}
