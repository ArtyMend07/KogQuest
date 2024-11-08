using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;


public class ButtonNavigator : MonoBehaviour
{
    public Image nome_imagem_e;
    public Image nome_imagem_d;
    public Sprite[] nomes;  
    public Button[] buttons;  
    public Image characterImage;  
    public Image imagemdireita;   
    public Sprite[] characterImages; 
    public Sprite[] happyCharacterImages; 

    public AudioSource audioSource;  
    public AudioClip somSelecaoNeymar;

    public AudioClip somAmostradinho;
    public AudioClip selecionar;
    private int selectedIndex = 0;
    private bool isFirstSelection = false;  
    private bool isSecondSelectionMade = false;  
    private Temporizador temporizador;
    public Image fadeImage;  // A imagem que fará o fade
    public float fadeDuration = 2f;  // Duração do fade out

    void Start()
    {
        imagemdireita.gameObject.SetActive(false);
        nome_imagem_d.gameObject.SetActive(false);
        UpdateButtonSelection();
    }

    void Update()
    {
        if (!isSecondSelectionMade)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                selectedIndex = (selectedIndex - 1 + buttons.Length) % buttons.Length;
                UpdateButtonSelection();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                selectedIndex = (selectedIndex + 1) % buttons.Length;
                UpdateButtonSelection();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            SelectCharacter();
        }
        
        if (temporizador != null && temporizador.tempoCorrendo == false && isSecondSelectionMade)
        {
            StartCoroutine(FadeOutAndChangeScene());
        }
    }

    void UpdateButtonSelection()
    {   audioSource.PlayOneShot(selecionar);
        for (int i = 0; i < buttons.Length; i++)
        {
            var colors = buttons[i].colors;
            colors.normalColor = i == selectedIndex ? Color.yellow : Color.white;
            buttons[i].colors = colors;
        }

        if (!isFirstSelection)
        {
            if (selectedIndex == 0)
            {
                characterImage.sprite = characterImages[0];
                nome_imagem_e.sprite = nomes[0];
            }
            else if (selectedIndex == 1)
            {
                characterImage.sprite = characterImages[1];
                nome_imagem_e.sprite = nomes[1];
            }
        }

        if (isFirstSelection)
        {
            imagemdireita.gameObject.SetActive(true);
            nome_imagem_d.gameObject.SetActive(true);
            if (selectedIndex == 0)
            {
                imagemdireita.sprite = characterImages[0];
                nome_imagem_d.sprite = nomes[0];
            }
            else if (selectedIndex == 1)
            {
                imagemdireita.sprite = characterImages[1];
                nome_imagem_d.sprite = nomes[1];
            }
        }
    }

    void SelectCharacter()
    {
        if (!isFirstSelection)
        {
            if (selectedIndex == 0)
            {
                characterImage.sprite = happyCharacterImages[0];
                audioSource.PlayOneShot(somSelecaoNeymar);
            }
            else if (selectedIndex == 1)
            {
                characterImage.sprite = happyCharacterImages[1];
                audioSource.PlayOneShot(somAmostradinho);
            }

            isFirstSelection = true;
            imagemdireita.gameObject.SetActive(true);
            nome_imagem_d.gameObject.SetActive(true);
        }
        else if (!isSecondSelectionMade)
        {
            if (selectedIndex == 0)
            {
                imagemdireita.sprite = happyCharacterImages[0];
                audioSource.PlayOneShot(somSelecaoNeymar);
            }
            else if (selectedIndex == 1)
            {
                imagemdireita.sprite = happyCharacterImages[1];
                audioSource.PlayOneShot(somAmostradinho);
            }

            isSecondSelectionMade = true;
            temporizador = gameObject.AddComponent<Temporizador>();
            temporizador.Inicializa(3f);
        }

        Debug.Log("Personagem Selecionado: " + buttons[selectedIndex].name);
    }

    IEnumerator FadeOutAndChangeScene()
    {
        float elapsedTime = 0f;
        fadeImage.gameObject.SetActive(true);  // Torna a imagem de fade visível
        Color startColor = fadeImage.color;
        startColor.a = 0f;  // Começa transparente
        fadeImage.color = startColor;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            startColor.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Aumenta o alpha para 1
            fadeImage.color = startColor;
            yield return null;
        }

        SceneManager.LoadScene("Fase_luta");  // Troca a cena após o fade
    }
}
