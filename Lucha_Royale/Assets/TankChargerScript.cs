using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TankChargerScript : MonoBehaviour
{
    public GameObject luchador;
    public WrestlerScript wrestler;
    float damage = 5.0f;
    bool gotPunched = false;
    float speed = 0.2f;
    bool isStunned = false;
    float timer = 0.0f;
    float stunDuration = 1.0f;
    bool burning = false;
    float burningDuration = 2.0f;
    float timerBurning = 0.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        luchador = GameObject.FindGameObjectWithTag("Luchador");
        wrestler = luchador.GetComponent<WrestlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }

        if (!isStunned)
        {
            var final_pos = luchador.transform.position;
            var direction = final_pos - transform.position;
            direction = Vector3.Normalize(direction);
            transform.position += direction * speed * Time.deltaTime;
        }
        else
        {
            if (timer < stunDuration)
            {
                timer += Time.deltaTime;
            }
            else
            {
                isStunned = false;
                timer = 0.0f;
            }
        }

        if (burning)
        {
            if (timerBurning < burningDuration)
            {
                timer += Time.deltaTime;
                damage += 0.5f * Time.deltaTime;
            }
            else
            {
                burning = false;
                timer = 0.0f;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 11 && !burning)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position -= Vector3.right * damage / 2 * Time.deltaTime;
            }
            else
            {
                transform.position -= Vector3.right * damage / 2 * Time.deltaTime;
            }
            damage += 0.5f;
            burning = true; ;
        }
        else if (collision.gameObject.layer == 12)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position -= Vector3.right * damage * 100 * Time.deltaTime;
            }
            else
            {
                transform.position -= Vector3.right * damage * 100 * Time.deltaTime;
            }
            damage += 100.0f;
        }
        else
        {
            gettingPunched(collision);
        }

        if (collision.gameObject.layer != 3 && gotPunched)
        {
            gotPunched = false;
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        gettingPunched(collision);

        if (collision.gameObject.layer != 3 && gotPunched)
        {
            gotPunched = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (gotPunched)
        {
            gotPunched = false;
        }
    }

    private void gettingPunched(Collider2D collision)
    {
        if (collision.gameObject.layer == 3 && !gotPunched)
        {
            switch (wrestler.weapon)
            {
                case 0:
                    if (collision.transform.position.x > transform.position.x)
                    {
                        transform.position -= Vector3.right * damage * Time.deltaTime;
                    }
                    else
                    {
                        transform.position += Vector3.right * damage * Time.deltaTime;
                    }
                    damage += wrestler.power;
                    break;

                case 1:
                    isStunned = true;
                    damage += wrestler.power * 3.0f;
                    wrestler.chair_hits--;
                    break;
            }

            gotPunched = true;
        }
    }
}
