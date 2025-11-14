
using UnityEngine;
using UnityEditor; // only required if running in editor, not in builds

public static class ClipTools
{
    public static void Unpack(AnimationClip clip, out Sprite[] frames, out float[] times)
    {
        var bindings = AnimationUtility.GetObjectReferenceCurveBindings(clip);
        if (bindings.Length == 0)
        {
            frames = null;
            times = null;
            return;
        }

        var curve = AnimationUtility.GetObjectReferenceCurve(clip, bindings[0]);

        frames = new Sprite[curve.Length];
        times = new float[curve.Length];

        for (int i = 0; i < curve.Length; i++)
        {
            frames[i] = curve[i].value as Sprite;
            times[i] = curve[i].time;
        }
    }
}
