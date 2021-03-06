﻿using UnityEngine;
using FMOD.Studio;
using FMODUnity;
using System;

public enum SoundType
{
    is2D,
    is3D
}

[System.Serializable]
public struct ParameterData
{
    //Instance of parameter
    private ParameterInstance m_parameterInstance;
    private string m_parameterName;
    private float m_minIndex;
    private float m_maxIndex;
    private float m_value;

    public string ParameterName   { get { return m_parameterName; } }
    public float MinIndex         { get { return m_minIndex; } }
    public float MaxIndex         { get { return m_maxIndex; } }
    public float Value            { get { return m_value; }
                                    set { m_value = value;
                                        m_parameterInstance.setValue(Value); }
                                 }

    public ParameterData(ParameterInstance _parameterInstance, string _parameterName, float _minParameter, float _maxParameter, float _currentVale)
    {
        m_parameterInstance =   _parameterInstance;
        m_parameterName     =   _parameterName;
        m_minIndex          =   _minParameter;
        m_maxIndex          =   _maxParameter;
        m_value             =   _currentVale;
    }
}

[CreateAssetMenu(menuName = "FmodEvent/NewEvent")]
public class FmodEvent : ScriptableObject
{
    #region Private-Field

    //Event Path
    [EventRef]
    [SerializeField]
    private String m_eventPath;
    //FMOD event Instance
    [SerializeField]
    private EventInstance m_fmodEventInstance;
    //Check if has cue
    [SerializeField]
    private bool m_hasCue;
    //check if is 2D or 3D event
    [SerializeField]
    private SoundType m_soundType;
    //Minimum distance to hear this event
    [SerializeField]
    private float m_minumDistance;
    //Maximum distance to hear this event
    [SerializeField]
    private float m_maxDistance;
    //Number of instance of this event 
    [SerializeField]
    private int m_instanceCount;

    [SerializeField]
    private string m_tag;

    //Collection of all parameter on this event
    private ParameterData[] m_parameterInfo;
    #endregion

    #region Public-Field
    public bool HasCue { get { return m_hasCue; } set { m_hasCue = false; } }
    public string EventPath { get { return m_eventPath; } set { m_eventPath = value; } }
    public EventInstance FmodEventInstance { get { return m_fmodEventInstance; } set { m_fmodEventInstance = value; } }
    public bool is2D { get { return m_soundType == SoundType.is2D; } }
    public ParameterData[] ParameterInfo { get { return m_parameterInfo; } }
    #endregion

    #region Public-Method
    /// <summary>
    /// Called to initialize the fmod event amd parameters
    /// </summary>
    public void InitFmodEvent()
    {
        if (m_fmodEventInstance.hasHandle())
        {
            m_fmodEventInstance.start();
            return;
        }

        ///Check if event path is different of null
        if (m_eventPath == null)
            UnityEngine.Debug.LogError("Event path not available");

        ///Create the event
        m_fmodEventInstance = RuntimeManager.CreateInstance(m_eventPath);

        if(Application.isEditor)
            FollowTarget(Camera.main.transform);
        
        ///Get event info: is3D, hasCue, exc...
        GetEventInfo(m_fmodEventInstance);

        ///Create all parameters
        ///and Set number of parameter
        int _parameterCount;
        m_fmodEventInstance.getParameterCount(out _parameterCount);

        ///Set Lenght of ParameterInfo on inspector
        m_parameterInfo = new ParameterData[_parameterCount];
        if (_parameterCount == 0)
            return;

        ///foreach parameters Set ParameterInfo and ParameterInstances
        for (int i = 0; i < _parameterCount; i++)
        {
            ParameterInstance _currentParameter;
            m_fmodEventInstance.getParameterByIndex(i, out _currentParameter);
            m_parameterInfo[i] = GetParameterName(_currentParameter);
        }
    }

    /// <summary>
    /// Used to play a Event
    /// </summary>
    public void PlayAudio()
    {
        if (m_fmodEventInstance.hasHandle())
            m_fmodEventInstance.start();
        else
            Debug.LogWarning("Build this event befor play!");
    }

    /// <summary>
    /// Attach the fmodEvent to a transfom
    /// </summary>
    /// <param name="_target">target transfomr</param>
    public void FollowTarget(Transform _target)
    {
        RuntimeManager.AttachInstanceToGameObject(m_fmodEventInstance, _target, _target.GetComponent<Rigidbody>());
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="_position"></param>
    public void PlayOneShoot(Vector3 _position)
    {
        RuntimeManager.PlayOneShot(m_eventPath, _position);
    }

    /// <summary>
    /// Check if this fmoodevent is playing
    /// </summary>
    /// <param name="_eventInstances"></param>
    /// <returns></returns>
    public bool IsPlaying(EventInstance _eventInstances)
    {
        PLAYBACK_STATE playbackState;
        _eventInstances.getPlaybackState(out playbackState);
        return playbackState != PLAYBACK_STATE.STOPPED;
    }

    /// <summary>
    /// Used to stop a played Event
    /// </summary>
    public void StopAudio()
    {
        if (m_fmodEventInstance.hasHandle())
        {
            m_fmodEventInstance.stop(FMOD.Studio.STOP_MODE.IMMEDIATE);
        }
    }

    /// <summary>
    /// Used to change parameter
    /// </summary>
    /// <param name="_parameterName">parameter name</param>
    /// <param name="_value">next value</param>
    public void ChangeParameter(string _parameterName, float _value)
    {
        int parameterIndex = 0;
        if (HasParameter(_parameterName, out parameterIndex))
            ParameterInfo[parameterIndex].Value = _value;
    }

    /// <summary>
    /// Used to change parameter
    /// </summary>
    /// <param name="_parameterIndex">parameter index on array</param>
    /// <param name="_value">next value</param>
    public void ChangeParameter(int _parameterIndex, float _value)
    {
        if (_parameterIndex < ParameterInfo.Length)
            ParameterInfo[_parameterIndex].Value = _value;
        else
            Debug.LogError("Parameter index out of range");
    }
    #endregion

    #region Private-Method
    /// <summary>
    /// Get all important information of EventInstance
    /// </summary>
    /// <param name="eventInstance">current EvnentInstance</param>
    private void GetEventInfo(EventInstance eventInstance)
    {
        ///Create EventDescription
        EventDescription eventDescription = new EventDescription();
        eventInstance.getDescription(out eventDescription);

        ///Get min and max distance
        eventDescription.getMaximumDistance(out m_maxDistance);
        eventDescription.getMinimumDistance(out m_minumDistance);

        //USER_PROPERTY ada;
        //int userPropertyCount;
        //eventDescription.getUserPropertyCount(out userPropertyCount);
        //Debug.Log(userPropertyCount);

        //for (int i = 0; i < userPropertyCount; i++)
        //{
        //    eventDescription.getUserPropertyByIndex(i, out ada);
        //    Debug.Log(ada.name);
        //}

        ///Get number of instance enabled
        eventDescription.getInstanceCount(out m_instanceCount);

        ///Check if has cue
        eventDescription.hasCue(out m_hasCue);
        
        ///Check if is 3D or 2D
        bool _is3D = false;
        eventDescription.is3D(out _is3D);
        if (_is3D)
            m_soundType = SoundType.is3D;
        else
            m_soundType = SoundType.is2D; 
    }

    /// <summary>
    /// Get all important information of ParameterInstance
    /// Return a parameterInfo with:
    /// -ParameterName
    /// -ParameterRange(start,end)
    /// </summary>
    /// <param name="instance">current ParameterInstance</param>
    /// <returns></returns>
    private ParameterData GetParameterName(ParameterInstance instance) 
    {
        ///Create the parameter description
        ///useflue to get all information
        PARAMETER_DESCRIPTION desc = new PARAMETER_DESCRIPTION();
        instance.getDescription(out desc);
        ParameterData parameterInfo = new ParameterData(instance, desc.name, desc.minimum, desc.maximum, desc.defaultvalue);
        return parameterInfo;
    }

    /// <summary>
    /// Check if this parameter exist
    /// get out the parameter index if exist
    /// </summary>
    /// <param name="_name">parameter name</param>
    /// <param name="index">parameter index out</param>
    /// <returns></returns>
    private bool HasParameter(string _name, out int index)
    {
        for (int i = 0; i < m_parameterInfo.Length; i++)
        {
            if (m_parameterInfo[i].ParameterName == _name)
            {
                index = i;
                return true;
            }
        }
        Debug.LogError("Parameter dosen't exist");
        index = -1;
        return false;
    }
    #endregion
}