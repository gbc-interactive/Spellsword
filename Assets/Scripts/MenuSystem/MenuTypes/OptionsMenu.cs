using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

namespace Spellsword
{
    public class OptionsMenu : MonoBehaviour , IMenu
    {
        public GameObject _optionsMenu;
        [SerializeField] private Button _quitButton;
        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private TMP_Dropdown _resolutionDropdown;
        public Resolution[] _resolutions;

        void Start()
        {
            Disable();
            _resolutions = Screen.resolutions;
            _resolutionDropdown.ClearOptions();
            List<String> options = new List<String>();
            int currentIndex = 0;
            for(int i = 0; i < _resolutions.Length; i++){
                string option = _resolutions[i].width + " x " + _resolutions[i].height;
                options.Add(option);

                if(_resolutions[i].width == Screen.width && _resolutions[i].height == Screen.height)
                {
                    currentIndex = i; 
                }
            }
            _resolutionDropdown.AddOptions(options);
            _resolutionDropdown.value = currentIndex;
            _resolutionDropdown.RefreshShownValue();
        }
        public void Enable()
        {
            _optionsMenu.SetActive(true);
        }

        public void Disable()
        {
            _optionsMenu.SetActive(false);
        }
        public void HandleInput()
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MenuManager.Instance.ChangeMenu(FindObjectOfType<JournalMenu>());
            }
        }
        public void SetVolume(float volume)
        {
            _audioMixer.SetFloat("volume", volume);
        }
        public void SetRsolution(int index)
        {
            Resolution resolution = _resolutions[index];
            Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);

        }
        public void SetFullscreen(bool isFullscreen)
        {
            Screen.fullScreen = isFullscreen;
        }
        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }
        public void QuitGame()
        {
            //UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}