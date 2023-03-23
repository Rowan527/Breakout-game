using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{
    public int hitsToDestroy = 1;
    public int points = 10;
    private int numberOfHits = 0;
    private Vector3 blockPosition;
    private Vector2 powerUpForce;
    int randomNumber;
    private bool weaken;
    
    public SpriteRenderer spriteRenderer;
    public Sprite YellowBlock;
    public Sprite OrangeBlock;

    public Rigidbody2D rigidbody;
    public ParticleSystem destroyParticle;
    public GameObject ExpandPowerUp;
    public GameObject ExtraBalls;
    public GameObject gameManager;
    public GameObject ball;
    
    void Start()
    {
        blockPosition = transform.position;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rigidbody = GetComponent<Rigidbody2D>();
        weaken = false;
        switch(hitsToDestroy)
            {
                case 1:
                this.spriteRenderer.sprite = YellowBlock;
                break;
                case 2:
                this.spriteRenderer.sprite = OrangeBlock;
                break;
            }
        powerUpForce = new Vector2(0f, -100f);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject ball = GameObject.FindWithTag("Ball");
        if(collision.gameObject.tag == "Ball")
        {
            numberOfHits++;

            UpdateColor();

            if(numberOfHits == hitsToDestroy)
            {
                destroyParticle.transform.position = blockPosition;

                randomNumber = Random.Range(0, 100);
                if(randomNumber < 20)
                {
                    SpawnExpand();
                } else if(randomNumber >= 21 && randomNumber <= 40)
                {
                    SpawnExtraBalls();
                }
                DestroyBlock();
            }
        }
    }

    void OnTriggerEneter2D(Collider2D collision)
    {
        Debug.Log("Out if");
        if(collision.gameObject.tag == "PowerBall")
        {
            Debug.Log("In if");
            DestroyBlock();
        }
    }

    public void DestroyBlock()
    {
        GameObject player = GameObject.FindGameObjectsWithTag("Player")[0];
                
        gameManager.SendMessage("addPoints", points);
        GameManager.numberOfBlocks--;
        
        gameManager.GetComponent<GameManager>().blocksBroken++;
        Destroy(this.gameObject);
        
        ParticleSystem tempDestroyPart = Instantiate(destroyParticle, blockPosition, Quaternion.Euler(90, 0, 0));
        tempDestroyPart.Play();

        Debug.Log("Destroy");
    }

    void Update()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        if(numberOfHits == 1)
        this.spriteRenderer.sprite = YellowBlock;
    }

    void SpawnExpand()
    {
        GameObject tempExpand = Instantiate(ExpandPowerUp, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);
        tempExpand.GetComponent<Rigidbody2D>().isKinematic = false;
        tempExpand.GetComponent<Rigidbody2D>().AddForce(powerUpForce);
    }

    void SpawnExtraBalls()
    {
        GameObject tempExtraBalls = Instantiate(ExtraBalls, new Vector2 (this.transform.position.x, this.transform.position.y), Quaternion.identity);
        tempExtraBalls.GetComponent<Rigidbody2D>().isKinematic = false;
        tempExtraBalls.GetComponent<Rigidbody2D>().AddForce(powerUpForce);
    }
} 

