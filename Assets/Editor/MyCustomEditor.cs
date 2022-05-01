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

        //ai hearing
        Handles.color = Color.yellow;
        Handles.DrawWireArc(info.ai.transform.position, Vector3.up, Vector3.forward, 360, info.hearingRadius);
        Handles.color = Color.red;
        Handles.DrawWireArc(info.ai.transform.position, Vector3.up, Vector3.forward, 360, info.instantHeardRadius);

        //ai sight
        Handles.color = Color.cyan;
        Handles.DrawWireArc(info.ai.transform.position, info.ai.transform.up, info.ai.transform.forward, 360, info.sightDistance);
        Vector3 AIViewAngleA = info.DirFromAngleAI(-info.sightRadius / 2, false);
        Vector3 AIViewAngleB = info.DirFromAngleAI(info.sightRadius / 2, false);
        Handles.DrawLine(info.ai.transform.position, info.ai.transform.position + AIViewAngleA * info.sightDistance);
        Handles.DrawLine(info.ai.transform.position, info.ai.transform.position + AIViewAngleB * info.sightDistance);

        //player sight
        Vector3 playerViewAngleA = info.DirFromAnglePlayer(-info.playerSightAngle / 2, false);
        Vector3 playerViewAngleB = info.DirFromAnglePlayer(info.playerSightAngle / 2, false);
        Handles.DrawLine(info.player.transform.position, info.player.transform.position + playerViewAngleA * info.playerSightLength);
        Handles.DrawLine(info.player.transform.position, info.player.transform.position + playerViewAngleB * info.playerSightLength);

        //distance from player to enemy close
        Handles.color = Color.red;
        Handles.DrawWireArc(info.player.transform.position, Vector3.up, Vector3.forward, 360, info.enemyDistance);

        //distance from player to enemy far
        Handles.color = Color.green;
        Handles.DrawWireArc(info.player.transform.position, Vector3.up, Vector3.forward, 360, info.calmDistance);



    }

    public override void OnInspectorGUI() 
    {
        GUIStyle WanderTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        WanderTitle.normal.textColor = Color.white;

        EditorGUI.LabelField((new Rect(15, 25, 400, 10)), "Wander Settings", WanderTitle);

        GUIStyle GettersTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        GettersTitle.normal.textColor = Color.green;

        EditorGUI.LabelField((new Rect(15, 225, 400, 10)), "Character Getters", GettersTitle);

        GUIStyle DirectorTitle = new GUIStyle
        {
            fontSize = 18,
            fontStyle = FontStyle.Bold
        };
        DirectorTitle.normal.textColor = Color.cyan;

        EditorGUI.LabelField((new Rect(15, 285, 400, 10)), "Director Settings", DirectorTitle);

        base.OnInspectorGUI(); 

        
    }
}
