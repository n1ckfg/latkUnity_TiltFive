/*
 * Copyright (C) 2020 Tilt Five, Inc.
 *
 * Licensed under the Apache License, Version 2.0 (the "License");
 * you may not use this file except in compliance with the License.
 * You may obtain a copy of the License at
 *
 *      http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS,
 * WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 * See the License for the specific language governing permissions and
 * limitations under the License.
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Repeating sound after collision. Inherits from Sound Collision.
    /// </summary>
    public class SoundCollisionRepeating : SoundCollision
    {
        /// <summary>
        /// The bpm of the repeating sound.
        /// </summary>
        [SerializeField]
        private float _bpm = 90f;

        /// <summary>
        /// Whether to start toggled or not.
        /// </summary>
        [SerializeField]
        private bool _startToggled = true;

        /// <summary>
        /// Whether the sound is toggled.
        /// </summary>
        protected bool _toggled = false;

        /// <summary>
        /// Coroutine of the repeating sound.
        /// </summary>
        private Coroutine _repeatSoundCoroutine;

        protected override void Start()
        {
            StartCoroutine(DelayedToggleCoroutine(_startToggled));

            base.Start();
        }

        /// <summary>
        /// On trigger enter, make sure the wand is touching the object and play the sound.
        /// </summary>
        /// <param name="other"></param>
        protected override void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag(WAND_COLLIDER_TAG))
            {
                AddParticles(other.transform.position);
                _animator.SetTrigger(HIT_ANIMATOR_TRIGGER); // Trigger the animation in the animator.

                _toggled = !_toggled;
                OnToggle();
            }
        }

        /// <summary>
        /// Toggle after a random amount of seconds between 0 and 3.
        /// </summary>
        /// <param name="pToggle"></param>
        /// <returns></returns>
        private IEnumerator DelayedToggleCoroutine(bool pToggle)
        {
            float waitTime = Random.Range(0f,3f);

            yield return new WaitForSeconds(waitTime);

            Toggle(pToggle);
        }

        /// <summary>
        /// Toggle the sound with a specific state.
        /// </summary>
        /// <param name="pToggle">The </param>
        public void Toggle(bool pToggle)
        {
            _toggled = pToggle;
            OnToggle();
        }

        /// <summary>
        /// Stop the sound and stop all coroutines on toggle.
        /// </summary>
        public void OnToggle()
        {
            if (_toggled) _repeatSoundCoroutine = StartCoroutine(RepeatSoundCoroutine());
            else
            {
                _audioSource.Stop();
                StopCoroutines();
            }

        }

        /// <summary>
        /// Play the sound until the player turns off.
        /// </summary>
        /// <returns></returns>
        private IEnumerator RepeatSoundCoroutine()
        {
            _audioSource.Play();
            while (_toggled)
            {
                _animator.SetTrigger(HIT_ANIMATOR_TRIGGER); // Trigger the animation in the animator.

                yield return new WaitForSeconds(60f/_bpm);

                if (!_toggled) break;
            }
        }

        /// <summary>
        /// Stop all coroutines.
        /// </summary>
        private void StopCoroutines()
        {
            if(_repeatSoundCoroutine != null)
            {
                StopCoroutine(_repeatSoundCoroutine);
                _repeatSoundCoroutine = null;
            }
        }
    }
}
