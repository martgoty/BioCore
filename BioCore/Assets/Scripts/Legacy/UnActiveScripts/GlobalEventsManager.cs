using UnityEngine.Events;

public class GlobalEventsManager 
{
    public static readonly UnityEvent OnPlayerGetDamage = new UnityEvent();

    public static void SendPlayerDamage()
    {
        OnPlayerGetDamage.Invoke();
    }
}
