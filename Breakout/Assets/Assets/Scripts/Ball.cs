using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public bool ballIsMoving;
    public bool powerBallActive;
    int randomNumber;
    
    private Vector3 resetBallPosition;
    public Vector3 currentBallPosition;
    private Vector2 ballInitialForceLeft;
    private Vector2 ballInitialForceRight;
    private Vector2 ballInitialForceUp;
    
    public GameObject playerBar;
    public GameObject powerBall;
    public GameManager gameManager;
    public Collider2D yellowBar;
    public Collider2D orangeBar;

    private Rigidbody2D rigidbody2D;
    public ParticleSystem collisionParticle;
    


    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ballInitialForceLeft = new Vector2(-100f, 300f);
        ballInitialForceRight = new Vector2(100f, 300f);
        ballInitialForceUp = new Vector2(0f, 300f);
        powerBall.SetActive(false);

        powerBallActive = false;
        if(gameManager.numberOfBalls == 1)
        {
            ballIsMoving = false; 
        }else 
        {
            ballIsMoving = true;
        }
        
        resetBallPosition = transform.position;
    }

    void Update()
    {
        currentBallPosition = transform.position;
        
        if(gameManager.numberOfBalls == 1 && ballIsMoving == false)
        {
            if(Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.Q))
            {
                ShootLeft();
            } else if(Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.E))
            {
                ShootRight();
            } else if(Input.GetKeyDown(KeyCode.Space))
            {
                ShootUp();
            }
        }

        if(gameManager.blocksBroken >= 10)
        {
            EnablePowerBall();
            Debug.Log("Enable PB");
        }

        //Follow player bar
        if(ballIsMoving == false && playerBar != null)
        {
            resetBallPosition.x = playerBar.transform.position.x;
            transform.position = resetBallPosition;
        }

        //Reset ball position
        if(ballIsMoving == true && transform.position.y < -5.2)
        {
            if(gameManager.playerLives < 0 || gameManager.numberOfBalls > 1)
            {
                gameManager.numberOfBalls -= 1;
                Destroy(this.gameObject);
            }
            else if(gameManager.numberOfBalls == 1)
            {
                ResetPosition();
                gameManager.SendMessage("TakeLife");
                gameManager.blocksBroken = 0;
            }
        }

        if(ballIsMoving == true)
        {
            if(Input.GetKey(KeyCode.R))
            {
                if(gameManager.numberOfBalls == 1)
                {
                    ResetPosition(); 
                }
            }
        }
    }

    void ResetPosition()
    {
        if(gameManager.numberOfBalls == 1 && gameManager.playerLives >= 1)
        {
            ballIsMoving = false;
            resetBallPosition.x = playerBar.transform.position.x;
            resetBallPosition.y = -3.5f;
            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0;
            rigidbody2D.isKinematic = true;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Block" || collision.gameObject.tag == "Wall")
        {
            ParticleSystem tempCollisionPart = Instantiate(collisionParticle, currentBallPosition, Quaternion.Euler(90, 0, 0));
            tempCollisionPart.Play();
        }
    }

    void ShootLeft()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ballInitialForceLeft = new Vector2(-100f, 300f);
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(ballInitialForceLeft);
        ballIsMoving = true;
    }

    void ShootRight()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ballInitialForceRight = new Vector2(100f, 300f);
        rigidbody2D.isKinematic = false;
        rigidbody2D.AddForce(ballInitialForceRight);
        ballIsMoving = true;
    }

    void ShootUp()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        ballInitialForceUp = new Vector2(0f, 300f);
        rigidbody2D.isKinematic = false;
        ballIsMoving = true;
        rigidbody2D.AddForce(ballInitialForceUp);
        
    }

    public void EnablePowerBall()
    {
        Debug.Log("Power Ball Activated");
        powerBallActive = true;
        powerBall.SetActive(true);
        gameManager.GetComponent<GameManager>().blocksBroken = 0;
        StartCoroutine(DisablePowerBall());
    }

    IEnumerator DisablePowerBall()
    {
        yield return new WaitForSeconds(5f);
        powerBall.SetActive(false);
        powerBallActive = false;
    }
        
}
