using UnityEngine;
using System;
using UnityEngine.InputSystem;

[DefaultExecutionOrder(-100)]
public class JoystickInputManager : MonoBehaviour
{

    private Vector2 _move;
    private bool _attack;
    private bool _jump;
    
    private void Update() {
        _move = Keybinds.Map["Move"].ReadValue<Vector2>();
        _attack = Keybinds.Map["Attack"].ReadValue<float>() > 0;
        _jump = Keybinds.Map["Jump"].ReadValue<float>() > 0;

        Debug.Log($"Move: {_move}, Attack: {_attack}, Jump: {_jump}");
    }
}