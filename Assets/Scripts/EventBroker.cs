using System;
using UnityEngine;

public class EventBroker
{
    public static event Action<Transform> setPossessed;
    public static void CallSetPossessedFrog(Transform frog)
    {
        setPossessed?.Invoke(frog);
    }

}
