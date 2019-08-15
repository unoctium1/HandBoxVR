﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HandVR
{
    namespace Core
    {
        public class GamePanel : MonoBehaviour
        {
            [SerializeField]
            private Button startButton;
            [SerializeField]
            private Button stopButton;
            [SerializeField]
            private InputField numMinutes;
            [SerializeField]
            private Text label;
            [SerializeField]
            private Text errorText;

            private ITest _test;
            private GameObject _testPrefab;

            private GameObject _testInScene;

            public void Init(GameObject testPrefab, string text)
            {
                _testPrefab = testPrefab;
                label.text = text;
                startButton.onClick.AddListener(StartPressed);
                stopButton.onClick.AddListener(StopPressed);
                errorText.gameObject.SetActive(false);
            }

            public void StartPressed()
            {
                bool isFloat = float.TryParse(numMinutes.text, out float numMins);
                if (isFloat)
                {
                    StartTest(numMins);
                }
                else
                {
                    errorText.gameObject.SetActive(true);
                }


            }

            public void StartTest(float minutes)
            {
                errorText.gameObject.SetActive(false);
                stopButton.interactable = true;
                startButton.interactable = false;
                _testInScene = Instantiate(_testPrefab);
                _test = _testInScene.GetComponent(typeof(ITest)) as ITest;
                _test.SetNumMinutes(minutes);
                StartCoroutine(_test.StartTest());
            }

            public bool IsRunning()
            {
                return _test.IsRunning;
            }

            public void StopPressed()
            {
                stopButton.interactable = false;
                startButton.interactable = true;
                _test.StopTest();
                _test = null;
                Destroy(_testInScene);
            }




        }
    }
}