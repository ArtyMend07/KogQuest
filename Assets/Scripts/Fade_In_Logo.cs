using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LogoFadeIn : MonoBehaviour
{
    public CanvasGroup logoCanvasGroup; // Arraste seu CanvasGroup aqui no Inspector
    public float fadeDuration = 1f; // Duração do fade in
    private float waitTime = 2f; // Tempo de espera antes do fade in

    void Start()
    {
        logoCanvasGroup.alpha = 0; // Inicializa a opacidade como 0
        logoCanvasGroup.interactable = false; // Desativa interatividade se necessário
        logoCanvasGroup.blocksRaycasts = false; // Desativa o bloqueio de raios

        StartCoroutine(ShowLogo());
    }

    private IEnumerator ShowLogo()
    {
        // Espera 2 segundos
        yield return new WaitForSeconds(waitTime);

        // Inicia o fade in
        float startTime = Time.time;
        while (Time.time < startTime + fadeDuration)
        {
            logoCanvasGroup.alpha = Mathf.Lerp(0, 1, (Time.time - startTime) / fadeDuration);
            yield return null; // Espera até o próximo frame
        }

        // Certifique-se de que a opacidade final é 1
        logoCanvasGroup.alpha = 1;
        logoCanvasGroup.interactable = true; // Ativa a interatividade se necessário
        logoCanvasGroup.blocksRaycasts = true; // Ativa o bloqueio de raios
    }
}
