using System;
using UnityEngine;

public class EventBroker
{
    public static event Action<Transform> setPossessed;
    public static Action gameOver;
    public static Action rootdied;
    public static Action goOnCoolDown;
    public static void CallSetPossessedFrog(Transform frog)
    {
        setPossessed?.Invoke(frog);
    }

    public static void CallGameOver()
    {
        gameOver?.Invoke();
    }

    public static void CallRootdied()
    {
        rootdied?.Invoke();
    }

    public static void CallGoOnCoolDown()
    {
        goOnCoolDown?.Invoke();
    }
}
