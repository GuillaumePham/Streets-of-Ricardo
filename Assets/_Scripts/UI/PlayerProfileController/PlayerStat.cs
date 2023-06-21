using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerStat : CustomButton {
    
    [SerializeField] private PlayerProfile _playerProfile;

    [SerializeField] private TextMeshProUGUI _speedStatText;
    [SerializeField] private TextMeshProUGUI _strengthStatText;
    [SerializeField] private TextMeshProUGUI _jumpStatText;


    public override void OnSubmit(BaseEventData eventData) {
        base.OnSubmit(eventData);
        _speedStatText.text = "Waiting for new Stats...";
        _strengthStatText.text = "";
        SetSpeed();
    }

    private async void SetSpeed() {
        await System.Threading.Tasks.Task.Delay(200);
        KeyboardController.current.GetNumber(OnSetPlayerSpeed, "Set your Speed !!");
    }
    private void OnSetPlayerSpeed(string speedChar) {
        float speed = 5f;
        try {
            speed = int.Parse(speedChar);
        } finally {
            _playerProfile.SetSpeed(speed);
            SetSpeedText(speed.ToString());
            SetStrength();
        }
    }


    private async void SetStrength() {
        await System.Threading.Tasks.Task.Delay(200);
        KeyboardController.current.GetNumber(OnSetPlayerStrength, "Set your Strength !!");
    }
    private void OnSetPlayerStrength(string strengthChar) {
        float strength = 5f;
        try {
            strength = int.Parse(strengthChar);
        } finally {
            _playerProfile.SetStrength(strength);
            SetStrengthText(strength.ToString());
            SetJump();
        }
    }


    private async void SetJump() {
        await System.Threading.Tasks.Task.Delay(200);
        KeyboardController.current.GetNumber(OnSetPlayerJump, "Set your Jump !!");
    }
    private void OnSetPlayerJump(string jumpChar) {
        float jump = 5f;
        try {
            jump = int.Parse(jumpChar);
        } finally {
            _playerProfile.SetJumpForce(jump);
            SetJumpText(jump.ToString());
        }
    }


    private void SetSpeedText(string speed) {
        _speedStatText.text = $"Speed : {speed}";
    }
    private void SetStrengthText(string strength) {
        _strengthStatText.text = $"Strength : {strength}";
    }
    private void SetJumpText(string jump) {
        _jumpStatText.text = $"Jump : {jump}";
    }

    protected override void OnEnable() {
        base.OnEnable();
        SetSpeedText(_playerProfile.speed.ToString());
        SetStrengthText(_playerProfile.strength.ToString());
        SetJumpText(_playerProfile.jumpForce.ToString());
    }
}
