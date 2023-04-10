using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapCustomPoint : MonoBehaviour
{
    public enum ConnectionType
    {
        Ground,
        Wall,
        Roof
    }

    public ConnectionType Type;


    private void OnDrawGizmos()
    {
        switch (Type)
        {
            case ConnectionType.Ground:
                Gizmos.color = Color.yellow;
                break;
            case ConnectionType.Wall:
                Gizmos.color = Color.blue;
                break;
            case ConnectionType.Roof:
                Gizmos.color = Color.green;
                break;
        }
        Gizmos.DrawWireSphere(transform.position, 0.1f);
    }
}
