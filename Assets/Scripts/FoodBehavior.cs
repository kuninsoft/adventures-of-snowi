using UnityEngine;

public class FoodBehavior : MonoBehaviour
{
    private Rigidbody2D _rigidbody2D;
    private BoxCollider2D _boxCollider2D;
    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _rigidbody2D.gravityScale = 0;
        _boxCollider2D.isTrigger = true;
        _boxCollider2D.size = new Vector2(2.25f, 2.25f);
        _boxCollider2D.offset = new Vector2(0, 0);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
