using System;
using System.Collections;
using UnityEngine;

namespace HeroComponents
{
  [RequireComponent(typeof(CharacterController), typeof(Hero))]
  public class Movement : MonoBehaviour
  {
    private VariableJoystick _input;
    private CharacterController _characterController;
    private float _movementSpeed;
    
    private void Start()
    {
      _characterController = GetComponent<CharacterController>();
      Hero component = GetComponent<Hero>();
      
      _movementSpeed = component.MovementSpeed;
      _input = component.Input;
    }

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
      _characterController.Move(_movementSpeed * movementVector * Time.deltaTime);
    }
  }
}