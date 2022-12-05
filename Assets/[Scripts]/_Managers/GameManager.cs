using UnityEngine;
using UnityEngine.SceneManagement;

namespace EKTemplate
{
    public class GameManager : MonoBehaviour
    {
        [Header("LEVEL'S"), Space(5)]
        public int level = -1;
        public int levelCount = 10;
        public int levelLoopFrom = 3;

        [Header("CURRENCY"), Space(5)]
        public int money = 0;

        public int[] SpeedCost;
        public int[] RangeCost;
        //public int[] IncomeCosts;

        //public int incomeLevel;
        public int speedLevel;
        public int rangeLevel;
        public float speedfactor;
        public float rangefactor;
        //public int levelcounter;
        //public int moneyfactor;
        //public int[] goldrealfactors;
        public float[] speedrealfactors;
        public float[] rangerealfactors;

        #region Singleton
        public static GameManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(this);
                GetDependencies();
            }
            else
            {
                DestroyImmediate(this);
            }
        }
        #endregion

        private void GetDependencies()
        {
            //if level variable set -1, game run as mobile
            //if we want to play specific level on editor, change -1 to value we want
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer || level == -1) level = DataManager.instance.level;
            money = DataManager.instance.money;
            //incomeLevel = PlayerPrefs.GetInt("income-level", 0);
            speedLevel = PlayerPrefs.GetInt("speed-level", 0);
            rangeLevel = PlayerPrefs.GetInt("range-level", 0);
        }

        #region DataOperations
        public void AddMoney(int amount)
        {
            money += amount;
            DataManager.instance.SetMoney(money);
        }

        public void LevelUp()
        {
            DataManager.instance.SetLevel(++level);
        }
        #endregion

        #region SceneOperations
        public void RestartScene()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void OpenScene(int sceneIndex)
        {
            SceneManager.LoadScene(sceneIndex);
        }

        public void OpenScene(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
        #endregion
    }
}