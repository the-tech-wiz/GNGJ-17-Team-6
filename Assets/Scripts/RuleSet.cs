using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RuleSet", menuName = "Scriptable Objects/RuleSet")]
[Serializable]
public class RuleSet : ScriptableObject
{
    [Serializable]
    public class RuleEntry
    {
        public Direction condition;
        public SpriteAnim animatedSprite;
    }
    Dictionary<Direction, SpriteAnim> lookup;
    public List<RuleEntry> entries;

    void OnEnable()
    {
        lookup = new Dictionary<Direction, SpriteAnim>();
        foreach (var e in entries)
        {
            lookup[e.condition] = e.animatedSprite;
        }
    }

    public SpriteAnim GetAnim(Direction mask)
    {
        if (lookup.TryGetValue(mask, out var sprite))
            return sprite;

        return null;
    }

}
