using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Apple;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField] private Camera cam;
    [SerializeField] private GameObject projectile;
    [SerializeField] private Transform RHFirePoint;
    [SerializeField] private float projectileSpeed = 30;
    [SerializeField] private float arcRange = 1;

    [SerializeField] private float fireRate = 4;
    private float timeToFire;

    [SerializeField] private Animator animator;

    private Vector3 destination;

    private void Update()
    {
        if (Input.GetButton("Fire1") && Time.time >= timeToFire)
        {
            animator.SetBool("isShooting", true);
            timeToFire = Time.time + (1 / fireRate);
            ShootProjectile();
        }
        
        if (Input.GetButtonUp("Fire1"))
        {
            animator.SetBool("isShooting", false);
        }
    }

    private void ShootProjectile()
    {
        Ray ray = cam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        InstantiateProjectile(RHFirePoint);
    }

    private void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;

        iTween.PunchPosition(projectileObj, new Vector3(Random.Range(arcRange, arcRange), Random.Range(arcRange, arcRange), 0), Random.Range(.5f, 2f));
    }
}