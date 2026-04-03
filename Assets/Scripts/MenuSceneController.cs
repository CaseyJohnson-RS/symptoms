using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSceneController : MonoBehaviour
{
    [SerializeField] private string mainGameSceneName;
    [SerializeField] private AudioData music;

    private void Start()
    {
        AudioManager.Instance.PlayAudio(music);
    }

    public void StartGame()
    {
        SceneManager.LoadScene(mainGameSceneName);
    }

    public void Quit()
    {
        Application.Quit();
    }
}