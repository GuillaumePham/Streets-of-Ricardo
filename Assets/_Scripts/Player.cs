using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class Player : Character {

    public void AttackInput(InputAction.CallbackContext context) {
        if (context.performed) {
            Attack();
        }
    }
    public void JumpInput(InputAction.CallbackContext context) {
        if (context.performed) {
            Jump();
        }
    }
    public void MoveInput(InputAction.CallbackContext context) {
        Move(context.ReadValue<Vector2>());
    }
}
