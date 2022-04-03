using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtraUnit : MonoBehaviour
{
    private PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        Player.AddUnit(transform.position);
        Player.RemoveUnit(gameObject);
    }
}
