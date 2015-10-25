﻿using UnityEngine;
using System.Collections;

public class BubbleDelete : MonoBehaviour {

    public const float xRange = (float)(18 - 0.64);

    public Animator anim;

    private bool destroyBubble;

    // Use this for initialization
    void Start ()
    {
        this.gameObject.transform.Translate(Random.value * xRange - xRange / 2, 0, 0);
        //anim.AddClip(new AnimationClip(), "destroy");
        anim = GetComponent<Animator>();
        destroyBubble = false;

        Renderer rend = GetComponent<Renderer>();
        Color randomColor = new Color(Random.value * 20, Random.value * 20, Random.value * 20, 0.1f);

        rend.material.SetColor("_Color", randomColor);
    }
	
	// Update is called once per frame
	void Update ()
    {
        if(this.transform.position.y > 5.34)
        {
            Destroy(this.gameObject);
            UpdateScore.bubblesMissed++;
        }
        if(destroyBubble && !anim.GetCurrentAnimatorStateInfo(0).IsName("Idle") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > anim.GetCurrentAnimatorStateInfo(0).length))
        {
            print(anim.GetCurrentAnimatorStateInfo(0).normalizedTime + " | " + anim.GetCurrentAnimatorStateInfo(0).length);
            Destroy(this.gameObject);
        }
        if(Input.touchCount == 1 && !destroyBubble)
        {
            Vector3 wp = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
            Vector2 touchPos = new Vector2(wp.x, wp.y);
            if (GetComponent<Collider2D>() == Physics2D.OverlapPoint(touchPos))
            {
                anim.Play("Destroy", 0);
                destroyBubble = true;
                UpdateScore.bubblesHit++;
            }
        }
    }

    void OnMouseDown()
    {
        anim.Play("Destroy", 0);
        destroyBubble = true;
        UpdateScore.bubblesHit++;
    }
}
