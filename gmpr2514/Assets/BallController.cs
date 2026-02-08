using UnityEngine;

public class BallController : MonoBehaviour
{
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (PlayfieldSettings.Instance == null) return;

        // Live gravity tuning
        rb.gravityScale = PlayfieldSettings.Instance.gravityScale;

        // Live max speed limit
        rb.linearVelocity = Vector2.ClampMagnitude(
            rb.linearVelocity,
            PlayfieldSettings.Instance.maxBallSpeed
        );
    }
}
