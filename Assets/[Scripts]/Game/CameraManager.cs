using UnityEngine;
public class CameraManager : MonoBehaviour
{
    void Update()
    {
        if (PlayerController.instance.canMove)
        {
            transform.Translate(Vector3.forward * PlayerController.instance.moveSpeed * Time.deltaTime);
        }
    }
}
