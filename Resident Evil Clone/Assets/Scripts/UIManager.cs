using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI inventorySizeText;
    [SerializeField] TextMeshProUGUI healthText;
    public static UIManager instance;

    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setCurrentAmmo(int curAmmo)
    {
        currentAmmoText.text = curAmmo.ToString();
    }

    public void setInventorySize(int size)
    {
        inventorySizeText.text = size.ToString();
    }

    public void setHealth(float health)
    {
        healthText.text = health.ToString();
    }
}