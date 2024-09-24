using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected int currentLoadedAmmo;
    [SerializeField] protected int currentSpareAmmo;
    [SerializeField] protected bool canFire;
    [SerializeField] protected Transform firePoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void Reload()
    {
        if (currentLoadedAmmo < ammoCapacity && currentSpareAmmo > 0)
        {
            int bulletsToLoad = Mathf.Min(ammoCapacity - currentLoadedAmmo, currentSpareAmmo);
            currentSpareAmmo -= bulletsToLoad;
            currentLoadedAmmo += bulletsToLoad;
        }
    }

    protected virtual void Fire() 
    {
        if (currentLoadedAmmo > 0)
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
            //GameObject bullet = Instantiate(projectile, firePoint.position, firePoint.forward);
            //bullet.GetComponent<Rigidbody>().addForce(firePoint.forward * 10, ForceMode.Impulse);
        }
    }
}
