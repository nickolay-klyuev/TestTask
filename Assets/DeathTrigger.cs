using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathTrigger : MonoBehaviour
{
    [SerializeField] private ParticleSystem DeathExplosion;

    private PlayerController Player;

    // Start is called before the first frame update
    void Start()
    {
        Player = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag != "Extra")
        {
            Instantiate(DeathExplosion, transform.position, Quaternion.identity);
            Player.RemoveUnit(gameObject);
        }
    }
}
