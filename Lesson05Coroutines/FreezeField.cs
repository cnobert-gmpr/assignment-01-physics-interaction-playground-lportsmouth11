using UnityEngine;
using System.Collections;

public class FreezeField : MonoBehaviour
{
    // How long the ball stays frozen when it enters
    public float baseFreezeDuration = 2f;

    // Runs when something enters the freeze trigger
    [System.Obsolete]
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Only freeze the ball
        if (!collision.CompareTag("Ball")) return;

        Rigidbody2D rb = collision.GetComponent<Rigidbody2D>();
        if (rb == null) return;

        StartCoroutine(Freeze(rb));
    }

    [System.Obsolete]
    IEnumerator Freeze(Rigidbody2D rb)
    {
        // Store the ball's velocity so we can restore it later
        Vector2 savedVelocity = rb.velocity;

        // Stop all motion
        rb.velocity = Vector2.zero;

        // Switch to Kinematic so gravity and forces stop affecting it
        rb.bodyType = RigidbodyType2D.Kinematic;

        // Wait for the freeze duration
        yield return new WaitForSeconds(baseFreezeDuration);

        // Restore normal physics
        rb.bodyType = RigidbodyType2D.Dynamic;

        // Restore the velocity from before the freeze
        rb.velocity = savedVelocity;
    }
}

