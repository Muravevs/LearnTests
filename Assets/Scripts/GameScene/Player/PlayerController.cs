using GameScene;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;
    private IInventory inventory;
    private KeyCode pickupKey = KeyCode.E;
    private KeyCode dropKey = KeyCode.Q;

    private Rigidbody rb;

    void Start()
    {
        inventory = new Inventory();
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        if (movement.magnitude > 0.1f)
        {
            rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);

            Quaternion targetRotation = Quaternion.LookRotation(movement);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.fixedDeltaTime);
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey) && inventory.NearbyItems.Count > 0)
        {
            inventory.TryPickupClosestItem();
        }

        if (Input.GetKeyDown(dropKey) && inventory.Items.Count > 0)
        {
            inventory.RemoveItem();
        }
    }
    void OnTriggerEnter(Collider other)
    {
        Item item = other.GetComponent<Item>();
        if (item != null && !inventory.NearbyItems.Contains(item))
        {
            inventory.NearbyItems.Add(item);
            Debug.Log("Рядом предмет: " + item.itemName);
        }
    }
}