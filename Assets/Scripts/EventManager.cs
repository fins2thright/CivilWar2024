using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;
using System;


//Re-usable Interface to make parameter classes for different event applications
public interface IEventParam { }


public class EventManager : MonoBehaviour
{

    private Dictionary<string, Action<IEventParam>> eventDictionary;

    private static EventManager eventManager;

    public static EventManager Instance
    {
        get
        {
            if (!eventManager)
            {
                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

                if (!eventManager)
                {
                    Debug.LogError("EventManager not found.  There needs to be an active EventManger script on a GameObject in your scene.");
                }
                else
                {
                    eventManager.Init();
                }
            }

            return eventManager;
        }
    }

    void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, Action<IEventParam>>();
        }
    }

    public static void StartListening(string eventName, Action<IEventParam> listener)
    {
        Action<IEventParam> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Add more event to the existing one
            thisEvent += listener;

            //Update the Dictionary
            Instance.eventDictionary[eventName] = thisEvent;
        }
        else
        {
            //Add event to the Dictionary for the first time
            thisEvent += listener;
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }

    public static void StopListening(string eventName, Action<IEventParam> listener)
    {
        Action<IEventParam> thisEvent;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            //Remove event from the existing one
            thisEvent -= listener;

            //Update the Dictionary
            Instance.eventDictionary[eventName] = thisEvent;
        }
    }

    public static void TriggerEvent(string eventName, IEventParam eventParam)
    {
        Action<IEventParam> thisEvent = null;
        if (Instance.eventDictionary.TryGetValue(eventName, out thisEvent))
        {
            thisEvent.Invoke(eventParam);
            // OR USE  instance.eventDictionary[eventName](eventParam);
        }
    }
}

//This was the old class implementation without parameters in events
//public class EventManager : MonoBehaviour
//{

//    private Dictionary<string, UnityEvent> eventDictionary;

//    private static EventManager eventManager;

//    public static EventManager instance
//    {
//        get
//        {
//            if (!eventManager)
//            {
//                eventManager = FindObjectOfType(typeof(EventManager)) as EventManager;

//                if (!eventManager)
//                {
//                    Debug.LogError("There needs to be one active EventManger script on a GameObject in your scene.");
//                }
//                else
//                {
//                    eventManager.Init();
//                }
//            }

//            return eventManager;
//        }
//    }

//    void Init()
//    {
//        if (eventDictionary == null)
//        {
//            eventDictionary = new Dictionary<string, UnityEvent>();
//        }
//    }

//    public static void StartListening(string eventName, UnityAction listener)
//    {
//        UnityEvent thisEvent = null;
//        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
//        {
//            thisEvent.AddListener(listener);
//        }
//        else
//        {
//            thisEvent = new UnityEvent();
//            thisEvent.AddListener(listener);
//            instance.eventDictionary.Add(eventName, thisEvent);
//        }
//    }

//    public static void StopListening(string eventName, UnityAction listener)
//    {
//        if (eventManager == null) return;
//        UnityEvent thisEvent = null;
//        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
//        {
//            thisEvent.RemoveListener(listener);
//        }
//    }

//    public static void TriggerEvent(string eventName)
//    {
//        UnityEvent thisEvent = null;
//        if (instance.eventDictionary.TryGetValue(eventName, out thisEvent))
//        {
//            thisEvent.Invoke();
//        }
//    }
//}