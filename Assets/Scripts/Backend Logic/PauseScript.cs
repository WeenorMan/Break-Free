using Player;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

public class PauseScript : MonoBehaviour
{
    public PlayerInput PlayerInput;
    [SerializeField] private PlayerScript player;
    [SerializeField] private GameObject pauseMenu;
    [SerializeField] private GameObject optionsMenu;
    [SerializeField] private GameObject deathMenu;
    [SerializeField] private GameObject pauseFirst;
    [SerializeField] private GameObject deathFirst;
    [SerializeField] private GameObject optionsFirst;
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
            EventSystem.current.SetSelectedGameObject(pauseFirst);
            pauseMenu.SetActive(true);

        }
    }

    public void OnResume()
    {
        EventSystem.current.SetSelectedGameObject(null);
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

    public void TryAgain()
    {
        Time.timeScale = 1f;
        AudioManager.instance.PlayMusicClip(1);
        sceneChanger.ChangeSceneNow();

    }

    public void OpenOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenu.SetActive(false);
        optionsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(optionsFirst);
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void CloseOptionsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        pauseMenu.SetActive(true);
        optionsMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(pauseFirst);
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void OpenDeathMenu()
    {

        deathMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(deathFirst);

    }

    public void CloseDeathMenu()
    {

        deathMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);

    }
}
