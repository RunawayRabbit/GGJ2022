//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.2.0
//     from Assets/Input/MouseInputController.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @MouseInputController : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MouseInputController()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MouseInputController"",
    ""maps"": [
        {
            ""name"": ""PlayerOne"",
            ""id"": ""f2ffb4e2-8579-49b9-aa69-ad629a091aee"",
            ""actions"": [
                {
                    ""name"": ""MousePosition"",
                    ""type"": ""Value"",
                    ""id"": ""cd583d7c-93bb-4926-8424-b4991e214d5a"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""ConfirmMovementPoint"",
                    ""type"": ""Button"",
                    ""id"": ""86cd0f78-998e-405a-9990-0082fa0029e6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""Button"",
                    ""id"": ""fc6041b6-af12-4872-8ebe-55aa08ddfeb8"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""e16c56f2-a127-44fa-ac46-2e880284160a"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""MousePosition"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d55c7424-9e4c-4b98-8e8d-5ee738e3d454"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Player"",
                    ""action"": ""ConfirmMovementPoint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""0d4c9b03-1157-42cf-9ca5-0193e0ed900d"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Player"",
            ""bindingGroup"": ""Player"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // PlayerOne
        m_PlayerOne = asset.FindActionMap("PlayerOne", throwIfNotFound: true);
        m_PlayerOne_MousePosition = m_PlayerOne.FindAction("MousePosition", throwIfNotFound: true);
        m_PlayerOne_ConfirmMovementPoint = m_PlayerOne.FindAction("ConfirmMovementPoint", throwIfNotFound: true);
        m_PlayerOne_Move = m_PlayerOne.FindAction("Move", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // PlayerOne
    private readonly InputActionMap m_PlayerOne;
    private IPlayerOneActions m_PlayerOneActionsCallbackInterface;
    private readonly InputAction m_PlayerOne_MousePosition;
    private readonly InputAction m_PlayerOne_ConfirmMovementPoint;
    private readonly InputAction m_PlayerOne_Move;
    public struct PlayerOneActions
    {
        private @MouseInputController m_Wrapper;
        public PlayerOneActions(@MouseInputController wrapper) { m_Wrapper = wrapper; }
        public InputAction @MousePosition => m_Wrapper.m_PlayerOne_MousePosition;
        public InputAction @ConfirmMovementPoint => m_Wrapper.m_PlayerOne_ConfirmMovementPoint;
        public InputAction @Move => m_Wrapper.m_PlayerOne_Move;
        public InputActionMap Get() { return m_Wrapper.m_PlayerOne; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerOneActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerOneActions instance)
        {
            if (m_Wrapper.m_PlayerOneActionsCallbackInterface != null)
            {
                @MousePosition.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMousePosition;
                @ConfirmMovementPoint.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnConfirmMovementPoint;
                @ConfirmMovementPoint.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnConfirmMovementPoint;
                @ConfirmMovementPoint.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnConfirmMovementPoint;
                @Move.started -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PlayerOneActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PlayerOneActionsCallbackInterface = instance;
            if (instance != null)
            {
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @ConfirmMovementPoint.started += instance.OnConfirmMovementPoint;
                @ConfirmMovementPoint.performed += instance.OnConfirmMovementPoint;
                @ConfirmMovementPoint.canceled += instance.OnConfirmMovementPoint;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PlayerOneActions @PlayerOne => new PlayerOneActions(this);
    private int m_PlayerSchemeIndex = -1;
    public InputControlScheme PlayerScheme
    {
        get
        {
            if (m_PlayerSchemeIndex == -1) m_PlayerSchemeIndex = asset.FindControlSchemeIndex("Player");
            return asset.controlSchemes[m_PlayerSchemeIndex];
        }
    }
    public interface IPlayerOneActions
    {
        void OnMousePosition(InputAction.CallbackContext context);
        void OnConfirmMovementPoint(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
