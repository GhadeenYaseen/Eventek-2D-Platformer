using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlaterMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D _rb2d; //ref to gameobject's rigid body
    public GameObject gameOverScreen, winScreen;
   
    public TextMeshProUGUI coinCounterText;     //ref to textmesh pro ui

    public List<Image> healthIcons = new List<Image>();

    private int _coinAmount = 0;    //coin amount
    private int _currentIndex;      

    public float _horizontalDir; //get horizontal input

    public float speed = 12f;   //horizontal pseed
    public float jumpPower = 10f; //jump force

    public bool isFacingRight = true;  //horizontal flipping boolean
    private bool _jumpingState;     //ground check boolean

    void Start()
    {
        Time.timeScale =1;
        _currentIndex = healthIcons.Count - 1;
    }

    //update method is called once every frame
    private void Update() 
    {
        //Debug.Log("update method"); //output statement

        _horizontalDir = Input.GetAxisRaw("Horizontal");    //get horizontal input from left & right arrows
        Flip();     //horizontal flip

        // check if we can jump
        if(Input.GetButtonDown("Jump") && _jumpingState)
        {
            _rb2d.velocity = new Vector2(_rb2d.velocity.x, jumpPower); //jump on the y axis
        }
    }

    // fixed update is called every frame, independant from device's speed
    private void FixedUpdate() 
    {
        _rb2d.velocity = new Vector2(_horizontalDir * speed ,_rb2d.velocity.y);
    }

    // horizontal flip method
    private void Flip()
    {
        // whatever state we are at (facing right or left) and we want to move in the other direction, then flip
        if(isFacingRight && _horizontalDir < 0f || !isFacingRight && _horizontalDir > 0f)
        {
            isFacingRight = !isFacingRight;

            // inverse the value of the x scale
            Vector3 playerScale = transform.localScale;
            playerScale.x *= -1f; 
            transform.localScale = playerScale;
        }
    }

    // method from unity, check if we collided with an object that has the tag "ground"
    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("ground"))
        {
            Debug.Log("collided w ground");
            _jumpingState = true;
        }
        
        if(other.gameObject.CompareTag("Sharp"))
        {
            Debug.Log("collided w sharp"); //bug: is printed two times per collission 
            HealthIconHandler();
        }
    }

    private void HealthIconHandler()
    {
        if(_currentIndex >= 0 )
        {
            healthIcons[_currentIndex].gameObject.SetActive(false);

            _currentIndex -=1;
        }
        else
        {
            gameOverScreen.SetActive(true);
            Debug.Log("game over me3alem :()");
            Time.timeScale =0;
        }
    }

    // method from unity, check if we stopped colliding with an object that has the tag "ground"
    private void OnCollisionExit2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("ground"))
        {
            _jumpingState = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Coin"))
        {
            Debug.Log("coin collected :)");
            collision.gameObject.SetActive(false);
            _coinAmount += 1;
            UICoinUpdater();

            //Destroy(collision.gameObject);
        }

        if(collision.gameObject.CompareTag("Door"))
        {
            Debug.Log("level finished!");
            winScreen.SetActive(true);
            Time.timeScale =0;
        }

        if(collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("collided w sharp"); //bug: is printed two times per collission 
            HealthIconHandler();
        }
    }

    private void UICoinUpdater()
    {
        if(coinCounterText == null)
        {
            Debug.Log("u forgot drag and drop :(");
        }
        else
        {
            coinCounterText.text = "Coin Counter: " + _coinAmount.ToString();
        }
    }
}
