using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntrarFase : MonoBehaviour
{

    public string fase;
    public Collision collision;
     
    // Start is called before the first frame update
    void Start()
    {

       if (true)
        {

        }
        

        //if (interagiu com a pasta){
        //SceneManager.LoadScene("fase");
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

   private void OnCollisionEnter (Collision colisao)
    {
        if (colisao.gameObject.tag = "tag")
        {
            SceneManager.LoadScene(fase);
        }
    }
}
