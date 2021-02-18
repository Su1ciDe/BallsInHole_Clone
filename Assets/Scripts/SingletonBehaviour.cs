using System.Collections.Generic;
using UnityEngine;

public abstract class SingletonBehaviour : MonoBehaviour
{
    protected static List<SingletonBehaviour> singletons = new List<SingletonBehaviour>(8);

    protected virtual void OnLevelInitialized() { }
    protected virtual void OnLevelStarted() { }
    protected virtual void OnLevelFinished(bool success) { }
    protected virtual void OnLevelClosed() { }

    public static void TriggerLevelInitialized()
    {
        for (int i = 0; i < singletons.Count; i++)
            singletons[i].OnLevelInitialized();
    }

    public static void TriggerLevelStarted()
    {
        for (int i = 0; i < singletons.Count; i++)
            singletons[i].OnLevelStarted();
    }

    public static void TriggerLevelFinished(bool success)
    {
        for (int i = 0; i < singletons.Count; i++)
            singletons[i].OnLevelFinished(success);
    }

    public static void TriggerLevelClosed()
    {
        for (int i = 0; i < singletons.Count; i++)
            singletons[i].OnLevelClosed();
    }
}

public abstract class SingletonBehaviour<T> : SingletonBehaviour where T : SingletonBehaviour<T>
{
    public static T Instance { get; private set; }

    protected virtual void Awake()
    {
        if (Instance == null)
        {
            Instance = (T)this;
            singletons.Add(this);
        }
        else if (this != Instance)
            Destroy(this);
    }

    protected virtual void OnDestroy()
    {
        singletons.Remove(this);
    }
}