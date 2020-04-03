using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Posibles estados del videojuego
public enum GameState
{
    menu,
    inGame,
    gameOver,
}



public class GameManager : MonoBehaviour
{
    public static GameManager sharedInstance;
    //En que estado del juego nos encontramos
    //En el inicio queremos que empieze en el menu principal
    public GameState currentGameState = GameState.menu;
    public Canvas menuCanvas;

    private void Awake()
    {       
        GameManager.sharedInstance = this;      
    }

    private void Start()
    {
        this.BackToMenu();
    }
    private void Update()
    {
        if (Input.GetButtonDown("Start") && this.currentGameState != GameState.inGame)
        {
            this.StartGame();
        }

        if (Input.GetButtonDown("Pause"))
        {
            this.BackToMenu();
        }
    }
    //Metodo encargado de iniciar el juego
    public void StartGame()
    {
        this.SetGameState(GameState.inGame);
        GameObject camera = GameObject.FindGameObjectWithTag("MainCamera");
        CameraFollow cameraFollow = camera.GetComponent<CameraFollow>();
        cameraFollow.ResetCameraPosition();
        if (PlayerController.sharedInstance.transform.position.x > 10)
        {
            LevelGenerator.sharedInstance.RemoveAllTheBlocks();
            LevelGenerator.sharedInstance.GenerateInitialBlocks();
        }
        PlayerController.sharedInstance.StartGame();
    }

    //Metodo que se llamara cuando el jugador muera
    public void GameOver()
    {
        SetGameState(GameState.gameOver);
    }

    //Metodo para volver al menu principal cuando el usuario lo quiera hacer
    public void BackToMenu()
    {
        SetGameState(GameState.menu);
    }
    //Metodo encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {
        if(newGameState == GameState.menu)
        {
            this.menuCanvas.enabled = true;
        } else if(newGameState == GameState.inGame)
        {
            this.menuCanvas.enabled = false;
        }
        else if(newGameState == GameState.gameOver)
        {

        }
        this.currentGameState = newGameState;
    }
}
