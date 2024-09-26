using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minigun : Weapon
{
    [SerializeField] private float reloadCooldown = 2.0f;
    [SerializeField] private float fireCooldown = 0.05f;
    private float cooldownLeft;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void Fire()
    {
        base.Fire();
        cooldownLeft = fireCooldown;
    }

    public override void Reload(Magazine mag)
    {
        base.Reload(mag);
        cooldownLeft = reloadCooldown;
    }


}
