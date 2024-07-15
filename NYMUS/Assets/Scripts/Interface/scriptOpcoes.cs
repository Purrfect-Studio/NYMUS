using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptOpcoes : MonoBehaviour
{
    // Start is called before the first frame update
 
    // AINDA NAO FOI FINALIZADO
    // TODO: Fazer manter entre cenas, fazer funcionar a tecla esc para ligar




    // Update is called once per frame
    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
          
                gameObject.SetActive(true);

          

        }
        
    }
}
