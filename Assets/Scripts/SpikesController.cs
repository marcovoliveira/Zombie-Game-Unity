using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikesController : MonoBehaviour
{
    [SerializeField]
    private float damage = 25f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<PlayerController>().TakeDamage(damage);


    }
}
