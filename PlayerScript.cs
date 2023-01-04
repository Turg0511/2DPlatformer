using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    private Animator myAnimator;
    public float moveSpeed;
    private bool facingRight;
    [SerializeField]
    private Transform[] groundPoints; //creates an array of "points" (actually game objects) to collide with the ground
    [SerializeField]
    private float groundRadius; //creates the size of the collider
    [SerializeField]
    private LayerMask whatIsGround; //defines what is ground
    private bool isGrounded; //can be set to true or false based on our position
    private bool jump; //can be set to true or false when we press the space key
    [SerializeField]
    private float jumpForce; //allows us to determine the magnitude of the jump
    public bool isAlive;
    public GameObject reset;
    private Slider healthBar;
    public float health = 2f;
    private float healthBurn = 1f;
    private bool dash;
    public float dashx;
    public float dashy;
    private bool item;
    public bool Spring;
    [SerializeField]
    private Transform[] spring_check;
    [SerializeField]
    private float springRadius; //creates the size of the collider
    [SerializeField]
    private LayerMask whatIsSpring; //defines what is ground
    private bool is_spring; //can be set to true or false based on our position

    // Start is called before the first frame update
    void Start()
    {
        facingRight = true;
        myRigidbody = GetComponent<Rigidbody2D>(); //a variable to control the Player's body
        myAnimator = GetComponent<Animator>(); //A variable to control the Player animator's controller
        isAlive = true;
        reset.SetActive(false);
        healthBar = GameObject.Find("health slider").GetComponent<Slider>();
        healthBar.minValue = 0f;
        healthBar.maxValue = health;
        healthBar.value = healthBar.maxValue;
        item = false;
        Spring = false;
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal"); //a variable that stores the value of our horizontal movement
        //Debug.Log(horizontal);
        if (isAlive)
        {
            PlayerMovement(horizontal); //a function that controls the player on the x axis
            Flip(horizontal);
            HandleInput();
        }
        else
        {
            return;
        }
        isGrounded = IsGrounded();
        is_spring = stand_on_spring();
    }

    //FUNCTION DEFINITIONS
    private void PlayerMovement(float horizontal)
    {
        if (isGrounded && jump)
        {
            isGrounded = false;
            jump = false;
            myRigidbody.AddForce(new Vector2(0, jumpForce));
            if (is_spring == true)
            {
                is_spring = false;
                myRigidbody.AddForce(new Vector2(0, jumpForce));
            }
            myAnimator.SetBool("jumping", true);
        }
        if (dash == true)
        {
            dash = false;
            if (facingRight == true)
            {
                myRigidbody.AddForce(new Vector2(0, dashy));
                myRigidbody.AddForce(new Vector2(dashx, 0));
            }
            else
            {
                myRigidbody.AddForce(new Vector2(0, dashy));
                myRigidbody.AddForce(new Vector2(-dashx, 0));
            }
            myAnimator.SetBool("dashing", true);
            item = false;
        }
        myRigidbody.velocity = new Vector2(horizontal * moveSpeed, myRigidbody.velocity.y); //adds velocity to the player's body on the x axis
        myAnimator.SetFloat("speed", Mathf.Abs(horizontal));
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal >= 0 && !facingRight)
        {
            facingRight = !facingRight; //resets the bool to the opposite value
            Vector2 theScale = transform.localScale; //creating a Vector 2 and storing the local scale values
            theScale.x *= -1; //
            transform.localScale = theScale;
        }
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            jump = true;
            //Debug.Log("I'm Jumping");
        }
        if (item == true)
        {
            if (Input.GetKeyDown(KeyCode.X))
            {
                dash = true;
                //Debug.Log("I'm Jumping");
            }
        }
    }
    //function to test for collisions between the array of groundPoints and the Ground LayerMask

    private bool IsGrounded()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            //if the player is not moving vertically, test each of the Player's groundPoints for collision with Ground
            foreach (Transform point in groundPoints)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
                for (int i = 0; 1 < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject) //if any of the colliders in the array of groundPoints comes into contact with another gameobject, return true.
                    {
                        return true;
                    }
                }
            }
        }
        return false; //if the player is not moving along the y axis, return false.
    }
    void OnCollisionEnter2D(Collision2D target)
    {
        if (target.gameObject.tag == "ground")
        {
            myAnimator.SetBool("jumping", false);
            myAnimator.SetBool("dashing", false);
        }
        if (target.gameObject.tag == "deadly")
        {
            ImDead();
        }
        if (target.gameObject.tag == "damage")
        {
            UpdateHealth();
        }
        if (target.gameObject.tag == "powerup")
        {
            item = true;
        }
    }

    void UpdateHealth()
    {
        if (health > 0)
        {
            health -= healthBurn; //health = health - healthBurn
            healthBar.value = health;
        }
        if (health <= 0)
        {
            ImDead();
        }
    }

    public void ImDead()
    {
        isAlive = false;
        myAnimator.SetBool("dead", true);
        reset.SetActive(true);
        healthBar.value = 0f;
    }
    private bool stand_on_spring()
    {
        if (myRigidbody.velocity.y <= 0)
        {
            foreach (Transform point in spring_check)
            {
                Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, springRadius, whatIsSpring);
                for (int i = 0; 1 < colliders.Length; i++)
                {
                    if (colliders[i].gameObject != gameObject) //if any of the colliders in the array of groundPoints comes into contact with another gameobject, return true.
                    {
                        return true;
                    }
                }
            }
        }
        return false; //if the player is not moving along the y axis, return false.
    }
}