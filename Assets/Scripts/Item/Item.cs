using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    public Rigidbody ItemRigidbody { get; private set; }
    public Collider ItemCollider { get; private set; }

    [SerializeField] int id;

    private Vector3 mOffset;
    private float mZCoord;

    private void Awake()
    {
        ItemRigidbody = GetComponent<Rigidbody>();
        ItemCollider = GetComponent<Collider>();
    }

    void OnMouseDown()
    {
        ItemRigidbody.isKinematic = true;
        Vector3 position = transform.position;
        transform.DOMove(new Vector3(position.x, 8.0f, position.z), 0.2f);

        mZCoord = Camera.main.WorldToScreenPoint(position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = position - GetMouseAsWorldPoint();
    }

    void OnMouseDrag()
    {
        Vector3 pos = GetMouseAsWorldPoint() + mOffset;

        transform.position = new Vector3(pos.x, transform.position.y, pos.z);
    }

    private void OnMouseUp()
    {
        ItemRigidbody.isKinematic = false;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }

    public int GetId()
    {
        return id;
    }
}
