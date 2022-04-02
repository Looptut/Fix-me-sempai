using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolsAndMechanics.UserInterfaceManager
{
    /// <summary>
    /// Кнопка открытия окна
    /// </summary>
    public class OpenWindowButton : AbstractButton
    {
        [SerializeField]
        private WindowData window;
        [SerializeField]
        private bool closeCurrentWindow;

        protected override void OnEnable()
        {
            base.OnEnable();
            btn.interactable = true;
        }

        public override void OnButtonClick()
        {
            if (closeCurrentWindow)
                btn.interactable = false;
            WindowsController.Instance.SetWindow(window, closeCurrentWindow);
        }
    }
}