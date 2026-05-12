using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneChanger : MonoBehaviour
{
    public string sceneToLoad;

    public void ChangeSceneNow()
    {
        Time.timeScale = 1.0f;
        SceneManager.LoadScene(sceneToLoad);
    }

    public void ExitGame()
    {
        Application.Quit();
    }

    public void PlaySFXClip (int clipNumber)
    {
        AudioManager.instance.PlaySFXClip(clipNumber);
    }
}
