using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPack : MonoBehaviour, IPickupable
{
    [SerializeField] float healing;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PickUp(PlayerController player)
    {
        gameObject.SetActive(false);
        player.Heal(healing);
    }
}
