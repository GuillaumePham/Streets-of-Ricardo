using UnityEngine;

public abstract class Entity : MonoBehaviour {

    public EntityLayer layer = EntityLayer.Middle;

    public void SetLayer(EntityLayer layer) {
        this.layer = layer;
    }
    public void MoveLayer(EntityLayerMove move) {
        this.layer = (EntityLayer)Mathf.Clamp((int)layer + (int)move, -1, 1);
    }


}