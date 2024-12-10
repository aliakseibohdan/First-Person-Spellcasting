using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] private GameObject impactVFX;
    private bool collided = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Projectile")
            && !collision.gameObject.CompareTag("Player")
            && !collided)
        {
            collided = true;

            var impact = Instantiate(impactVFX, collision.contacts[0].point, Quaternion.identity) as GameObject;

            Destroy(impact, .1f);
            Destroy(gameObject);
        }
    }
}
