using System.Collections;
using UnityEngine;
using DG.Tweening;
using EKTemplate;
using TMPro;
public class PlayerController : MonoBehaviour
{
    [Header("Gun")]
    public bool canMove = false;
    public float minX, maxX;
    public float swerve;
    public float moveSpeed;
    public int gunLevel;
    public bool isGameOver = false;
    public TextMeshProUGUI gunLevelText;
    public bool isFever = false;
    public bool isFeverRight = false;
    [Header("Money")]
    public float moneyDestroyTime;
    public float moneySpawnTime;
    public float moneySpeed;
    public Transform moneySpawnPos;
    public Transform feverStartPosRight;
    public Transform feverStartPosLeft;
    public Transform feverEndPos;


    #region Singleton
    public static PlayerController instance = null;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    private void Start()
    {
        LevelManager.instance.startEvent.AddListener(OnGameStart);
    }
    public void MoneySpawn()
    {
        Instantiate(Resources.Load("money"),moneySpawnPos.position,Quaternion.identity);
    }
    IEnumerator MoneyDelay()
    {
        if (isGameOver) yield break;
        yield return new WaitForSeconds(moneySpawnTime);
        MoneySpawn();
        StartCoroutine(MoneyDelay());
    }
    private void OnGameStart()
    {
        canMove = true;
        StartCoroutine(MoneyDelay());
    }
    public void Movement()
    {
        Vector3 temp = transform.localPosition;
        temp.x += InputManager.instance.input.x * Time.deltaTime * swerve;
        temp.x = Mathf.Clamp(temp.x, -3.8f, 3.8f);
        transform.localPosition = temp;

        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        gunLevelText.text = "Lv." + gunLevel.ToString();
    }
    public void FeverModeRight() 
    {
        Vector3 temp = transform.localPosition;
        temp.x += InputManager.instance.input.x * Time.deltaTime * swerve;
        temp.x = Mathf.Clamp(temp.x, 9f, 17f);
        transform.localPosition = temp;

        transform.Translate(Vector3.forward * 8.8f * Time.deltaTime);
        gunLevelText.text = "Lv." + gunLevel.ToString();
    }
    public void FeverModeLeft()
    {
        Vector3 temp = transform.localPosition;
        temp.x += InputManager.instance.input.x * Time.deltaTime * swerve;
        temp.x = Mathf.Clamp(temp.x, -16f, -9f);
        transform.localPosition = temp;

        transform.Translate(Vector3.forward * 8.8f * Time.deltaTime);
        gunLevelText.text = "Lv." + gunLevel.ToString();
    }
    private void Update()
    {
        if (canMove)
        {
            if (!isFever)
            {
                Movement();
            }
            else
            {
                if (isFeverRight)
                {
                    FeverModeRight();
                }
                else
                {
                    FeverModeLeft();
                }
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Money"))
        {
            Instantiate(Resources.Load("particles/CloudMoney"),other.transform.position, Quaternion.identity);
            other.transform.DOScale(Vector3.zero,.2f).OnComplete(()=> { Destroy(other.gameObject); });
            GameManager.instance.AddMoney(50);

            if (other.transform.parent.parent.parent.GetComponent<Car>() != null)
            {
                other.transform.parent.parent.parent.GetComponent<Car>().GunLevelUp();
            }
            if (other.transform.parent.parent.parent.GetComponent<LuxCar>() != null)
            {
                other.transform.parent.parent.parent.GetComponent<LuxCar>().GunLevelUp();
            }
        }
        
    }
}
