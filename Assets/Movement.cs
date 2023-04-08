using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public float MovementSpeed = 10f;
        
        public CharacterController _characterController;
        public VariableJoystick _input;

        private void Update()
        {
          Vector3 axis = new Vector3(_input.Horizontal, 0, _input.Vertical);
          Vector3 movementVector = Vector3.zero;
          if (axis.sqrMagnitude > 0.01f)
          {
            movementVector.y = 0;
            movementVector = axis;
            movementVector.Normalize();
            transform.forward = movementVector;
            transform.eulerAngles = new Vector3 (0, transform.eulerAngles.y, 0);
    
          }
          movementVector += Physics.gravity;
          _characterController.Move(MovementSpeed * movementVector * Time.deltaTime);
          
        }
}
