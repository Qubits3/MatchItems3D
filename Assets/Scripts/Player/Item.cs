using DG.Tweening;
using UnityEngine;

public class Item : MonoBehaviour
{
    private Rigidbody rb;

    private Vector3 mOffset;
    private float mZCoord;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        rb.useGravity = false;

        Vector3 position = transform.position;
        transform.DOMove(new Vector3(position.x, 1.5f, position.z), 0.2f);

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
        rb.useGravity = true;
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);
    }
}

