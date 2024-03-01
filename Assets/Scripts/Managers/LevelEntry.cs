using System;
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

        [SerializeField] private string _nextLevelName;
        
        private void Awake()
        {
            Time.timeScale = 1;
            Application.targetFrameRate = 60;
        
            FindCars();
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
            SceneManager.LoadScene(_nextLevelName);
        }

        public void LoadRunner()
        {
            DOTween.KillAll();
            
            PlayerPrefs.SetString("NextScene", _nextLevelName);
            PlayerPrefs.SetString("PrevScene", SceneManager.GetActiveScene().name);
            
            SceneManager.LoadScene("RunnerScene");
        }
        public void ResetLevel()
        {
            DOTween.KillAll();
            
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
