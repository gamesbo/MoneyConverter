using UnityEngine;
using DG.Tweening;
public class GunObstacle : MonoBehaviour
{
    private PlayerController pc;
    private void Start()
    {
        pc = PlayerController.instance;
    }
    public void ObstacleTouch()
    {
        pc.gunLevel -= 10;
        pc.gunLevelText.color = new Color(1, 0.15f, 0.20f, 1);
        pc.gunLevelText.transform.DOScale(0.75f, 0.2f).OnComplete(() =>
        {
            pc.gunLevelText.transform.DOScale(0.5f, 0.25f);
            pc.gunLevelText.color = new Color(0.4f, 1, 0.4f, 1);
        });
        if (pc.gunLevel <= 0)
        {
            pc.gunLevel = 0;
            pc.gunLevelText.text = "Lv." + pc.gunLevel.ToString();
            WinLoseController.instance.Lose();
            StopAllCoroutines();
            pc.isGameOver = true;
        }
    }
    public void ShineParticle()
    {
        GameObject shine = Instantiate(Resources.Load("particles/Shine"), new Vector3(transform.position.x, transform.position.y + 2f,
            transform.position.z - 0.5f), Quaternion.identity) as GameObject;
        shine.transform.SetParent(transform);
    }
    public void FireRate()
    {
        ShineParticle();

        if (pc.moneySpawnTime > 0.16)
        {
            pc.moneySpawnTime -= 0.11f;
        }
        pc.moveSpeed += 0.05f;
    }
    public void FireRange()
    {
        ShineParticle();

        if (pc.moneyDestroyTime < 1.55)
        {
            pc.moneyDestroyTime += 0.15f;
        }
        if (pc.moneySpeed < 25.6f)
        {
            pc.moneySpeed += 0.625f;
        }
        pc.moveSpeed += 0.05f;
    }
    private void OnTriggerEnter(Collider other)
    {
        Obstacle obs = other.GetComponent<Obstacle>();
        if (obs)
        {
            other.GetComponent<Collider>().enabled = false;
            ObstacleTouch();
        }
        if (other.CompareTag("Rate"))
        {
            other.GetComponent<Collider>().enabled = false;
            FireRate();
        }
        if (other.CompareTag("Range"))
        {
            other.GetComponent<Collider>().enabled = false;
            FireRange();
        }
        if (other.CompareTag("StartFeverRight"))
        {
            CameraManager.instance.transform.DOMoveX(13f, 0.5f);
            CameraManager.instance.fever = true;
            PlayerController.instance.moneySpawnTime = 0.20f;
            PlayerController.instance.transform.DOJump(PlayerController.instance.feverStartPosRight.position, 1, 1, 0.5f).OnComplete(() =>
            {
                PlayerController.instance.isFever = true;
                PlayerController.instance.isFeverRight = true;
            });
        }
        if (other.CompareTag("StartFeverLeft"))
        {
            CameraManager.instance.transform.DOMoveX(-13f, 0.5f);
            CameraManager.instance.fever = true;
            PlayerController.instance.moneySpawnTime = 0.20f;
            PlayerController.instance.transform.DOJump(PlayerController.instance.feverStartPosLeft.position, 1, 1, 0.5f).OnComplete(() =>
            {
                PlayerController.instance.isFeverRight = false;
                PlayerController.instance.isFever = true;
            });
        }
        if (other.CompareTag("EndFever"))
        {
            CameraManager.instance.transform.DOMoveX(0f, 0.5f);
            CameraManager.instance.fever = false;
            PlayerController.instance.moneySpawnTime = 0.20f;
            PlayerController.instance.transform.DOJump(PlayerController.instance.feverEndPos.position, 1, 1, 0.5f).OnComplete(() =>
            {
                PlayerController.instance.isFever = false;
            });
        }
        if (other.CompareTag("MoneyInBag"))
        {
            Instantiate(Resources.Load("particles/CloudMoney"),other.transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            EKTemplate.GameManager.instance.AddMoney(1);
        }
    }
}
