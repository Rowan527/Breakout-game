using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructionBall : MonoBehaviour
{
    public GameObject ball;
    public ParticleSystem collisionParticle;


    void Start()
    {

    }

    void Update()
    {
        transform.position = ball.GetComponent<Ball>().currentBallPosition;
    }
}
