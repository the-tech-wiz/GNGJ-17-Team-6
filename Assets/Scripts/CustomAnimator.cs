using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class CustomAnimator : MonoBehaviour
{
    public SpriteRenderer sr;
    public SpriteAnim current;

    int index;
    float timer;

    void Reset()
    {
        sr = GetComponent<SpriteRenderer>();
    }
    void Update()
    {

        if (current == null || current.frames.Length == 0) return;
        timer += Time.deltaTime;
        float frameTime = 1f / current.fps;
        if (timer >= frameTime)
        {
            timer -= frameTime;
            index = (index + 1) % current.frames.Length;
            sr.sprite = current.frames[index];
        }
    }

    public void Play(SpriteAnim anim)
    {
        if (anim == null) return;
        current = anim;
        index = 0;
        timer = 0f;
        if (anim.frames.Length > 0) sr.sprite = anim.frames[0];
    }
}

