using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BgScorller : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += new Vector3(0, -5 * Time.deltaTime,0);

        if(transform.position.y < -12.5f)
            transform.position = new Vector3(transform.position.x, 12.25f, 10);
    }
}
