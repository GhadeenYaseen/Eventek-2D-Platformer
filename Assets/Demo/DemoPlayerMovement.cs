using TMPro;
using UnityEngine;

public class DemoPlayerMovement : MonoBehaviour
{
    private float _horizontalDir; //left right zero
    private bool _isFacingRight = true;
    private bool _onGround;
    private int _coinsCount = 0;

    [SerializeField] private Rigidbody2D rigidbody2D;
    [SerializeField] private TextMeshProUGUI coinsCounter;

    public float speed = 11f;
    public float jumpPower = 11f;

    private void Update() 
    {
        _horizontalDir = Input.GetAxisRaw("Horizontal");

        if(Input.GetButtonDown("Jump") && _onGround)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, jumpPower);
        }

        if(Input.GetButtonUp("Jump") && rigidbody2D.velocity.y >0f)
        {
            rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, rigidbody2D.velocity.y * 0.5f);
        }

        Flip();
    }

    private void FixedUpdate() 
    {
        rigidbody2D.velocity = new Vector2(_horizontalDir * speed, rigidbody2D.velocity.y);
    }

    private void Flip()
    {
        if(_isFacingRight && _horizontalDir <0f || !_isFacingRight && _horizontalDir >0f)
        {
            _isFacingRight = !_isFacingRight;

            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("ground"))
        {
            _onGround = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("ground"))
        {
            _onGround = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.gameObject.CompareTag("Coin"))
        {
            other.gameObject.SetActive(false);
            _coinsCount += 1;

            coinsCounter.text = "Coins: " + _coinsCount.ToString();
        }
    }
}