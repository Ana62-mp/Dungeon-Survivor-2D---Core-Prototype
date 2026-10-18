using UnityEngine;

public class Coin : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            CoinUI.Instance.AddCoin();
            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ReproducirSfx(
                    AudioManager.Instance.sonidoMoneda
                );
            }

            Destroy(gameObject);
        }
    }
}