﻿// ----------------------------------------------------------------------------
// The MIT License
// LeopotamGroupLibrary https://github.com/Leopotam/LeopotamGroupLibraryUnity
// Copyright (c) 2012-2017 Leopotam <leopotam@gmail.com>
// ----------------------------------------------------------------------------

using UnityEngine;

namespace LeopotamGroup.Animation {
    /// <summary>
    /// Set Animator trigger parameter to new state on node exit.
    /// </summary>
    public sealed class SetTriggerOnStateExit : StateMachineBehaviour {
        [SerializeField]
        string _triggerName = null;

        [SerializeField]
        bool _triggerValue = false;

        int _fieldHash = -1;

        public override void OnStateExit (Animator animator, AnimatorStateInfo stateInfo, int layerIndex) {
            base.OnStateExit (animator, stateInfo, layerIndex);
            if (_fieldHash == -1) {
#if UNITY_EDITOR
                if (string.IsNullOrEmpty (_triggerName)) {
                    Debug.LogWarning ("Trigger field name is empty", animator);
                    return;
                }
#endif
                _fieldHash = Animator.StringToHash (_triggerName);
            }

            if (_triggerValue) {
                animator.SetTrigger (_fieldHash);
            } else {
                animator.ResetTrigger (_fieldHash);
            }
        }
    }
}