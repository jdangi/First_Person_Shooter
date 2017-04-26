using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace FirstPersonController
{
    [RequireComponent(typeof (CharacterController))]
    [RequireComponent(typeof (AudioSource))]
    public class FPCNCT : MonoBehaviour
    {
        [SerializeField] private bool isWalking;
        [SerializeField] private float walkingSpeed;
        [SerializeField] private float runningSpeed;
        [SerializeField] [Range(0f, 1f)] private float runStepLen;
        [SerializeField] private float jumpingSpeed;
        [SerializeField] private float groundingForce;
        [SerializeField] private float gravityMux;
        [SerializeField] private bool useFOV;
        [SerializeField] private FieldOfView m_FovKick = new FieldOfView();
        [SerializeField] private bool useHeadBob;
        [SerializeField] private GroundedHeadBob headBob = new GroundedHeadBob();
        [SerializeField] private JumpingHeadBob jumpBob = new JumpingHeadBob();
        [SerializeField] private float stepInterval;
        [SerializeField] private AudioClip[] footstepAudios;    // an array of footstep sounds that will be randomly selected from.
        [SerializeField] private AudioClip jumpingAudio;           // the sound played when character leaves the ground.
        [SerializeField] private AudioClip landingSound;           // the sound played when character touches back on ground.

        private Camera m_Camera;
        private bool m_Jump;
        private float m_YRotation;
        private Vector2 m_Input;
        private Vector3 m_MoveDir = Vector3.zero;
        private CharacterController m_CharacterController;
        private CollisionFlags m_CollisionFlags;
        private bool m_PreviouslyGrounded;
        private Vector3 m_OriginalCameraPosition;
        private float m_StepCycle;
        private float m_NextStep;
        private bool m_Jumping;
        private AudioSource m_AudioSource;


		//Mouse Movement
		public float XSensitivity = 2f;
		public float YSensitivity = 2f;
		public bool clampVerticalRotation = true;
		public float MinimumX = -90F;
		public float MaximumX = 90F;
		public bool smooth;
		public float smoothTime = 5f;

		private Quaternion m_CharacterTargetRot;
		private Quaternion m_CameraTargetRot;
        // Use this for initialization
        private void Start()
        {
		//	Cursor.visible = false;
			Cursor.lockState = CursorLockMode.Locked;

            m_CharacterController = GetComponent<CharacterController>();
            m_Camera = Camera.main;
            m_OriginalCameraPosition = m_Camera.transform.localPosition;
            m_FovKick.Setup(m_Camera);
            headBob.Setup(m_Camera, stepInterval);
            m_StepCycle = 0f;
            m_NextStep = m_StepCycle/2f;
            m_Jumping = false;
            m_AudioSource = GetComponent<AudioSource>();

			//Initalize Mouse Look Components
			m_CharacterTargetRot = transform.localRotation;
			m_CameraTargetRot = m_Camera.transform.localRotation;
        }


        // Update is called once per frame
        private void Update()
        {
            RotateView();
            // the jump state needs to read here to make sure it is not missed
            if (!m_Jump)
            {
                m_Jump = Input.GetButtonDown("Jump");
            }

            if (!m_PreviouslyGrounded && m_CharacterController.isGrounded)
            {
                StartCoroutine(jumpBob.DoBobCycle());
                PlayLandingSound();
                m_MoveDir.y = 0f;
                m_Jumping = false;
            }
            if (!m_CharacterController.isGrounded && !m_Jumping && m_PreviouslyGrounded)
            {
                m_MoveDir.y = 0f;
            }

            m_PreviouslyGrounded = m_CharacterController.isGrounded;
        }


        private void PlayLandingSound()
        {
            m_AudioSource.clip = landingSound;
            m_AudioSource.Play();
            m_NextStep = m_StepCycle + .5f;
        }


        private void FixedUpdate()
        {
            float speed;
            GetInput(out speed);
            // always move along the camera forward as it is the direction that it being aimed at
            Vector3 desiredMove = transform.forward*m_Input.y + transform.right*m_Input.x;

            // get a normal for the surface that is being touched to move along it
            RaycastHit hitInfo;
            Physics.SphereCast(transform.position, m_CharacterController.radius, Vector3.down, out hitInfo,
                               m_CharacterController.height/2f);
            desiredMove = Vector3.ProjectOnPlane(desiredMove, hitInfo.normal).normalized;

            m_MoveDir.x = desiredMove.x*speed;
            m_MoveDir.z = desiredMove.z*speed;


            if (m_CharacterController.isGrounded)
            {
                m_MoveDir.y = -groundingForce;

                if (m_Jump)
                {
                    m_MoveDir.y = jumpingSpeed;
                    PlayJumpSound();
                    m_Jump = false;
                    m_Jumping = true;
                }
            }
            else
            {
                m_MoveDir += Physics.gravity*gravityMux*Time.fixedDeltaTime;
            }
            m_CollisionFlags = m_CharacterController.Move(m_MoveDir*Time.fixedDeltaTime);

            ProgressStepCycle(speed);
            UpdateCameraPosition(speed);
        }


        private void PlayJumpSound()
        {
            m_AudioSource.clip = jumpingAudio;
            m_AudioSource.Play();
        }


        private void ProgressStepCycle(float speed)
        {
            if (m_CharacterController.velocity.sqrMagnitude > 0 && (m_Input.x != 0 || m_Input.y != 0))
            {
                m_StepCycle += (m_CharacterController.velocity.magnitude + (speed*(isWalking ? 1f : runStepLen)))*
                             Time.fixedDeltaTime;
            }

            if (!(m_StepCycle > m_NextStep))
            {
                return;
            }

            m_NextStep = m_StepCycle + stepInterval;

            PlayFootStepAudio();
        }


        private void PlayFootStepAudio()
        {
            if (!m_CharacterController.isGrounded)
            {
                return;
            }
            // pick & play a random footstep sound from the array,
            // excluding sound at index 0
            int n = Random.Range(1, footstepAudios.Length);
            m_AudioSource.clip = footstepAudios[n];
            m_AudioSource.PlayOneShot(m_AudioSource.clip);
            // move picked sound to index 0 so it's not picked next time
            footstepAudios[n] = footstepAudios[0];
            footstepAudios[0] = m_AudioSource.clip;
        }


        private void UpdateCameraPosition(float speed)
        {
            Vector3 newCameraPosition;
            if (!useHeadBob)
            {
                return;
            }
            if (m_CharacterController.velocity.magnitude > 0 && m_CharacterController.isGrounded)
            {
                m_Camera.transform.localPosition =
                    headBob.DoHeadBob(m_CharacterController.velocity.magnitude +
                                      (speed*(isWalking ? 1f : runStepLen)));
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_Camera.transform.localPosition.y - jumpBob.Offset();
            }
            else
            {
                newCameraPosition = m_Camera.transform.localPosition;
                newCameraPosition.y = m_OriginalCameraPosition.y - jumpBob.Offset();
            }
            m_Camera.transform.localPosition = newCameraPosition;
        }


        private void GetInput(out float speed)
        {
            // Read input
            float horizontal = Input.GetAxis("Horizontal");
            float vertical = Input.GetAxis("Vertical");

            bool waswalking = isWalking;

#if !MOBILE_INPUT
            // On standalone builds, walk/run speed is modified by a key press.
            // keep track of whether or not the character is walking or running
            isWalking = !Input.GetKey(KeyCode.LeftShift);
#endif
            // set the desired speed to be walking or running
            speed = isWalking ? walkingSpeed : runningSpeed;
            m_Input = new Vector2(horizontal, vertical);

            // normalize input if it exceeds 1 in combined length:
            if (m_Input.sqrMagnitude > 1)
            {
                m_Input.Normalize();
            }

            // handle speed change to give an fov kick
            // only if the player is going to a run, is running and the fovkick is to be used
            if (isWalking != waswalking && useFOV && m_CharacterController.velocity.sqrMagnitude > 0)
            {
                StopAllCoroutines();
                StartCoroutine(!isWalking ? m_FovKick.FOVKickUp() : m_FovKick.FOVKickDown());
            }
        }

		Quaternion ClampRotationAroundXAxis(Quaternion q)
		{
			q.x /= q.w;
			q.y /= q.w;
			q.z /= q.w;
			q.w = 1.0f;
			
			float angleX = 2.0f * Mathf.Rad2Deg * Mathf.Atan (q.x);
			
			angleX = Mathf.Clamp (angleX, MinimumX, MaximumX);
			
			q.x = Mathf.Tan (0.5f * Mathf.Deg2Rad * angleX);
			
			return q;
		}

        private void RotateView()
        {
			float yRot = Input.GetAxis("Mouse X") * XSensitivity;
			float xRot = Input.GetAxis("Mouse Y") * YSensitivity;
			
			m_CharacterTargetRot *= Quaternion.Euler (0f, yRot, 0f);
			m_CameraTargetRot *= Quaternion.Euler (-xRot, 0f, 0f);
			
			if(clampVerticalRotation)
				m_CameraTargetRot = ClampRotationAroundXAxis (m_CameraTargetRot);
			
			if(smooth)
			{
				transform.localRotation = Quaternion.Slerp (transform.localRotation, m_CharacterTargetRot,
				                                            smoothTime * Time.deltaTime);
				m_Camera.transform.localRotation = Quaternion.Slerp (m_Camera.transform.localRotation, m_CameraTargetRot,
				                                         smoothTime * Time.deltaTime);
			}
			else
			{
				transform.localRotation = m_CharacterTargetRot;
				m_Camera.transform.localRotation = m_CameraTargetRot;
			}
        }


        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            Rigidbody body = hit.collider.attachedRigidbody;
            //dont move the rigidbody if the character is on top of it
            if (m_CollisionFlags == CollisionFlags.Below)
            {
                return;
            }

            if (body == null || body.isKinematic)
            {
                return;
            }
            body.AddForceAtPosition(m_CharacterController.velocity*0.1f, hit.point, ForceMode.Impulse);
        }
    }
}
