using Player;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PauseScript : MonoBehaviour
{
    public PlayerInput PlayerInput;
    [SerializeField] private PlayerScript player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private SceneChanger sceneChanger; 
    private bool pausePressed;


    private void Awake()
    {
        
    }


    private void Update()
    {
        HandlePause();
    }

    public void OnPause(InputValue value)
    {
        if (value.isPressed)
        {
            pausePressed = true;
        }
    }

    public void HandlePause()
    {
        if (pausePressed)
        {
            pausePressed = false;

            Debug.Log("Pause");
            Time.timeScale = 0f;

            player.isControlLocked = true;
            pauseMenu.SetActive(true);
        }
    }

    public void OnResume()
    {
        pauseMenu.SetActive(false);
        player.isControlLocked = false;
        Time.timeScale = 1f;
    }

    public void OnQuit()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusicClip(0);
        sceneChanger.ChangeSceneNow();
    }
}
