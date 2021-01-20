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
    ///     Represents a virtual joystick with values that range from -1.0 to 1.0.
    /// </summary>
    public partial class VirtualJoystick : VirtualInput
    {
        /// <summary>
        ///     Gets the collection of <see cref="Node"/> instances that
        ///     represent this <see cref="VirtualButton"/>
        /// </summary>
        public List<Node> Nodes { get; private set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value that indicates if the
        ///     xy-axes value of this <see cref="VirtualJoystick"/> should snap
        ///     to cardinal and intercardinal angles.
        /// </summary>
        public bool IsSnapped { get; set; }

        /// <summary>
        ///     Gets or Sets a <see cref="bool"/> value that indicates if the
        ///     xy-axes value of this <see cref="VirtualJoystick"/> should be
        ///     normalized.
        /// </summary>
        public bool IsNormalized { get; set; }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value that represents the current value of
        ///     this <see cref="VirtualJoystick"/>, where <c>Value.X</c> is equal
        ///     to the value of the x-axis of this joystick and <c>Value.Y</c> is
        ///     equal to the value of the y-axis of this joystick.
        /// </summary>
        public Vector2 Value { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value that represents the previous value of
        ///     this <see cref="VirtualJoystick"/>, where <c>Value.X</c> is equal
        ///     to the value of the x-axis of this joystick and <c>Value.Y</c> is
        ///     equal to the value of the y-axis of this joystick.
        /// </summary>
        public Vector2 PreviousValue { get; private set; }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value that represents the delta, or difference,
        ///     between the current value and previous value of this <see cref="VirtualJoystick"/>.
        /// </summary>
        public Vector2 Delta
        {
            get
            {
                return Value - PreviousValue;
            }
        }

        /// <summary>
        ///     Creates a new <see cref="VirtualJoystick"/> instance.
        /// </summary>
        public VirtualJoystick() : this(false, false) { }

        /// <summary>
        ///     Creates a new <see cref="VirtualJoystick"/> instance.
        /// </summary>
        /// <param name="snapped">
        ///     A <see cref="bool"/> value that indicates if the value of this
        ///     <see cref="VirtualJoystick"/> should snap to cardinal and
        ///     intercardinal angles.
        /// </param>
        /// <param name="normalized">
        ///     A <see cref="bool"/> value that indicates if the value of this
        ///     <see cref="VirtualJoystick"/> should be normalized.
        /// </param>
        public VirtualJoystick(bool snapped, bool normalized) : base()
        {
            Nodes = new List<Node>();
            IsSnapped = snapped;
            IsNormalized = normalized;
        }

        /// <summary>
        ///     Updates this <see cref="VirtualJoystick"/> instance.
        /// </summary>
        /// <param name="time"></param>
        public override void Update()
        {
            for (int i = 0; i < Nodes.Count; i++)
            {
                Nodes[i].Update();
            }

            PreviousValue = Value;
            Value = Vector2.Zero;

            for (int i = 0; i < Nodes.Count; i++)
            {
                Vector2 newValue = Nodes[i].Value;

                if (newValue != Vector2.Zero)
                {
                    if (IsNormalized)
                    {
                        if (IsSnapped)
                        {
                            newValue = newValue.SnapNormal(8);
                        }
                        else
                        {
                            newValue.Normalize();
                        }
                    }
                    else if (IsSnapped)
                    {
                        newValue = newValue.Snap(8);
                    }

                    Value = newValue;
                    break;
                }
            }
        }

        public abstract class Node : VirtualNode
        {
            public abstract Vector2 Value { get; }
        }
    }
}
