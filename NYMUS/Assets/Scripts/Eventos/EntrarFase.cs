using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EntrarFase : MonoBehaviour
{
    [Header("Nome da Fase")]
    [SerializeField] public string nomeDaFase;
 
       public void esperarCarregar(float delay)
       {
            // Chama a fun��o para carregar o pr�ximo n�vel ap�s 3 segundos
            Invoke("CarregarNivel", delay);
       }

        public void CarregarNivel()
        {
            SceneManager.LoadScene(nomeDaFase);
        }

}
