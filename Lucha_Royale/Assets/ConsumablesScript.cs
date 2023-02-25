using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesScript : MonoBehaviour
{
    float timer = 0;
    float duration = 10.0f;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.gameObject.layer == 0 || collision.gameObject.layer == 1)
        {
            Destroy(gameObject);
        }
    }
}
