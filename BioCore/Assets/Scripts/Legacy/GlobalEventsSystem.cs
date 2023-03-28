using UnityEngine.Events;

public static class GlobalEventsSystem
{
    public static readonly UnityEvent OnPlayerTakeCoin = new UnityEvent();

    public static void Collect()
    {
        OnPlayerTakeCoin.Invoke();
    }
}
