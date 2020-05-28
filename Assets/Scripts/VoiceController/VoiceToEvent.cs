using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Windows.Speech;

public class VoiceToEvent : MonoBehaviour
{

    [SerializeField]
    private KeywordEvent[] _Actions;

    private Dictionary<string, UnityEvent> _KeywordEvents;

    private KeywordRecognizer _KWR;

	private void Start()
    {
        PopulateDictionary();
        _KWR = new KeywordRecognizer(_KeywordEvents.Keys.ToArray());
        _KWR.OnPhraseRecognized += (args) =>
        {
            Debug.Log($"Recognized [{args.text}] with a confidence of [{args.confidence}]\nTime taken [{DateTime.Now - args.phraseStartTime}");
            _KeywordEvents[args.text]?.Invoke();
        };
        _KWR.Start();
    }

    private void PopulateDictionary()
    {
        _KeywordEvents = new Dictionary<string, UnityEvent>(_Actions.Length);
        foreach (var item in _Actions)
        {
            _KeywordEvents[item.Keyword] = item.ActionToDo;
        }
    }

}
