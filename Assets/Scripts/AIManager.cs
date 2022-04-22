using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AIManager : MonoBehaviour
{
    private static AIManager instance;

    [Space(20)]
    public float passiveWanderDistance;
    public float stoppingDistance;
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

    //BT
    public static float GetPassiveWander { get { return instance.passiveWanderDistance; } }
    public static float GetStoppingDist { get { return instance.stoppingDistance; } }
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


    private void Update()
    {
        instance = this;

        if(playerHealth <= 0)
        {
            SceneManager.LoadScene(0);
        }
    }

    public Vector3 DirFromAngle(float angleInDegrees, bool angleIsGlobal) 
    {
        if (!angleIsGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }
}

