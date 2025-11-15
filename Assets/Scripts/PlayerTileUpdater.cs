using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static Direction;

[RequireComponent(typeof(Movable))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(CustomAnimator))]
public class PlayerSpriteUpdater : MonoBehaviour
{
    public RuleSet ruleset;
    public bool hasHead;
    public GameObject headPrefab;
    GameObject head;
    public CustomAnimator animator;
    public Movable body;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (hasHead)
        {
            head = Instantiate(headPrefab, transform, false);
        }
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
            // if (neighs == Direction.Down)
            if (hasHead)
            {
                var angle = NeighsToRotation(neighs);
                head.transform.eulerAngles = angle;
            }
        }

    }
    Vector3 NeighsToRotation(Direction dir) =>

        dir switch
        {
            Up => new Vector3(0, 0, 0),
            Up | Left | Right => new Vector3(0, 0, 0),
            Left => new Vector3(0, 0, 90f),
            Left | Up | Down => new Vector3(0, 0, 90f),
            Down => new Vector3(0, 0, 180f),
            Down | Left | Right => new Vector3(0, 0, 180f),
            Right => new Vector3(0, 0, 270f),
            Up | Down | Right => new Vector3(0, 0, 270f),

            Up | Down => new Vector3(0, 0, 90f),
            Left | Right => new Vector3(0, 0, 0),

            _ => head.transform.eulerAngles
        };
}
