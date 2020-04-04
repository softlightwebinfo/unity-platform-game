using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriggerMovement : MonoBehaviour
{
    public bool movingForward;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.movingForward)
        {
            Enemy.turnAround = true;
            print("Hola");
        }
        else
        {
            Enemy.turnAround = false;
        }
        this.movingForward = !this.movingForward;
    }
}
