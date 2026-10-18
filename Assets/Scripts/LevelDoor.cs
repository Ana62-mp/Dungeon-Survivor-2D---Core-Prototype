using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelDoor : MonoBehaviour
{
    public string nombreSiguienteEscena = "Nivel2";

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player") || other.gameObject.name == "Player"){

            if (AudioManager.Instance != null)
            {
                AudioManager.Instance.ReproducirSfx(
                    AudioManager.Instance.sonidoLevelDoor
                );
            }

            SceneManager.LoadScene(nombreSiguienteEscena);
        }
    }
}