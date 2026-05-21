using UnityEngine;

public class EndSceneManager : MonoBehaviour
{

    private void Start()
    {
        AudioManager.instance.PlayMusicClip(0);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
