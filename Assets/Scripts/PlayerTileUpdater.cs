using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movable))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CustomAnimator))]
public class PlayerSpriteUpdater : MonoBehaviour
{
    public RuleSet ruleset;

    public CustomAnimator animator;
    public Movable body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        body = GetComponent<Movable>();
        animator = GetComponent<CustomAnimator>();
        UpdateAnimation();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateAnimation();
    }
    public void UpdateAnimation()
    {
        var neighs = body.Neighbours();
        var anim = ruleset.GetAnim(neighs);
        if (anim != null && anim != animator.current)
        {
            // playerAnimation.AddClip(clip, clip.name);
            // Debug.Log("From" + body.gameObject + ": " + playerAnimation.clip);
            // playerAnimation.Play(clip.name);
            animator.Play(anim);
        }

    }
}
