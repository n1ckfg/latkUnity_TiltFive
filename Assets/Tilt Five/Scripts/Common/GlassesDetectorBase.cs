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
using TiltFive;
using UnityEngine;
using Display = TiltFive.Display;

namespace TiltFiveDemos
{
    /// <summary>
    /// Detects whether the glasses are connected.
    /// </summary>
    public class GlassesDetectorBase : MonoBehaviour
    {
        /// <summary>
        /// Create a flag to avoid checking repeatedly.
        /// </summary>
        protected bool _glassesAvailable = true;

        protected DescriptionPanel _instructions;

        protected bool _usingGlasses = false;

        // Start is called before the first frame update
        protected virtual void Start()
        {
            _instructions = FindObjectOfType<DescriptionPanel>();
            // Flag should be the opposite of the starting state of the glasses, so that the first event can always occur.
            if (Display.Instance != null)
            {
                _glassesAvailable = !Display.GetGlassesAvailability();
            }

            // Check the glasses the first time, but force to switch to glasses on the first frame, if the glasses are connected.
            CheckGlasses(true);
        }

        // Update is called once per frame
        protected virtual void Update()
        {
            // Force automatically switching if instructions are opened.
            // The demo behaviour should only work during the demo (but this decision could be removed if used for other purposes).
            if (_instructions.gameObject.activeSelf) CheckGlasses(true);
            else CheckGlasses(false);
        }

        /// <summary>
        /// Check the current state of the glasses and call the proper event.
        /// </summary>
        private void CheckGlasses(bool pForce)
        {
            // Make sure that the TiltFive plugin is enabled and available.
            if (Display.Instance != null)
            {
                if (Display.GetGlassesAvailability())
                {
                    OnGlassesAvailable(pForce);
                }
                else
                {
                    OnGlassesUnavailable();
                }
            }
            else
            {
                OnGlassesUnavailable();
            }
        }

        /// <summary>
        /// On Glasses Available event.
        /// </summary>
        private void OnGlassesAvailable(bool pForce = false)
        {
            if(!_glassesAvailable)
            {
                _glassesAvailable = true;
                DoGlassesAvailable(pForce);
            }
        }

        /// <summary>
        /// On Glasses Unavailable event.
        /// </summary>
        private void OnGlassesUnavailable()
        {
            if (_glassesAvailable)
            {
                _glassesAvailable = false;
                DoGlassesUnavailable();
            }
        }

        /// <summary>
        /// Perform the glasses available action.
        /// </summary>
        protected virtual void DoGlassesAvailable(bool pForce = false)
        {

        }

        /// <summary>
        /// Perform the glasses unavailable action.
        /// </summary>
        protected virtual void DoGlassesUnavailable()
        {
            _usingGlasses = false;
        }
    }
}
