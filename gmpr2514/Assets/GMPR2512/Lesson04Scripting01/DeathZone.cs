using UnityEngine;

namespace GMPR2512.Lesson04Scripting01
{
    public class DeathZone : MonoBehaviour
    {
        [SerializeField] private int _year = 1001;
        private float _seconds = 0f;
        private int _deathCount = 0;
        // Awake is called once, before Start
        void Awake()
        {
            Debug.Log($"I'm awake, the year is {_year}");
            _year += 1026;
        }
        // Start is called once before the first execution of Update after the MonoBehaviour is created
        void Start()
        {
            Debug.Log($"I'm in the Start method now, and the year is {_year}");
        }

        // Update is called once per frame
        void Update()
        {
            _seconds += Time.deltaTime;
            // Debug.Log($"This scene has been running for {_seconds} seconds.");
        }
        // if two game objects touch
        // and both of them have colliders
        // and at least one of the has a Rigidbody2D
        // and at least one of the colliders has "Is Trigger" checked
        // then this method will be invoked
        void OnTriggerEnter2D(Collider2D collider)
        {
            _deathCount++;
            // Debug.Log($"This bumped into me {collider.gameObject.name}");
            Debug.Log($"Deathzone has deathed this many: {_deathCount}");

            Rigidbody2D rb = collider.gameObject.GetComponent<Rigidbody2D>();
            if(rb != null)
            {
                rb.linearVelocity = Vector2.zero;
                rb.gravityScale = 0;
                //Destroy(rb);
            }
            
            //Destroy(collider.gameObject);
        }
    }
}
