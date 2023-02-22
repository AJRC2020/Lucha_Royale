using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrestlerScript : MonoBehaviour
{
    public Animator animator;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var position = transform.position;

        if (Input.GetKey(KeyCode.W))
        {
            transform.position += Vector3.up * 3 * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.A))
        {
            transform.position -= Vector3.right * 3 * Time.deltaTime;
            animator.SetBool("isLeft", true);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.position += Vector3.right * 3 * Time.deltaTime;
            animator.SetBool("isLeft", false);
        }

        if (Input.GetKey(KeyCode.S))
        {
            transform.position -= Vector3.up * 3 * Time.deltaTime;
        }

        if (position.x - transform.position.x == 0 && position.y - transform.position.y == 0)
        {
            animator.SetFloat("Speed", 0);
        }
        else
        {
            animator.SetFloat("Speed", 1);
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            animator.SetBool("isPunching", true);
        }

        if (Input.GetKeyUp(KeyCode.R))
        {
            animator.SetBool("isPunching", false);
        }
    }
}
