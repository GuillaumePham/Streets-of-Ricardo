using UnityEngine;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class Player : Character {
    private Vector2 _moveDirection;

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


    public override void Attack() {
        Debug.Log("Attack");
    }

    public override void Jump() {
        Debug.Log("Jump");
    }

    public override void Move(Vector2 direction) {
        _moveDirection = Vector3.ClampMagnitude(direction, 1f);
        Debug.Log($"Move: {_moveDirection}");
    }
}
