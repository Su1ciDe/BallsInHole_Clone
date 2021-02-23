using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    [HideInInspector] public bool CanPlay = true;

    [HideInInspector] public int Score;
    [HideInInspector] public int Moves;

    [HideInInspector] public int LevelIndex = 0;

    [HideInInspector] public int BallCountInLevel;
    [HideInInspector] public int BallCollected;

    public static List<Portal> Portals = new List<Portal>();

    protected override void Awake()
    {
        base.Awake();
    }

    void Start()
    {
        LevelGenerator.Instance.GenerateLevel(LevelIndex);
    }

    public void AddScore(int value)
    {
        Score += value;
        UIManager.Instance.UpdateScore(Score);
    }

    public void CheckIfLevelCompleted()
    {
        if (BallCountInLevel == BallCollected)
        {
            StartCoroutine(LevelCompleted());
        }
    }

    public void CheckIfLevelFailed()
    {
        if (BallCountInLevel <= 0)
        {
            StartCoroutine(LevelFailed());
        }
    }

    public IEnumerator LevelFailed()
    {
        CanPlay = false;

        yield return new WaitForSeconds(1);

        UIManager.Instance.Show_LevelFailedPanel();
    }

    public IEnumerator LevelCompleted()
    {
        CanPlay = false;

        yield return new WaitForSeconds(1);

        UIManager.Instance.Show_LevelCompletedPanel();
    }


    public void LevelReset()
    {
        ControllableObjects.controllableObjects.Clear();
        Portals.Clear();
        BallCollected = 0;
        BallCountInLevel = 0;
    }
}