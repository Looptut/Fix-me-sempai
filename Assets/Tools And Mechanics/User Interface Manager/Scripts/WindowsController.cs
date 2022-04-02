using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ToolsAndMechanics.UserInterfaceManager
{
    /// <summary>
    /// Контроллер окон
    /// </summary>
    public class WindowsController : MonoBehaviour
    {
        public static WindowsController Instance { get; private set; } = null;

        [SerializeField]
        private Canvas canvas;
        [SerializeField]
        [Header("Создавать все окна сразу или по мере вызова")]
        private bool createAllWindows;

        [SerializeField]
        private WindowData firstWindowData;
        [SerializeField]
        private List<WindowData> windowsData;

        private Dictionary<string, Window> windowsDic = new Dictionary<string, Window>();

        private Window lastWindow;
        private Window nextWindow;
        private bool needClose;

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            string canvasName = canvas.name;
            canvas = Instantiate(canvas);
            canvas.name = canvasName;
            CreateWindow(firstWindowData);
            SetWindow(firstWindowData, false);

            if (createAllWindows)
            {
                foreach (WindowData data in windowsData)
                {
                    CreateWindow(data);
                }
            }
        }

        private void CreateWindow(WindowData data)
        {
            if (windowsDic.ContainsKey(data.Id))
            {
                Debug.LogError($"Повторяющийся Id '{data.Id}' в объекте '{data.name}'");
                return;
            }

            Window window = Instantiate(data.WindowPrefab, canvas.transform);
            window.gameObject.name = data.Id;
            window.gameObject.SetActive(false);
            windowsDic.Add(data.Id, window);
        }

        /// <summary>
        /// Установить выбранное окно
        /// </summary>
        /// <param name="data"></param>
        /// <param name="closeLastWindow"> Нужно ли закрывать предыдущее окно </param>
        public void SetWindow(WindowData data, bool closeLastWindow)
        {
            if (windowsDic.TryGetValue(data.Id, out Window window))
            {
                nextWindow = window;
                needClose = closeLastWindow;
                if (closeLastWindow)
                {
                    lastWindow.CloseWindow(SetWindow);
                }
                else
                {
                    SetWindow();
                }
            }
            else if (windowsData.Contains(data))
            {
                CreateWindow(data);
                SetWindow(data, closeLastWindow);
            }
            else
            {
                Debug.LogError($"Не найдено окно с Id '{data.Id}'");
            }
        }

        private void SetWindow()
        {
            if (lastWindow)
            {
                lastWindow.gameObject.SetActive(!needClose);
                lastWindow.IsFocus = false;
            }
            nextWindow.IsFocus = true;
            lastWindow = nextWindow;
            nextWindow.gameObject.SetActive(true);
        }
    }
}