using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

namespace Project2.General
{
    public delegate void CamStateBehaviour();
    public class CameraManager : MonoBehaviour
    {
        #region Variables
        private CamStateBehaviour _camStateBehaviour;
        private CameraState _camState;
        public CameraState CamState
        {
            get { return _camState; }
            set
            {
                _camState = value;
                StateBehaviour();
            }
        }
        

        public CinemachineBrain BrainCam;
        public CinemachineVirtualCamera MainCamera;
        public CinemachineVirtualCamera WinCamera;
        public Animator WinCameraAnimator;
        #endregion
        void Start()
        {
            LevelManager.OnLevelStarted += StopCamera;
        }

        private void OnDisable() {
            LevelManager.OnLevelStarted -= StopCamera;
        }

        private void StateBehaviour()
        {
            _camStateBehaviour = null;
            _camStateBehaviour = CamState switch
            {
                CameraState.Follow => FollowState,
                CameraState.Win => WinState,
                CameraState.Death => DeathState,
                _ => FollowState
            };

            _camStateBehaviour();

        }

        void ChangeCamPriority(CameraState camState)
        {
            switch (camState)
            {
                case CameraState.Follow:
                    MainCamera.Priority = 10;
                    WinCamera.Priority = 1;
                    break;
                case CameraState.Win:
                    MainCamera.Priority = 1;
                    WinCamera.Priority = 10;
                    break;
                default:
                    MainCamera.Priority = 10;
                    WinCamera.Priority = 1;
                    break;
            }
        }

        private void FollowState()
        {
            ChangeCamPriority(CameraState.Follow);
        }

        private void WinState()
        {
            ChangeCamPriority(CameraState.Win);
            StartCoroutine(DelayedAnim(BrainCam.m_DefaultBlend.BlendTime));
        }

        private void DeathState()
        {
            ChangeCamPriority(CameraState.Follow);
            StartCoroutine(DelayedFollowCancel(1.5f));
        }

        IEnumerator DelayedAnim(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            WinCameraAnimator.SetBool("Rotate", true);
        }

        IEnumerator DelayedFollowCancel(float delayTime)
        {
            yield return new WaitForSeconds(delayTime);

            MainCamera.Follow = null;
        }

        void StopCamera(){
            WinCameraAnimator.SetBool("Rotate", false);
        }
    }

    public enum CameraState
    {
        Follow,
        Win,
        Death
    }
}

