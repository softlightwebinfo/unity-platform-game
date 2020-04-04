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
    public Canvas menuCanvas, gameCanvas, gameOverCanvas;
    public int collectedObjects = 0;

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

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            this.ExitGame();
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
        this.collectedObjects = 0;
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
    // Metodo para salir del juego
    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    //Metodo encargado de cambiar el estado del juego
    void SetGameState(GameState newGameState)
    {
        if (newGameState == GameState.menu)
        {
            this.menuCanvas.enabled = true;
            this.gameCanvas.enabled = false;
            this.gameOverCanvas.enabled = false;
        }
        else if (newGameState == GameState.inGame)
        {
            this.menuCanvas.enabled = false;
            this.gameCanvas.enabled = true;
            this.gameOverCanvas.enabled = false;
        }
        else if (newGameState == GameState.gameOver)
        {
            this.menuCanvas.enabled = false;
            this.gameCanvas.enabled = false;
            this.gameOverCanvas.enabled = true;
        }
        this.currentGameState = newGameState;
    }

    public void CollectObject(int objectValue)
    {
        this.collectedObjects += objectValue;
    }
}
