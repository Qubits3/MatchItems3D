using System.Collections;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public static ItemManager Instance { get; private set; }

    private int itemCount;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        itemCount = GameObject.FindGameObjectsWithTag("Item").Length;
    }

    public IEnumerator OnItemDestroyed()
    {
        itemCount--;

        if (itemCount == 0)
        {
            GameManager.Instance.GameFinished();
        }

        yield return null;
    }
}
