using JS;
using System.Collections.Generic;
using UnityEngine;

namespace Melozing
{
    public class PopupManager : ManualSingletonMono<PopupManager>
    {
        [SerializeField] private PopupDictionary _scrDictionary;
        [SerializeField] private Transform _defaultParent;
        [SerializeField] private Transform _highParent;
        private Dictionary<PopupName, BasePopup> _dictionScreen;
        public PopupName CurrentPopup;
        public PopupName HightPopup;

        public Transform DefaultParent { get => _defaultParent; }

        public void Start()
        {
            _dictionScreen = new Dictionary<PopupName, BasePopup>();
        }


        public GameObject GetPopup(PopupName popupName)
        {
            OnCheckScreen(popupName, ParentPopup.Default);
            CurrentPopup = popupName;
            GameObject obj = _dictionScreen[popupName].gameObject;
            return obj;
        }
        public void OnShowScreen(PopupName scr, ParentPopup parent = ParentPopup.Default)
        {
            this.OnCheckScreen(scr, parent);
            if (parent == ParentPopup.Default)
            {
                CurrentPopup = scr;
            }
            else
            {
                HightPopup = scr;
            }
            _dictionScreen[scr].OnShowScreen();
        }

        public void OnShowScreen(PopupName scr, object arg, ParentPopup parent = ParentPopup.Default)
        {
            this.OnCheckScreen(scr, parent);
            if (parent == ParentPopup.Default)
            {
                CurrentPopup = scr;
            }
            else
            {
                HightPopup = scr;
            }
            _dictionScreen[scr].OnShowScreen(arg);
        }

        public void OnShowScreen(PopupName scr, object[] args, ParentPopup parent = ParentPopup.Default)
        {
            this.OnCheckScreen(scr, parent);
            if (parent == ParentPopup.Default)
            {
                CurrentPopup = scr;
            }
            else
            {
                HightPopup = scr;
            }
            if (!_dictionScreen[scr].IsShow && !_dictionScreen[scr].IsShowing)
                _dictionScreen[scr].OnShowScreen(args);
        }

        public void OnCloseScreen(PopupName scr)
        {
            if (_dictionScreen.ContainsKey(scr))
            {
                _dictionScreen[scr].OnCloseScreen();
            }
            if (CurrentPopup == scr)
                CurrentPopup = PopupName.None;
            else if (HightPopup == PopupName.None)
                HightPopup = PopupName.None;
        }

        public void OnDeActiveScreen(PopupName scr)
        {
            _dictionScreen[scr].OnDeActived();
        }

        public bool IsPopupShowing(PopupName scr)
        {
            if (!_dictionScreen.ContainsKey(scr))
                return false;
            else
            {
                if (_dictionScreen[scr].IsShow || _dictionScreen[scr].IsShowing)
                    return true;
                else
                    return false;
            }
        }

        private void OnCheckScreen(PopupName scr, ParentPopup parent)
        {
            if (!_dictionScreen.ContainsKey(scr))
            {
                BasePopup ctrl = this.OnCreateScreen(scr, parent);
                _dictionScreen.Add(scr, ctrl);
            }

            if (parent != ParentPopup.Hight && CurrentPopup != PopupName.None && CurrentPopup != scr)
            {
                if (_dictionScreen[scr].IsShow || _dictionScreen[scr].IsShowing)
                    OnCloseScreen(CurrentPopup);
                CurrentPopup = PopupName.None;
            }
        }

        private BasePopup OnCreateScreen(PopupName scr, ParentPopup parent = ParentPopup.Default)
        {
            GameObject prfScr = Resources.Load<GameObject>($"UI/{_scrDictionary[scr]}");
            GameObject instance = Instantiate(prfScr, parent == ParentPopup.Default ? _defaultParent : _highParent);
            instance.transform.localPosition = Vector3.zero;
            instance.transform.localScale = Vector3.one;
            BasePopup scrCtrl = instance.GetComponent<BasePopup>();
            return scrCtrl;
        }

        public void CheckResumeGame()
        {
            if (_dictionScreen == null)
            {
                return;
            }
            foreach (KeyValuePair<PopupName, BasePopup> popup in _dictionScreen)
            {
                if (popup.Value.IsShow || popup.Value.IsShowing)
                    return;
            }
        }

        public void CloseCurrentPopup()
        {
            OnDeactiveAllScreen();
        }

        private void OnDeactiveAllScreen(PopupName scr = PopupName.None)
        {
            OnCloseScreen(HightPopup);
            OnCloseScreen(CurrentPopup);
            foreach (KeyValuePair<PopupName, BasePopup> popup in _dictionScreen)
            {
                if (popup.Value.IsShow && scr != popup.Key && popup.Key != HightPopup && popup.Key != CurrentPopup)
                    popup.Value.OnCloseScreen();
            }
            CurrentPopup = PopupName.None;
            HightPopup = PopupName.None;
        }
    }
}