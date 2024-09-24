using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private float reloadCooldown = 1.0f;
    [SerializeField] private float fireCooldown = 0.1f;
    private float cooldownLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && canFire)
        {
            Fire();
        }
        if (Input.GetButtonDown("Reload"))
        {
            Reload();
        }
        if (!canFire)
        {
            if (cooldownLeft <= 0)
            {
                canFire = true;
            }
            else
                cooldownLeft -= Time.deltaTime;
        }
    }

    protected override void Fire()
    {
        //Debug.Log("Gun fired");
        if (currentLoadedAmmo > 1)
        {
            currentLoadedAmmo-= 2;

            RaycastHit hit1;
            RaycastHit hit2;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit1, 500))
            {
                Debug.DrawRay(firePoint.position, firePoint.forward * hit1.distance, Color.red, 2f);
                if (hit1.transform.CompareTag("Zombie"))
                {
                    hit1.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            if(Physics.Raycast(firePoint.position, firePoint.forward, out hit2, 500))
            {
                Debug.DrawRay(firePoint.position, firePoint.forward * hit2.distance, Color.red, 2f);
                if (hit2.transform.CompareTag("Zombie"))
                {
                    hit2.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            canFire = false;
            cooldownLeft = fireCooldown;
        }
        else if (currentLoadedAmmo > 0)
        {
            currentLoadedAmmo--;

            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500))
            {
                Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                if (hit.transform.CompareTag("Zombie"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            canFire = false;
            cooldownLeft = fireCooldown;
        }
        //Debug.Log("Ammo left: " + currentLoadedAmmo);
    }

    protected override void Reload()
    {
        //Debug.Log("Gun reloaded");
        base.Reload();
        canFire = false;
        cooldownLeft = reloadCooldown;
    }
}
