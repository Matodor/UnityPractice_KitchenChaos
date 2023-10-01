using System;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

public class StoveCounter : BaseCounter, IHasProgress
{
    public class StageChangedEventArgs : EventArgs
    {
        public State State;
    }

    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned,
    }

    public event EventHandler<IHasProgress.ProgressChangedArgs> OnProgressChanged;
    public event EventHandler<StageChangedEventArgs> StageChanged;

    [SerializeField]
    private FryingRecipeSO[] _fryingRecipes;

    [SerializeField]
    private BurningRecipeSO[] _burningRecipes;

    private State _state = State.Idle;

    [CanBeNull]
    private FryingRecipeSO _fryingRecipe;
    private float _fryingTimer = 0f;

    [CanBeNull]
    private BurningRecipeSO _burningRecipe;
    private float _burningTimer = 0f;

    private void SetState(State state)
    {
        if (_state == state)
            return;

        _state = state;
        StageChanged?.Invoke(this, new StageChangedEventArgs
        {
            State = state,
        });
    }

    protected void Reset()
    {
        SetState(State.Idle);

        _fryingTimer = 0;
        _fryingRecipe = null;

        _burningTimer = 0f;
        _burningRecipe = null;

        OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
        {
            ProgressNormalized = 0f,
        });
    }

    public override void Interact(Player player)
    {
        if (HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                if (player.GetKitchenObject() is PlateKitchenObject plateKitchenObject)
                {
                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().Destroy();
                        Reset();
                    }
                }
            }
            else
            {
                GetKitchenObject().SetParent(player);
                Reset();
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                if (HasFryingRecipe(player.GetKitchenObject().GetKitchenObjectSO()))
                {
                    player.GetKitchenObject().SetParent(this);

                    SetState(State.Frying);

                    _fryingTimer = 0;
                    _fryingRecipe = FindFryingRecipe(GetKitchenObject().GetKitchenObjectSO());

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                    {
                        ProgressNormalized = 0f,
                    });
                }
            }
        }
    }

    private void Update()
    {
        if (HasKitchenObject())
        {
            switch (_state)
            {
                case State.Idle:
                    break;

                case State.Frying:
                    _fryingTimer += Time.deltaTime;

                    OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                    {
                        ProgressNormalized = _fryingTimer / _fryingRecipe!.FryingTimer,
                    });

                    if (_fryingTimer > _fryingRecipe!.FryingTimer)
                    {
                        SetState(State.Fried);

                        _fryingTimer = 0;

                        GetKitchenObject().Destroy();
                        KitchenObject.Spawn(_fryingRecipe.Output, this);

                        _burningTimer = 0f;
                        _burningRecipe = FindBurningRecipe(GetKitchenObject().GetKitchenObjectSO());
                    }
                    break;

                case State.Fried:
                    if (_burningRecipe != null)
                    {
                        _burningTimer += Time.deltaTime;

                        OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                        {
                            ProgressNormalized = _burningTimer / _burningRecipe!.BurningTimer,
                        });

                        if (_burningTimer > _burningRecipe!.BurningTimer)
                        {
                            SetState(State.Burned);

                            _burningTimer = 0;

                            GetKitchenObject().Destroy();
                            KitchenObject.Spawn(_burningRecipe!.Output, this);

                            OnProgressChanged?.Invoke(this, new IHasProgress.ProgressChangedArgs
                            {
                                ProgressNormalized = 0f,
                            });
                        }
                    }
                    break;

                case State.Burned:
                    break;
            }
        }
    }

    private bool HasFryingRecipe(KitchenObjectSO input)
    {
        return _fryingRecipes.Any(so => so.Input == input);
    }

    [CanBeNull]
    private FryingRecipeSO FindFryingRecipe(KitchenObjectSO input)
    {
        return _fryingRecipes.FirstOrDefault(so => so.Input == input);
    }

    private bool HasBurningRecipe(KitchenObjectSO input)
    {
        return _burningRecipes.Any(so => so.Input == input);
    }

    [CanBeNull]
    private BurningRecipeSO FindBurningRecipe(KitchenObjectSO input)
    {
        return _burningRecipes.FirstOrDefault(so => so.Input == input);
    }
}
