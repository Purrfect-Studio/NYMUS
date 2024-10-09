using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinguaAlterar : MonoBehaviour
{

    public GameObject lingua;
    // Start is called before the first frame update
    void Start()
    {
      lingua = GameObject.FindWithTag("localizacao"); 
    }

    // Update is called once per frame
    public void metodo(string linguagem){
    lingua.GetComponent<GerenciadorDeLocalizacao>().DefinirIdioma(linguagem);
    }
}
