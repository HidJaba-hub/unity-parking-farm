using System;
using Enums;
using ParkingObjects;
using UnityEngine;

namespace Managers
{
    public class InputController : MonoBehaviour
    {
        private Vector2 mousePosFirst;
        private Vector2 mouseVector;

        private Orientation orientation;
        private bool isPositive;

        private Camera _mainCamera;
        private ParkingElement _selectedElement;

        [SerializeField] private LayerMask _gameObjectMask; 

        
        private void Start()
        {
            _mainCamera = Camera.main;
        }

        private void Update()
        {
            if (ExecuteSelectedCar()) return;

            ActOnSelectedCar();
        }

        private void ActOnSelectedCar()
        {
            if (Input.GetMouseButtonUp(0))
            {
                mouseVector.x = Input.mousePosition.x - mousePosFirst.x;
                mouseVector.y = Input.mousePosition.y - mousePosFirst.y;
                CalculateDirection(mouseVector.x, mouseVector.y);
                MoveSelectedCar();
            }
        }

        private bool ExecuteSelectedCar()
        {
            if (Input.GetMouseButtonDown(0))
            {
                var ray = _mainCamera.ScreenPointToRay(Input.mousePosition);

                if (Physics.Raycast(ray, out var hit, 100.0f, _gameObjectMask))
                {
                    if (!hit.transform.TryGetComponent(out _selectedElement))
                    {
                        return true;
                    }
                }

                mousePosFirst = Input.mousePosition;
            }

            return false;
        }

        private void CalculateDirection(float x, float y)
        {
            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                orientation = Orientation.Horizontal;
                isPositive = !(x < 0);
            }
            else
            {
                orientation = Orientation.Vertical;
                isPositive = !(y < 0);
            }
        }

        private void MoveSelectedCar ()
        {
            if (_selectedElement != null)
                _selectedElement.Act(orientation, isPositive);
            _selectedElement = null;
        }

    }
}
