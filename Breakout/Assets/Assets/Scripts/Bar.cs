using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bar : MonoBehaviour
{
    private int speed = 7;
    private bool expanded = false;
    public GameObject gameManager;
    public GameObject ball;
   
    void Start()
    {

    }

    void Update()
    {
        move();
    }

    void move()
    {
        if(Input.GetKey(KeyCode.A))
        {
            transform.position += transform.right * -speed * Time.deltaTime;
        }

        if(Input.GetKey(KeyCode.D))
        {
            transform.position -= transform.right * -speed * Time.deltaTime;
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
       //Ball ball = gameObject.GetComponent<Ball>();
        if(collision.gameObject.tag == "Expand")
        {
            if(expanded == false)
            {
                EnableExpand();
            }
            Destroy(collision.gameObject);
        }

        if(collision.gameObject.tag == "ExtraBalls")
        {
            gameManager.SendMessage("SpawnExtraBalls");
            Destroy(collision.gameObject);
        }
    }

    public void EnableExpand()
    {
        expanded = true;
        transform.localScale += new Vector3(1, 0, 0);
        StartCoroutine(DisableExpand());
    }

    IEnumerator DisableExpand()
    {
        yield return new WaitForSeconds(10f);
        transform.localScale -= new Vector3(1, 0, 0);
        expanded = false;
    }
}
