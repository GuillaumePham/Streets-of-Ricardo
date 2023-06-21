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
    [SerializeField] private TextMeshProUGUI _headerText;
    [SerializeField] private TextMeshProUGUI _inputText;
    [SerializeField] private GameObject _keyPrefab;
    [SerializeField] private GameObject _letterContainer;
    [SerializeField] private GameObject _numberContainer;
    [SerializeField] private KeyboardKey[] _letterKeys = new KeyboardKey[26];
    [SerializeField] private KeyboardKey[] _numberKeys = new KeyboardKey[10];
    [SerializeField] private Button _eraseButton;
    [SerializeField] private Button _deleteButton;
    [SerializeField] private Button _confirmButton;

    private Action<String> _onSubmit;



    public void GetString(Action<String> onSubmit, string headerText) {
        _onSubmit = onSubmit;
        _headerText.text = headerText;
        _numberContainer.SetActive(false);
        _letterContainer.SetActive(true);
        Enable();
    }

    public void GetNumber(Action<String> onSubmit, string headerText) {
        _onSubmit = onSubmit;
        _headerText.text = headerText;
        _letterContainer.SetActive(false);
        _numberContainer.SetActive(true);
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


    private void CreateLetterKeyboard() {

        _letterKeys = new KeyboardKey[26];

        if (_letterKeys.Length == 26 && _letterKeys[0] != null) {
            return;
        }

        string letterCharacters = "QWERTYUIOPASDFGHJKLZXCVBNM";

        for (int i = 0; i < letterCharacters.Length; i++) {
            GameObject keyObject = Instantiate(_keyPrefab, _letterContainer.transform);
            _letterKeys[i] = keyObject.GetComponent<KeyboardKey>();
            _letterKeys[i].key = letterCharacters[i];
        }
    }
    private void CreateNumberKeyboard() {

        _numberKeys = new KeyboardKey[10];

        if (_numberKeys.Length == 10 && _numberKeys[0] != null) {
            return;
        }

        string numberCharacters = "0123456789";

        for (int i = 0; i < numberCharacters.Length; i++) {
            GameObject keyObject = Instantiate(_keyPrefab, _numberContainer.transform);
            _numberKeys[i] = keyObject.GetComponent<KeyboardKey>();
            _numberKeys[i].key = numberCharacters[i];
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
        foreach (KeyboardKey key in _letterKeys) {
            key.DisableInteraction();
        }
        _eraseButton.interactable = false;
        _deleteButton.interactable = false;
        _confirmButton.interactable = false;
    }

    public override void EnableInteraction() {
        foreach (KeyboardKey key in _letterKeys) {
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
        if (_letterContainer.activeSelf) {
            EventSystem.current.SetSelectedGameObject(_letterKeys[0].gameObject);
        } else if (_numberContainer.activeSelf) {
            EventSystem.current.SetSelectedGameObject(_numberKeys[0].gameObject);
        } else {
            EventSystem.current.SetSelectedGameObject(_eraseButton.gameObject);
        }
    }
    
    private void Awake() {
        CreateLetterKeyboard();
        CreateNumberKeyboard();
        // GetString(null);
    }
}
