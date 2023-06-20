using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.Utility;

public class GameManager : Singleton<GameManager> {
    
    public GameState gameState { get; private set; } = GameState.MainMenu;



    public void SetGameState(GameState state) {
        switch(state) {
            case GameState.MainMenu:
                // UIManager.current.OpenMainMenu();
                break;
            case GameState.ScoreMenu:
                // UIManager.current.OpenScoreMenu();
                break;
            case GameState.Setup:
                // UIManager.current.OpenSetupMenu();
                break;
            case GameState.Play:
                // UIManager.current.OpenPlayMenu();
                break;
            case GameState.GameEnd:
                // UIManager.current.OpenWinMenu();
                break;
        }
    }
    public enum GameState {
        MainMenu,
        ScoreMenu,
        Setup,
        Play,
        GameEnd
    }
}
