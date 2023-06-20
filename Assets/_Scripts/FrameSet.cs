using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "FrameSet", menuName = "Arcade/FrameSet")]
public class FrameSet : ScriptableObject {

    public Texture2D this[uint index] {
        get {
            return frames[index];
        }
    }

    [Header("Animation")]
    public Texture2D[] frames;
    public bool looping = false;
    public float frameRate = 1f / 24f;

    public List<AttackData> attacks = new List<AttackData>();

    [Header("Movement")]
    [Range(0f, 3f)] public float movementSpeed = 1f;
    public bool lockMovement = false;
    public bool stopJump = false;

    [Header("Cancel")]
    public bool cancellable = false;
    public uint cancelFrame = 0;

    [Header("Rotation")]
    public bool directional = false;
    public bool stopRotation;


    [System.Serializable]
    public struct AttackData {
        public uint frameIndex;
        public float damage;

        public Vector2 position;
        public float radius;

        public AttackData(uint frameIndex, float damage, Vector2 position, float radius) {
            this.frameIndex = frameIndex;
            this.damage = damage;
            this.position = position;
            this.radius = radius;
        }
    }
}
