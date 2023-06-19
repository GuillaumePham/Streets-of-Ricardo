using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if( Input.GetKey("right") ){
            transform.Translate (0.1f, 0, 0);
        }
            
        if( Input.GetKey("left") ){
            transform.Translate (-0.1f, 0, 0);

        }
        // horizontalInput=Input.GetAxis("Horizontal");
        // verticalIntput=Input.GetAxis("Vertical");
        // transform.Translate(Vector3.forward * verticalIntput * Time.deltaTime * speed);
        // transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
    }
}
