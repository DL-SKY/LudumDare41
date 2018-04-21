using UnityEngine;

public class CameraController : MonoBehaviour
{
    #region Variables
    public float speed = 1.5f;

    private Camera cam;
    private Quaternion camRotationDef;
    #endregion

    #region Unity methods
    private void Start()
    {
        cam = GetComponent<Camera>();
        camRotationDef = cam.transform.localRotation;
    }

    private void LateUpdate()
    {
        if (Input.GetMouseButton(1))
        {
            var X = (Input.mousePosition.x - Screen.width / 2) / Screen.width * speed;
            var Y = (Input.mousePosition.y - Screen.height / 2) / Screen.height * speed;
            var eulerX = cam.transform.localRotation.eulerAngles.x;
            var eulerY = cam.transform.localRotation.eulerAngles.y;

            eulerX = (cam.transform.localRotation.eulerAngles.x - Y) % 360;
            eulerY = (cam.transform.localRotation.eulerAngles.y + X) % 360;

            cam.transform.localRotation = Quaternion.Euler(eulerX, eulerY, 0);
        }
        else if (cam.transform.localRotation != camRotationDef)
            cam.transform.localRotation = camRotationDef;
    }
    #endregion
}
