using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroControll : MonoBehaviour
{
    public float moveSpeed;
    Animator anim;
    SpriteRenderer render;
    // Start is called before the first frame update
    void Start()
        {
            anim = GetComponent<Animator>();  // get animation component
            render = GetComponent<SpriteRenderer>();  // get sprite render component
        }

    // Update is called once per frame
    void Update()
    {
        //Hero walks Horizontal
        if (Input.GetAxisRaw("Horizontal") > 0.5f || Input.GetAxisRaw("Horizontal") < -0.5f)
        {
            transform.Translate(new Vector3(Input.GetAxisRaw("Horizontal") * moveSpeed * Time.deltaTime, 0f, 0f));
            //check for battle while walking
            CheckForBattle();

        }
        //Hero Walks Vertical
        if (Input.GetAxisRaw("Vertical") > 0.5f || Input.GetAxisRaw("Vertical") < -0.5f)
        {
            transform.Translate(new Vector3(0f, Input.GetAxisRaw("Vertical") * moveSpeed * Time.deltaTime, 0f));
            //check for battle while walking
            CheckForBattle();  
        }
        //Hero X axis Animations load
        if (Input.GetAxisRaw("Horizontal") > 0)
        {
            anim.SetBool("WalkX", true);
            anim.SetBool("WalkYUp", false);
            anim.SetBool("WalkYDown", false);
            render.flipX = true;
            anim.speed = 1;
        }
        else if (Input.GetAxisRaw("Horizontal") < 0)
        {
            anim.SetBool("WalkX", true);
            anim.SetBool("WalkYUp", false);
            anim.SetBool("WalkYDown", false);
            render.flipX = false;
            anim.speed = 1;
        }

        //Hero Y animations load
        else if (Input.GetAxisRaw("Vertical") < 0)
        {
            anim.SetBool("WalkX", false);
            anim.SetBool("WalkYUp", false);
            anim.SetBool("WalkYDown", true);
            anim.speed = 1;

        }
        else if (Input.GetAxisRaw("Vertical") > 0)
        {
            anim.SetBool("WalkX", false);
            anim.SetBool("WalkYUp", true);
            anim.SetBool("WalkYDown", false);
            anim.speed = 1;
        }
        else { anim.speed = 0; }
    }

    public void CheckForBattle()
    {
        int enemyEncounter = Random.Range(1, 244);

        if (enemyEncounter == 47)  //if random number hits 47, start battle scene
        {
            Application.LoadLevel("Battle");
        }
    }
}


