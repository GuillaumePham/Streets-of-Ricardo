using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[RequireComponent(typeof(Rigidbody))]
public abstract class Character : Entity {

    private Rigidbody _rigidbody;

    protected float _moveMagnitude;
    protected EntityLayerMove _lastLayerMove = EntityLayerMove.Stay;


    public new Rigidbody rigidbody {
        get {
            _rigidbody ??= GetComponent<Rigidbody>();
            return _rigidbody;
        }
    }

    protected virtual float moveSpeed => 5f;
    protected virtual float jumpForce => 10f;


    public virtual void Move(Vector2 direction) {
        _moveMagnitude = Mathf.Clamp(direction.x, -1f, 1f);

        Debug.Log($"Move: {direction}");

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
        rigidbody.AddForce(Vector3.up * 10f, ForceMode.Impulse);
    }


    protected virtual void Update() {
        if (_moveMagnitude == 0) {
            return;
        }

        transform.position += transform.right * _moveMagnitude * Time.deltaTime;
    }

}
