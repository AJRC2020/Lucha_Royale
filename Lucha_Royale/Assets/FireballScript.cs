using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireballScript : MonoBehaviour
{
    public WrestlerScript luchador;
    int direction;
    float timer = 0.0f;
    float duration = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        var wrest = GameObject.FindGameObjectWithTag("Luchador");
        luchador = wrest.GetComponent<WrestlerScript>();
        if (luchador.isLeft)
        {
            direction = -1;
        }
        else
        {
            direction = 1;
        }
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position += Vector3.right * 3.0f * direction * Time.deltaTime;

        if (timer < duration)
        {
            timer += Time.deltaTime;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
