//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.3.0
//     from Assets/CosmoShip/Scripts/Modules/Player/PlayerInputControls.inputactions
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

namespace CosmoShip.Scripts.Modules.Player
{
    public partial class @PlayerInputControls : IInputActionCollection2, IDisposable
    {
        public InputActionAsset asset { get; }
        public @PlayerInputControls()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""PlayerInputControls"",
    ""maps"": [
        {
            ""name"": ""SpaceShip"",
            ""id"": ""b338b386-6a80-4989-b114-fc7a71862218"",
            ""actions"": [
                {
                    ""name"": ""Move"",
                    ""type"": ""Value"",
                    ""id"": ""7ad7827d-1a44-47b4-80ec-b48cd306df57"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""Rotation"",
                    ""type"": ""Value"",
                    ""id"": ""e6219ae4-558c-4d9f-bbf7-6efa232f6ba9"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": ""InvertVector2(invertY=false)"",
                    ""interactions"": """",
                    ""initialStateCheck"": true
                },
                {
                    ""name"": ""BlasterShoot"",
                    ""type"": ""Button"",
                    ""id"": ""dbe410a4-f7c6-46de-b53f-ee2735291fd0"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""LaserShoot"",
                    ""type"": ""Button"",
                    ""id"": ""55527e65-b51c-4578-bcc1-8629a7fbf2ef"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                },
                {
                    ""name"": ""Spawn"",
                    ""type"": ""Button"",
                    ""id"": ""90c07a88-ab59-4686-8dcf-7f5298c745dd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""86cb0ec7-4704-48bc-a2c2-470ab237f770"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""cd3bf830-7f29-467d-a166-1f1d4e3c0b9d"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""08465a27-677b-45fb-81bb-d8ef8dbb0c33"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""e42fcf9f-98d7-4318-a2d7-96a0f6454189"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""Up"",
                    ""id"": ""8479113a-3756-466b-9b38-53731cbcbfdb"",
                    ""path"": ""<Keyboard>/upArrow"",
                    ""interactions"": """",
                    ""processors"": ""Clamp(max=1)"",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Down"",
                    ""id"": ""365d64bf-e023-4327-b322-e6bbe239d9f0"",
                    ""path"": """",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""7eee2b91-3c65-4113-b0bf-fa00910bf968"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e9174085-96e4-4d8c-b828-24e54b299c12"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""385cb1c9-0a2a-45c7-9b9c-609b1ffaeb0c"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""e8037ddc-e82e-477c-a612-304f3fef5adb"",
                    ""path"": ""<Gamepad>/leftStick/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""f1f83512-ca5d-4702-a5bb-6d58112a657d"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""BlasterShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""38f9e60e-bed8-42d0-9313-5ae1cf365fae"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""BlasterShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8115f557-e073-4623-85a3-139846adeb07"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""LaserShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41310b9d-0f42-4055-a80d-ac4f8c7816a7"",
                    ""path"": ""<Gamepad>/buttonNorth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""LaserShoot"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""D-Pad"",
                    ""id"": ""094e54f3-b639-4133-9ca7-324486266400"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""fa2fd80a-2cf7-40b0-838d-b5f19cc6c3ed"",
                    ""path"": ""<Gamepad>/dpad/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""325c4958-5afa-4024-aecf-f82188a32a64"",
                    ""path"": ""<Gamepad>/dpad/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Left Stick"",
                    ""id"": ""377c16d9-d4e6-4223-bfd9-08d4961dd20b"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""743820c9-85fd-44b6-badd-fee82110953a"",
                    ""path"": ""<Gamepad>/leftStick/left"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""b4e3ae4b-3fcb-4180-8bbc-6eeafd0d518b"",
                    ""path"": ""<Gamepad>/leftStick/right"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""WASD"",
                    ""id"": ""c5f89c56-5726-4ff7-8094-c740c6de32a9"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""8da86370-3d1f-4381-aa0f-5f6bb7d34084"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""14215538-4575-4804-a4ce-922733160609"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""Arrows"",
                    ""id"": ""61fcf6f5-6974-40f2-b9dc-c25b7fa4db93"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Rotation"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7b1a8a2a-2141-42ab-910a-8e963108fcca"",
                    ""path"": ""<Keyboard>/leftArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""af83b0eb-9179-492a-9b3e-a414bf1a24b6"",
                    ""path"": ""<Keyboard>/rightArrow"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Rotation"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""6313d260-0a4b-49a7-ae00-764a9939cd70"",
                    ""path"": ""<Keyboard>/g"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard"",
                    ""action"": ""Spawn"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Keyboard"",
            ""bindingGroup"": ""Keyboard"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // SpaceShip
            m_SpaceShip = asset.FindActionMap("SpaceShip", throwIfNotFound: true);
            m_SpaceShip_Move = m_SpaceShip.FindAction("Move", throwIfNotFound: true);
            m_SpaceShip_Rotation = m_SpaceShip.FindAction("Rotation", throwIfNotFound: true);
            m_SpaceShip_BlasterShoot = m_SpaceShip.FindAction("BlasterShoot", throwIfNotFound: true);
            m_SpaceShip_LaserShoot = m_SpaceShip.FindAction("LaserShoot", throwIfNotFound: true);
            m_SpaceShip_Spawn = m_SpaceShip.FindAction("Spawn", throwIfNotFound: true);
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

        // SpaceShip
        private readonly InputActionMap m_SpaceShip;
        private ISpaceShipActions m_SpaceShipActionsCallbackInterface;
        private readonly InputAction m_SpaceShip_Move;
        private readonly InputAction m_SpaceShip_Rotation;
        private readonly InputAction m_SpaceShip_BlasterShoot;
        private readonly InputAction m_SpaceShip_LaserShoot;
        private readonly InputAction m_SpaceShip_Spawn;
        public struct SpaceShipActions
        {
            private @PlayerInputControls m_Wrapper;
            public SpaceShipActions(@PlayerInputControls wrapper) { m_Wrapper = wrapper; }
            public InputAction @Move => m_Wrapper.m_SpaceShip_Move;
            public InputAction @Rotation => m_Wrapper.m_SpaceShip_Rotation;
            public InputAction @BlasterShoot => m_Wrapper.m_SpaceShip_BlasterShoot;
            public InputAction @LaserShoot => m_Wrapper.m_SpaceShip_LaserShoot;
            public InputAction @Spawn => m_Wrapper.m_SpaceShip_Spawn;
            public InputActionMap Get() { return m_Wrapper.m_SpaceShip; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(SpaceShipActions set) { return set.Get(); }
            public void SetCallbacks(ISpaceShipActions instance)
            {
                if (m_Wrapper.m_SpaceShipActionsCallbackInterface != null)
                {
                    @Move.started -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnMove;
                    @Rotation.started -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnRotation;
                    @Rotation.performed -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnRotation;
                    @Rotation.canceled -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnRotation;
                    @BlasterShoot.started -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnBlasterShoot;
                    @BlasterShoot.performed -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnBlasterShoot;
                    @BlasterShoot.canceled -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnBlasterShoot;
                    @LaserShoot.started -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnLaserShoot;
                    @LaserShoot.performed -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnLaserShoot;
                    @LaserShoot.canceled -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnLaserShoot;
                    @Spawn.started -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnSpawn;
                    @Spawn.performed -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnSpawn;
                    @Spawn.canceled -= m_Wrapper.m_SpaceShipActionsCallbackInterface.OnSpawn;
                }
                m_Wrapper.m_SpaceShipActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Rotation.started += instance.OnRotation;
                    @Rotation.performed += instance.OnRotation;
                    @Rotation.canceled += instance.OnRotation;
                    @BlasterShoot.started += instance.OnBlasterShoot;
                    @BlasterShoot.performed += instance.OnBlasterShoot;
                    @BlasterShoot.canceled += instance.OnBlasterShoot;
                    @LaserShoot.started += instance.OnLaserShoot;
                    @LaserShoot.performed += instance.OnLaserShoot;
                    @LaserShoot.canceled += instance.OnLaserShoot;
                    @Spawn.started += instance.OnSpawn;
                    @Spawn.performed += instance.OnSpawn;
                    @Spawn.canceled += instance.OnSpawn;
                }
            }
        }
        public SpaceShipActions @SpaceShip => new SpaceShipActions(this);
        private int m_KeyboardSchemeIndex = -1;
        public InputControlScheme KeyboardScheme
        {
            get
            {
                if (m_KeyboardSchemeIndex == -1) m_KeyboardSchemeIndex = asset.FindControlSchemeIndex("Keyboard");
                return asset.controlSchemes[m_KeyboardSchemeIndex];
            }
        }
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        public interface ISpaceShipActions
        {
            void OnMove(InputAction.CallbackContext context);
            void OnRotation(InputAction.CallbackContext context);
            void OnBlasterShoot(InputAction.CallbackContext context);
            void OnLaserShoot(InputAction.CallbackContext context);
            void OnSpawn(InputAction.CallbackContext context);
        }
    }
}
