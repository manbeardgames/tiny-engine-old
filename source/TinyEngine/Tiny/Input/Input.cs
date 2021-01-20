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

using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Tiny
{
    /// <summary>
    ///     Manages the state of input for the game.
    /// </summary>
    public static class Input
    {
        internal static List<VirtualInput> VirtualInputs { get; private set; }

        /// <summary>
        ///     Gets the state of keyboard input.
        /// </summary>
        public static KeyboardInfo Keyboard { get; private set; }

        /// <summary>
        ///     Gets the state of mouse input.
        /// </summary>
        public static MouseInfo Mouse { get; private set; }

        /// <summary>
        ///     Gets the state of gamepad inputs.
        /// </summary>
        public static GamePadInfo[] GamePads { get; private set; }

        /// <summary>
        ///     Initializes the input manager.
        /// </summary>
        public static void Initialize()
        {
            Keyboard = new KeyboardInfo();
            Mouse = new MouseInfo();

            GamePads = new GamePadInfo[4];
            for (int i = 0; i < 4; i++)
            {
                GamePads[i] = new GamePadInfo((PlayerIndex)i);
            }

            VirtualInputs = new List<VirtualInput>();
        }

        /// <summary>
        ///     Updates the input manager.
        /// </summary>
        public static void Update()
        {
            Keyboard.Update();
            Mouse.Update();

            for (int i = 0; i < 4; i++)
            {
                GamePads[i].Update();
            }

            for (int i = 0; i < VirtualInputs.Count; i++)
            {
                VirtualInputs[i].Update();
            }
        }
    }
}
