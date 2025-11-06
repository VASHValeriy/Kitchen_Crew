/*
    =========================================
    🧭 CHEATSHEET: ComponentActions
    Назначение: лёгкий способ добавить callback-методы к GameObject без создания отдельного MonoBehaviour.
    Особенности:
                - Позволяет динамически назначать функции на события объекта
                - События: OnDestroy, OnEnable, OnDisable, Update
                - Можно использовать для дебага, таймеров, временных объектов
    =========================================
*/

using System;
using UnityEngine;

namespace VashValeriy {

    public class ComponentActions : MonoBehaviour {

        public Action OnDestroyFunc;
        public Action OnEnableFunc;
        public Action OnDisableFunc;
        public Action OnUpdate;

        void OnDestroy() {
            if (OnDestroyFunc != null) OnDestroyFunc();
        }
        void OnEnable() {
            if (OnEnableFunc != null) OnEnableFunc();
        }
        void OnDisable() {
            if (OnDisableFunc != null) OnDisableFunc();
        }
        void Update() {
            if (OnUpdate != null) OnUpdate();
        }


        public static void CreateComponent(Action OnDestroyFunc = null, Action OnEnableFunc = null, Action OnDisableFunc = null, Action OnUpdate = null) {
            GameObject gameObject = new GameObject("ComponentActions");
            AddComponent(gameObject, OnDestroyFunc, OnEnableFunc, OnDisableFunc, OnUpdate);
        }
        public static void AddComponent(GameObject gameObject, Action OnDestroyFunc = null, Action OnEnableFunc = null, Action OnDisableFunc = null, Action OnUpdate = null) {
            ComponentActions componentFuncs = gameObject.AddComponent<ComponentActions>();
            componentFuncs.OnDestroyFunc = OnDestroyFunc;
            componentFuncs.OnEnableFunc = OnEnableFunc;
            componentFuncs.OnDisableFunc = OnDisableFunc;
            componentFuncs.OnUpdate = OnUpdate;
        }
    }

}