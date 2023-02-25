using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumablesSpawnScript : MonoBehaviour
{
    public GameObject boots;
    public GameObject roids;
    public GameObject cheer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Random.Range(0, 3000) > 2998)
        {
            var position = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 1.75f), 0);
            Instantiate(boots, position, transform.rotation);
        }

        if (Random.Range(0, 3000) > 2998)
        {
            var position1 = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 1.75f), 0);
            Instantiate(roids, position1, transform.rotation);
        }

        if (Random.Range(0, 3000) > 2998)
        {
            var position2 = new Vector3(Random.Range(-7f, 7f), Random.Range(-3f, 1.75f), 0);
            Instantiate(cheer, position2, transform.rotation);
        }
    }
}
