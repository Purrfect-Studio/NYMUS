using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarFase : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public string nomeDaFase;
    [SerializeField] public int delay;

       public void esperarCarregar()
    {
        // Chama a função para carregar o próximo nível após 3 segundos
        Invoke("CarregarNivel", 3f);
    }

        public void CarregarNivel()
    {
        SceneManager.LoadScene(nomeDaFase);
    }

}
