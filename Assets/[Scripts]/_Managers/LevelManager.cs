using UnityEngine;
using UnityEngine.Events;

namespace EKTemplate
{
    public class LevelManager : MonoBehaviour
    {
        [HideInInspector] public UnityEvent startEvent = new UnityEvent();
        [HideInInspector] public EndGameEvent endGameEvent = new EndGameEvent();

        #region Singleton
        public static LevelManager instance = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        #endregion

        private void Start() => ConstructLevel();
        private void ConstructLevel()
        {
            int _level = GameManager.instance.level;
            while (_level > GameManager.instance.levelCount)
                _level = _level - GameManager.instance.levelCount + (GameManager.instance.levelLoopFrom - 1);

            Instantiate(Resources.Load<GameObject>("levels/level-" + _level));
        }
        public void StartGame()
        {
            startEvent.Invoke();
        }
        public void Success()
        {
            if (Application.platform == RuntimePlatform.Android || Application.platform == RuntimePlatform.IPhonePlayer) GameManager.instance.LevelUp();
            endGameEvent.Invoke(true);
        }
        public void Fail()
        {
            endGameEvent.Invoke(false);
        }
    }
    public class EndGameEvent : UnityEvent<bool> { }
}