using PlasticPipe.PlasticProtocol.Messages;
using System;
using UnityEditor;
using UnityEditor.EditorTools;
using UnityEngine;

[EditorTool("Custom Snap Tool", typeof(SnapCustom))]
public class CustomSnapTool : EditorTool
{
    public Texture2D ToolIcon;
    public override GUIContent toolbarIcon
    {
        get
        {
            return new GUIContent
            {
                image = ToolIcon,
                text = "Custom Snap Move Tool",
                tooltip = "Custom Snap Move Tool by martgoty "
            };
        }
    }

    public override void OnToolGUI(EditorWindow window)
    {
        Transform targetTransform = ((SnapCustom)target).transform;

        EditorGUI.BeginChangeCheck();
        Vector3 newPosition = Handles.PositionHandle(targetTransform.position, Quaternion.identity);

        if (EditorGUI.EndChangeCheck())
        {
            targetTransform.position = newPosition;
        }
    }
}
