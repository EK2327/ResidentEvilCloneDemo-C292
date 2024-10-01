using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    [SerializeField] float jumpForce;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float verticalLookLimit;
    [SerializeField] private float maxHealth = 5f;

    private float currentHealth;
    private Boolean isGrounded = true;
    private float xRotation;

    [SerializeField] Transform fpsCamera;
    private Rigidbody rb;
    [SerializeField] private Transform firePoint;

    [SerializeField] Weapon currentWeapon;
    private List<IPickupable> inventory = new List<IPickupable>();
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        currentHealth = maxHealth;

        if (currentWeapon != null)
        {
            UIManager.instance.setCurrentAmmo(currentWeapon.CheckAmmo());
        }
        UIManager.instance.setInventorySize(inventory.Count);
        int healthToShow = Mathf.RoundToInt(currentHealth / maxHealth * 100);
        UIManager.instance.setHealth(healthToShow);
    }

    // Update is called once per frame
    void Update()
    {
        LookAround();
        MovePlayer();
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            Jump();
        }
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Fire();
        }
        if (Input.GetButtonDown("Reload"))
        {
            
            AttemptReload();
            UIManager.instance.setInventorySize(inventory.Count);
        }
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        transform.Rotate(Vector3.up * mouseX);

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -verticalLookLimit, verticalLookLimit);
        fpsCamera.localRotation = Quaternion.Euler(xRotation, 0, 0);
        firePoint.localRotation = Quaternion.Euler(xRotation, mouseX, 0);
    }

    void MovePlayer() 
    {
        float moveX = Input.GetAxis("Horizontal");
        float moxeZ = Input.GetAxis("Vertical");

        Vector3 move = transform.right * moveX + transform.forward * moxeZ;
        move.Normalize();
        Vector3 moveVelocity = move * moveSpeed;

        moveVelocity.y = rb.velocity.y;

        rb.velocity = moveVelocity;
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);

        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        else if (collision.gameObject.GetComponent<IPickupable>() != null)
        {

            collision.gameObject.GetComponent<IPickupable>().PickUp(this);
            if (collision.gameObject.GetComponent<HealthPack>() == null)
            {
                inventory.Add(collision.gameObject.GetComponent<IPickupable>());
                UIManager.instance.setInventorySize(inventory.Count);

            }
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Player took Damage: " + damage);
        currentHealth -= damage;
        if (currentHealth <= 0)
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        int healthToShow = Mathf.RoundToInt(currentHealth / maxHealth * 100);
        UIManager.instance.setHealth(healthToShow);
    }

    public void AttemptReload()
    {
        if (currentWeapon != null)
        {
            Enums.MagazineType gunMagType = currentWeapon.magazineType;
            foreach (Magazine item in inventory)
            {
                
                if (item.GetMagazineType() == gunMagType)
                {
                    currentWeapon.Reload(item);
                    inventory.Remove(item);
                    break;
                }
        }
        }
    }

    public void Heal(float heal)
    {
        currentHealth += heal;
        if ( currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
        int healthToShow = Mathf.RoundToInt(currentHealth / maxHealth * 100);
        UIManager.instance.setHealth(healthToShow);
    }
}
