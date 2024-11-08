using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.Video;
using UnityEngine.UI;


public class PlayerMovement : MonoBehaviour
{
    public Transform player2;
    public float moveSpeed = 5f;
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Temporizador temporizador;
    
    [Header("Health Management")]
    public Vida_manager vidaManager;
    public bool isPlayer2Attacking;
    public bool usado;
    
    public BoxCollider2D attackCollider; // A hitbox de ataque do Player 1
    public AudioSource audioSource;  // Componente AudioSource
    public AudioClip musicaCena;  // A música para tocar
    public VideoPlayer videoPlayer;
    public RawImage rawImage; 
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        temporizador = gameObject.AddComponent<Temporizador>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider.enabled = false;  // Desabilita a hitbox de ataque inicialmente
        usado = false;
        rawImage.enabled = false; 
        videoPlayer.loopPointReached += OnVideoEnd;
    }
        
       

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal"); 
        animator.SetFloat("Speed", movement.sqrMagnitude);
        FaceOpponent();
        atacar();
    }

    void FaceOpponent()
    {
        if (player2.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false;
        }
        else if (player2.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true;
        }
    }
    public void SetPlayer2AttackStatus(bool status)
    {
        isPlayer2Attacking = status;
    }
    void atacar()
    {
        if (temporizador != null && !temporizador.tempoCorrendo)
        {
            if (Input.GetKeyDown(KeyCode.I))
            {
                animator.SetTrigger("kicking"); // Aciona a animação de chute
                attackCollider.enabled = true;  // Ativa a hitbox de ataque
                temporizador.Inicializa(0.5f); // Inicializa o cooldown para a hitbox

                // Desativa a hitbox após o tempo do ataque (pode ser chamado via Animation Event)
                StartCoroutine(DesativarHitboxAposAtaque());
                temporizador.Inicializa(1f); // Inicializa o cooldown do ataque
            }
            if (Input.GetKeyDown(KeyCode.U) && usado == false)
            {
                rawImage.enabled = true; 
                usado = true;
                audioSource.clip = musicaCena;  // Define o áudio da cena
                audioSource.Play();
                videoPlayer.Play();
                
                

                
                

            }
        }
    }

    IEnumerator DesativarHitboxAposAtaque()
    {
        yield return new WaitForSeconds(0.5f); // Espera o tempo do ataque
        attackCollider.enabled = false; // Desativa a hitbox de ataque
    }

    void OnTriggerEnter2D(Collider2D other)
{
    if (other.CompareTag("Player2Attack")) // Verifica se Player 2 está atacando
    {
        Debug.Log("Player 1 entrou na hitbox de ataque do Player 2");
        if (vidaManager.playerIdentifier == "Player1")
        {
            vidaManager.TakeDamage(5f); // Player 1 perde vida
        }
    }
}
  

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }
    private void OnVideoEnd(VideoPlayer vp)
    {
        rawImage.enabled = false;  // Torna a RawImage invisível
    }
}