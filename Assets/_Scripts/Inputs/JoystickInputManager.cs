using UnityEngine;
using System;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
[RequireComponent(typeof(PlayerInputManager))]
public class JoystickInputManager : MonoBehaviour {

    [SerializeField] private PlayerInputManager _playerInputManager;

    private Vector2 _move;
    private bool _attack;
    private bool _jump;

    private void Reset() {
        _playerInputManager ??= GetComponent<PlayerInputManager>();
    }
    
    private void OnEnable() {
        Keybinds.PlayerActions.Attack.performed += TryJoinPlayer;
        Keybinds.PlayerMap.Enable();
    }

    private void OnDisable() {
        Keybinds.PlayerActions.Attack.performed -= TryJoinPlayer;
        Keybinds.PlayerMap.Disable();
    }

    private void TryJoinPlayer(InputAction.CallbackContext context) {
        
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

        int playerIndex = _playerInputManager.playerCount;

        // If there is no player paired to the device, pair it
        if (pairedPlayer == null || pairedPlayer.currentControlScheme != scheme) {
            PlayerInput playerInput = _playerInputManager.JoinPlayer(playerIndex, playerIndex, scheme, context.control.device);
            Debug.Log($"Player {playerIndex} joined with scheme {scheme}");
            return;
        }

    }
}