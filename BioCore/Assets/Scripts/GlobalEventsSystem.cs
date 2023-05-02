using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventsSystem
{
    public static TakeDamageEvent OnPlayerTakeDamage = new TakeDamageEvent();
    public static void PlayerTakeDamage(Vector2 outDirection)
    {
        OnPlayerTakeDamage.Invoke(outDirection);
    }

}

public class TakeDamageEvent: UnityEvent<Vector2>
{

}
