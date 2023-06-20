using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;


[RequireComponent(typeof(PlayerInput))]
public class Player : Character {

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private MeshRenderer _playerIndicator;


    private uint _attackProgress = 0;



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
        base.Attack();

        if (!_currentFrames.cancellable || _currentFrames.cancelFrame > _nextFrameIndex) {
            return;
        }

        _attackProgress = ++_attackProgress % 4;

        FrameSet attack = Addressables.LoadAssetAsync<FrameSet>($"Ricardo/Attack{_attackProgress}").WaitForCompletion();
        SetAnimation(attack, () => {
            _attackProgress = 0;
            RegularAnimation();
        });
    }

    public override void Jump() {
        base.Jump();
        FrameSet jumpAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Jump").WaitForCompletion();
        SetAnimation(jumpAnimation, RegularAnimation);
    }

    protected override void CheckCollisions() {
        base.CheckCollisions();

        // if (onGround.started) {
        //     FrameSet landAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Land").WaitForCompletion();
        //     SetAnimation(landAnimation, RegularAnimation);
        // }
    }

    private void RegularAnimation() {
        // string animationName = _moveMagnitude == 0f ? "Idle" : "Walk";
        string animationName = "Idle";
        FrameSet idle = Addressables.LoadAssetAsync<FrameSet>($"Ricardo/{animationName}").WaitForCompletion();
        SetAnimation(idle);
    }

    protected override void Awake() {
        base.Awake();

        int playerIndex = _playerInput.playerIndex;
        Color color = playerIndex == 0 ? Color.red : Color.blue;
        MaterialPropertyBlock mpb = new MaterialPropertyBlock();
        mpb.SetColor("_Color", color);
        _playerIndicator.SetPropertyBlock(mpb);

        RegularAnimation();
    }
}
