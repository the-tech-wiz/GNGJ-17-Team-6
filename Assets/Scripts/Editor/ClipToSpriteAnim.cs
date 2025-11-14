
using UnityEngine;
using UnityEditor;

public class ClipToSpriteAnim : MonoBehaviour
{
    [MenuItem("Tools/Convert Clip to SpriteAnim")]
    static void Convert()
    {
        // Example: select an AnimationClip in the Project
        foreach (var obj in Selection.objects)
        {
            if (obj is not AnimationClip clip) continue;

            var anim = ScriptableObject.CreateInstance<SpriteAnim>();

            // Grab the sprite keyframes
            var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
            if (bindings.Length == 0) continue;

            var curve = AnimationUtility.GetObjectReferenceCurve(clip, bindings[0]);
            anim.frames = new Sprite[curve.Length];
            for (int i = 0; i < curve.Length; i++)
            {
                anim.frames[i] = curve[i].value as Sprite;
            }

            // Optional: derive fps from keyframes if evenly spaced
            if (curve.Length > 1)
            {
                float dt = curve[1].time - curve[0].time;
                anim.fps = 1f / dt;
            }

            // Save as asset
            string path = "Assets/Art/Animations/" + clip.name + "_Anim.asset";
            AssetDatabase.CreateAsset(anim, path);
            AssetDatabase.SaveAssets();

            Debug.Log($"Created SpriteAnim: {path}");
        }
    }
}
