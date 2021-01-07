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

using Microsoft.Xna.Framework;

namespace Tiny
{
    public partial class VirtualJoystick
    {
        public partial class GamePad
        {
            public class LeftStick : Node
            {
                //  The index of the gamepad.
                private int _index;

                /// <summary>
                ///     Gets or Sets a <see cref="Vector2"/> value where
                ///     <see cref="Vector2.X"/> is the minimum the left
                ///     thumbstick must move on the x-axis for be considered
                ///     valid and <see cref="Vector2.Y"/> is the minimum the
                ///     left thumbstick must move on the y-axis to be considered
                ///     valid.
                /// </summary>
                public Vector2 Deadzone { get; set; }

                /// <summary>
                ///     Gets or Sets a <see cref="bool"/> value indicating if
                ///     the this node should use the <see cref="GamePadInfo.LeftStickThreshold"/>
                ///     value as the deadzone value.
                /// </summary>
                public bool UseGlobalDeadzone { get; set; }

                /// <summary>
                ///     Gets a <see cref="Vector2"/> value where <see cref="Value.X"/> is
                ///     the value of this node on its x-axis and <see cref="Value.Y"/> is
                ///     the value of this node on its y-axis
                /// </summary>
                public override Vector2 Value
                {
                    get
                    {
                        if (UseGlobalDeadzone)
                        {
                            return Input.GamePads[_index].LeftStick;
                        }
                        else
                        {
                            return Input.GamePads[_index].GetLeftStickWithDeadzone(Deadzone);
                        }
                    }
                }
                /// <summary>
                ///     Creates a new <see cref="LeftStick"/> instnace.
                /// </summary>
                /// <param name="index">
                ///     A <see cref="PlayerIndex"/> value that represents the index
                ///     of the gamepad this will pull values from.
                /// </param>
                public LeftStick(PlayerIndex index) : this((int)index) { }

                /// <summary>
                ///     Creates a new <see cref="LeftStick"/> instance.
                /// </summary>
                /// <param name="index">
                ///     The index of the gamepad this will pull values from.
                /// </param>
                public LeftStick(int index)
                {
                    _index = index;
                    Deadzone = Vector2.Zero;
                    UseGlobalDeadzone = true;
                }

                /// <summary>
                ///     Creates a new <see cref="LeftStick"/> instnace.
                /// </summary>
                /// <param name="index">
                ///     A <see cref="PlayerIndex"/> value that represents the index
                ///     of the gamepad this will pull values from.
                /// </param>
                /// <param name="deadzone">
                ///     A <see cref="float"/> value whoes value is the minimum
                ///     left thumbstick must move on the x-axis or y-axis to
                ///     be considered valid 
                /// </param>
                public LeftStick(PlayerIndex index, float deadzone)
                    : this((int)index, new Vector2(deadzone, deadzone)) { }

                /// <summary>
                ///     Creates a new <see cref="LeftStick"/> instance.
                /// </summary>
                /// <param name="index">
                ///     The index of the gamepad this will pull values from.
                /// </param>
                /// <param name="deadzone">
                ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/>
                ///     is the minimum the left thumbstick must move on the x-axis for be considered
                ///     valid and <see cref="Vector2.Y"/> is the minimum the left
                ///     thumbstick must move on the y-axis to be considered valid.
                /// </param>
                public LeftStick(int index, Vector2 deadzone)
                {
                    _index = index;
                    Deadzone = deadzone;
                    UseGlobalDeadzone = false;
                }
            }
        }
    }
}
