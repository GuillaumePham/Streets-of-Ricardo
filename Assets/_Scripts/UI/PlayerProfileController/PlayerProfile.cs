using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerProfile : MonoBehaviour {

    [SerializeField] private string _playerId;
    [SerializeField] private string _playerName;
    [SerializeField] private InputDevice _inputDevice;
    [SerializeField] private string _inputControlScheme;

    public PlayerName playerNameInput;
    public PlayerDevice playerDeviceInput;
    public PlayerStat playerStatInput;


    public string playerId => _playerId;
    public string playerName => _playerName;
    public InputDevice inputDevice => _inputDevice;
    public string inputControlScheme => _inputControlScheme;


    public float speed => PlayerPrefs.GetFloat($"{playerId}_speed", 5f);
    public float strength => PlayerPrefs.GetFloat($"{playerId}_strength", 1f);
    public float jumpForce => PlayerPrefs.GetFloat($"{playerId}_jumpForce", 5f);


    public void SetName(string name) {
        _playerName = name;
    }

    public void SetDevice(InputDevice device, string controlScheme) {
        _inputDevice = device;
        _inputControlScheme = controlScheme;
    }

    public void SetSpeed(float speed) {
        PlayerPrefs.SetFloat($"{playerId}_speed", speed);
    }
    public void SetStrength(float strength) {
        PlayerPrefs.SetFloat($"{playerId}_strength", strength);
    }
    public void SetJumpForce(float jumpForce) {
        PlayerPrefs.SetFloat($"{playerId}_jumpForce", jumpForce);
    }

    public bool CheckValidity() {
        return _playerName.Length > 1 && _inputDevice != null && _inputControlScheme.Length > 1;
    }

    public void EnableInteraction() {
        playerNameInput.EnableInteraction();
        playerDeviceInput.EnableInteraction();
        playerStatInput.EnableInteraction();
    }

    public void DisableInteraction() {
        playerNameInput.DisableInteraction();
        playerDeviceInput.DisableInteraction();
        playerStatInput.DisableInteraction();
    }

}
