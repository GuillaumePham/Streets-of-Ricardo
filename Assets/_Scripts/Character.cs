using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : Entity
{

    private float moveMagnitude;

    public void Move(Vector2 direction) {
        
    }
    

    void Update() {

        if( Input.GetKey("right") ){
            transform.Translate (0.1f, 0, 0);
        }
            
        if( Input.GetKey("left") ){
            transform.Translate (-0.1f, 0, 0);

        }
    }
}
