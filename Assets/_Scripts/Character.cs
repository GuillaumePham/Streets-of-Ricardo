using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using SevenGame.Utility;
using System;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Health))]
public abstract class Character : Entity, IDamageable {

    [SerializeField] protected SpriteRenderer _renderer;
    [SerializeField] protected Health _health;
    [SerializeField] protected Rigidbody _rigidbody;
    [SerializeField] protected Collider _collider;

    protected float _depthVelocity;
    protected float _lastMoveInput;
    protected bool _lastMoveDirection;
    protected float _moveMagnitude;
    protected EntityLayerMove _lastLayerMove = EntityLayerMove.Stay;

    public BoolData onGround;
    protected RaycastHit _groundHit;

    private Action _onAnimationStopped;
    private Action _onAnimationComplete;
    protected float lastFrameTime;
    protected FrameSet _currentFrames;
    protected uint _nextFrameIndex;

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

    public new SpriteRenderer renderer {
        get {
            _renderer ??= GetComponentInChildren<SpriteRenderer>();
            return _renderer;
        }
    }
    public Health health {
        get {
            _health ??= GetComponentInChildren<Health>();
            
            return _health;
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

    public virtual void Damage(float damage, bool direction) {
        health.amount -= damage;
    }

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

    private void ProcessAttack(FrameSet.AttackData attack, bool directional) {
        Vector2 position = attack.position;
        if (directional) {
            position.x *= _lastMoveDirection ? 1f : -1f;
        }

        Vector3 worldPosition = transform.position + (Vector3)position;
        Debug.DrawLine(worldPosition, worldPosition + Vector3.up * 2f, Color.red, 1f);

        Collider[] colliders = Physics.OverlapSphere(worldPosition, attack.radius, LayerMask.GetMask("Entity"));

        foreach (Collider collider in colliders) {
            if (collider == this.collider || collider == null) {
                continue;
            }

            if ( collider.TryGetComponent(out IDamageable damageable) || (collider.attachedRigidbody?.TryGetComponent(out damageable) ?? false) ) {
                damageable.Damage(attack.damage, _lastMoveDirection);
            }
            
        }

    }
    public virtual void Attack() {
    }

    public virtual void Jump() {
        if ( !onGround ) {
            return;
        }
        rigidbody.velocity.NullifyInDirection(Vector3.down);
        rigidbody.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }


    public void SetAnimation(FrameSet frames, Action onAnimationStopped = null, Action onAnimationComplete = null) {

        _onAnimationStopped?.Invoke();

        if (frames == null || frames == _currentFrames) {
            return;
        }

        _nextFrameIndex = 0;
        _currentFrames = frames;
        _onAnimationStopped = onAnimationStopped;
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

        if (!_currentFrames.stopRotation) {
            _lastMoveDirection = _moveMagnitude >= 0;
        }

        // check for collisions
        Vector3 displacement = transform.right * _moveMagnitude * moveSpeed * (_currentFrames?.movementSpeed ?? 1f) * Time.deltaTime;
        bool castHit = _collider.ColliderCast(Vector3.zero, displacement, out RaycastHit walkHit, 0.1f, LayerMask.GetMask("Default"));

        if ( !castHit ) {
            transform.position += displacement;
            return;
        }

        // displace up to collision
        transform.position += displacement.normalized * Mathf.Min(displacement.magnitude, walkHit.distance);
        Collider worldCollider = walkHit.collider;

        // fix penetration
        Vector3 penetrationDirection = default;
        float penetrationDistance = default;
        bool penetrationHit = Physics.ComputePenetration(_collider, _collider.transform.position, _collider.transform.rotation, worldCollider, worldCollider.transform.position, worldCollider.transform.rotation, out penetrationDirection, out penetrationDistance);

        if (penetrationHit) {
            transform.position += penetrationDirection * penetrationDistance;
        }
    }

    protected virtual void CheckCollisions() {
        if (collider == null || !(collider is BoxCollider box)) {
            return;
        }

        Vector3 start = transform.position + box.transform.rotation * box.center;
        Vector3 direction = Vector3.down * (box.size.y/2f + 0.1f);
        Debug.DrawRay(start, direction, Color.red);
        onGround.SetVal( _collider.ColliderCast(Vector3.zero, Vector3.down * 0.3f, out _groundHit, 0.1f, LayerMask.GetMask("Default")) );
    }

    private void UpdateFrame() {

        if (_currentFrames == null || Time.time - lastFrameTime < _currentFrames.frameRate) {
            return;
        }
        lastFrameTime += _currentFrames.frameRate;
        
        if (_nextFrameIndex == _currentFrames.frames.Length - 1) {
            _onAnimationComplete?.Invoke();
        }
        mpb.SetTexture("_MainTex", _currentFrames[_nextFrameIndex]);

        foreach (FrameSet.AttackData attack in _currentFrames.attacks) {
            if (attack.frameIndex == _nextFrameIndex) {
                ProcessAttack(attack, _currentFrames.directional);
            }
        }



        _nextFrameIndex =
            (uint)(_currentFrames.looping ? 
            (++_nextFrameIndex % _currentFrames.frames.Length) : 
            (Math.Min(++_nextFrameIndex, _currentFrames.frames.Length - 1)));

        // renderer.transform.rotation = Quaternion.Euler(0f, _currentFrames.directional && animationDirection ? 0f : 180f, 0f);
        renderer.flipX = _currentFrames.directional && !_lastMoveDirection;

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
