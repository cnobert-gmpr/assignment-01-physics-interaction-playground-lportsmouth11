using System.Collections;
using System.Text;
using UnityEngine;

namespace GMPR2512.Lesson05Coroutines
{
    public class Bumper : MonoBehaviour
    {
        [SerializeField] float _bumperForce = 150, _litDuration = 0.2f;
        [SerializeField] private Color _litColour = Color.magenta;

        private bool _isLit = false;
        private Color _originalColour;
        private SpriteRenderer _spriteRenderer;
        void Awake()
        {
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _originalColour = _spriteRenderer.color;
        }

        void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider.CompareTag("Ball"))
            {
                #region apply force
                // Debug.Log($"A game object with the tag {collision.collider.tag} just hit me!!!");
                Vector2 normal = Vector2.zero;
                if(collision.rigidbody != null)
                {
                    if(collision.contactCount > 0)
                    {
                        ContactPoint2D contact = collision.GetContact(0);
                        normal = contact.normal;
                    }
                    // if for some reason we didn't get a contact normal
                    else if(normal == Vector2.zero)
                    {
                        Vector2 direction = (collision.rigidbody.position - (Vector2)transform.position).normalized;
                        normal = direction;
                    }
                    // it turns out that the "normal" that I calculated was the opposite direction to what I wanted
                    // no problem, I just switched the sign
                    Vector2 impulse = -normal * _bumperForce;
                    collision.rigidbody.AddForce(impulse, ForceMode2D.Impulse);
                }
                #endregion
            
                if(!_isLit)
                {
                    StartCoroutine(LightUp());
                }

            }
        }
        private IEnumerator LightUp()
        {
            _isLit = true;
            _spriteRenderer.color = _litColour;
            yield return new WaitForSeconds(_litDuration);
            _spriteRenderer.color = _originalColour;
            _isLit = false;
        }
    }
}
