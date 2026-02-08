using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PortalGate : MonoBehaviour
{
    // The portal this one sends the ball to
    public Transform exitPortal;

    // Multiplies the ball's speed when it exits
    // 1 = same speed, 2 = double speed, 0.5 = half speed
    public float velocityMultiplier = 1f;

    // Rotates the ball's exit direction (in degrees)
    public float exitAngleOffset = 0f;

    // How long the ball must wait before using any portal again
    public float cooldownTime = 0.5f;

    // Keeps track of all balls that are currently on cooldown
    static HashSet<Rigidbody2D> cooldownBalls = new HashSet<Rigidbody2D>();

    // Called when something enters this portal's trigger
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only the ball should teleport
        if (!collision.CompareTag("Ball")) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        // If this ball recently used a portal, ignore it
        if (cooldownBalls.Contains(rb)) return;

        StartCoroutine(Teleport(rb));
    }

    [System.Obsolete]
    IEnumerator Teleport(Rigidbody2D rb)
    {
        // Put this ball on cooldown so it can't instantly teleport again
        cooldownBalls.Add(rb);

        // Save the velocity before teleporting
        Vector2 incomingVelocity = rb.velocity;

        // Move the ball to the paired portal's position
        rb.position = exitPortal.position;

        // Convert exit angle to radians for math
        float angle = exitAngleOffset * Mathf.Deg2Rad;

        // Rotate the velocity vector if needed
        Vector2 rotatedVelocity = new Vector2(
            incomingVelocity.x * Mathf.Cos(angle) - incomingVelocity.y * Mathf.Sin(angle),
            incomingVelocity.x * Mathf.Sin(angle) + incomingVelocity.y * Mathf.Cos(angle)
        );

        // Apply the new velocity and include the global portal speed multiplier
        rb.velocity = rotatedVelocity * velocityMultiplier * PlayfieldSettings.Instance.portalSpeedMultiplier;

        // Wait before allowing this ball to use portals again
        yield return new WaitForSeconds(cooldownTime);

        // Remove the ball from cooldown
        cooldownBalls.Remove(rb);
    }
}
