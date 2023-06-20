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
    /// Simple class to calculate the speed of an object in the game.
    /// e.g. Calculate the current speed of the wand or some object held by it.
    /// </summary>
    public class SpeedCalculator : MonoBehaviour
    {
        /// <summary>
        /// The last position.
        /// </summary>
        private Vector3 _lastPosition = Vector3.zero;
        /// <summary>
        /// The current speed.
        /// </summary>
        private float _speed = 0f;

        /// <summary>
        /// Make the speed accessible.
        /// </summary>
        public float Speed { get => _speed; }

        /// <summary>
        /// Calculate the speed every physics update.
        /// </summary>
        private void FixedUpdate()
        {
            _speed = (transform.position - _lastPosition).magnitude;
            _lastPosition = transform.position; // Store the position in the current update.
        }
    }
}