using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using DG.Tweening;
public class Car : MonoBehaviour
{
    public GameObject[] carParts;
    public List<GameObject> carMoneyList = new List<GameObject>();
    public int carLevel;
    private int carLevelOther;
    [Header("Car UI")]
    public TextMeshProUGUI carLevelText;
    public int carMoneyOne;
    public int carMoneyTwo;
    public int carMoneyThree;
    public int carMoneyFour;
    private Tween bounce;
    private void Start()
    {
        carLevelOther = carLevel;
        carLevelOther = (carLevelOther / 2);
    }
    void Update()
    {
        CarLevelUIColor();

        CarToMoney(0, carMoneyOne);
        CarToMoney(1, carMoneyTwo);
        CarToMoney(2, carMoneyThree);
        CarToMoney(3, carMoneyFour);
    }
    public void CarToMoney(int _moneyCarPart,int _currentMoneyPart)
    {
        if (carLevel == _currentMoneyPart)
        {
            carParts[_moneyCarPart].SetActive(false);
            carMoneyList[_moneyCarPart].SetActive(true);

            carMoneyList[_moneyCarPart].transform.DOScale(1.25f, 0.15f).OnComplete(() =>
            {
                carMoneyList[_moneyCarPart].transform.DOScale(1f, 0.15f);
            });
        }    
    }
    public void CarLevelUIColor()
    {
        carLevelText.text = "Lv." + carLevel.ToString();

        if (carLevel > PlayerController.instance.gunLevel)
        {
            carLevelText.transform.parent.GetComponent<Image>().color = new Color(1, 0.15f, 0.20f, 1);
        }
        else
        {
            carLevelText.transform.parent.GetComponent<Image>().color = new Color(0.3333334f, 1, 0.3333334f, 1);
        }

        if (carLevel <= 0)
        {
            carLevelText.transform.parent.gameObject.SetActive(false);
        }
    }
    public void CarBounce()
    {
        if (bounce != null) return;
        Haptic.LightTaptic();
        bounce = transform.DOScale(1.5f, 0.1f).OnComplete(() =>
        {
            transform.DOScale(1.12f, 0.1f);
            bounce = null;
        });
        Instantiate(Resources.Load("particles/Cloud"), new Vector3(carMoneyList[2].transform.position.x, carMoneyList[2].transform.position.y + 2f,
               carMoneyList[2].transform.position.z), Quaternion.identity);
    }
    public void GunLevelUp()
    {
        if (carLevelOther > 0)
        {
            PlayerController.instance.gunLevel++;
            carLevelOther--;
            PlayerController.instance.gunLevelText.transform.DOScale(0.75f, 0.1f).OnComplete(() =>
            {
                PlayerController.instance.gunLevelText.transform.DOScale(0.5f, 0.1f);
            });
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FireMoney"))
        {
            CarBounce();
        }
    }
}
