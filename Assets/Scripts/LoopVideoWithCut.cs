using UnityEngine;
using UnityEngine.Video;

public class LoopVideo : MonoBehaviour
{
    public VideoPlayer videoPlayer;  // Arraste o Video Player aqui no Inspector

    void Start()
    {
        // Inicia a reprodução do vídeo
        videoPlayer.Play();

        // Adiciona um evento para reiniciar o vídeo quando ele atingir o final
        videoPlayer.loopPointReached += OnVideoEnd;
    }

    void OnVideoEnd(VideoPlayer vp)
    {
        // Reinicia o vídeo a partir do início
        vp.Play();
    }
}
