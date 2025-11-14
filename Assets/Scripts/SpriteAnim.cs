using UnityEngine;

[CreateAssetMenu(fileName = "SpriteAnim", menuName = "Scriptable Objects/SpriteAnim")]
public class SpriteAnim : ScriptableObject
{
    
    public Sprite[] frames;      // the frame sequence
    public float fps = 8f;       // uniform frame speed
}
