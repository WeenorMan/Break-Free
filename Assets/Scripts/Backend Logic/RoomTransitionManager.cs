using Player;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomTransitionManager : MonoBehaviour
{
    [SerializeField] private ScreenFader screenFader;
    [SerializeField] private CameraManager cameraManager;
    private string currentRoom = "";
    private bool isTransitioning;

    void Start()
    {
        EnterRoom("", "");
    }

    public void EnterRoom(string sceneName, string spawnID)
    {
        if (isTransitioning)
        {
            return;
        }

        StartCoroutine(Transition(sceneName, spawnID));
    }

    private IEnumerator Transition(string sceneName, string spawnID)
    {
        isTransitioning = true;

        PlayerScript player = ServiceLocator.Get<PlayerScript>();
        player.isControlLocked = true;

        if (!string.IsNullOrEmpty(spawnID))
        {
            yield return screenFader.Fade(0f, 1f, 1f);

        }

        if (!string.IsNullOrEmpty(currentRoom))
        {
            yield return SceneManager.UnloadSceneAsync(currentRoom);
            yield return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Additive);

        } 

        Scene newScene = SceneManager.GetSceneByName(sceneName);
        
        if (newScene.IsValid())
        {
            SceneManager.SetActiveScene(newScene);
        }
        currentRoom = SceneManager.GetActiveScene().name;

        yield return null;
        RoomService service = ServiceLocator.Get<RoomService>();
        SetupRoom(service, spawnID);
        SetupCameraConfiner(service);
        //ResetParallax(service);

        isTransitioning = false;
        player.isControlLocked = false;

        yield return new WaitForSeconds(0.3f);
        if (!string.IsNullOrEmpty(spawnID))
            yield return screenFader.Fade(1f, 0f, 0.5f);
    }

    private void SetupRoom(RoomService service,string spawnID)
    {

        if (string.IsNullOrEmpty(spawnID))
        {
            return;
        }
        SpawnPoint spawnToUse = service.GetSpawn(spawnID);

        if(spawnToUse != null)
        {
            transform.position = spawnToUse.transform.position;
        }
    }

    private void SetupCameraConfiner(RoomService service)
    {
        if(service.provider != null)
        {
            cameraManager.SetConfiner(service.provider.confiner);

        }
    }

    /*private void ResetParallax()
    {
        ParallaxManager parallax = FindFirstObjectByType<ParallaxManager>();
        if(parallax != null)
        {
            parallax.Initialise(cameraManager.camTransform);
        }
    }*/
}
