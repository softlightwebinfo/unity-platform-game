using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaveZone : MonoBehaviour
{
    float timeSinceLastDestruction = 0.0f;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.timeSinceLastDestruction > 1.0f)
        {
            LevelGenerator.sharedInstance.AddLevelBlock();
            LevelGenerator.sharedInstance.RemoveOldestLevelBlock();
            this.timeSinceLastDestruction = 0.0f;
        }
    }

    private void Update()
    {
        this.timeSinceLastDestruction *= Time.deltaTime;
    }
}
