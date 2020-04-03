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

        } else if(newGameState == GameState.inGame)
        {

        }
        else if(newGameState == GameState.gameOver)
        {

        }
        this.currentGameState = newGameState;
    }
}
