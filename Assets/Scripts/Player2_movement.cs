using UnityEngine;
using System.Collections;


public class Player2Movement : MonoBehaviour
{
    public Transform player1; // Referência para o Player 1
    public float moveSpeed = 5f; // Velocidade do personagem
    private Rigidbody2D rb;
    private Vector2 movement;
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    
    public AudioSource audioSource;  // Componente de AudioSource para tocar o som
    public AudioClip hitSound;  // O som a ser tocado quando o Player 2 for atingido

    [Header("Health Management")]
    public Vida_manager vidaManager; // Referência para o script de gerenciamento de vida

    // Para saber se o Player 1 está atacando
    public bool isPlayer1Attacking = false;

    [Header("Attack Management")]
    public BoxCollider2D attackCollider; // A hitbox de ataque do Player 2

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        attackCollider.enabled = false; // Desabilita a hitbox de ataque inicialmente
        spriteRenderer.flipX = false; // Inicialmente, o sprite não está invertido
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal2"); // Controla o movimento com as setas ou outras teclas
        animator.SetFloat("Speed", movement.sqrMagnitude);
        
        FaceOpponent();
        atacar();
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    // Função chamada para ativar o ataque do Player 1
    public void SetPlayer1AttackStatus(bool status)
    {
        isPlayer1Attacking = status;
    }

    // Detecção de colisão com a hitbox de ataque do Player 1
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player1Attack")) // Verifica se o Player 1 está atacando
        {
            Debug.Log("Player 2 entrou na hitbox de ataque do Player 1");
            // Player 2 perde vida
            vidaManager.TakeDamage(10f);
            audioSource.PlayOneShot(hitSound); // Reproduz o som de dano
        }
    }

    // Função para fazer o Player 2 olhar na direção do Player 1
    void FaceOpponent()
    {
        if (player1.position.x > transform.position.x)
        {
            spriteRenderer.flipX = false; // Player 2 olhando para a direita
        }
        else if (player1.position.x < transform.position.x)
        {
            spriteRenderer.flipX = true; // Player 2 olhando para a esquerda
        }
    }

    // Função para lidar com os ataques do Player 2
    void atacar()
    {
        if (Input.GetKeyDown(KeyCode.K)) // Tecla K para o ataque de Player 2
        {
            animator.SetTrigger("Gancho"); // Animação para o ataque "Gancho"
            attackCollider.enabled = true;  // Ativa a hitbox de ataque
            StartCoroutine(DesativarHitboxAposAtaque()); // Desativa a hitbox após o ataque
        }

        if (Input.GetKeyDown(KeyCode.O)) // Tecla O para outro ataque do Player 2
        {
            animator.SetTrigger("Amostradinho_combo"); // Animação para o combo
            attackCollider.enabled = true;  // Ativa a hitbox de ataque
            StartCoroutine(DesativarHitboxAposAtaque()); // Desativa a hitbox após o ataque
        }

        if (Input.GetKeyDown(KeyCode.P)) // Tecla P para o ataque counter
        {
            animator.SetTrigger("Amostradinho_counter"); // Animação para o counter
            attackCollider.enabled = true;  // Ativa a hitbox de ataque
            StartCoroutine(DesativarHitboxAposAtaque()); // Desativa a hitbox após o ataque
        }
    }

    // Função para desativar a hitbox após o tempo do ataque
    IEnumerator DesativarHitboxAposAtaque()
    {
        yield return new WaitForSeconds(0.5f); // Espera o tempo do ataque
        attackCollider.enabled = false; // Desativa a hitbox de ataque
    }
}
