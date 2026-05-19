using UnityEngine;
using UnityEngine.EventSystems;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;

    [SerializeField] private GameObject menuFirst;
    [SerializeField] private GameObject settingsFirst;

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(menuFirst);
    }

    public void OpenSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        mainMenu.SetActive(false);
        settingsMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(settingsFirst);
        Debug.Log(EventSystem.current.currentSelectedGameObject.name);
    }

    public void CloseSettingsMenu()
    {
        EventSystem.current.SetSelectedGameObject(null);
        settingsMenu.SetActive(false);
        mainMenu.SetActive(true);
        EventSystem.current.SetSelectedGameObject(menuFirst);

    }


}
