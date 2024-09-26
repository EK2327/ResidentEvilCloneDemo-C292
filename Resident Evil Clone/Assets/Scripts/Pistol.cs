using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pistol : Weapon
{
    [SerializeField] private float reloadCooldown = 1.0f;
    [SerializeField] private float fireCooldown = 0.2f;
    private float cooldownLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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

    public override void Fire()
    {
        if (magazine.GetRounds() > 1)
        {
            //Debug.Log("Gun fired a double");
            magazine.RemoveRound();
            magazine.RemoveRound();

            RaycastHit hit1;
            RaycastHit hit2;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit1, 500))
            {
                //Debug.DrawRay(firePoint.position, firePoint.forward * hit1.distance, Color.red, 2f);
                if (hit1.transform.CompareTag("Zombie"))
                {
                    hit1.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            if(Physics.Raycast(firePoint.position, firePoint.forward, out hit2, 500))
            {
                //Debug.DrawRay(firePoint.position, firePoint.forward * hit2.distance, Color.red, 2f);
                if (hit2.transform.CompareTag("Zombie"))
                {
                    hit2.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            canFire = false;
            cooldownLeft = fireCooldown;
        }
        else if (magazine.GetRounds() > 0)
        {
            //Debug.Log("Gun fired a single");
            magazine.RemoveRound();

            RaycastHit hit;

            if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500))
            {
                //Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                if (hit.transform.CompareTag("Zombie"))
                {
                    hit.transform.GetComponent<Enemy>().TakeDamage(1);
                }
            }

            canFire = false;
            cooldownLeft = fireCooldown;
           
        }
        UIManager.instance.setCurrentAmmo(magazine.GetRounds());
    }

    public override void Reload(Magazine mag)
    {
        base.Reload(mag);
        cooldownLeft = reloadCooldown;
    }

    
}
