using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    private static AIManager instance; //fixed by setting in start and chaning the script execution order, to run ai manager before default

    [Space(20)]
    public float passiveWanderDistance;
    public float huntWanderDistance;
    public float stoppingDistance;
    public float hearingRadius;
    public float instantHeardRadius;
    public float sightDistance;
    public float sightRadius;
    public float walkSpeed;
    public float runSpeed;
    public GameObject[] caves;
    public GameObject setPoint; //test
    [Space(20)]
    public GameObject player;
    public GameObject ai;
    [Space(20)]
    public float playerHealth = 0;
    public float playerSightLength;
    public float playerSightAngle;
    public float enemyDistance;
    public float calmDistance;
    [Space(20)]
    public float buildIntensity;
    public float peakIntensity;
    public float fadeIntensity;
    public float relaxIntensity;
    public bool hideBool;

    //BT
    public static float GetPassiveWander { get { return instance.passiveWanderDistance; } }
    public static float GetHuntWander { get { return instance.huntWanderDistance; } }
    public static float SetPassiveWander { set { instance.passiveWanderDistance = value; } }
    public static float SetHuntWander { set { instance.huntWanderDistance = value; } }
    public static float GetStoppingDist { get { return instance.stoppingDistance; } }
    public static float GetHearingRadius { get { return instance.hearingRadius; } }
    public static float GetInstantHeardRadius { get { return instance.instantHeardRadius; } }
    public static float GetWalkSpeed { get { return instance.walkSpeed; } }
    public static float SetWalkSpeed { set { instance.walkSpeed = value; } }
    public static float GetRunSpeed { get { return instance.runSpeed; } }
    public static float SetRunSpeed { set { instance.runSpeed = value; } }
    public static GameObject[] GetCaves { get { return instance.caves; } }
    public static GameObject GetSetPoint { get { return instance.setPoint; } }
    //character getter
    public static GameObject GetPlayer { get { return instance.player; } }
    public static GameObject GetAI { get { return instance.ai; } }
    //player/director
    public static float GetPlayerHealth { get { return instance.playerHealth; } }
    public static float SetPlayerHealth { set { instance.playerHealth = value; } }
    public static float GetSightPlayerLength { get { return instance.playerSightLength; } }
    public static float GetSightPlayerAngle { get { return instance.playerSightAngle; } }
    public static float GetEnemyDistance { get { return instance.enemyDistance; } }
    public static float GetCalmDistance { get { return instance.calmDistance; } }
    //director
    public static float GetBuildIntensity { get { return instance.buildIntensity; } }
    public static float GetPeakIntensity { get { return instance.peakIntensity; } }
    public static float GetFadeIntensity { get { return instance.fadeIntensity; } }
    public static float GetRelaxIntensity { get { return instance.relaxIntensity; } }
    public static bool GetHideBool { get { return instance.hideBool; } }
    public static bool SetHideBool { set { instance.hideBool = value; } }

    private void Start()
    {
        instance = this;
    }

    private void Update()
    {
        
        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public Vector3 DirFromAngleAI(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += ai.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public Vector3 DirFromAnglePlayer(float angleInDegrees, bool angleIsGlobal)
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += player.transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

