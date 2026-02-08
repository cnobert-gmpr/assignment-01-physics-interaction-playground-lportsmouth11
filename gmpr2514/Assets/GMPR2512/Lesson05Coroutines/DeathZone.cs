
using System.Collections;
using UnityEngine;

namespace GMPR2512.Lesson05Coroutines
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private Transform _spawnPoint;

        void OnTriggerEnter2D(Collider2D collider2D)
        {
            if(collider2D.CompareTag("Ball"))
            {
                GameObject ball = collider2D.gameObject;
                // wait two seconds before doing something
                StartCoroutine(RespawnBall(ball));
            }
            
        }
        // "StartCoroutine must be passed a method that returns an IEnumerator
        private IEnumerator RespawnBall(GameObject ball)
        {
            yield return new WaitForSeconds(2);

            Rigidbody2D ballRB = ball.GetComponent<Rigidbody2D>();
            if(ballRB != null)
            {
                ballRB.linearVelocity = Vector2.zero;
                ballRB.angularVelocity = 0;
            }
            ball.transform.position = _spawnPoint.position;
        }
    }
}
