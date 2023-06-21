using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PlayerProfileController : UIMenu<PlayerProfileController> {


    [SerializeField] private GameObject _deviceMenu;
    [SerializeField] private PlayerProfile _player1Profile;
    [SerializeField] private PlayerProfile _player2Profile;
    [SerializeField] private Button _confirmButton;


    public void OnConfirm() {
        if (_player1Profile.CheckValidity() && _player2Profile.CheckValidity()) {
            Disable();
            GameManager.current.StartGame(_player1Profile, _player2Profile);
        }
    }
    
    public override void Enable() {
        base.Enable();
        _deviceMenu.SetActive(true);
        ResetGamePadSelection();
        EnableInteraction();
    }

    public override void Disable() {
        base.Disable();
        _deviceMenu.SetActive(false);
        DisableInteraction();
    }


    public override void DisableInteraction() {
        _player1Profile.DisableInteraction();
        _player2Profile.DisableInteraction();
        _confirmButton.interactable = false;
    }

    public override void EnableInteraction() {
        _player1Profile.EnableInteraction();
        _player2Profile.EnableInteraction();
        _confirmButton.interactable = true;
    }

    public override void Refresh() {
    }

    public override void ResetGamePadSelection() {
        EventSystem.current.SetSelectedGameObject(_player1Profile.playerNameInput.gameObject);
    }

    private void Awake() {
        Enable();
    }
}
