using System.Collections.Generic;
using UnityEngine;

namespace _Project._Scripts.UI
{
    [CreateAssetMenu(menuName = "Project/UI/Popup Text Config", fileName = "PopupTextConfig", order = -1000)]
    public class PopupTextConfig : ScriptableObject
    {
        [SerializeField] private List<PopupTextForEnum> _popupTexts;

        private Dictionary<PopupEnum, string> _popupTextDictionary = new();

        public void Init()
        {
            foreach (var item in _popupTexts)
            {
                _popupTextDictionary.Add(item.PopupEnum, item.Text);
            }
        }

        public string GetText(PopupEnum popupEnum)
        {
            return _popupTextDictionary[popupEnum];
        }
    }
}