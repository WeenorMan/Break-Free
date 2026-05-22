using Player;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class TextScript : MonoBehaviour
{
    PlayerScript playerScript;
    PauseScript pauseScript;

    void Start()
    {
        GameObject obj = GameObject.FindGameObjectWithTag("PauseManager");

        pauseScript = obj.GetComponent<PauseScript>();
    }

    void Update()
    {
        if (pauseScript.pauseMenu.activeSelf || pauseScript.optionsMenu.activeSelf || pauseScript.controlsMenu.activeSelf || pauseScript.deathMenu.activeSelf)
        {
            gameObject.GetComponent<Image>().enabled = false;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<Image>().enabled = true;
            gameObject.GetComponentInChildren<TextMeshProUGUI>().enabled = true;
        }
    }
}
