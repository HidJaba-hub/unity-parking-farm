
using DG.Tweening;
using ParkingObjects;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class LevelEntry : MonoBehaviour
    {
        [SerializeField] private Transform _carsHolder;
        private int _carsAmount;
        
        [SerializeField] private string _menuScene;

        public int levelIndex;
        private void Awake()
        {
            Time.timeScale = 1;
            Application.targetFrameRate = 60;
            
            FindCars();
            SaveLevelSystem.InitializeCurrentLevel(levelIndex);
        }

        private void FindCars ()
        {
            ParkingElement[] elements = _carsHolder.GetComponentsInChildren<ParkingElement>();
            _carsAmount = elements.Length;

            foreach (var element in elements)
                element.OnCarExitAction += ReduceParkingElements;
        }

        private void ReduceParkingElements(ParkingElement crrElement)
        {
            _carsAmount -= 1;
            crrElement.OnCarExitAction -= ReduceParkingElements;
        
            if(_carsAmount == 0)
                CompleteLevel();
        }

        public UnityEvent CompleteEvent;

        private void CompleteLevel() => CompleteEvent?.Invoke();

        public void LoadNextLevel()
        {
            DOTween.KillAll();
            SaveLevelSystem.levelToSave.IsCompleted = true;
            SaveLevelSystem.SaveLevel();
            if (SceneExists(SceneManager.GetActiveScene().buildIndex + 1))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
            }
            else
            {
                LoadMenu();
            }
        }
        public static bool SceneExists(int index)
        {
            var x = SceneUtility.GetScenePathByBuildIndex(index);
            return x != "";
        }
        public void LoadRunner()
        {
            DOTween.KillAll();
            
            PlayerPrefs.SetInt("NextScene", SceneManager.GetActiveScene().buildIndex + 1);
            PlayerPrefs.SetInt("PrevScene", SceneManager.GetActiveScene().buildIndex);
            SaveLevelSystem.SaveLevel();
            
            SceneManager.LoadScene("RunnerScene");
        }
        public void ResetLevel()
        {
            DOTween.KillAll();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public static void Pause()
        {
            Time.timeScale = 0;
        }

        public static void Continue()
        {
            Time.timeScale = 1;
        }
        public void LoadMenu()
        {
            Time.timeScale = 1;
            SceneManager.LoadScene(_menuScene);
        }
    }
}
