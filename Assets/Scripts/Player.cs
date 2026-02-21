using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private float interactDistance = 2f;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDir;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.9f;
        //Testeado desde el editor
        float playerHeight = 4f;
        //Si no pega con nada, se puede mover
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, moveDistance);

        if (!canMove)
        {
            //No se puede mover en moveDir (permite movimiento diagonal al colisionar con un objeto)

            //Intentar solo movimiento en X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized; //.normalized para que tenga la misma velocidad que si presionara solo A/D
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, moveDistance);
            if (canMove)
            {
                //Solo se puede mover en X
                moveDir = moveDirX;
            }
            else
            {
                //No se puede mover solo en X

                //Intentar en movimiento solo Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, moveDistance);

                if (canMove)
                {
                    //Solo se puede mover en Z
                    moveDir = moveDirZ;
                }
                else
                {
                    //No se puede mover en ninguna dirección
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * moveDistance;
        }


        isWalking = moveDir != Vector3.zero;

        float rotateSpeed = 10f;
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);

        //Ayuda a guardar la última interacción, ya que al dejar de moverse, no se lanza el rayo.
        if (moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        //Con out RaycastHit raycastHit obtenemos el objeto que apunta el rayo (en caso de que choque con algo)
        if (Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                //Tiene ClearCunter
                clearCounter.Interact();
            }
        }
    }
}
