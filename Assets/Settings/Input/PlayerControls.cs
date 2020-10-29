// GENERATED AUTOMATICALLY FROM 'Assets/Settings/Input/PlayerControls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @PlayerControls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @PlayerControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerControls"",
    ""maps"": [
        {
            ""name"": ""Movement"",
            ""id"": ""82a078a6-6c66-49ef-aa41-c658d082b2db"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""07359076-c232-4a2a-bca9-364fe643dbb0"",
                    ""expectedControlType"": ""Stick"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Walk/Run/Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""3a569bfb-fc3d-4260-87e5-c3fa17840ef5"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""c13c503a-9b83-4c5c-9449-8137640469a0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""951a02e8-0528-4dec-b0ac-51686f7988c5"",
                    ""path"": ""<DualShockGamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""491f4870-0a09-41ab-82fd-f007d12fd73f"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Walk/Run/Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""65b935c3-22de-4b01-99f1-a4d1019a5839"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""HUD"",
            ""id"": ""3ca9db5f-748a-4af7-94d8-6dc8ce636fc4"",
            ""actions"": [
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""f3b9da6a-4389-48db-a855-f175ed23ea51"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Accept"",
                    ""type"": ""Button"",
                    ""id"": ""c1483532-d3b3-47f3-bff9-8d20afbf819f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""84e63cf7-9b5f-46b6-b008-f3228e61ded2"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a6c335a4-136d-47a3-834f-194bfb9b21f4"",
                    ""path"": ""<DualShockGamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09c17115-a578-4b07-90d7-d28348b41b91"",
                    ""path"": ""<DualShockGamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Accept"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fabdcd35-878b-4029-a8a7-01c83aa33370"",
                    ""path"": ""<DualShockGamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""Actions"",
            ""id"": ""15a55565-adc8-46e7-8856-40329986d8c2"",
            ""actions"": [
                {
                    ""name"": ""Use"",
                    ""type"": ""Button"",
                    ""id"": ""cc68fd79-0684-4e8c-a556-a9ad9672a18f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": ""Hold,Press(behavior=2)""
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b37afbdc-f334-4afa-9f03-90b24de1952f"",
                    ""path"": ""<DualShockGamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Use"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Movement
        m_Movement = asset.FindActionMap("Movement", throwIfNotFound: true);
        m_Movement_Move = m_Movement.FindAction("Move", throwIfNotFound: true);
        m_Movement_WalkRunSprint = m_Movement.FindAction("Walk/Run/Sprint", throwIfNotFound: true);
        m_Movement_Jump = m_Movement.FindAction("Jump", throwIfNotFound: true);
        // HUD
        m_HUD = asset.FindActionMap("HUD", throwIfNotFound: true);
        m_HUD_Menu = m_HUD.FindAction("Menu", throwIfNotFound: true);
        m_HUD_Accept = m_HUD.FindAction("Accept", throwIfNotFound: true);
        m_HUD_Back = m_HUD.FindAction("Back", throwIfNotFound: true);
        // Actions
        m_Actions = asset.FindActionMap("Actions", throwIfNotFound: true);
        m_Actions_Use = m_Actions.FindAction("Use", throwIfNotFound: true);
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

    // Movement
    private readonly InputActionMap m_Movement;
    private IMovementActions m_MovementActionsCallbackInterface;
    private readonly InputAction m_Movement_Move;
    private readonly InputAction m_Movement_WalkRunSprint;
    private readonly InputAction m_Movement_Jump;
    public struct MovementActions
    {
        private @PlayerControls m_Wrapper;
        public MovementActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Move => m_Wrapper.m_Movement_Move;
        public InputAction @WalkRunSprint => m_Wrapper.m_Movement_WalkRunSprint;
        public InputAction @Jump => m_Wrapper.m_Movement_Jump;
        public InputActionMap Get() { return m_Wrapper.m_Movement; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(MovementActions set) { return set.Get(); }
        public void SetCallbacks(IMovementActions instance)
        {
            if (m_Wrapper.m_MovementActionsCallbackInterface != null)
            {
                @Move.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @Move.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnMove;
                @WalkRunSprint.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalkRunSprint;
                @WalkRunSprint.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalkRunSprint;
                @WalkRunSprint.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnWalkRunSprint;
                @Jump.started -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_MovementActionsCallbackInterface.OnJump;
            }
            m_Wrapper.m_MovementActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Move.started += instance.OnMove;
                @Move.performed += instance.OnMove;
                @Move.canceled += instance.OnMove;
                @WalkRunSprint.started += instance.OnWalkRunSprint;
                @WalkRunSprint.performed += instance.OnWalkRunSprint;
                @WalkRunSprint.canceled += instance.OnWalkRunSprint;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
            }
        }
    }
    public MovementActions @Movement => new MovementActions(this);

    // HUD
    private readonly InputActionMap m_HUD;
    private IHUDActions m_HUDActionsCallbackInterface;
    private readonly InputAction m_HUD_Menu;
    private readonly InputAction m_HUD_Accept;
    private readonly InputAction m_HUD_Back;
    public struct HUDActions
    {
        private @PlayerControls m_Wrapper;
        public HUDActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Menu => m_Wrapper.m_HUD_Menu;
        public InputAction @Accept => m_Wrapper.m_HUD_Accept;
        public InputAction @Back => m_Wrapper.m_HUD_Back;
        public InputActionMap Get() { return m_Wrapper.m_HUD; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(HUDActions set) { return set.Get(); }
        public void SetCallbacks(IHUDActions instance)
        {
            if (m_Wrapper.m_HUDActionsCallbackInterface != null)
            {
                @Menu.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnMenu;
                @Menu.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnMenu;
                @Menu.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnMenu;
                @Accept.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnAccept;
                @Accept.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnAccept;
                @Accept.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnAccept;
                @Back.started -= m_Wrapper.m_HUDActionsCallbackInterface.OnBack;
                @Back.performed -= m_Wrapper.m_HUDActionsCallbackInterface.OnBack;
                @Back.canceled -= m_Wrapper.m_HUDActionsCallbackInterface.OnBack;
            }
            m_Wrapper.m_HUDActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Menu.started += instance.OnMenu;
                @Menu.performed += instance.OnMenu;
                @Menu.canceled += instance.OnMenu;
                @Accept.started += instance.OnAccept;
                @Accept.performed += instance.OnAccept;
                @Accept.canceled += instance.OnAccept;
                @Back.started += instance.OnBack;
                @Back.performed += instance.OnBack;
                @Back.canceled += instance.OnBack;
            }
        }
    }
    public HUDActions @HUD => new HUDActions(this);

    // Actions
    private readonly InputActionMap m_Actions;
    private IActionsActions m_ActionsActionsCallbackInterface;
    private readonly InputAction m_Actions_Use;
    public struct ActionsActions
    {
        private @PlayerControls m_Wrapper;
        public ActionsActions(@PlayerControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Use => m_Wrapper.m_Actions_Use;
        public InputActionMap Get() { return m_Wrapper.m_Actions; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(ActionsActions set) { return set.Get(); }
        public void SetCallbacks(IActionsActions instance)
        {
            if (m_Wrapper.m_ActionsActionsCallbackInterface != null)
            {
                @Use.started -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUse;
                @Use.performed -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUse;
                @Use.canceled -= m_Wrapper.m_ActionsActionsCallbackInterface.OnUse;
            }
            m_Wrapper.m_ActionsActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Use.started += instance.OnUse;
                @Use.performed += instance.OnUse;
                @Use.canceled += instance.OnUse;
            }
        }
    }
    public ActionsActions @Actions => new ActionsActions(this);
    public interface IMovementActions
    {
        void OnMove(InputAction.CallbackContext context);
        void OnWalkRunSprint(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
    }
    public interface IHUDActions
    {
        void OnMenu(InputAction.CallbackContext context);
        void OnAccept(InputAction.CallbackContext context);
        void OnBack(InputAction.CallbackContext context);
    }
    public interface IActionsActions
    {
        void OnUse(InputAction.CallbackContext context);
    }
}
