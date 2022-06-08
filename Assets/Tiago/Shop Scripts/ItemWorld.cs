using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemWorld : MonoBehaviour
{
 
    public static ItemWorld SpawmnItemWorld(Vector3 position, ItemClass item)
    {
        Transform transform = Instantiate(ItemAssets.Instance.PfItemWorld, position, Quaternion.identity);

        ItemWorld itemWorld = transform.GetComponent<ItemWorld>();
        itemWorld.SetItem(item);

        return itemWorld;

    }


    private ItemClass item;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
    public void SetItem(ItemClass item)
    {
        this.item = item;
        spriteRenderer.sprite = item.GetSprite();
    }

    public ItemClass GetItem()
    {
        return item;
    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
