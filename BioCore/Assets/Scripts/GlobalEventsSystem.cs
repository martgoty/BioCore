using UnityEngine;
using UnityEngine.Events;

public static class GlobalEventsSystem
{
    public static TakeDamageEvent OnPlayerTakeDamage = new TakeDamageEvent();
    public static UnityEvent OnOpenMenu = new UnityEvent();
    public static UnityEvent OnTakeDamage = new UnityEvent();
    public static UnityEvent OnHealth = new UnityEvent();
    public static UnityEvent OnTakeCoin = new UnityEvent();

    public static void PlayerTakeDamage(Vector2 outDirection)
    {
        OnPlayerTakeDamage.Invoke(outDirection);
    }

    public static void TakeCoin(){
        OnTakeCoin.Invoke();
    }
    public static void HeathUp()
    {
        OnHealth.Invoke();
    }
    public static void TakeDamage()
    {
        OnTakeDamage.Invoke();
    }
    public static void OpenMenu()
    {
        OnOpenMenu.Invoke();
    }
}



public class TakeDamageEvent : UnityEvent<Vector2>
{

}
