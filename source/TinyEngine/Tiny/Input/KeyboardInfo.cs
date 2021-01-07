/* ----------------------------------------------------------------------------
    MIT License

    Copyright (c) 2020 Christopher Whitley

    Permission is hereby granted, free of charge, to any person obtaining a copy
    of this software and associated documentation files (the "Software"), to deal
    in the Software without restriction, including without limitation the rights
    to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
    copies of the Software, and to permit persons to whom the Software is
    furnished to do so, subject to the following conditions:

    The above copyright notice and this permission notice shall be included in all
    copies or substantial portions of the Software.

    THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
    IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
    FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
    AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
    LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
    OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
    SOFTWARE.
---------------------------------------------------------------------------- */

using Microsoft.Xna.Framework.Input;

namespace Tiny
{
    public class KeyboardInfo
    {
        /// <summary>
        ///     Gets the state of keyboard input during the previous update cycle.
        /// </summary>
        public KeyboardState PreviousState { get; private set; }

        /// <summary>
        ///     Gets the state of keyboard input during the current update cycle.
        /// </summary>
        public KeyboardState CurrentState { get; private set; }

        /// <summary>
        ///     Gets a value indicating if any keyboard key is currently down.
        /// </summary>
        public bool AnyKeyCheck
        {
            get
            {
                return CurrentState.GetPressedKeyCount() > 0;
            }
        }

        /// <summary>
        ///     Gets a value indicating if any keyboard key was just pressed.
        /// </summary>
        /// <remarks>
        ///     Pressed means that the key was up on the previous frame and is
        ///     down on the current frame.
        /// </remarks>
        public bool AnyKeyPressed
        {
            get
            {
                return CurrentState.GetPressedKeyCount() > PreviousState.GetPressedKeyCount();
            }
        }

        /// <summary>
        ///     Gets a value indicating if any keyboard key was just released.
        /// </summary>
        /// <remarks>
        ///     Released means that the key was down on the previous frame and is
        ///     up on the current frame.
        /// </remarks>
        public bool AnyKeyReleased
        {
            get
            {
                return CurrentState.GetPressedKeyCount() < PreviousState.GetPressedKeyCount();
            }
        }

        /// <summary>
        ///     Creates a new KeyboardInfo instance.
        /// </summary>
        public KeyboardInfo()
        {
            PreviousState = new KeyboardState();
            CurrentState = Keyboard.GetState();
        }

        /// <summary>
        ///     Updates this instance.
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Keyboard.GetState();
        }

        /// <summary>
        ///     Checks if the given keyboard key is currently down.
        /// </summary>
        /// <param name="key">
        ///     The keyboard key to check.
        /// </param>
        /// <returns>
        ///     True if the given keyboard key is currently down; otherwise, false.
        /// </returns>
        public bool KeyCheck(Keys key)
        {
            return CurrentState.IsKeyDown(key);
        }

        /// <summary>
        ///     Checks if the given keyboard key was pressed on the current frame.
        /// </summary>
        /// <remarks>
        ///     Pressed means that the key was up on the previous frame and is
        ///     down on the current frame.
        /// </remarks>
        /// <param name="key">
        ///     The keyboard key to check.
        /// </param>
        /// <returns>
        ///     True if the given keyboard key was pressed on the current frame;
        ///     otherwise, false.
        /// </returns>
        public bool KeyPressed(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        /// <summary>
        ///     Checks if the given keyboard key was released on the current frame.
        /// </summary>
        /// <remarks>
        ///     Released means that the key was down on the previous frame and is
        ///     up on the current frame.
        /// </remarks>
        /// <param name="key">
        ///     The keyboard key to check.
        /// </param>
        /// <returns>
        ///     True if the given keyboard key was released on the current frame;
        ///     otherwise, false.
        /// </returns>
        public bool KeyReleased(Keys key)
        {
            return CurrentState.IsKeyDown(key) && PreviousState.IsKeyUp(key);
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive or the
        ///     negative negative keyboard key is currently down.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <param name="both">
        ///     The value to return if both the positive and negative keyboard
        ///     keys are currently down.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key is currently down, -1 is returned
        ///     if the negative keyboard key is currently down, 0 is returned if neither or
        ///     both keyboard keys are currently down.
        /// </returns>
        public int AxisCheck(Keys positive, Keys negative)
        {
            return AxisCheck(positive, negative, 0);
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive, the
        ///     negative, or both keyboard keys are currently down.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <param name="both">
        ///     The value to return if both the positive and negative keyboard
        ///     keys are currently down.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key is currently down, -1 is returned
        ///     if the negative keyboard key is currently down, 0 is returned if neither
        ///     keyboard key is currently down, or the value of <paramref name="both"/>
        ///     is returned if both keyboard keys are down.
        /// </returns>
        public int AxisCheck(Keys positive, Keys negative, int both)
        {
            if (KeyCheck(positive))
            {
                if (KeyCheck(negative))
                {
                    return both;
                }
                else
                {
                    return 1;
                }
            }
            else if (KeyCheck(negative))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive or the
        ///     negative negative keyboard key was just pressed.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key was just pressed, -1 is returned
        ///     if the negative keyboard key was just pressed , 0 is returned if neither or
        ///     both keyboard keys were pressed
        /// </returns>
        public int AxisPressed(Keys positive, Keys negative)
        {
            return AxisPressed(positive, negative, 0);
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive, the
        ///     negative, or both keyboard keys are were just pressed.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <param name="both">
        ///     The value to return if both the positive and negative keyboard
        ///     keys were pressed.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key was just pressed, -1 is returned
        ///     if the negative keyboard key was just pressed , 0 is returned if neither
        ///     keyboard key was just pressed, or the value of <paramref name="both"/>
        ///     is returned if both keyboard keys were just pressed.
        /// </returns>
        public int AxisPressed(Keys positive, Keys negative, int both)
        {
            if (KeyPressed(positive))
            {
                if (KeyPressed(negative))
                {
                    return both;
                }
                else
                {
                    return 1;
                }
            }
            else if (KeyPressed(negative))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive or the
        ///     negative negative keyboard key was just released.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key was just released, -1 is returned
        ///     if the negative keyboard key was just released , 0 is returned if neither or
        ///     both keyboard keys were released
        /// </returns>
        public int AxisReleased(Keys positive, Keys negative)
        {
            return AxisReleased(positive, negative, 0);
        }

        /// <summary>
        ///     Given a positive keyboard key and a negative keyboard
        ///     key, returns a value indicating if the positive, the
        ///     negative, or both keyboard keys were just released.
        /// </summary>
        /// <param name="positive">
        ///     The keyboard key to represent the positive axis.
        /// </param>
        /// <param name="negative">
        ///     The keyboard key to represent the negative axis.
        /// </param>
        /// <param name="both">
        ///     The value to return if both the positive and negative keyboard
        ///     keys were released.
        /// </param>
        /// <returns>
        ///     1 is returned if the positive keyboard key was just released, -1 is returned
        ///     if the negative keyboard key was just released , 0 is returned if neither
        ///     keyboard key was just released, or the value of <paramref name="both"/>
        ///     is returned if both keyboard keys were just released.
        /// </returns>
        public int AxisReleased(Keys positive, Keys negative, int both)
        {
            if (KeyReleased(positive))
            {
                if (KeyReleased(negative))
                {
                    return both;
                }
                else
                {
                    return 1;
                }
            }
            else if (KeyReleased(negative))
            {
                return -1;
            }
            else
            {
                return 0;
            }
        }
    }
}
