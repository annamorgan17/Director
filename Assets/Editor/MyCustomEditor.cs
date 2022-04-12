using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor (typeof (AIManager))] 
public class MyCustomEditor : Editor
{
    private void OnSceneGUI()
    {
        AIManager info = (AIManager)target; 

        //wander
        Handles.color = Color.white;
        Handles.DrawWireArc(Vector3.zero, Vector3.up, Vector3.forward, 360, info.passiveWanderDistance);
    }

    public override void OnInspectorGUI() 
    {
        GUIStyle WanderTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        WanderTitle.normal.textColor = Color.white;

        EditorGUI.LabelField((new Rect(15, 20, 400, 10)), "Wander Settings", WanderTitle);

        base.OnInspectorGUI(); 

        
    }
}
