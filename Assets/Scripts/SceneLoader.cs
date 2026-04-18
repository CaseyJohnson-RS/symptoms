using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using System.Collections;



public class SceneLoader : MonoBehaviour
{
    public UnityEvent onAwake;
    public UnityEvent onLoadQuery;
    public float loadDelay = 1f;

    void Awake()
    {
        onAwake.Invoke();
    }

    public void LoadScene(string name)
    {
        onLoadQuery.Invoke();
        StartCoroutine(LoadSceneDelayed(name));
    }

    private IEnumerator LoadSceneDelayed(string name)
    {
        yield return new WaitForSeconds(loadDelay);
        SceneManager.LoadScene(name);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
