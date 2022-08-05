using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private void Awake()
    {
        SetPerspectiveSize();
    }

    private void SetPerspectiveSize()
    {
        float currentAspect = (float) Screen.width / (float) Screen.height;
        float ratio = 56f;
        Camera.main.fieldOfView = Mathf.Floor(1920 / currentAspect / ratio);
    }

    private void SetOrthographicSize()
    {
        var currentAspect = (float) Screen.width / (float) Screen.height;
        var ratio = 105f;
        Camera.main.orthographicSize = 1920 / currentAspect / ratio;
    }
}
