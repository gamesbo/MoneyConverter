using UnityEngine;
using TMPro;
public class MoneyDoor : MonoBehaviour
{
    public int doorLevel;
    public TextMeshPro doorText;
    void Update()
    {
        doorText.text = doorLevel.ToString();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireMoney"))
        {
            if (doorLevel > 1)
            {
                doorLevel--;
                Destroy(other.gameObject);
                PlayerController.instance.gunLevel--;
                if (PlayerController.instance.gunLevel <= 0)
                {
                    Over();
                }
            }
            else
            {
                Debug.Log("1");
                Instantiate(Resources.Load("Wall"),new Vector3(transform.position.x-2, transform.position.y, transform.position.z + 40f), Quaternion.identity);
                GetComponent<Collider>().enabled = false;
                Destroy(gameObject);
            }
        }
        if (other.CompareTag("Gun"))
        {
            Over();
        }
    }
    public void Over()
    {
        EKTemplate.LevelManager.instance.Success();
        Haptic.NotificationSuccessTaptic();
        PlayerController.instance.isGameOver = true;
        PlayerController.instance.canMove = false;
        WinLoseController.instance.confetti.Play();
    }
}
