using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewInGame : MonoBehaviour
{
    public Text collectableLabel;
    public Text scoreLabel;
    public Text maxScoreLabel;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.sharedInstance.currentGameState == GameState.inGame || GameManager.sharedInstance.currentGameState == GameState.gameOver)
        {
            int currentObjects = GameManager.sharedInstance.collectedObjects;
            this.collectableLabel.text = currentObjects.ToString();
        }

        if (GameManager.sharedInstance.currentGameState == GameState.inGame)
        {
            float travelledDistance = PlayerController.sharedInstance.GetDistance();
            this.scoreLabel.text = "Score\n" + travelledDistance.ToString("f2");
            if (this.maxScoreLabel)
            {
                this.maxScoreLabel.text = "Max Score\n" + PlayerPrefs.GetFloat("maxscore", 0.0f).ToString("f2");
            }
        }
    }
}
