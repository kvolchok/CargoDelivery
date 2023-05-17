using System.Collections;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private const string GAME_SCENE = "game";

    [SerializeField]
    private CoroutineManager _coroutineManager;
    [SerializeField]
    private GameObject _stageFailedScreen;
    [SerializeField]
    private GameObject _stageClearScreen;
    
    [SerializeField]
    private float _changeScreenDelay = 1f;

    public void ShowStageFailedScreen()
    {
        _coroutineManager.StopAllCoroutines();
        _coroutineManager.StartCoroutine(ShowStageFailedScreenCoroutine());
    }

    public void ShowStageClearScreen()
    {
        _coroutineManager.StartCoroutine(ShowStageClearScreenCoroutine());
    }

    [UsedImplicitly]
    public void ReStartStage()
    {
        SceneManager.LoadScene(GAME_SCENE);
    }

    [UsedImplicitly]
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif

        Application.Quit();
    }

    private IEnumerator ShowStageFailedScreenCoroutine()
    {
        yield return new WaitForSeconds(_changeScreenDelay);

        _stageFailedScreen.SetActive(true);
    }

    private IEnumerator ShowStageClearScreenCoroutine()
    {
        yield return new WaitForSeconds(_changeScreenDelay);

        _stageClearScreen.SetActive(true);
    }
}