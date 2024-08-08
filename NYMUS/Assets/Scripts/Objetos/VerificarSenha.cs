using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;

public class VerificarSenha : MonoBehaviour
{
    public int[] senhaNescessariaParaAbrir;
    public int[] senhaInserida;
    public int contadorSenha = 0;
    public int contadorVerificarSenha = 0;
    public bool digitoCorreto;
    public TextMeshProUGUI texto;

    [SerializeField] private UnityEvent senhaCorreta;
    [SerializeField] private UnityEvent senhaIncorreta;

    private void Start()
    {
        
    }
    private void Update()
    {
        
    }

    public void verificarSenha()
    {
        digitoCorreto = true;
        while(digitoCorreto == true && contadorVerificarSenha < senhaNescessariaParaAbrir.Length) 
        {
            if (senhaInserida[contadorVerificarSenha] == senhaNescessariaParaAbrir[contadorVerificarSenha])
            {
                digitoCorreto = true;
                contadorVerificarSenha++;
                if(contadorVerificarSenha == senhaNescessariaParaAbrir.Length)
                {
                    senhaCorreta.Invoke();
                }
            }
            else
            {
                digitoCorreto = false;
            }
            if(digitoCorreto == false)
            {
                senhaIncorreta.Invoke();
            }
        }
        contadorVerificarSenha = 0;
    }

    public void adicionarNumeroNaSenha(int numero)
    {
        senhaInserida[contadorSenha] = numero;
        contadorSenha++;
        texto.text += numero;
        if(contadorSenha >= senhaNescessariaParaAbrir.Length)
        {
            contadorSenha = 0;
            verificarSenha();
            //texto.text = "";
        }
    }
}
