using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity {

    private float moveMagnitude;

    public abstract void Move(Vector2 direction);
    public abstract void Attack();
    public abstract void Jump();

}
