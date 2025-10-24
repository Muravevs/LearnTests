using UnityEngine;

public class Item : MonoBehaviour
{
    [Header("Item Settings")]
    public string itemName;
    public int itemId;
    public Sprite itemIcon;

    private bool canPickup = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = true;
            // Можно добавить подсказку для игрока
            Debug.Log("Нажмите E чтобы поднять " + itemName);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canPickup = false;
        }
    }

    public bool CanPickup()
    {
        return canPickup;
    }

    public void Pickup()
    {
        if (GetComponent<Collider>() && GetComponent<Renderer>())
        {
            GetComponent<Collider>().enabled = false;
            GetComponent<Renderer>().enabled = false;
        }

        Debug.Log("Подобран предмет: " + itemName);
    }

    public void Restore()
    {
        if (GetComponent<Collider>() && GetComponent<Renderer>())
        {
            // Восстанавливаем предмет (если нужно выбросить)
            GetComponent<Collider>().enabled = true;
            GetComponent<Renderer>().enabled = true;
        }
        canPickup = false;
    }
}