using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.InputSystem;

using SevenGame.Utility;

[RequireComponent(typeof(PlayerInput))]
public class Player : Character {

    public string playerName;

    [SerializeField] private PlayerInput _playerInput;
    [SerializeField] private MeshRenderer _playerIndicator;


    [SerializeField] private ScaledTimeInterval _attackComboCooldown;
    [SerializeField] private int _attackProgress = 0;


    public PlayerInput playerInput => _playerInput;

    public void Win() {
        FrameSet winAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Win").WaitForCompletion();
        SetAnimation(winAnimation, null, null);
    }
    protected void Death() {
        GameManager.current.Defeat(this);
        FrameSet deathAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Death").WaitForCompletion();
        SetAnimation(deathAnimation, null, null);
    }
    public override void Damage(float damage, bool direction) {
        base.Damage(damage, direction);
        FrameSet hitAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Damage").WaitForCompletion();
        _moveMagnitude = direction ? 1f : -1f;
        SetAnimation(hitAnimation, null, RegularAnimation);

        if (health.amount <= 0f) {
            Death();
        }
    }
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

        if (!_currentFrames.cancellable || _nextFrameIndex < _currentFrames.cancelFrame ) {
            return;
        }

        _attackProgress = _attackProgress % 3 + 1;

        FrameSet attack = Addressables.LoadAssetAsync<FrameSet>($"Ricardo/Attack{_attackProgress}").WaitForCompletion();
        _attackComboCooldown.SetDuration(0.7f);
        SetAnimation(attack, null, RegularAnimation);
    }

    public override void Jump() {
        base.Jump();
        FrameSet jumpAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Jump").WaitForCompletion();
        SetAnimation(jumpAnimation, null, RegularAnimation);
    }

    protected override void CheckCollisions() {
        base.CheckCollisions();

        if (onGround.started && _currentFrames.cancellable && _nextFrameIndex >= _currentFrames.cancelFrame ) {
            FrameSet landAnimation = Addressables.LoadAssetAsync<FrameSet>("Ricardo/Landing").WaitForCompletion();
            SetAnimation(landAnimation, null, RegularAnimation);
        }
    }

    private void RegularAnimation() {
        string animationName = _moveMagnitude == 0f ? "Idle" : "Walk";
        if (_currentFrames != null && _currentFrames.name == animationName) {
            return;
        }

        FrameSet animation = Addressables.LoadAssetAsync<FrameSet>($"Ricardo/{animationName}").WaitForCompletion();
        SetAnimation(animation);
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

    protected override void Update() {
        base.Update();
        if (_attackComboCooldown.isDone) {
            _attackProgress = 0;
        }

        if ( _currentFrames.name == "Walk" || _currentFrames.name == "Idle" ) {
            RegularAnimation();
        }
    }
}
