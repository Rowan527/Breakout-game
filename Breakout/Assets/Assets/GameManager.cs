using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject ballPrefab;
    public GameObject playerBar;
    public ParticleSystem fireworks;
    
    public Text resultText;
    public Button mainMenuButton;
    public Text mainMenuText;
    public Button nextLevelButton;
    public Text nextLevelText;
    public Button retryLevelButton;
    public Text retryLevelText;
    
    public int playerLives;
    public int blocksBroken;
    private int points;
    public int numberOfBalls;
    public static int numberOfBlocks;
    int randomNumber;
    
    private Vector3 ballPosition;

    void Start()
    {
        playerLives = 3;
        points = 0;
        blocksBroken = 0;
        numberOfBalls = 1;
        numberOfBlocks = 25;

        mainMenuButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);
        retryLevelButton.gameObject.SetActive(false);
    }

    void Update()
    {
        GameObject ball = GameObject.FindWithTag("Ball");
        ballPosition = ball.GetComponent<Ball>().currentBallPosition;

        if(numberOfBlocks <= 0)
        {
            fireworks.transform.position = new Vector3(-0.32f,-11.83f,-0.098f);
            resultText.text = "You Win!";
            mainMenuButton.gameObject.SetActive(true);
            nextLevelButton.gameObject.SetActive(true);
        } else if (playerLives < 0)
        {
            resultText.text = "You Lose!";
            mainMenuButton.gameObject.SetActive(true);
            retryLevelButton.gameObject.SetActive(true);
        } else 
        {
            resultText.text = "";
        }
/*
        while(ball.GetComponent<Ball>().powerBallActive == true && blocksBroken)
        {
            blocksBroken = 0;
        }
*/
        if(blocksBroken == 10 && ball.GetComponent<Ball>().transform.position.y < -3.5)
        {
            ball.GetComponent<Ball>().EnablePowerBall();
        }
    }

    void addPoints(int addedPoints)
    {
        points += addedPoints;
    }

    void TakeLife()
    {
        if(numberOfBalls == 1)
        {
            playerLives--;
        }
    }

    void OnGUI()
    {
        GUI.Label(new Rect(5f, 3f, 300f, 200f), "Lives: " + playerLives + " || Score: " + points + " || Power Ball Progress " + blocksBroken + "/10");
    }

    public void SpawnExtraBalls()
    {
        numberOfBalls ++;
        GameObject ball = Instantiate(ballPrefab, new Vector3(playerBar.transform.position.x, -3.5f, playerBar.transform.position.z), Quaternion.identity);
        ball.GetComponent<Ball>().ballIsMoving = true;
        randomNumber = Random.Range(1, 3);
        switch(randomNumber)
        {
            case 1:
                ball.SendMessage("ShootRight");
                break;
            case 2:
                ball.SendMessage("ShootLeft");
                break;
            default:
                ball.SendMessage("ShootUp");
                break;
        }
        
        ball.GetComponent<Ball>().gameManager = this;
        ball.GetComponent<Ball>().playerBar = playerBar;
    }
}
