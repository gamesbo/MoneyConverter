using UnityEngine;
public class CameraManager : MonoBehaviour
{
    public bool fever = false;
    #region Singleton
    public static CameraManager instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    void Update()
    {
        if (PlayerController.instance.canMove)
        {
            if (!fever)
            {
                transform.Translate(Vector3.forward * PlayerController.instance.moveSpeed * Time.deltaTime);

            }
            else
            {
                transform.Translate(Vector3.forward * 8.8f * Time.deltaTime);

            }
        }
    }
}
