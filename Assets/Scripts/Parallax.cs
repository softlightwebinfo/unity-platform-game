using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{
    private Rigidbody2D rigibody;
    public float speed = 0.0f;
    private float position = 24.5f;
    private void Awake()
    {
        this.rigibody = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    private void FixedUpdate()
    {
        this.rigibody.velocity = new Vector2(this.speed, 0);
        float parentPosition = this.transform.parent.transform.position.x;

        if (this.transform.position.x - parentPosition >= this.position)
        {
            this.transform.position = new Vector3(parentPosition - this.position, this.transform.position.y, this.transform.position.z);
        }
    }
}
