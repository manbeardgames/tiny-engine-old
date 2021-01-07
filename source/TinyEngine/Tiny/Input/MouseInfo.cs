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

using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tiny
{
    public class MouseInfo
    {
        /// <summary>
        ///     Gets the current state of mouse input.
        /// </summary>
        public MouseState PreviousState { get; private set; }

        /// <summary>
        ///     Gets the previous state of mouse input.
        /// </summary>
        public MouseState CurrentState { get; private set; }

        /// <summary>
        ///     Gets or Sets the xy-coordinate screen position of the 
        ///     mouse cursor
        /// </summary>
        public Point Position
        {
            get
            {
                return CurrentState.Position;
            }
            set
            {
                Mouse.SetPosition(value.X, value.Y);
            }
        }

        /// <summary>
        ///     Gets or Sets the x-coordinate screen position of the
        ///     mouse cursor.
        /// </summary>
        public int X
        {
            get
            {
                return Position.X;
            }
            set
            {
                Position = new Point(value, Position.Y);
            }
        }

        /// <summary>
        ///     Gets or Sets the y-coordinate screen position of the
        ///     mouse cursor.
        /// </summary>
        public int Y
        {
            get
            {
                return Position.Y;
            }
            set
            {
                Position = new Point(Position.X, value);
            }
        }

        /// <summary>
        ///     Gets a value indicating if the mouse position was
        ///     moved between the previous and current frames.
        /// </summary>
        public bool WasMoved
        {
            get
            {
                return CurrentState.Position != PreviousState.Position;
            }
        }

        /// <summary>
        ///     Gets the difference in the mouse's x-coordinate screen
        ///     position between the previous frame and current frame.
        /// </summary>
        public int DeltaX
        {
            get
            {
                return PreviousState.X - CurrentState.X;
            }
        }

        /// <summary>
        ///     Gets the difference in the mouse's y-coordinate screen
        ///     position between the previous frame and current frame.
        /// </summary>
        public int DeltaY
        {
            get
            {
                return PreviousState.Y - CurrentState.Y;
            }
        }

        /// <summary>
        ///     Gets the difference in the mouse's xy-coordinate screen
        ///     position between the previous frame and current frame.
        /// </summary>
        public Point DeltaPosition
        {
            get
            {
                return new Point(DeltaX, DeltaY);
            }
        }

        /// <summary>
        ///     Gets the value of the scroll wheel on the current frame.
        /// </summary>
        public int ScrollWheel
        {
            get
            {
                return CurrentState.ScrollWheelValue;
            }
        }

        /// <summary>
        ///     Gets the difference in the mouse's scroll wheel value
        ///     between the previous frame and the current frame.
        /// </summary>
        public int DeltaScrollWheel
        {
            get
            {
                return PreviousState.ScrollWheelValue - CurrentState.ScrollWheelValue;
            }
        }

        /// <summary>
        ///     Creates a new MouseInfo instance.
        /// </summary>
        public MouseInfo()
        {
            PreviousState = new MouseState();
            CurrentState = Mouse.GetState();
        }

        /// <summary>
        ///     Updates this instance.
        /// </summary>
        public void Update()
        {
            PreviousState = CurrentState;
            CurrentState = Mouse.GetState();
        }

        /// <summary>
        ///     Gets a value indicating if the given mouse button is currently down.
        /// </summary>
        /// <param name="button">
        ///     The mouse button to check.
        /// </param>
        /// <returns>
        ///     True if the given mouse button is currently down; otherwise,
        ///     false.
        /// </returns>
        public bool ButtonCheck(MouseButton button)
        {
            bool isDown(Func<MouseState, ButtonState> btn)
            {
                return btn(CurrentState) == ButtonState.Pressed;
            }

            switch (button)
            {
                case MouseButton.Left:
                    return isDown(m => m.LeftButton);
                case MouseButton.Middle:
                    return isDown(m => m.MiddleButton);
                case MouseButton.Right:
                    return isDown(m => m.RightButton);
                case MouseButton.XButton1:
                    return isDown(m => m.XButton1);
                case MouseButton.XButton2:
                    return isDown(m => m.XButton2);
                default:
                    throw new ArgumentException($"Invalid MouseButton value. Value given is: {button}", nameof(button));
            }
        }

        /// <summary>
        ///     Gets a value indicating if the given mouse button was
        ///     just pressed on the current frame.
        /// </summary>
        /// <remarks>
        ///     Pressed means the mouse button is down on the current frame
        ///     and up on the previous frame.
        /// </remarks>
        /// <param name="button">
        ///     The mouse button to check.
        /// </param>
        /// <returns>
        ///     True if the given mouse button was just pressed; otherwise,
        ///     false.
        /// </returns>
        public bool ButtonPressed(MouseButton button)
        {
            bool isPressed(Func<MouseState, ButtonState> btn)
            {
                return btn(CurrentState) == ButtonState.Pressed &&
                       btn(PreviousState) == ButtonState.Released;
            }

            switch (button)
            {
                case MouseButton.Left:
                    return isPressed(m => m.LeftButton);
                case MouseButton.Middle:
                    return isPressed(m => m.MiddleButton);
                case MouseButton.Right:
                    return isPressed(m => m.RightButton);
                case MouseButton.XButton1:
                    return isPressed(m => m.XButton1);
                case MouseButton.XButton2:
                    return isPressed(m => m.XButton2);
                default:
                    throw new ArgumentException($"Invalid MouseButton value. Value given is: {button}", nameof(button));
            }
        }

        /// <summary>
        ///     Gets a value indicating if the given mouse button was
        ///     just pressed on the current frame.
        /// </summary>
        /// <remarks>
        ///     Pressed means the mouse button is down on the current frame
        ///     and up on the previous frame.
        /// </remarks>
        /// <param name="button">
        ///     The mouse button to check.
        /// </param>
        /// <returns>
        ///     True if the given mouse button was just pressed; otherwise,
        ///     false.
        /// </returns>
        public bool ButtonReleased(MouseButton button)
        {
            bool isReleased(Func<MouseState, ButtonState> btn)
            {
                return btn(CurrentState) == ButtonState.Released &&
                       btn(PreviousState) == ButtonState.Pressed;
            }

            switch (button)
            {
                case MouseButton.Left:
                    return isReleased(m => m.LeftButton);
                case MouseButton.Middle:
                    return isReleased(m => m.MiddleButton);
                case MouseButton.Right:
                    return isReleased(m => m.RightButton);
                case MouseButton.XButton1:
                    return isReleased(m => m.XButton1);
                case MouseButton.XButton2:
                    return isReleased(m => m.XButton2);
                default:
                    throw new ArgumentException($"Invalid MouseButton value. Value given is: {button}", nameof(button));
            }
        }
    }
}
