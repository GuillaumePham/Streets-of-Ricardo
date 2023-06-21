using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.UI;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerDevice : CustomButton {
    
    [SerializeField] private PlayerProfile _playerProfile;

    [SerializeField] private TextMeshProUGUI _deviceNameText;
    [SerializeField] private TextMeshProUGUI _schemeNameText;

    public static PlayerDevice _assigningDevice = null;


    public override void OnSubmit(BaseEventData eventData) {

        if (_assigningDevice != null) {
            return;
        }
        base.OnSubmit(eventData);

        _assigningDevice = this;
        Keybinds.PlayerActions.Attack.performed += TryAssignDevice;

        _deviceNameText.text = "Press the Attack button on your device...";
        _schemeNameText.gameObject.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void AssignDevice(InputDevice device, string scheme) {
        _playerProfile.SetDevice(device, scheme);

        _assigningDevice = null;
        Keybinds.PlayerActions.Attack.performed -= TryAssignDevice;
        Debug.Log($"Device assigned: {device.displayName} with scheme: {scheme}");

        _deviceNameText.text = device.displayName;
        _schemeNameText.text = scheme;
        _schemeNameText.gameObject.SetActive(true);
        ResetSelection();
    }

    private async void ResetSelection() {
        await System.Threading.Tasks.Task.Delay(200);
        EventSystem.current.SetSelectedGameObject(gameObject);
    }

    private void TryAssignDevice(InputAction.CallbackContext context) {
        
        // Retrieve device and scheme
        PlayerInput pairedPlayer = PlayerInput.FindFirstPairedToDevice(context.control.device);
        
        string scheme = null;
        foreach (InputBinding binding in Keybinds.PlayerActions.Attack.bindings) {
            InputBinding? triggerBinding = context.action.GetBindingForControl(context.control);
            if (triggerBinding == null) {
                continue;
            }
            if (binding.effectivePath == triggerBinding.Value.effectivePath) {
                scheme = binding.groups.Split(InputBinding.Separator)[0] ?? null;
                break;
            }
        }

        if (scheme == null) {
            return;
        }
        AssignDevice(context.control.device, scheme);

    }

    protected override void OnEnable() {
        base.OnEnable();
        Keybinds.PlayerMap.Enable();
    }

    protected override void OnDisable() {
        base.OnDisable();
        Keybinds.PlayerMap.Disable();
    }
}
