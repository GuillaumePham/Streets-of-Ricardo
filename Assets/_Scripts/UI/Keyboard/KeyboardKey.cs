using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using UnityEngine.EventSystems;
using TMPro;

public class KeyboardKey : CustomButton {

    [SerializeField] private TextMeshProUGUI _keyText;
    [SerializeField] private char _key;

    public char key {
        get => _key;
        set {
            _key = value;
            _keyText.text = _key.ToString();
        }
    }

    public override void OnSubmit(BaseEventData eventData) {
        base.OnSubmit(eventData);
        KeyboardController.current.OnKeySubmit(_key);
    }
}
