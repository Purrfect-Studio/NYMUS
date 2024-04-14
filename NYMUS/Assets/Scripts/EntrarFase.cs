using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarFase : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string nomeDaFase;
 
       public void esperarCarregar(float delay)
    {
        // Chama a função para carregar o próximo nível após 3 segundos
        Invoke("CarregarNivel", delay);
    }

        public void CarregarNivel()
    {
        SceneManager.LoadScene(nomeDaFase);
    }

}
