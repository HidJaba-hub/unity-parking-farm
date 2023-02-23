using System;
using System.Collections;
using DG.Tweening;
using Gui.Views;
using UnityEngine;

namespace Gui.ModelViews
{
    [Serializable]
    public class DialogueBoxViewModel
    {
        [SerializeField] private DialogueBoxView _view;
        [SerializeField] private float _charAppearTimeDelay = 0.05f;
        [SerializeField] private float _boxInAppearTimeDelay = 0.25f;
        [SerializeField] private float _boxOutAppearTimeDelay = 0.1f;

        public void ShowText(Vector3 position, string text)
        {
            _view.message.text = "";
            _view.gameObject.SetActive(true);
            _view.transform.position = position;

            _view.transform.localScale = Vector3.zero;
            _view.transform.DOScale(Vector3.one, _boxInAppearTimeDelay)
                .SetEase(Ease.InCubic)
                .OnComplete(() =>
            {
                _view.StartCoroutine(ShowMessage(text));
            });
        }

        private IEnumerator ShowMessage(string text)
        {
            foreach (char c in text)
            {
                _view.message.text += c;
                yield return new WaitForSeconds(_charAppearTimeDelay);
            }
        }

        public void Hide()
        {
            _view.gameObject.SetActive(false);
            _view.transform.DOScale(Vector3.zero, _boxOutAppearTimeDelay)
                .SetEase(Ease.OutCubic);
        }
    }
}