using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ClamMove : MonoBehaviour
{
    public float Speed;
    public float Head;
    public float Off;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {


        transform.position = new Vector2(transform.position.x - Speed * Time.deltaTime, transform.position.y);
        if (transform.position.x <= Off)
        {
            if (gameObject.tag == "StarClam")
            {
                Destroy(gameObject);
            }
            else
            {
                transform.position = new Vector2(Head, transform.position.y);
            }

        }
    }
}

