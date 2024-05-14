using System;
using System.Collections.Generic;
using Enums;
using ParkingObjects;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Managers
{
    public class InputController : MonoBehaviour
    {
        private List<LooseConditionController> looseConditionControllers = new List<LooseConditionController>();
        private Vector2 mousePosFirst;
        private Vector2 mouseVector;

        private Orientation orientation;
        private bool isPositive;

        private Camera _mainCamera;
        private ParkingElement _selectedElement;

        [SerializeField] private LayerMask _gameObjectMask; 

        
        private void Start()
        {
            gameObject.GetComponents(looseConditionControllers);
            _mainCamera = Camera.main;
            foreach (var loose in looseConditionControllers)
            {
                loose.SetBonuses();
            }
        }

        private void Update()
        {
            if (ExecuteSelectedCar()) return;
            
            ActOnSelectedCar();
        }

        private void ActOnSelectedCar()
        {
            if (CheckUIOverlap()) return;
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
            if (CheckUIOverlap()) return false;
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

        private bool CheckUIOverlap()
        {
            return EventSystem.current.IsPointerOverGameObject();
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
            {
                if (!_selectedElement.Act(orientation, isPositive)) return;

                foreach (var loose in looseConditionControllers)
                {
                    loose.StartCondition();
                }
            }
            _selectedElement = null;
        }

    }
}
