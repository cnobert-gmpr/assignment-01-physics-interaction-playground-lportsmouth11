using UnityEngine;

public class PlayfieldSettings : MonoBehaviour
{
    public static PlayfieldSettings Instance;

    [Header("Ball Physics")]
    public float gravityScale = 1f;
    public float maxBallSpeed = 15f;

    [Header("Gameplay")]
    public float bumperForce = 10f;
    public float portalSpeedMultiplier = 1f;

    void Awake()
    {
        Instance = this;
    }
}
