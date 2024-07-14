using System.Collections;
using UnityEngine;

public class CutsceneControlador : MonoBehaviour
{
    public CameraSegue cameraSegue;
    public EfeitosCamera efeitosCamera;

    public GameObject pontoInicial; // Ponto onde a câmera começa a cutscene
    public GameObject pontoFinal; // Ponto onde a câmera termina a cutscene
    public float delayZoom = 3f; // Duração do zoom

    private void Start()
    {
       
    }
    public void comecar()
    {
        StartCoroutine(RodarCutscene());
    }
    public IEnumerator RodarCutscene()
    {
        // Mover a câmera para o ponto inicial
        cameraSegue.MoverCamera(pontoInicial);
        yield return new WaitForSeconds(1f); // Espera para a câmera se posicionar

        // Fazer o zoom in
        efeitosCamera.DarZoom();
        yield return new WaitForSeconds(delayZoom); // Espera o tempo do zoom in

        // Mover a câmera para o ponto final
        cameraSegue.MoverCamera(pontoFinal);
        efeitosCamera.DarZoomOut();
        yield return new WaitForSeconds(delayZoom); // Espera o tempo do zoom out

        // Voltar a seguir o jogador
        //cameraSegue.VoltarASeguirJogador();
    }
}
