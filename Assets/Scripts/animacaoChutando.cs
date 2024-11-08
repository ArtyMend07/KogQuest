using UnityEngine;

public class animacaoChutando : StateMachineBehaviour
{
    private SpriteRenderer spriteRenderer;

    // Esse método é chamado quando a animação começa
    override public void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        // Tenta pegar o SpriteRenderer do Animator
        spriteRenderer = animator.GetComponent<SpriteRenderer>();

        // Verifica se o SpriteRenderer foi encontrado
        if (spriteRenderer != null)
        {
            // Inverte a direção do sprite com flipX
            spriteRenderer.flipX = !spriteRenderer.flipX;  // Inverte o flipX
        }
        else
        {
            Debug.LogWarning("SpriteRenderer não encontrado no GameObject!");
        }
    }
}
