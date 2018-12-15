using UnityEngine;
using UnityEditor;
using System;

[CustomEditor(typeof(FmodEvent))]
[CanEditMultipleObjects]
public class FmodEventEditor : Editor
{
    /// Target class of editor
    private FmodEvent m_fmodEVent;
    /// Used to chack if the eventpath is changed
    private String m_oldEventPath;
    /// Slider value of parameter value
    private float[] m_sliderValue;

    public void OnEnable()
    {
        if (m_oldEventPath == null)
        {
            return;
        }

        m_fmodEVent = (FmodEvent)target;
        m_oldEventPath = m_fmodEVent.EventPath;
        InitVariable();
    }

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        EditorGUILayout.BeginVertical();

        if (m_fmodEVent == null)
        {
            Debug.LogWarning("aaaaaaaaaaaaaa-----------------");

            return;
        }

        ///Check if event path is changed
        if (m_oldEventPath != m_fmodEVent.EventPath)
            InitVariable();

        EditorGUILayout.Space();
        ///Create a slider for all parameter
        ShowParameterSlider();

        EditorGUILayout.Space();
        #region Test-Button
            EditorGUILayout.BeginHorizontal();
            if (GUILayout.Button("Build Event"))
            {
                m_fmodEVent.InitFmodEvent();
                InitVariable();
                m_fmodEVent.StopAudio();
            }

            if (GUILayout.Button("PlayAudio"))
            {
                m_fmodEVent.PlayAudio();
                EditorUtility.SetDirty(m_fmodEVent);
            }

            if (GUILayout.Button("StopAudio"))
            {
                m_fmodEVent.StopAudio();
            }
            EditorGUILayout.EndHorizontal();
    
        #endregion
        EditorGUILayout.EndVertical();
    }

    private void ShowParameterSlider()
    {
        if (m_sliderValue.Length == 0)
        {
            EditorGUILayout.LabelField("This event has no Parameter");
            return;
        }

        for (int i = 0; i < m_sliderValue.Length; i++)
        {
            EditorGUILayout.BeginHorizontal();
            EditorGUILayout.LabelField(m_fmodEVent.ParameterInfo[i].ParameterName);
            m_sliderValue[i] = EditorGUILayout.Slider(m_sliderValue[i], m_fmodEVent.ParameterInfo[i].MinIndex, m_fmodEVent.ParameterInfo[i].MaxIndex);
            EditorGUILayout.EndHorizontal();
            m_fmodEVent.ChangeParameter(i, m_sliderValue[i]);
            EditorUtility.SetDirty(m_fmodEVent);
        }
    }

    private void InitVariable()
    {
        m_fmodEVent.StopAudio();

        m_fmodEVent.InitFmodEvent();

        m_oldEventPath = m_fmodEVent.EventPath;

        m_sliderValue = new float[m_fmodEVent.ParameterInfo.Length];
        for (int i = 0; i < m_sliderValue.Length; i++)
        {
            m_sliderValue[i] = m_fmodEVent.ParameterInfo[i].Value;
        }
    }

}
