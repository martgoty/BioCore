using UnityEngine.Events;

public static class GlobalEventsSystem
{
    public static readonly UnityEvent OnPlayerTakeDamage = new UnityEvent();
    public static readonly UnityEvent OnPlayerTakeHealth = new UnityEvent();
    public static readonly UnityEvent ScoreUp = new UnityEvent();

    public static void Score()
    {
        ScoreUp.Invoke();
    }
    public static void TakeDamage()
    {
        OnPlayerTakeDamage.Invoke();
    }

    public static void Health()
    {
        OnPlayerTakeHealth.Invoke();
    }
}
