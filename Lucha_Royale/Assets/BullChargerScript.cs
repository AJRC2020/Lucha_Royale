using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullChargerScript : MonoBehaviour
{
    public GameObject luchador;
    public GameObject burningEffect;
    public GameObject stunEffect;
    public WrestlerScript wrestler;
    public UIScript ui;
    public EnemySpawner spawner;
    public Animator enemyAnimation;
    float damage = 10.0f;
    bool gotPunched = false;
    float speed = 0.5f;
    bool isStunned = false;
    float timer = 0.0f;
    float stunDuration = 1.0f;
    bool burning = false;
    float burningDuration = 2.0f;
    float timerBurning = 0.0f;
    GameObject currentBurn;
    GameObject currentStun;

    // Start is called before the first frame update
    void Start()
    {
        luchador = GameObject.FindGameObjectWithTag("Luchador");
        wrestler = luchador.GetComponent<WrestlerScript>();
        ui = GameObject.FindGameObjectWithTag("UI").GetComponent<UIScript>();
        spawner = GameObject.FindGameObjectWithTag("Spawner").GetComponent<EnemySpawner>();
        enemyAnimation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
            ui.eliminated();
            spawner.current_enemy--;
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
                Destroy(currentStun);
            }
        }

        if (burning)
        {
            if (timerBurning < burningDuration)
            {
                timerBurning += Time.deltaTime;
                damage += 0.5f * Time.deltaTime;
            }
            else
            {
                burning = false;
                timerBurning = 0.0f;
                Destroy(currentBurn);
            }
        }

        
        if(damage >= 30.0f && damage < 50.0f){
            enemyAnimation.SetInteger("damagedLevel", 1);
        }
        if(damage >= 50.0f){
            enemyAnimation.SetInteger("damagedLevel", 2);
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
                transform.position += Vector3.right * damage / 2 * Time.deltaTime;
            }
            damage += 0.5f;
            burning = true;
            currentBurn = Instantiate(burningEffect, gameObject.transform.position, new Quaternion(-90, 0, 0, 0), gameObject.transform);
        }
        else if(collision.gameObject.layer == 12)
        {
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position -= Vector3.right * damage * 100 * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * damage * 100 * Time.deltaTime;
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
                    if (wrestler.chair_hits > 0)
                    {
                        isStunned = true;
                        damage += wrestler.power * 3.0f;
                        wrestler.chair_hits--;
                        var position = gameObject.transform.position + new Vector3(0, 0.5f, 0);
                        Destroy(currentStun);
                        timer = 0.0f;
                        currentStun = Instantiate(stunEffect, position, gameObject.transform.rotation, gameObject.transform);
                    }
                    break;
            }

            gotPunched = true;
        }
    }
}
