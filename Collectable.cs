using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (Door.instance != null)
        {
            Door.instance.collectablesCount++;
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "player")
        {
            Destroy(gameObject);
            if (Door.instance != null)
            {
                Door.instance.DecrementCollectables();
            }
        }
    }
}