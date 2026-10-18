using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerHealth : MonoBehaviour
{
    [Header("Vidas")]
    public int lives = 3;

    [Header("Daño")]
    public float invulnerabilityTime = 1f;

    [Header("Empujón")]
    public float knockbackForce = 7f;
    public float knockbackUpForce = 4f;
    public float knockbackTime = 0.25f;

    private bool isInvulnerable = false;

    public bool IsKnockback { get; private set; }

    private Rigidbody2D rd;
    private Animator animator;

    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy") && !isInvulnerable)
        {
            RecibirDanio(collision.transform);
        }
    }

    public void RecibirDanio(Transform enemy)
    {
        if (isInvulnerable) return;

        lives--;

        if (AudioManager.Instance != null)
        {
            AudioManager.Instance.ReproducirSfx(
                AudioManager.Instance.sonidoHurt
            );
        }

      

        Debug.Log("Vidas restantes: " + lives);

        if (lives <= 0)
        {
            RestartLevel();
            return;
        }

        float direction;

        if (transform.position.x < enemy.position.x)
        {
            direction = -1f;
        }
        else
        {
            direction = 1f;
        }

        StartCoroutine(DamageRoutine(direction));
    }

    IEnumerator DamageRoutine(float direction)
    {
        isInvulnerable = true;
        IsKnockback = true;

        rd.linearVelocity = Vector2.zero;

        rd.AddForce(
            new Vector2(
                direction * knockbackForce,
                knockbackUpForce
            ),
            ForceMode2D.Impulse
        );

        yield return new WaitForSeconds(knockbackTime);

        IsKnockback = false;

        yield return new WaitForSeconds(
            invulnerabilityTime - knockbackTime
        );

        isInvulnerable = false;
    }

    void RestartLevel()
    {
        SceneManager.LoadScene(
            SceneManager.GetActiveScene().buildIndex
        );
    }
}