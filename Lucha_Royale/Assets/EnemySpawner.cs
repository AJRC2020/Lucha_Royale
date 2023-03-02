using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject bullCharger;
    public GameObject tankCharger;
    public int current_enemy = 0;
    float timer = 0.0f;
    float entrance_time = 3.0f;
    int max_enemy = 6;
    // Start is called before the first frame update
    void Start()
    {
        spawn();
    }

    // Update is called once per frame
    void Update()
    {
        if (timer < entrance_time || current_enemy >= max_enemy)
        {
            timer += Time.deltaTime;
        }
        else
        {
            spawn();
        }
    }

    private void spawn()
    {
        var position = new Vector3(9 - 18 * Random.Range(0, 2), Random.Range(-3f, 1.75f), 0);
        if (Random.value < 0.75)
        {
            Instantiate(bullCharger, position, gameObject.transform.rotation);
        }
        else
        {
            Instantiate(tankCharger, position, gameObject.transform.rotation);
        }
        timer = 0.0f;
        current_enemy++;
    }
}
