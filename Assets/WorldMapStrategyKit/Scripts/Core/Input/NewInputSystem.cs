#if ENABLE_INPUT_SYSTEM

using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

namespace WorldMapStrategyKit {
    /// <summary>
    /// This class provides an input layer for the new input system that can be replaced or overriden to provide custom support
    /// </summary>
    public class NewInputSystem : IInputProxy {

        public virtual void Init() {
            UnityEngine.InputSystem.EnhancedTouch.EnhancedTouchSupport.Enable();
        }

        public virtual Vector3 mousePosition { get { return Mouse.current.position.ReadValue(); } }

        public virtual bool touchSupported { get { return Touchscreen.current != null; } }

        public virtual int touchCount { get { return UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches.Count; } }

        public virtual LocationService location { get { return null; } }

        public virtual bool GetMouseButton(int buttonIndex) {
            switch (buttonIndex) {
                case 1: return Mouse.current.rightButton.isPressed;
                case 2: return Mouse.current.middleButton.isPressed;
                default: return Mouse.current.leftButton.isPressed;
            }
        }

        public virtual bool GetMouseButtonDown(int buttonIndex) {
            switch (buttonIndex) {
                case 1: return Mouse.current.rightButton.wasPressedThisFrame;
                case 2: return Mouse.current.middleButton.wasPressedThisFrame;
                default: return Mouse.current.leftButton.wasPressedThisFrame;
            }
        }

        public virtual bool GetMouseButtonUp(int buttonIndex) {
            switch (buttonIndex) {
                case 1: return Mouse.current.rightButton.wasReleasedThisFrame;
                case 2: return Mouse.current.middleButton.wasReleasedThisFrame;
                default: return Mouse.current.leftButton.wasReleasedThisFrame;
            }
        }

        public virtual bool IsTouchStarting(int touchIndex) {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[touchIndex];
            return touch.phase == UnityEngine.InputSystem.TouchPhase.Began;
        }
        
        public virtual bool IsTouchEnding(int touchIndex) {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[touchIndex];
            return touch.phase == UnityEngine.InputSystem.TouchPhase.Ended;
        }

        public virtual int GetFingerIdFromTouch(int touchIndex) {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[touchIndex];
            return touch.finger.index;
        }

        public virtual bool GetKey(string name) {
            return ((KeyControl)Keyboard.current[name]).isPressed;
        }

        public virtual bool GetKeyDown(string name) {
            return ((KeyControl)Keyboard.current[name]).wasPressedThisFrame;
        }

        public virtual bool GetKeyUp(string name) {
            return ((KeyControl)Keyboard.current[name]).wasReleasedThisFrame;
        }


        // Note: the followihg methods are not implemented. Feel free to add your own input 
        public virtual float GetAxis(string axisName) {
            return 0;
        }

        public virtual bool GetButtonDown(string buttonName) {
            return false;
        }

        public virtual bool GetButtonUp(string buttonName) {
            return false;
        }

        public virtual bool GetKey(KeyCode keyCode) {
            return false;
        }

        public virtual bool GetKeyDown(KeyCode keyCode) {
            return false;
        }

        public virtual bool GetKeyUp(KeyCode keyCode) {
            return false;
        }

        public virtual Vector2 GetTouchDeltaPosition(int touchIndex) {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[touchIndex];
            return touch.delta;
        }

        public virtual Vector2 GetTouchPosition(int touchIndex) {
            UnityEngine.InputSystem.EnhancedTouch.Touch touch = UnityEngine.InputSystem.EnhancedTouch.Touch.activeTouches[touchIndex];
            return touch.screenPosition;
        }

    }
}

#endif
