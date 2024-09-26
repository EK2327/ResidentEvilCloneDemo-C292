using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [SerializeField] protected int ammoCapacity;
    [SerializeField] protected bool canFire;
    [SerializeField] protected Transform firePoint;

    [SerializeField] protected Magazine magazine;

    [SerializeField] public Enums.MagazineType magazineType;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Reload(Magazine newMag)
    {
        if (newMag != null)
        {
            magazine = newMag;
            UIManager.instance.setCurrentAmmo(magazine.GetRounds());

        }
    }

    public virtual void Fire() 
    {
        if (magazine != null)
        {
            if (magazine.GetRounds() > 0)
            {
                magazine.RemoveRound();

                RaycastHit hit;
                if (Physics.Raycast(firePoint.position, firePoint.forward, out hit, 500))
                {
                    Debug.DrawRay(firePoint.position, firePoint.forward * hit.distance, Color.red, 2f);
                    if (hit.transform.CompareTag("Zombie"))
                    {
                        hit.transform.GetComponent<Enemy>().TakeDamage(1);
                    }
                    UIManager.instance.setCurrentAmmo(magazine.GetRounds());
                }
            }
        }
        
    }

    public int CheckAmmo()
    {
        if (magazine != null)
        {
            return magazine.GetRounds();
        }
        else
            return 0;
    }
}
