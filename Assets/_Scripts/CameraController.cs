using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour {
    
    private Vector3 _cameraVelocity;

    void Update() {
        uint i = 0;
        Vector3 meanPosition = Vector3.zero;
        foreach (PlayerInput player in PlayerInput.all) {
            meanPosition += player.transform.position;
            i++;
        }
        if (i != 0) {
            meanPosition /= i;
        }

        const float border = 3.4f;
        float X = Mathf.Clamp( meanPosition.x, -border, border );
        Vector3 targetPosition = new Vector3(X, transform.position.y, transform.position.z);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _cameraVelocity, 1.2f);
    }
}
