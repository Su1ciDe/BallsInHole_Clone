using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : SingletonBehaviour<UIManager>
{
#pragma warning disable 0649
    [Header("Score")]
    [SerializeField] private Text txtScore;
    [SerializeField] private string txtScore_prefix = "Score: ";

    [Header("Level Completed")]
    [SerializeField] private GameObject levelCompleted_Panel;

    [Header("Level Failed")]
    [SerializeField] private GameObject levelFailed_Panel;
#pragma warning restore 0649

    public void UpdateScore(int score)
    {
        txtScore.text = txtScore_prefix + score;
    }

    public void Show_LevelCompletedPanel()
    {
        levelCompleted_Panel.SetActive(true);
    }

    public void Hide_LevelCompletedPanel()
    {
        levelCompleted_Panel.SetActive(false);
    }

    public void Show_LevelFailedPanel()
    {
        levelFailed_Panel.SetActive(true);
    }

    public void Hide_LevelFailedPanel()
    {
        levelFailed_Panel.SetActive(false);
    }

    public void RestartLevel()
    {
        GameManager.Instance.LevelReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetryLevel()
    {
        GameManager.Instance.LevelReset();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

}
