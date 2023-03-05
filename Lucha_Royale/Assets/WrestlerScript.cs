using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WrestlerScript : MonoBehaviour
{
    public Animator animator;
    public GameObject fireBall;
    public float power = 2.0f;
    public UIScript ui;
    public float damage = 1.0f;
    public float cheer_up = 0.0f;
    public int weapon = 0;
    public int chair_hits = 4;
    public int paper_count = 0;
    public bool isLeft = false;
    public bool isFlying = false;
    bool punch = false;
    bool wings = false;
    float timer_wings = 0.0f;
    float wings_duration = 6.0f;
    float timer_finisher = 0.0f;
    float finisher_duration = 0.915f;
    float speed = 3.0f;
    float hit = 0.5f;
    // Start is called before the first frame update
    void Start()
    {
        animator.applyRootMotion = true;
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;

        if (wings) 
        {
            if (timer_wings < wings_duration)
            {
                timer_wings += Time.deltaTime;
            }
            else
            {
                timer_wings = 0.0f;
                speed = 3.0f;
                gameObject.layer = 0;
                wings = false;
            }
        }

        if (isFlying)
        {
            if (timer_finisher < finisher_duration)
            {
                timer_finisher += Time.deltaTime;
            }
            else
            {
                timer_finisher = 0.0f;
                gameObject.layer = 0;
                isFlying = false;
                animator.SetBool("isFlying", false);
                animator.applyRootMotion = true;
            }
        }

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * speed * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * speed * Time.deltaTime;
            animator.SetBool("isLeft", true);
            isLeft = true;
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * speed * Time.deltaTime;
            animator.SetBool("isLeft", false);
            isLeft = false;
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.up * speed * Time.deltaTime;
        }

        if (position.x - transform.position.x == 0 && position.y - transform.position.y == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKeyDown(KeyCode.E) && paper_count > 0)
        {
            var initial = gameObject.transform.position;
            if (isLeft)
            {
                position = new Vector3(initial.x - 1.5f, initial.y, 0.0f);
            }
            else
            {
                position = new Vector3(initial.x + 1.5f, initial.y, 0.0f);
            }
            animator.SetBool("isPunching", true);
            Instantiate(fireBall, position, gameObject.transform.rotation);
            paper_count--;
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("isPunching", true);
            gameObject.layer = 3;
            punch = true;

        }

        if (Input.GetKeyDown(KeyCode.Q) && cheer_up == 1.0f)
        {
            animator.applyRootMotion = false;
            animator.SetBool("isFlying", true);
            isFlying = true;
            gameObject.layer = 12;
            cheer_up = 0.0f;
        }

        if (Input.GetKeyUp(KeyCode.R) || Input.GetKeyUp(KeyCode.E))
        {
            animator.SetBool("isPunching", false);
            gameObject.layer = 0;
            punch = false;
        }

        if (weapon == 1 && chair_hits == 0) {
            weapon = 0;
            animator.SetBool("isWeapon", false);
            ui.disableChair();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!punch && collision.gameObject.layer == 0)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position -= Vector3.right * damage * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * damage * Time.deltaTime;
            }
            damage += hit;
        }

        if (!isFlying) { 
            if (collision.gameObject.layer == 6)
            {
                wing_up();
            }

            if (collision.gameObject.layer == 7)
            {
                into_veins();
            }

            if (collision.gameObject.layer == 8)
            {
                cheer_up += 0.1f;
                if (cheer_up > 1.0f)
                {
                    cheer_up = 1.0f;
                }
            }

            if (collision.gameObject.layer == 9)
            {
                animator.SetBool("isWeapon", true);
                weapon = 1;
                chair_hits = 4;
                ui.enableChair();
            }

            if (collision.gameObject.layer == 10)
            {
                paper_count++;
            }
        }
    }

    private void wing_up()
    {
        gameObject.layer = 1;
        speed = 5.0f;
        timer_wings = 0.0f;
        wings = true;
    }

    private void into_veins()
    {
        hit /= 2;
        damage /= 2;
        if (damage < 1.0f)
        {
            damage = 1.0f;
        }
        power *= 2;
    }
}
