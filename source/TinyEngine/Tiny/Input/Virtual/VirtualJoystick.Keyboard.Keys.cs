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
        public partial class Keyboard
        {
            public class Keys : Node
            {
                //  The value of this node.
                private Vector2 _value;

                /// <summary>
                ///     Gets a <see cref="Vector2"/> value where <see cref="Value.X"/> is
                ///     the value of this node on its x-axis and <see cref="Value.Y"/> is
                ///     the value of this node on its y-axis
                /// </summary>
                public override Vector2 Value => _value;

                /// <summary>
                ///     Gets or Sets the <see cref="Tiny.InputOverlapBehavior"/> value to use
                ///     when two or more inputs are detected.
                /// </summary>
                public InputOverlapBehavior OverlapBehavior { get; set; }

                /// <summary>
                ///     Gets or Sets the <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represents pushing
                ///     the <see cref="VirtualJoystick"/> upwards.
                /// </summary>
                public Microsoft.Xna.Framework.Input.Keys Up { get; set; }

                /// <summary>
                ///     Gets or Sets the <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represetns pushing
                ///     the <see cref="VirtualJoystick"/> downwards.
                /// </summary>
                public Microsoft.Xna.Framework.Input.Keys Down { get; set; }

                /// <summary>
                ///     Gets or Sets the <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represents pushing
                ///     the <see cref="VirtualJoystick"/> left.
                /// </summary>
                public Microsoft.Xna.Framework.Input.Keys Left { get; set; }

                /// <summary>
                ///     Gets or Sets the <see cref="Microsoft.Xna.Framework.Input.Keys"/> value that represents pushing
                ///     the <see cref="VirtualJoystick"/> right.
                /// </summary>
                public Microsoft.Xna.Framework.Input.Keys Right { get; set; }

                /// <summary>
                ///     Creates a new <see cref="Keys"/> instance.
                /// </summary>
                /// <param name="behavior">
                ///     A <see cref="OverlapBehavior"/> value to use when two or
                ///     more inputs are detected at the same time.
                /// </param>
                /// <param name="up">
                ///     A <see cref="Microsoft.Xna.Framework.Input.Keys"/> value
                ///     that represents pushing the <see cref="VirtualJoystick"/>
                ///     upwards.
                /// </param>
                /// <param name="down">
                ///     A <see cref="Microsoft.Xna.Framework.Input.Keys"/> value
                ///     that represents pushing the <see cref="VirtualJoystick"/>
                ///     downwards.
                /// </param>
                /// <param name="left">
                ///     A <see cref="Microsoft.Xna.Framework.Input.Keys"/> value
                ///     that represents pushing the <see cref="VirtualJoystick"/>
                ///     left.
                /// </param>
                /// <param name="right">
                ///     A <see cref="Microsoft.Xna.Framework.Input.Keys"/> value
                ///     that represents pushing the <see cref="VirtualJoystick"/>
                ///     right.
                /// </param>
                public Keys(InputOverlapBehavior behavior,
                            Microsoft.Xna.Framework.Input.Keys up,
                            Microsoft.Xna.Framework.Input.Keys down,
                            Microsoft.Xna.Framework.Input.Keys left,
                            Microsoft.Xna.Framework.Input.Keys right)
                {
                    OverlapBehavior = behavior;
                    Up = up;
                    Down = down;
                    Left = left;
                    Right = right;
                }

                /// <summary>
                ///     Updates the value of this <see cref="Buttons"/> instance.
                /// </summary>
                /// <param name="time">
                ///     Timing values provided by the TinyEngine.
                /// </param>
                public override void Update(Time time)
                {
                    bool isUp = Input.Keyboard.KeyCheck(Up);
                    bool isDown = Input.Keyboard.KeyCheck(Down);
                    bool isLeft = Input.Keyboard.KeyCheck(Left);
                    bool isRight = Input.Keyboard.KeyCheck(Right);

                    if (isUp)
                    {
                        if (isDown)
                        {
                            //  Both Up and Down are pressed so the value is determiend
                            //  by the overlap behavior.
                            switch (OverlapBehavior)
                            {
                                default:
                                case InputOverlapBehavior.Cancel:
                                    _value.Y = 0;
                                    break;
                                case InputOverlapBehavior.Positive:
                                    _value.Y = 1;
                                    break;
                                case InputOverlapBehavior.Negative:
                                    _value.Y = -1;
                                    break;
                            }
                        }
                        else
                        {
                            _value.Y = 1;
                        }
                    }
                    else if (isDown)
                    {
                        _value.Y = -1;
                    }
                    else
                    {
                        _value.Y = 0;
                    }


                    if (isLeft)
                    {
                        if (isRight)
                        {
                            //  Both Left and Right are pressed so the value is determiend
                            //  by the overlap behavior.
                            switch (OverlapBehavior)
                            {
                                default:
                                case InputOverlapBehavior.Cancel:
                                    _value.X = 0;
                                    break;
                                case InputOverlapBehavior.Positive:
                                    _value.X = 1;
                                    break;
                                case InputOverlapBehavior.Negative:
                                    _value.X = -1;
                                    break;

                            }
                        }
                        else
                        {
                            _value.X = -1;
                        }
                    }
                    else if (isRight)
                    {
                        _value.X = 1;
                    }
                    else
                    {
                        _value.X = 0;
                    }
                }

            }
        }
    }
}
