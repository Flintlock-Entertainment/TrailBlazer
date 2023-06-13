using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    [SerializeField] GameObject loadingScreen;
    string sceneName;

    private void Awake()
    {
        Instance = this;
    }

    public void ActivateLoadingScreen()
    {
        loadingScreen.SetActive(true);
    }
    public void LoadScene(string sceneName)
    {
        this.sceneName = sceneName;
        loadingScreen.SetActive(true);
        Invoke("loadScene", 2f);

    }

    private void loadScene()
    {
        SceneManager.LoadScene(sceneName);
    }
}
