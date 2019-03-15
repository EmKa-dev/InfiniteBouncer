using UnityEngine;

public class DifficultyManager : MonoBehaviour
{
    [SerializeField]
    DifficultyProgression DifficultyContainer;
    [SerializeField]
    BulletTimer eventtimer;

    public int StartBulletTimerAtDiff;
    [SerializeField]
    int StartAtDifficulty;

    int Difficulty;

    #region Static variables
    //Platforms-specific
    public static int MinForwardOffset;
    public static int MaxForwardOffset;
    public static int MinSidewaysOffset;
    public static int MaxSidewaysOffset;

    public static int SpecialPlatformFrequency;
    public static int ObstacleFrequency;
    public static int Windstrenght;
    public static int MovingPlatformSpeed;
    #endregion

    #region Initializon

    private void Awake()
    {
        SpecialPlatformFrequency = DifficultyContainer.SpecialPlatformFrequency;

        Difficulty = StartAtDifficulty - 1;

        if (SpecialPlatformFrequency < 6)
        {
            Debug.Log("Special platform frequency lower than 6 will result in pool running out");
        }
    }

    #endregion


    public void IncrementAndUpdateDifficulties()
    {
        Difficulty++;

        if (Difficulty >= StartBulletTimerAtDiff)
        {
            eventtimer.enabled = true;
        }

        switch (Difficulty)
        {
            case 0:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_0;
                MaxForwardOffset = DifficultyContainer.MaxForwardOffset_0;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_0;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_0;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_0;
                Windstrenght = DifficultyContainer.Windstrenght_0;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_0;
                break;
            case 1:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_1;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_1;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_1;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_1;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_1;
                Windstrenght = DifficultyContainer.Windstrenght_1;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_1;
                break;
            case 2:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_2;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_2;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_2;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_2;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_2;
                Windstrenght = DifficultyContainer.Windstrenght_2;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_2;
                break;
            case 3:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_3;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_3;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_3;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_3;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_3;
                Windstrenght = DifficultyContainer.Windstrenght_3;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_3;
                break;
            case 4:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_4;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_4;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_4;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_4;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_4;
                Windstrenght = DifficultyContainer.Windstrenght_4;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_4;
                break;
            case 5:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_5;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_5;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_5;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_5;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_5;
                Windstrenght = DifficultyContainer.Windstrenght_5;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_5;
                break;
            case 6:
                MinForwardOffset = DifficultyContainer.MinForwardOffset_6;
                MaxForwardOffset  = DifficultyContainer.MaxForwardOffset_6;
                MinSidewaysOffset = DifficultyContainer.MinSidewaysOffset_6;
                MaxSidewaysOffset = DifficultyContainer.MaxSidewaysOffset_6;
                ObstacleFrequency = DifficultyContainer.ObstacleFrequency_6;
                Windstrenght = DifficultyContainer.Windstrenght_6;
                MovingPlatformSpeed = DifficultyContainer.MovingPlatformSpeed_6;
                break;
            default:
                Debug.Log("Highest difficulty already set");
                break;
        }
    }
}
