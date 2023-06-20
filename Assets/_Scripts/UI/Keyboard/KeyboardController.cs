using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using TMPro;

using SevenGame.UI;
using UnityEngine.EventSystems;
using System;

public class KeyboardController : UIModal<KeyboardController> {

    private string _input = "";

    [SerializeField] private GameObject _keyMenu;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private GameObject _keyContainer;
    [SerializeField] private KeyboardKey[] _keys = new KeyboardKey[26];
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _confirmButton;

    private Action<String> _onSubmit;



    public void GetString(Action<String> onSubmit) {
        _onSubmit = onSubmit;
        Enable();
    }

    public void OnKeySubmit(char key) {
        _input += key;
        Refresh();
        // Disable();
    }

    public void Erase() {
        if (_input.Length > 0) {
            _input = _input.Substring(0, _input.Length - 1);
            Refresh();
        }
    }

    public void Delete() {
        _input = "";
        Refresh();
    }

    public void Confirm() {
        _onSubmit?.Invoke(_input);
        _input = "";
        Refresh();
        Disable();
    }


    private void CreateKeyboard() {

        _keys = new KeyboardKey[26];

        if (_keys.Length == 26 && _keys[0] != null) {
            return;
        }

        string keyCharacters = "QWERTYUIOPASDFGHJKLZXCVBNM";

        for (int i = 0; i < keyCharacters.Length; i++) {
            GameObject keyObject = Instantiate(_keyPrefab, _keyContainer.transform);
            _keys[i] = keyObject.GetComponent<KeyboardKey>();
            _keys[i].key = keyCharacters[i];
        }
    }


    public override void Enable() {
        base.Enable();
        _keyMenu.SetActive(true);

        ResetGamePadSelection();
        EnableInteraction();
    }

    public override void Disable() {
        base.Disable();
        _keyMenu.SetActive(false);
        DisableInteraction();
    }


    public override void DisableInteraction() {
        foreach (KeyboardKey key in _keys) {
            key.DisableInteraction();
        }
        _eraseButton.interactable = false;
        _deleteButton.interactable = false;
        _confirmButton.interactable = false;
    }

    public override void EnableInteraction() {
        foreach (KeyboardKey key in _keys) {
            key.EnableInteraction();
        }
        _eraseButton.interactable = true;
        _deleteButton.interactable = true;
        _confirmButton.interactable = true;
    }

    public override void Refresh() {
        _inputText.text = _input;
    }

    public override void ResetGamePadSelection() {
        EventSystem.current.SetSelectedGameObject(_keyContainer.transform.GetChild(0).gameObject);
    }
    
    private void Awake() {
        CreateKeyboard();
        // GetString(null);
    }
}
