using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadingScreenManager : MonoBehaviour
{
    [SerializeField] private float _waitTimeBeforeStart = 15f;
    [SerializeField] private string _mainSceneName;

    void Start()
    {
        StartCoroutine(WaitThenStartGame());
    }

    private IEnumerator WaitThenStartGame()
    {
        yield return new WaitForSeconds(_waitTimeBeforeStart);
        StartGame();
    }

    public void StartGame()
    {
        SceneManager.LoadSceneAsync(_mainSceneName);
    }
}
