using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Spellsword
{
    public class DialogueManager : MonoBehaviour
    {
        public static DialogueManager Instance { get; private set; }
        public GameObject _canvas;
        public TMP_Text _text;
        public TMP_Text _speaker;
        private Queue<string> _dialogue = new Queue<string>();
        private bool _started = false;
        
    
        // Start is called before the first frame update
        void Start()
        {
            //Disabled when game starts
            SetEnabled(false);
            
        }

        private void Awake()
        {
            if (Instance != null && Instance != this) 
            { 
                Destroy(this); 
            } 
            else 
            { 
                Instance = this; 
            } 
        }

        void StartDialogue(TextAsset dialogueText)
        {
            SetEnabled(true);
            ReadFile(dialogueText);
            DisplayDialogue();
            _started = true;
        }

        public void ReadDialogue(TextAsset dialogueText)
        {
            if (!_started)
            {
                StartDialogue(dialogueText);
            }
            else
            {
                DisplayDialogue();
            }
        }
        void ReadFile(TextAsset dialogueText)
        {
            string txt = dialogueText.text;

            string[] lines = txt.Split(System.Environment.NewLine.ToCharArray()); 

            foreach (string line in lines) 
            {
                if (!string.IsNullOrEmpty(line) )
                {
                    if (line.StartsWith("[")) 
                    {
                        string special = line.Substring(0, line.IndexOf(']') + 1);
                        string curr = line.Substring(line.IndexOf(']') + 1); 
                        _dialogue.Enqueue(special); 
                        _dialogue.Enqueue(curr);
                    }
                    else
                    {
                        _dialogue.Enqueue(line); 
                    }
                }
            }
            _dialogue.Enqueue("EndQueue");
        }

        void DisplayDialogue()
        {
            if (_dialogue.Count == 0 || _dialogue.Peek().Contains("EndQueue")) // special phrase to stop dialogue
            {
                _dialogue.Dequeue(); // Clear Queue
                EndDialogue();
            }
            else if (_dialogue.Peek().Contains("[NAME="))
            {
                string name = _dialogue.Peek();
                name = _dialogue.Dequeue().Substring(name.IndexOf('=') + 1, name.IndexOf(']') - (name.IndexOf('=') + 1));
                _speaker.text = name;
                DisplayDialogue(); // print the rest of this line
            }
            else
            {
                _text.text = _dialogue.Dequeue();
            }
            
        }

        void EndDialogue()
        {
            _text.text = "";
            _text.text = "";
            _dialogue.Clear();
            SetEnabled(false);
            _started = false;
        }

        void SetEnabled(bool value)
        {
            _canvas.SetActive(value);
            _text.enabled = value;
            _speaker.enabled = value;
        }
    }  
}

