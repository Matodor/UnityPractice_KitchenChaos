using System;
using System.Collections;
using System.Collections.Generic;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Serialization;

public class Player : MonoBehaviour, IKitchenObjectParent
{
    public class SelectedCounterChangedEventArgs : EventArgs
    {
        public BaseCounter Selected;
    }

    public static Player Instance { get; private set; }

    public bool IsWalking => _isWalking;

    public event EventHandler<SelectedCounterChangedEventArgs> OnSelectedCounterChanged;

    [SerializeField]
    private float _moveSpeed = 7f;

    [SerializeField]
    private GameInput _gameInput;

    [SerializeField]
    private LayerMask _layerMask;

    [SerializeField]
    private Transform _kitchenObjectHoldPoint;

    private bool _isWalking;
    private Vector3 _lastInteractDirection;
    private BaseCounter _selectedCounter;
    private KitchenObject _kitchenObject;

    private void Awake()
    {
        if (Instance != null)
            Debug.LogError("There is more than one player instance");

        Instance = this;
    }

    private void Start()
    {
        _gameInput.OnInteract += GameInputOnInteract;
        _gameInput.OnInteractAlternate += GameInputOnInteractAlternate;
    }

    private void GameInputOnInteractAlternate(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInputOnInteract(object sender, EventArgs e)
    {
        if (_selectedCounter != null)
        {
            _selectedCounter.Interact(this);
        }
    }

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    private void HandleInteractions()
    {
        var inputDir = _gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputDir.x, 0, inputDir.y).normalized;

        if (moveDir != Vector3.zero)
            _lastInteractDirection = moveDir;

        const float interactDistance = 2f;

        if (Physics.Raycast(transform.position, _lastInteractDirection, out var raycastHit, interactDistance, _layerMask))
        {
            if (raycastHit.transform.TryGetComponent<BaseCounter>(out var baseCounter))
            {
                if (_selectedCounter != baseCounter)
                {
                    SetSelectedCounter(baseCounter);
                }
            }
            else
            {
                SetSelectedCounter(null);
            }
        }
        else
        {
            SetSelectedCounter(null);
        }
    }

    private void HandleMovement()
    {
        var inputDir = _gameInput.GetMovementVectorNormalized();
        var moveDir = new Vector3(inputDir.x, 0, inputDir.y).normalized;
        var moveDistance = _moveSpeed * Time.deltaTime;

        const float playerRadius = 0.7f;
        const float playerHeight = 2f;
        const float rotateSpeed = 15f;

        var canMove = !Physics.CapsuleCast(
            point1: transform.position,
            point2: transform.position + Vector3.up * playerHeight,
            radius: playerRadius,
            direction: moveDir,
            maxDistance: moveDistance
        );

        if (canMove == false)
        {
            var moveDirX = new Vector3(moveDir.x, 0, 0).normalized;

            canMove = moveDir.x != 0 && !Physics.CapsuleCast(
                point1: transform.position,
                point2: transform.position + Vector3.up * playerHeight,
                radius: playerRadius,
                direction: moveDirX,
                maxDistance: moveDistance
            );

            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                var moveDirZ = new Vector3(0, 0, moveDir.z).normalized;

                canMove = moveDir.z != 0 && !Physics.CapsuleCast(
                    point1: transform.position,
                    point2: transform.position + Vector3.up * playerHeight,
                    radius: playerRadius,
                    direction: moveDirZ,
                    maxDistance: moveDistance
                );

                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }

        // Debug.DrawLine(transform.position, transform.position + moveDir * 10, Color.blue);
        // Debug.DrawLine(transform.position, transform.position + moveDir * 10, Color.blue);

        if (canMove)
            transform.position += moveDir * (Time.deltaTime * _moveSpeed);

        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * rotateSpeed);
        _isWalking = canMove && moveDir != Vector3.zero;
    }

    private void SetSelectedCounter([CanBeNull] BaseCounter counter)
    {
        _selectedCounter = counter;

        OnSelectedCounterChanged?.Invoke(this, new SelectedCounterChangedEventArgs
        {
            Selected = counter,
        });
    }

    public Transform GetKitchenObjectFollowTransform()
    {
        return _kitchenObjectHoldPoint;
    }

    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
    }

    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }

    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }

    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
