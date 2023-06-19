using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideMovement : MonoBehaviour
{
    public void Movements()
    {
        if(Input.GetKey ("right")){
            transform.Translate (0.01f, 0, 0);
        }

        if(Input.GetKey ("left")){
            transform.Translate (-0.01f, 0, 0);

        }
    }
    private void Update()
    {
        Movements();
    }
}
