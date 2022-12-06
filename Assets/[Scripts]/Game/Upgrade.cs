using UnityEngine;
using UnityEngine.UI;
using EKTemplate;
using TMPro;
using System.Collections;
public class Upgrade : MonoBehaviour
{
    private GameManager gm;
    [Header("SPEED")]
    public TextMeshProUGUI speedAmountTxt;
    public TextMeshProUGUI speedAmountTxt2;
    public GameObject speedPanel;
    public GameObject speedPanelfake;
    [Header("RANGE")]
    public TextMeshProUGUI rangeAmountTxt;
    public TextMeshProUGUI rangeAmountTxt2;
    public GameObject rangePanel;
    public GameObject rangePanelfake;
    [Header("ADD MONEY")]
    public Animator Speedanim;
    public Animator Rangeanim;
    #region Singleton
    public static Upgrade instance = null;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    #endregion
    public void LoadResources()
    {
        speedAmountTxt.text = gm.SpeedCost[gm.speedLevel + 1] + "$".ToString();
        speedAmountTxt2.text = gm.SpeedCost[gm.speedLevel + 1] + "$".ToString();

        rangeAmountTxt.text = gm.RangeCost[gm.rangeLevel + 1] + "$".ToString();
        rangeAmountTxt2.text = gm.RangeCost[gm.rangeLevel + 1] + "$".ToString();

        gm.speedLevel = PlayerPrefs.GetInt("speed-level", 0);
        gm.rangeLevel = PlayerPrefs.GetInt("range-level", 0);

        PlayerController.instance.moneyDestroyTime = gm.rangerealfactors[gm.rangeLevel + 1];
        PlayerController.instance.moneySpawnTime = gm.speedrealfactors[gm.speedLevel + 1];
    }
    private void Start()
    {
        gm = GameManager.instance;
        LoadResources();
    }
    private void Update()
    {
        if (gm.speedLevel < 7)
        {
            if (gm.money >= gm.SpeedCost[gm.speedLevel + 1])
            {
                speedPanel.SetActive(true);
                speedPanelfake.SetActive(false);
            }
            else
            {
                speedPanel.SetActive(false);
                speedPanelfake.SetActive(true);
            }
        }
        else
        {
            speedPanel.SetActive(false);
            speedPanelfake.SetActive(true);
            speedAmountTxt2.text = "MAX".ToString();
        }
        if (gm.rangeLevel < 7)
        {
            if (gm.money >= gm.RangeCost[gm.rangeLevel + 1])
            {
                rangePanel.SetActive(true);
                rangePanelfake.SetActive(false);
            }
            else
            {
                rangePanel.SetActive(false);
                rangePanelfake.SetActive(true);
            }
        }
        else
        {
            rangePanel.SetActive(false);
            rangePanelfake.SetActive(true);
            rangeAmountTxt2.text = "MAX".ToString();
        }
    }
    public void SpeedFactor()
    {
        if (gm.speedLevel == 10) return;
        if (gm.money >= gm.SpeedCost[gm.speedLevel + 1])
        {
            Haptic.LightTaptic();
            Speedanim.SetTrigger("Click");
            PlayerPrefs.SetInt("speed-level", ++gm.speedLevel);
            PlayerPrefs.Save();
            speedAmountTxt.text = gm.SpeedCost[gm.speedLevel + 1] + "$".ToString();
            speedAmountTxt2.text = gm.SpeedCost[gm.speedLevel + 1] + "$".ToString();
            PlayerController.instance.moneySpawnTime = gm.speedrealfactors[gm.speedLevel + 1];
            gm.AddMoney(-(gm.SpeedCost[gm.speedLevel]));
            UIManager.instance.gamePanel.AddMoney(-(gm.SpeedCost[gm.speedLevel]));
        }
    }
    public void RangeFactor()
    {
        if (gm.rangeLevel == 10) return;
        if (gm.money >= gm.RangeCost[gm.rangeLevel + 1])
        {
            Haptic.LightTaptic();
            Rangeanim.SetTrigger("Click");
            PlayerPrefs.SetInt("range-level", ++gm.rangeLevel);
            PlayerPrefs.Save();
            rangeAmountTxt.text = gm.RangeCost[gm.rangeLevel + 1] + "$".ToString();
            rangeAmountTxt2.text = gm.RangeCost[gm.rangeLevel + 1] + "$".ToString();
            PlayerController.instance.moneyDestroyTime = gm.rangerealfactors[gm.rangeLevel + 1];
            gm.AddMoney(-(gm.RangeCost[gm.rangeLevel]));
            UIManager.instance.gamePanel.AddMoney(-(gm.RangeCost[gm.rangeLevel]));
        }
    }
}
