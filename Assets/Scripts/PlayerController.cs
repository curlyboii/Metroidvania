using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Rigidbody2D theRB;

    public float moveSpeed;
    public float jumpForce;

    public Transform groundPoint;
    private bool isOnGround;
    public LayerMask whatIsGround;

    public Animator anim;

    public BulletController shotToFire;
    public Transform shotPoint;

    private bool canDoubleJump;

    public float dashSpeed, dashTime;
    private float dashCounter;

    public SpriteRenderer theSR, afterImage;
    public float afterImageLifeTime, timeBetweenAfterImages;
    private float afterImageCounter; // how long we're waiting between each image
    public Color afterImageColor; // Sprite color while we dash

    public float waitAfterDashing;
    private float dashRechargeCounter;

    public GameObject standing, ball; // two modes
    public float waitToBall; // how long it takes us to switch between those two modes
    private float ballCounter;

    public Animator ballAnim;

    public BombManager bombManager; // Reference to the BombManager script

    private PlayerAbilityTracker abilities;

    public bool canMove;




    // Start is called before the first frame update
    void Start()
    {

        abilities = GetComponent<PlayerAbilityTracker>();

        canMove = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {

            if (dashRechargeCounter > 0)
            {

                dashRechargeCounter -= Time.deltaTime;

            }
            else
            {

                if (Input.GetButtonDown("Fire2") && standing.activeSelf && abilities.canDash)  // standing.activeSelf - object currently active 
                {

                    dashCounter = dashTime;

                    ShowAfterImage();

                }
            }

            if (dashCounter > 0)
            {

                dashCounter = dashCounter - Time.deltaTime;

                theRB.velocity = new Vector2(dashSpeed * transform.localScale.x, theRB.velocity.y);

                afterImageCounter -= Time.deltaTime;
                if (afterImageCounter <= 0)
                {

                    ShowAfterImage();

                }

                dashRechargeCounter = waitAfterDashing;

            }
            else
            {
                //move sideways
                theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);


                //handle direction change 
                if (theRB.velocity.x < 0)
                {
                    transform.localScale = new Vector3(-1f, 1f, 1f);
                }
                else if (theRB.velocity.x > 0)
                {
                    transform.localScale = Vector3.one;
                }
            }

            //checking if on the ground
            isOnGround = Physics2D.OverlapCircle(groundPoint.position, .2f, whatIsGround);


            //jumping
            if (Input.GetButtonDown("Jump") && (isOnGround || (canDoubleJump && abilities.canDoubleJump)))
            {
                if (isOnGround)
                {
                    canDoubleJump = true;
                }
                else
                {
                    canDoubleJump = false;

                    anim.SetTrigger("doubleJump");
                }

                theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
            }


            // shooting
            if (Input.GetButtonDown("Fire1"))
            {
                if (standing.activeSelf)
                {
                    Instantiate(shotToFire, shotPoint.position, shotPoint.rotation).moveDir = new Vector2(transform.localScale.x, 0f);

                    anim.SetTrigger("shotFired");
                }
                else if (ball.activeSelf && abilities.canDropBomb)
                {
                    if (bombManager != null) // Ensure the reference is assigned
                    {
                        bombManager.SpawnBomb(); // Call the SpawnBomb() method from the BombManager
                    }
                }
            }

            //ball mode
            if (!ball.activeSelf)
            {

                if (Input.GetAxisRaw("Vertical") < -0.9f && abilities.canBecomeBall)
                {

                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(true);
                        standing.SetActive(false);
                    }

                }
                else
                {
                    ballCounter = waitToBall;
                }

            }
            else
            {
                if (Input.GetAxisRaw("Vertical") > 0.9f)
                {

                    ballCounter -= Time.deltaTime;
                    if (ballCounter <= 0)
                    {
                        ball.SetActive(false);
                        standing.SetActive(true);
                    }

                }
                else
                {
                    ballCounter = waitToBall;
                }


            }
        }
        else
        {

            theRB.velocity = Vector2.zero;

        }


        if (standing.activeSelf)
        {
            anim.SetBool("isOnGround", isOnGround);
            anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        }

        if (ball.activeSelf) // ball animation
        {
            ballAnim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));

        }

    }


    public void ShowAfterImage()
    {

        SpriteRenderer image = Instantiate(afterImage, transform.position, transform.rotation);
        image.sprite = theSR.sprite;
        image.transform.localScale = transform.localScale;
        image.color = afterImageColor;

        afterImageCounter = timeBetweenAfterImages;

        Destroy(image.gameObject, afterImageLifeTime);
    }


}
