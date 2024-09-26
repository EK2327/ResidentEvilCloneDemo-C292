using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI currentAmmoText;
    [SerializeField] TextMeshProUGUI spareAmmoText;
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

    public void setSpareAmmo(int spareAmmo)
    {
        spareAmmoText.text = spareAmmo.ToString();
    }
}
