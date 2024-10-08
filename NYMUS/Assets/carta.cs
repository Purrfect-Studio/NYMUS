using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class carta : MonoBehaviour
{
    public GameObject texto;
    public GameObject tagfeliz;
    // Start is called before the first frame update
    void Start()
    {
        texto = GameObject.FindWithTag("SENHA SECRETA");   
        tagfeliz = GameObject.FindWithTag("tag19"); 
      
    }

    public void metodo()
    {
        texto.GetComponent<TextMeshProUGUI>().text = "8241";
        tagfeliz.GetComponent<ContatoTrigger>().ativarSempre = true;
        tagfeliz.SetActive(true);
    }

    // Update is called once per frame
    
}
