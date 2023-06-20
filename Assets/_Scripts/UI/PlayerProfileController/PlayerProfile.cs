using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerProfile : MonoBehaviour {

    [SerializeField] private string _playerName;
    [SerializeField] private InputDevice _inputDevice;
    [SerializeField] private string _inputControlScheme;

    public PlayerName playerNameButton;
    public PlayerDevice playerDeviceButton;


    public string playerName => _playerName;
    public InputDevice inputDevice => _inputDevice;
    public string inputControlScheme => _inputControlScheme;


    public void SetName(string name) {
        _playerName = name;
    }

    public void SetDevice(InputDevice device, string controlScheme) {
        _inputDevice = device;
        _inputControlScheme = controlScheme;
    }

    public bool CheckValidity() {
        return _playerName.Length > 1 && _inputDevice != null && _inputControlScheme.Length > 1;
    }

    public void EnableInteraction() {
        playerNameButton.EnableInteraction();
        playerDeviceButton.EnableInteraction();
    }

    public void DisableInteraction() {
        playerNameButton.DisableInteraction();
        playerDeviceButton.DisableInteraction();
    }

}
