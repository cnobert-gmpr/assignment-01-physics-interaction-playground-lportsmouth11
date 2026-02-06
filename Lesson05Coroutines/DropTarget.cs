using UnityEngine;

namespace GMPR2512.Lesson05Coroutines
{
    public class DropTarget : MonoBehaviour
    {
        [SerializeField] private Color _hitColour = Color.darkTurquoise;
        [SerializeField] private float _hideDelay = 0.1f, _resetDelay = 2f;

        private bool _isDown = false;
        private SpriteRenderer _renderer;
        private Color _originalColour;

        void Awake()
        {
            _renderer = GetComponent<SpriteRenderer>();
            _originalColour = _renderer.color;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            // in-class exercise: write an "if" statement that prints a debug message if
            // _isDown is false and that the collider that collided with us is tagged with "Ball"
            if(!_isDown && collision.collider.CompareTag("Ball"))
            {
                _isDown = true;
                _renderer.color = _hitColour;
                // "nameof" returns a string that represents the name of whatever we passed it
                Invoke(nameof(HideTarget), _hideDelay); //call the named method in _hideDelay seconds
            }
        }
        void HideTarget()
        {
            gameObject.SetActive(false);
            Invoke(nameof(ResetTarget), _resetDelay);
        }
        void ResetTarget()
        {
            _renderer.color = _originalColour;
            gameObject.SetActive(true);
            _isDown = false;
        }
    }
}
