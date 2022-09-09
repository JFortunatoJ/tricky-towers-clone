//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.2
//     from Assets/_Project/Game/Gameplay/Scripts/Player/PlayerInputActions.inputactions
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

public partial class @PlayerInputActions : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerInputActions()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputActions"",
    ""maps"": [
        {
            ""name"": ""PiecesActions"",
            ""id"": ""5c01e73e-ac67-4a37-82f4-38fbe856c152"",
            ""actions"": [
                {
                    ""name"": ""Rotate"",
                    ""type"": ""Button"",
                    ""id"": ""822291b8-e163-488d-8e73-ff77fe3d7adf"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""TouchDown"",
                    ""type"": ""Button"",
                    ""id"": ""35a97131-7cca-4806-9862-51f2ae62d42b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Press"",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""00703d40-f51b-4714-a630-fa9e398cea07"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""f55392c1-b1ec-43ac-af9c-08298f740cd3"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": ""Tap"",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6bb33fd9-8dc6-4035-88fb-e4f26c74c2b0"",
                    ""path"": ""<Touchscreen>/touch0/tap"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Rotate"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""7277eeb0-1a27-41c8-a6e5-19d6894b402d"",
                    ""path"": ""<Touchscreen>/Press"",
                    ""interactions"": ""Press(pressPoint=0.1)"",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""TouchDown"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""89c80ba3-6a7d-4e86-a023-a0ee95ec7c99"",
                    ""path"": ""<Touchscreen>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Touch"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""2D Vector"",
                    ""id"": ""3b40ed49-894c-4b65-9297-7cb7df232133"",
                    ""path"": ""2DVector"",
                    ""interactions"": ""Press"",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""5c8fc23e-af19-4771-8c4e-1028cf18b934"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""67e85399-bf0f-468b-a362-9fc9cefaaf96"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""bc0797df-0fa9-43f7-8f78-f89500fff937"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""6bbf225a-4645-442b-9293-0a8df4fa643a"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": []
        },
        {
            ""name"": ""Touch"",
            ""bindingGroup"": ""Touch"",
            ""devices"": []
        }
    ]
}");
        // PiecesActions
        m_PiecesActions = asset.FindActionMap("PiecesActions", throwIfNotFound: true);
        m_PiecesActions_Rotate = m_PiecesActions.FindAction("Rotate", throwIfNotFound: true);
        m_PiecesActions_TouchDown = m_PiecesActions.FindAction("TouchDown", throwIfNotFound: true);
        m_PiecesActions_Move = m_PiecesActions.FindAction("Move", throwIfNotFound: true);
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

    // PiecesActions
    private readonly InputActionMap m_PiecesActions;
    private IPiecesActionsActions m_PiecesActionsActionsCallbackInterface;
    private readonly InputAction m_PiecesActions_Rotate;
    private readonly InputAction m_PiecesActions_TouchDown;
    private readonly InputAction m_PiecesActions_Move;
    public struct PiecesActionsActions
    {
        private @PlayerInputActions m_Wrapper;
        public PiecesActionsActions(@PlayerInputActions wrapper) { m_Wrapper = wrapper; }
        public InputAction @Rotate => m_Wrapper.m_PiecesActions_Rotate;
        public InputAction @TouchDown => m_Wrapper.m_PiecesActions_TouchDown;
        public InputAction @Move => m_Wrapper.m_PiecesActions_Move;
        public InputActionMap Get() { return m_Wrapper.m_PiecesActions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PiecesActionsActions set) { return set.Get(); }
        public void SetCallbacks(IPiecesActionsActions instance)
        {
            if (m_Wrapper.m_PiecesActionsActionsCallbackInterface != null)
            {
                @Rotate.started -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnRotate;
                @Rotate.performed -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnRotate;
                @Rotate.canceled -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnRotate;
                @TouchDown.started -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnTouchDown;
                @TouchDown.performed -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnTouchDown;
                @TouchDown.canceled -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnTouchDown;
                @Move.started -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_PiecesActionsActionsCallbackInterface.OnMove;
            }
            m_Wrapper.m_PiecesActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Rotate.started += instance.OnRotate;
                @Rotate.performed += instance.OnRotate;
                @Rotate.canceled += instance.OnRotate;
                @TouchDown.started += instance.OnTouchDown;
                @TouchDown.performed += instance.OnTouchDown;
                @TouchDown.canceled += instance.OnTouchDown;
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
            }
        }
    }
    public PiecesActionsActions @PiecesActions => new PiecesActionsActions(this);
    private int m_KeyboardSchemeIndex = -1;
    public InputControlScheme KeyboardScheme
    {
        get
        {
            if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
            return asset.controlSchemes[m_KeyboardSchemeIndex];
        }
    }
    private int m_TouchSchemeIndex = -1;
    public InputControlScheme TouchScheme
    {
        get
        {
            if (m_TouchSchemeIndex == -1) m_TouchSchemeIndex = asset.FindControlSchemeIndex("Touch");
            return asset.controlSchemes[m_TouchSchemeIndex];
        }
    }
    public interface IPiecesActionsActions
    {
        void OnRotate(InputAction.CallbackContext context);
        void OnTouchDown(InputAction.CallbackContext context);
        void OnMove(InputAction.CallbackContext context);
    }
}
