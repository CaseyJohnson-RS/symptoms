using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField] private string mainGameSceneName;

    public void StartGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}