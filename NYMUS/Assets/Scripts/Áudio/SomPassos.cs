using UnityEngine;

public class SomPassos : MonoBehaviour
{
    [SerializeField] private GameObject audioPrefab; // Prefab com o AudioSource
    [SerializeField] private AudioClip[] passosGramaMP3;

    // Método chamado para tocar um som de passo
    public void Passos()
    {
        int indiceAleatorio = Random.Range(0, passosGramaMP3.Length);
        AudioClip clipSelecionado = passosGramaMP3[indiceAleatorio];

        // Criar o objeto temporário para reproduzir o som
        GameObject audioObj = Instantiate(audioPrefab, transform.position, Quaternion.identity);
        AudioSource audioSource = audioObj.GetComponent<AudioSource>();

        // Configura o AudioSource com o clip selecionado
        audioSource.clip = clipSelecionado;
        audioSource.Play();

        // Destroi o objeto após a reprodução do som
        Destroy(audioObj, clipSelecionado.length);
    }
}
