using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public string sceneToReturn = "Fase_luta";  // O nome da cena da luta

    void Start()
    {
        // Inicia o vídeo assim que a cena for carregada
        videoPlayer.Play();
        videoPlayer.loopPointReached += OnVideoEnd; // Define o que acontecerá quando o vídeo terminar
    }

    // Método que é chamado quando o vídeo termina
    private void OnVideoEnd(VideoPlayer vp)
    {
        // Carrega a cena da luta novamente
        SceneManager.LoadScene(sceneToReturn);
    }
}
