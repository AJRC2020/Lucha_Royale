using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BullChargerScript : MonoBehaviour
{
    float damage = 10.0f;
    bool gotPunched = false;
    float speed = 0.5f;
    public GameObject luchador;
    public WrestlerScript wrestler;
    
    // Start is called before the first frame update
    void Start()
    {
        wrestler = luchador.GetComponent<WrestlerScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.x > 10 || transform.position.x < -10)
        {
            Destroy(gameObject);
        }

        var final_pos = luchador.transform.position;
        var direction = final_pos - transform.position;
        direction = Vector3.Normalize(direction);
        transform.position += direction * speed * Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        gettingPunched(collision);

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
            if (collision.transform.position.x > transform.position.x)
            {
                transform.position -= Vector3.right * damage * Time.deltaTime;
            }
            else
            {
                transform.position += Vector3.right * damage * Time.deltaTime;
            }
            damage += wrestler.power;

            gotPunched = true;
        }
    }
}
