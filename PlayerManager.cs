using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static Vector2 lastCheckPointPos = new Vector2(-6.26f, -5.89f);

    private void Awake()
    {
        GameObject.FindGameObjectWithTag("player").transform.position = lastCheckPointPos;
    }
}
