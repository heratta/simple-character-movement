using UnityEngine;

namespace helab
{
    public class SimpleCharacter : MonoBehaviour
    {
        private const float GravitationalAcceleration = 9.8f;

        [SerializeField] private float thrust = 1.0f;

        [SerializeField] private float turnSpeed = 1.0f;

        [SerializeField] private CharacterController characterController;

        [SerializeField] private SimpleCamera simpleCamera;

        [SerializeField] private Vector3 viewDirection = Vector3.forward;

        [SerializeField] private float gravityVelocity;

        private void Start()
        {
            SetRotation(viewDirection);
        }

        private void Update()
        {
            UpdateGravity();

            var moveDirection = simpleCamera.TransoformToCameraSpace(Helper.GetMoveDirectionByInput());
            UpdateMovement(moveDirection);

            if (0f < moveDirection.magnitude)
            {
                UpdateDirection(moveDirection);
            }
        }

        private void UpdateGravity()
        {
            if (characterController.isGrounded)
            {
                gravityVelocity = 0f;
            }
            else
            {
                gravityVelocity += GravitationalAcceleration * Time.deltaTime;
            }
        }

        private void UpdateMovement(Vector3 moveDirection)
        {
            var deltaMove = Vector3.zero;
            deltaMove += Vector3.down * gravityVelocity;

            if (0f < moveDirection.magnitude)
            {
                if (Physics.Raycast(characterController.transform.position, Vector3.down, out var hit))
                {
                    var right = Vector3.Cross(hit.normal, moveDirection);
                    var forward = Vector3.Cross(right, hit.normal);
                    deltaMove += forward.normalized * thrust;
                }
            }

            characterController.Move(deltaMove * Time.deltaTime);
        }

        private void UpdateDirection(Vector3 moveDirection)
        {
            viewDirection = Vector3.Lerp(viewDirection, moveDirection, turnSpeed * Time.deltaTime);
            SetRotation(viewDirection);
        }

        private void SetRotation(Vector3 direction)
        {
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }
}
