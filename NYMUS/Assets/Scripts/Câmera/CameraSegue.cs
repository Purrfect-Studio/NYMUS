using UnityEngine;

public class CameraSegue : MonoBehaviour
{
    [Header("Jogador")]
    public Transform jogador; // Posi��o do jogador

    [Header("Posi��o M�nima do X")]
    public float minX; // Movimenta��o m�xima para a esquerda
    [Header("Posi��o M�xima do X")]
    public float maxX; // Movimenta��o m�xima para a direita
    [Header("Delay para alcan�ar o Jogador")]
    public float delayTempo; // Delay da c�mera chegar no jogador

    private Vector3 cameraDestination; // Posi��o de destino da c�mera quando n�o estiver seguindo o jogador
    private bool isFollowingPlayer = true; // Flag para indicar se a c�mera est� seguindo o jogador

    private void FixedUpdate()
    {
        if (isFollowingPlayer)
        {
            Vector3 novaPosicao = jogador.position + new Vector3(0, 0, -10); // Afasta a c�mera do cen�rio
            novaPosicao = Vector3.Lerp(transform.position, novaPosicao, delayTempo); // Delay para a c�mera chegar no jogador
            transform.position = new Vector3(Mathf.Clamp(novaPosicao.x, minX, maxX), novaPosicao.y, novaPosicao.z);
            // Limita o posicionamento horizontal da c�mera
        }
        else
        {
            // Se n�o estiver seguindo o jogador, move para a posi��o de destino
            transform.position = Vector3.Lerp(transform.position, cameraDestination, delayTempo * Time.deltaTime);
        }
    }

    // M�todo para fazer a c�mera parar de seguir o jogador e ir para uma posi��o espec�fica
    public void MoverCamera(Vector3 destino)
    {   
        isFollowingPlayer = false;
        cameraDestination = destino + new Vector3(0, 0, -10); // Afasta a c�mera do cen�rio
    }

    // M�todo para fazer a c�mera voltar a seguir o jogador
    public void VoltarASeguirPlayer()
    {
        isFollowingPlayer = true;
    }
}