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
    public partial class VirtualButton
    {
        public partial class GamePad
        {
            public class RightStickUp : Node
            {
                //  The index of the gamepad.
                private int _index;

                /// <summary>
                ///     Gets or Sets a <see cref="float"/> value representing
                ///     the minimum value the right stick must reach when moving
                ///     right to be considered a valid value.
                /// </summary>
                public float Deadzone { get; set; }

                /// <summary>
                ///     Gets or Sets a <see cref="bool"/> value indicating if
                ///     the this node should use the <see cref="GamePadInfo.RightStickThreshold"/>
                ///     value as the deadzone value.
                /// </summary>
                public bool UseGlobalDeadzone { get; set; }

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="RightStickUp"/> node is pressed down.
                /// </summary>
                public override bool Check
                {
                    get
                    {
                        if (UseGlobalDeadzone)
                        {
                            return Input.GamePads[_index].RightStickUpCheck();
                        }
                        else
                        {
                            return Input.GamePads[_index].RightStickUpCheck(Deadzone);
                        }
                    }
                }

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="RightStickUp"/> node was just pressed on the
                ///     current frame only.
                /// </summary>
                public override bool Pressed
                {
                    get
                    {
                        if (UseGlobalDeadzone)
                        {
                            return Input.GamePads[_index].RightStickUpPressed();
                        }
                        else
                        {
                            return Input.GamePads[_index].RightStickUpPressed(Deadzone);
                        }
                    }
                }

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="RightStickUp"/> node was just released on the
                ///     current frame only.
                /// </summary>
                public override bool Released
                {
                    get
                    {
                        if (UseGlobalDeadzone)
                        {
                            return Input.GamePads[_index].RightStickUpReleased();
                        }
                        else
                        {
                            return Input.GamePads[_index].RightStickUpReleased(Deadzone);
                        }
                    }
                }

                /// <summary>
                ///     Creates a new <see cref="RightStickUp"/> instnace.
                /// </summary>
                /// <param name="index">
                ///     A <see cref="PlayerIndex"/> value that represents the index
                ///     of the gamepad this will pull values from.
                /// </param>
                public RightStickUp(PlayerIndex index) : this((int)index) { }

                /// <summary>
                ///     Creates a new <see cref="RightStickUp"/> instance.
                /// </summary>
                /// <param name="index">
                ///     The index of the gamepad this will pull values from.
                /// </param>
                public RightStickUp(int index)
                {
                    _index = index;
                    Deadzone = 0.0f;
                    UseGlobalDeadzone = true;
                }

                /// <summary>
                ///     Creates a new <see cref="RightStickUp"/> instnace.
                /// </summary>
                /// <param name="index">
                ///     A <see cref="PlayerIndex"/> value that represents the index
                ///     of the gamepad this will pull values from.
                /// </param>
                /// <param name="deadzone">
                ///     The minimum value the right thumbstick must reach when pushed
                ///     upwards to be considered a valid value.
                /// </param>
                public RightStickUp(PlayerIndex index, float deadzone) : this((int)index, deadzone) { }

                /// <summary>
                ///     Creates a new <see cref="RightStickUp"/> instance.
                /// </summary>
                /// <param name="index">
                ///     The index of the gamepad this will pull values from.
                /// </param>
                /// <param name="deadzone">
                ///     The minimum value the right thumbstick must reach when pushed
                ///     upwards to be considered a valid value.
                /// </param>
                public RightStickUp(int index, float deadzone)
                {
                    _index = index;
                    Deadzone = deadzone;
                    UseGlobalDeadzone = false;
                }
            }
        }
    }
}
