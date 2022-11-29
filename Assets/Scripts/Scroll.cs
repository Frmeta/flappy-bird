using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    public float min;

    GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.left * gm.pipeSpeed * Time.deltaTime;
        if (transform.position.x < min){
            transform.Translate(-min, 0, 0);
        }
    }
}
