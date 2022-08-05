using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class ItemCollector : MonoBehaviour
{
    public List<Item> Items = new List<Item>();

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        Items.Add(other.GetComponent<Item>());
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        if (!other.GetComponent<Item>().ItemRigidbody.isKinematic)
        {
            if (Items.Count == 1)
            {
                Items[0].ItemRigidbody.constraints = RigidbodyConstraints.FreezePosition;

                Items[0].DOItemTransform(new Vector3(-1.1f, transform.position.y, transform.position.z), Vector3.zero);
            }
            else if (Items.Count == 2)
            {
                if (CompareIds(Items[0], Items[1]))
                {
                    Items[1].ItemRigidbody.constraints = RigidbodyConstraints.FreezePosition;

                    Items[1].DOItemTransform(new Vector3(1.1f, transform.position.y, transform.position.z), Vector3.zero).OnComplete(() =>
                    {
                        Sequence sequence = DOTween.Sequence();

                        foreach (Item item in Items)
                        {
                            item.ItemCollider.enabled = false;

                            sequence.Join(item.transform.DOMove(transform.position, 0.2f));
                            sequence.Join(item.transform.DOScale(0, 0.3f));
                        }

                        sequence.Play();

                        sequence.OnComplete(() =>
                        {
                            foreach (Item item in Items)  // TODO: InvalidOperationException: Collection was modified; enumeration operation may not execute.
                            {
                                Items.Remove(item);
                                Destroy(item.gameObject);
                                StartCoroutine(ItemManager.Instance.OnItemDestroyed());
                            }
                        });
                    });
                }
                else
                {
                    Items[1].DOItemTransform(new Vector3(0, 4.5f, 0), Vector3.zero);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Item")) return;

        Item item = other.GetComponent<Item>();

        item.ItemRigidbody.constraints = RigidbodyConstraints.None;
        Items.Remove(item);
    }

    private bool CompareIds(Item firstItem, Item secondItem)
    {
        return firstItem.GetId() == secondItem.GetId();
    }
}
