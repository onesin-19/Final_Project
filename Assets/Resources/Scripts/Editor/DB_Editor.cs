using System;
using System.Runtime.Remoting.Messaging;
using UnityEditor;
using UnityEngine;

namespace DefaultNamespace
{
    [UnityEditor.CustomEditor(typeof(DB_Manager))]
    public class DB_Editor :UnityEditor.Editor
    {
        private DB_Manager manager;
        
        private void OnEnable()
        {
            manager=target as DB_Manager;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            manager.password = EditorGUILayout.PasswordField("password", manager.password);
        }
    }
}