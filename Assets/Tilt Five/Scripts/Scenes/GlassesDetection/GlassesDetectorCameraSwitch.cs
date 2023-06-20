using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TiltFiveDemos
{
    /// <summary>
    /// Perform switching cameras on glasses availability.
    /// Inherited from GlassesDetectorBase().
    /// </summary>
    public class GlassesDetectorCameraSwitch : GlassesDetectorBase
    {
        /// <summary>
        /// The TiltFive Camera.
        /// </summary>
        [SerializeField]
        private GameObject _tiltFiveCamera;

        /// <summary>
        /// The backup camera for when the glasses are unavailable.
        /// </summary>
        [SerializeField]
        private GameObject _backupCamera;


        /// <summary>
        /// Switches to glasses camera when glasses are avaiable.
        /// </summary>
        protected override void DoGlassesAvailable(bool pForce = false)
        {
            _usingGlasses = true;
            _tiltFiveCamera.SetActive(true);
            _backupCamera.SetActive(false);

            base.DoGlassesAvailable(pForce);
        }

        /// <summary>
        /// Switches to backup camera when glasses are unavailable.
        /// </summary>
        protected override void DoGlassesUnavailable()
        {
            _usingGlasses = false;
            _tiltFiveCamera.SetActive(false);
            _backupCamera.SetActive(true);

            base.DoGlassesUnavailable();
        }
    }
}
