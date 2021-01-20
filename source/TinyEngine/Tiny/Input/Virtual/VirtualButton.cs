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

namespace Tiny
{
    /// <summary>
    ///     Represents a button type input whos values are either <c>true</c>
    ///     or <c>false</c>.
    /// </summary>
    public partial class VirtualButton : VirtualInput
    {
        //  A value indicating if a Pressed value of true should
        //  be consumed and return false.
        private bool _consumePress;

        /// <summary>
        ///     Gets the collection of <see cref="Node"/> instances that
        ///     represent this <see cref="VirtualButton"/>
        /// </summary>
        public List<Node> Nodes { get; private set; }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this
        ///     <see cref="VirtualButton"/> is pressed down.
        /// </summary>
        public bool Check
        {
            get
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i].Check)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this
        ///     <see cref="VirtualButton"/> was just pressed on the current
        ///     frame only.
        /// </summary>
        public bool Pressed
        {
            get
            {
                if (_consumePress)
                {
                    return false;
                }
                else
                {
                    for (int i = 0; i < Nodes.Count; i++)
                    {
                        if (Nodes[i].Pressed)
                        {
                            return true;
                        }
                    }

                    return false;
                }
            }
        }

        /// <summary>
        ///     Gets a <see cref="bool"/> value indicating if this
        ///     <see cref="VirtualButton"/> was just released on the current
        ///     frame only.
        /// </summary>
        public bool Released
        {
            get
            {
                for (int i = 0; i < Nodes.Count; i++)
                {
                    if (Nodes[i].Released)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        /// <summary>
        ///     Creates a new <see cref="VirtualButton"/> instance.
        /// </summary>
        public VirtualButton()
        {
            Nodes = new List<Node>();
            _consumePress = false;
        }

        /// <summary>
        ///     Updates this <see cref="VirtualButton"/> instance.
        /// </summary>
        /// <param name="time">
        ///     The timing values provided by TinyEngine.
        /// </param>
        public override void Update()
        {
            _consumePress = false;
        }

        /// <summary>
        ///     When this is called, this <see cref="VirtualButton"/> will not register a
        ///     <c>true</c> value for the <see cref="Pressed"/> property for the rest of
        ///     the current frame. 
        /// </summary>
        public void ConsumePress()
        {
            _consumePress = true;
        }

        public abstract class Node : VirtualNode
        {
            /// <summary>
            ///     Gets a <see cref="bool"/> value indicating if this
            ///     <see cref="Node"/> is pressed down.
            /// </summary>
            public abstract bool Check { get; }

            /// <summary>
            ///     Gets a <see cref="bool"/> value indicating if this
            ///     <see cref="Node"/> was just pressed on the current frame only.
            /// </summary>
            public abstract bool Pressed { get; }

            /// <summary>
            ///     Gets a <see cref="bool"/> value indicating if this
            ///     <see cref="Node"/> was just released on the current frame only.
            /// </summary>
            public abstract bool Released { get; }
        }
    }
}
