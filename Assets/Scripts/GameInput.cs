using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        Debug.Log(obj);
    }

    public Vector2 GetMovementVectorNormalized()
    {
        //Hace lo que antes hacían los if de WASD 
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        //Para moverse siempre en la misma dirección (línea recta o diagonales)
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
