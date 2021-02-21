using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonBehaviour<GameManager>
{
    public bool CanPlay = true;

    public int BallCountInLevel;
    public int BallCollected;

    public static List<Portal> Portals = new List<Portal>();

    protected override void Awake()
    {
        base.Awake();

    }

    void Start()
    {

    }

    public void CheckIfLevelCompleted()
    {
        if (BallCountInLevel <= 0 && BallCollected != 0)
        {
            LevelCompleted();
        }
    }

    public void CheckIfLevelFailed()
    {
        if (BallCountInLevel <= 0)
        {
            LevelFailed();
        }
    }

    public IEnumerator LevelFailed()
    {
        yield return new WaitForSeconds(1);


    }

    public IEnumerator LevelCompleted()
    {
        yield return new WaitForSeconds(1);


    }
}