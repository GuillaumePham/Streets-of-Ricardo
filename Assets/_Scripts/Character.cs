using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.Utility;
using System;

[RequireComponent(typeof(Rigidbody))]
public abstract class Character : Entity {

    [SerializeField] protected MeshRenderer _renderer;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Collider _collider;

    protected float _depthVelocity;
    protected float _lastMoveInput;
    protected float _moveMagnitude;
    protected EntityLayerMove _lastLayerMove = EntityLayerMove.Stay;

    public BoolData onGround;
    protected RaycastHit _groundHit;

    private Action _onAnimationComplete;
    protected float lastFrameTime;
    protected FrameSet _currentFrames;
    protected uint _nextFrameIndex;

    private bool animationDirection = true;

    [SerializeField] private MaterialPropertyBlock _mpb;


    public new Rigidbody rigidbody {
        get {
            _rigidbody ??= GetComponent<Rigidbody>();
            return _rigidbody;
        }
    }

    public new Collider collider {
        get {
            _collider ??= GetComponent<Collider>();
            return _collider;
        }
    }

    public new MeshRenderer renderer {
        get {
            _renderer ??= GetComponentInChildren<MeshRenderer>();
            return _renderer;
        }
    }

    public MaterialPropertyBlock mpb {
        get {
            _mpb ??= new MaterialPropertyBlock();
            return _mpb;
        }
    }

    protected virtual float moveSpeed => 5f;
    protected virtual float jumpForce => 5f;


    public virtual void Move(Vector2 direction) {
        _lastMoveInput = Mathf.Clamp(direction.x, -1f, 1f);

        if (_currentFrames?.lockMovement ?? false) {
            return;
        }

        EntityLayerMove layerMove = direction.y > 0 ? EntityLayerMove.Back : direction.y < 0 ? EntityLayerMove.Forward : EntityLayerMove.Stay;
        if (layerMove != _lastLayerMove) {
            MoveLayer(layerMove);
            _lastLayerMove = layerMove;
        }
    }
    public virtual void Attack() {
        Debug.Log("Attack");
    }

    public virtual void Jump() {
        if ( !onGround ) {
            return;
        }
        rigidbody.velocity.NullifyInDirection(Vector3.down);
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    public void SetAnimation(FrameSet frames, Action onAnimationComplete = null) {

        if (frames == null || frames == _currentFrames) {
            return;
        }

        animationDirection = _moveMagnitude >= 0;

        _nextFrameIndex = 0;
        _currentFrames = frames;
        _onAnimationComplete = onAnimationComplete;
    }


    protected void UpdateDepth() {
        float depth = 5f;
        switch (layer) {
            case EntityLayer.Far:
                depth = -3f;
                break;
            case EntityLayer.Middle:
                depth = -6f;
                break;
            case EntityLayer.Near:
                depth = -9f;
                break;
        }

        float newDepth = Mathf.SmoothDamp(transform.position.z, depth, ref _depthVelocity, 0.1f);
        transform.position = new Vector3(transform.position.x, transform.position.y, newDepth);
    }


    protected virtual void Movement() {

        if ( !_currentFrames.lockMovement ) {
            _moveMagnitude = _lastMoveInput;
        }

        if (_moveMagnitude == 0) {
            return;
        }

        transform.position += transform.right * _moveMagnitude * moveSpeed * (_currentFrames?.movementSpeed ?? 1f) * Time.deltaTime;
    }

    protected virtual void CheckCollisions() {
        if (collider == null || !(collider is BoxCollider box)) {
            return;
        }

        // onGround = _collider.ColliderCast(Vector3.zero, Vector3.down * 0.3f, out _groundHit, 0.1f, LayerMask.GetMask("Default"));
        Vector3 start = transform.position + box.transform.rotation * box.center;
        Vector3 direction = Vector3.down * (box.size.y/2f + 0.1f);
        Debug.DrawRay(start, direction, Color.red);
        onGround.SetVal( Physics.Raycast(start, Vector3.down, out _groundHit, box.size.y/2f + 0.1f, LayerMask.GetMask("Default")) );
    }

    private void UpdateFrame() {

        if (!_currentFrames.stopRotation) {
            animationDirection = _moveMagnitude >= 0;
        }

        if (_currentFrames == null || Time.time - lastFrameTime < _currentFrames.frameRate) {
            return;
        }
        lastFrameTime += _currentFrames.frameRate;
        
        if (_nextFrameIndex == _currentFrames.frames.Length - 1) {
            _onAnimationComplete?.Invoke();
        }

        mpb.SetTexture("_MainTex", _currentFrames[_nextFrameIndex]);
        _nextFrameIndex =
            (uint)(_currentFrames.looping ? 
            (++_nextFrameIndex % _currentFrames.frames.Length) : 
            (Math.Min(++_nextFrameIndex, _currentFrames.frames.Length - 1)));

        renderer.transform.rotation = Quaternion.Euler(0f, _currentFrames.directional && animationDirection ? 0f : 180f, 0f);

        renderer.SetPropertyBlock(mpb);
    }

    protected virtual void Awake() {
        lastFrameTime = Time.time;
    }


    protected virtual void Update() {
        Movement();
        UpdateDepth();

        CheckCollisions();

        UpdateFrame();
    }

    protected virtual void FixedUpdate() {
    }

}
