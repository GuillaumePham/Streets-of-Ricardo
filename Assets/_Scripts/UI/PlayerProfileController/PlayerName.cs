using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using UnityEngine.EventSystems;
using TMPro;

public class PlayerName : CustomButton {
    
    [SerializeField] private PlayerProfile _playerProfile;
    
    [SerializeField] private TextMeshProUGUI _playerNameText;


    public override void OnSubmit(BaseEventData eventData) {
        base.OnSubmit(eventData);
        _playerNameText.text = "Waiting for new Name...";
        KeyboardController.current.GetString(OnSetPlayerName);
    }

    private void OnSetPlayerName(string name) {
        _playerProfile.SetName(name);
        _playerNameText.text = name;
        EventSystem.current.SetSelectedGameObject(gameObject);
    }
}
