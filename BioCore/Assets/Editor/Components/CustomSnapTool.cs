using PlasticPipe.PlasticProtocol.Messages;
using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEditor.SceneManagement;
using UnityEditor.SearchService;
using UnityEngine;

[EditorTool("Custom Snap Tool", typeof(SnapCustom))]
public class CustomSnapTool : EditorTool
{
    public Texture2D ToolIcon;

    private Transform oldTarget;
    SnapCustomPoint[] allPoints;
    SnapCustomPoint[] targetPoints;
    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = ToolIcon,
                text = "Custom Snap Move Tool",
                tooltip = "Custom Snap Move Tool by \r\nEmerald Powder "
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {

        Transform targetTransform = ((SnapCustom)target).transform;

        if(targetTransform != oldTarget)
        {
            PrefabStage prefabStage = PrefabStageUtility.GetPrefabStage(targetTransform.gameObject);

            if(prefabStage != null)
            {
                allPoints = prefabStage.prefabContentsRoot.GetComponentsInChildren<SnapCustomPoint>();
            }
            else
            {
                allPoints = FindObjectsOfType<SnapCustomPoint>();
            }
            targetPoints = targetTransform.GetComponentsInChildren<SnapCustomPoint>();

            oldTarget = targetTransform;
        }

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            Undo.RecordObject(targetTransform, "Move with snap tool");
            MoveWithSnapping(targetTransform, newPosition);
        }
    }

    private void MoveWithSnapping(Transform targetTransform, Vector3 newPosition)
    {


        Vector3 bestPosition = newPosition;
        float closeDistance = float.PositiveInfinity;

        foreach (SnapCustomPoint point in allPoints)
        {
            if (point.transform.parent == targetTransform)
                continue;

            foreach (SnapCustomPoint ownPoint in targetPoints)
            {
                if (ownPoint.Type != point.Type)
                    continue;

                Vector3 targetPos = point.transform.position - (ownPoint.transform.position - targetTransform.position);
                float distance = Vector3.Distance(targetPos, newPosition);

                if (distance < closeDistance)
                {
                    closeDistance = distance;
                    bestPosition = targetPos;
                }
            }
        }

        if (closeDistance < 0.5f)
        {
            targetTransform.position = bestPosition;
        }
        else
        {
            targetTransform.position = newPosition;
        }

    }
}
