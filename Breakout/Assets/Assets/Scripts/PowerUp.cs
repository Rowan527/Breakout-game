using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if(transform.position.y < -5)
        {
            Destroy(this.gameObject);
        }
    }
}
