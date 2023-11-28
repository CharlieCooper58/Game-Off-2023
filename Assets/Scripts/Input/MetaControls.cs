//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.7.0
//     from Assets/Scripts/Input/MetaControls.inputactions
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

public partial class @MetaControls: IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @MetaControls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""MetaControls"",
    ""maps"": [
        {
            ""name"": ""GameHandler"",
            ""id"": ""dfa61183-31ca-4dcf-aa94-e67f94d56111"",
            ""actions"": [
                {
                    ""name"": ""PauseGame"",
                    ""type"": ""Button"",
                    ""id"": ""b196623a-e3b2-4ae6-bd27-c53db221a98f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""GrowShrink"",
                    ""type"": ""Button"",
                    ""id"": ""f05966c7-c4cb-4ed8-b75c-e2c416c7b3ab"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Restart"",
                    ""type"": ""Button"",
                    ""id"": ""0df51b2c-a9c1-454f-ab5a-06f026e67ed9"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""a557d6c9-0837-4ccf-9fee-43aeb5c52415"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ce81751d-eb64-4e00-81c4-c2435c087f0f"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PauseGame"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c50ef9f2-7744-4085-b7f1-e054c1ea74c8"",
                    ""path"": ""<Keyboard>/p"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""GrowShrink"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""5a8e2c70-4d0e-46db-ae22-74a60fd27ded"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""145f93a0-524c-45d2-a268-9ed68902342d"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Restart"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // GameHandler
        m_GameHandler = asset.FindActionMap("GameHandler", throwIfNotFound: true);
        m_GameHandler_PauseGame = m_GameHandler.FindAction("PauseGame", throwIfNotFound: true);
        m_GameHandler_GrowShrink = m_GameHandler.FindAction("GrowShrink", throwIfNotFound: true);
        m_GameHandler_Restart = m_GameHandler.FindAction("Restart", throwIfNotFound: true);
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

    // GameHandler
    private readonly InputActionMap m_GameHandler;
    private List<IGameHandlerActions> m_GameHandlerActionsCallbackInterfaces = new List<IGameHandlerActions>();
    private readonly InputAction m_GameHandler_PauseGame;
    private readonly InputAction m_GameHandler_GrowShrink;
    private readonly InputAction m_GameHandler_Restart;
    public struct GameHandlerActions
    {
        private @MetaControls m_Wrapper;
        public GameHandlerActions(@MetaControls wrapper) { m_Wrapper = wrapper; }
        public InputAction @PauseGame => m_Wrapper.m_GameHandler_PauseGame;
        public InputAction @GrowShrink => m_Wrapper.m_GameHandler_GrowShrink;
        public InputAction @Restart => m_Wrapper.m_GameHandler_Restart;
        public InputActionMap Get() { return m_Wrapper.m_GameHandler; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameHandlerActions set) { return set.Get(); }
        public void AddCallbacks(IGameHandlerActions instance)
        {
            if (instance == null || m_Wrapper.m_GameHandlerActionsCallbackInterfaces.Contains(instance)) return;
            m_Wrapper.m_GameHandlerActionsCallbackInterfaces.Add(instance);
            @PauseGame.started += instance.OnPauseGame;
            @PauseGame.performed += instance.OnPauseGame;
            @PauseGame.canceled += instance.OnPauseGame;
            @GrowShrink.started += instance.OnGrowShrink;
            @GrowShrink.performed += instance.OnGrowShrink;
            @GrowShrink.canceled += instance.OnGrowShrink;
            @Restart.started += instance.OnRestart;
            @Restart.performed += instance.OnRestart;
            @Restart.canceled += instance.OnRestart;
        }

        private void UnregisterCallbacks(IGameHandlerActions instance)
        {
            @PauseGame.started -= instance.OnPauseGame;
            @PauseGame.performed -= instance.OnPauseGame;
            @PauseGame.canceled -= instance.OnPauseGame;
            @GrowShrink.started -= instance.OnGrowShrink;
            @GrowShrink.performed -= instance.OnGrowShrink;
            @GrowShrink.canceled -= instance.OnGrowShrink;
            @Restart.started -= instance.OnRestart;
            @Restart.performed -= instance.OnRestart;
            @Restart.canceled -= instance.OnRestart;
        }

        public void RemoveCallbacks(IGameHandlerActions instance)
        {
            if (m_Wrapper.m_GameHandlerActionsCallbackInterfaces.Remove(instance))
                UnregisterCallbacks(instance);
        }

        public void SetCallbacks(IGameHandlerActions instance)
        {
            foreach (var item in m_Wrapper.m_GameHandlerActionsCallbackInterfaces)
                UnregisterCallbacks(item);
            m_Wrapper.m_GameHandlerActionsCallbackInterfaces.Clear();
            AddCallbacks(instance);
        }
    }
    public GameHandlerActions @GameHandler => new GameHandlerActions(this);
    public interface IGameHandlerActions
    {
        void OnPauseGame(InputAction.CallbackContext context);
        void OnGrowShrink(InputAction.CallbackContext context);
        void OnRestart(InputAction.CallbackContext context);
    }
}
