using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.Utility;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : Singleton<GameManager> {

    [SerializeField] private Player player1;
    [SerializeField] private Player player2;
    [SerializeField] private PlayerHUD player1HUD;
    [SerializeField] private PlayerHUD player2HUD;
    [SerializeField] private PlayerInputManager _playerInputManager;
    [SerializeField] private GameObject _gameWinPanel;
    [SerializeField] private TextMeshProUGUI _gameWinText;


    public void StartGame(PlayerProfile player1Profile, PlayerProfile player2Profile) {
        player1 = _playerInputManager.JoinPlayer(0, 0, player1Profile.inputControlScheme, player1Profile.inputDevice).GetComponent<Player>();
        player2 = _playerInputManager.JoinPlayer(1, 1, player2Profile.inputControlScheme, player2Profile.inputDevice).GetComponent<Player>();
        
        player1HUD.SetCharacter(player1);
        player1HUD.SetName(player1Profile.playerName);
        player1HUD.gameObject.SetActive(true);
        player1.transform.position = new Vector3(-5, 7f, 0);
            
        player2HUD.SetCharacter(player2);
        player2HUD.SetName(player2Profile.playerName);
        player2HUD.gameObject.SetActive(true);
        player2.transform.position = new Vector3(5, 7f, 0);
            
    }

    private async void Reset() {
        await System.Threading.Tasks.Task.Delay(7000);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Defeat(Player player) {
        Player winner = player == player1 ? player2 : player1;
        string winnerName = player == player1 ? player2HUD.playerName : player1HUD.playerName;
        winner?.Win();

        _gameWinPanel.SetActive(true);
        _gameWinText.text = winnerName + " Wins!";

        Reset();
    }

    private void OnEnable() {
        SetCurrent();
    }
}
