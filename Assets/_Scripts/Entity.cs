using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public EntityPlane plane = EntityPlane.Middle;

    public void SetPlane(EntityPlane plane) {
        this.plane = plane;
    }
    public void MovePlane(bool back) {
        this.plane = (EntityPlane)Mathf.Clamp((int)plane + (back ? 1 : -1), 0, 2);
    }


}