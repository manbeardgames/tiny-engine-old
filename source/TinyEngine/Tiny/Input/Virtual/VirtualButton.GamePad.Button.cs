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
using Microsoft.Xna.Framework.Input;

namespace Tiny
{
    public partial class VirtualButton
    {
        public partial class GamePad
        {
            /// <summary>
            ///     Represents a gamepad button input.
            /// </summary>
            public class Button : Node
            {
                //  The index of the gamepad.
                private int _index;

                //  The gamepad button value that represents this.
                private Buttons _button;

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="Button"/> is pressed down.
                /// </summary>
                public override bool Check => Input.GamePads[_index].ButtonCheck(_button);

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="Button"/> was just pressed on the current frame only.
                /// </summary>
                public override bool Pressed => Input.GamePads[_index].ButtonPressed(_button);

                /// <summary>
                ///     Gets a <see cref="bool"/> value indicating if this
                ///     <see cref="Button"/> was just released on the current frame only.
                /// </summary>
                public override bool Released => Input.GamePads[_index].ButtonReleased(_button);

                /// <summary>
                ///     Creates a new <see cref="Button"/> instance.
                /// </summary>
                /// <param name="index">
                ///     A <see cref="PlayerIndex"/> value that represents the index
                ///     of the gamepad this will pull values from.
                /// </param>
                /// <param name="button">
                ///     The <see cref="Buttons"/> value represented by this.
                /// </param>
                public Button(PlayerIndex index, Buttons button)
                    : this((int)index, button) { }


                /// <summary>
                ///     Creates a new <see cref="Button"/> instance.
                /// </summary>
                /// <param name="index">
                ///     The index of the gamepad this will pull values from.
                /// </param>
                /// <param name="button">
                ///     The <see cref="Buttons"/> value represented by this.
                /// </param>
                public Button(int index, Buttons button)
                {
                    _index = index;
                    _button = button;
                }
            }
        }
    }
}
