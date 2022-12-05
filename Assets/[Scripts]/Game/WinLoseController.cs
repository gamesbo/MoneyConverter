using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EKTemplate;
using DG.Tweening;
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
        PlayerController.instance.transform.DOMoveX(0, 0.5f);
        PlayerController.instance.isGameOver = true;
        confetti.Play();
        yield return new WaitForSeconds(1.9f);
        PlayerController.instance.canMove = false;
        LevelManager.instance.Success();
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
