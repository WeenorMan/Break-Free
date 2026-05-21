using Player;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DoorToEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        PlayerScript player = other.GetComponent<PlayerScript>();

        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene("EndScene");
        }
    }
}
