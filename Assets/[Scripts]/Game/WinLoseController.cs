using System.Collections;
using UnityEngine;
using EKTemplate;
public class WinLoseController : MonoBehaviour
{
    public ParticleSystem confetti;
    #region Singleton
    public static WinLoseController instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    public void Win()
    {
        StartCoroutine(WinDelay());
    }
    public void Lose()
    {
        StartCoroutine(LoseDelay());
    }
    IEnumerator WinDelay()
    {
        PlayerController.instance.moneySpawnTime = 0.10f;
        PlayerController.instance.moneyDestroyTime = 1f;
        PlayerController.instance.moveSpeed = PlayerController.instance.moveSpeed + 1.25f;
        yield return new WaitForSeconds(0.2f);
        Haptic.NotificationSuccessTaptic();
    }
    IEnumerator LoseDelay()
    {
        PlayerController.instance.canMove = false;
        LevelManager.instance.Fail();
        Haptic.NotificationErrorTaptic();
        yield return new WaitForSeconds(0.1f);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Win();
            other.GetComponent<Collider>().enabled = false;
        }
    }
}
