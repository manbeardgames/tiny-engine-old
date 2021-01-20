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
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Tiny
{
    public class GamePadInfo
    {
        //  These are all "buttons" that have analog values.
        private const Buttons _excludeButtons =
            Buttons.LeftThumbstickUp | Buttons.LeftThumbstickDown | Buttons.LeftThumbstickLeft | Buttons.LeftThumbstickRight |
            Buttons.RightThumbstickUp | Buttons.RightThumbstickDown | Buttons.RightThumbstickLeft | Buttons.RightThumbstickRight;

        //  A value indicating the strength of the gamepad's left motor vibration.
        private float _leftVibrateStrength;

        //  The amount of tiem remaining to vibrate the gamepad's left motor.
        private TimeSpan _leftVibrateTimeRemaining;

        //  A value indicating the strength of the gamepad's right motor vibration.
        private float _rightVibrateStrength;

        //  The amount of time remaining to vibrate the gamepad's right motor.
        private TimeSpan _rightVibrateTimeRemaining;

        /// <summary>
        ///     Gets the <see cref="PlayerIndex"/> value that describes
        ///     which player this gamepad is being used for.
        /// </summary>
        public PlayerIndex Player { get; private set; }

        /// <summary>
        ///     Gets the state of the gamepad input during the previous update
        ///     cycle.
        /// </summary>
        public GamePadState PreviousState { get; private set; }

        /// <summary>
        ///     Gets the state of the gamepad input during the current update
        ///     cycle.
        /// </summary>
        public GamePadState CurrentState { get; private set; }

        /// <summary>
        ///     Gets a value that indicates if this gamepad is currently attached.
        /// </summary>
        public bool IsAttached { get; private set; }

        /// <summary>
        ///     Gets or Sets the minimum values that the left thumbstick must reach
        ///     on the x or y axis to be considered a valid value.
        /// </summary>
        public Vector2 LeftStickThreshold { get; set; }

        /// <summary>
        ///      Gets or Sets the minimum values that the right thumbstick must reach
        ///     on the x or y axis to be considered a valid value.
        /// </summary>
        public Vector2 RightStickThreshold { get; set; }

        /// <summary>
        ///     Gets or Sets the minimum value that the left trigger must reach to be
        ///     considered a valid value.
        /// </summary>
        public float LeftTriggerThreshold { get; set; }

        /// <summary>
        ///     Gets or Sets the minimum value that the right trigger must reach to be
        ///     considered a valid value.
        /// </summary>
        public float RightTriggerThreshold { get; set; }

        /// <summary>
        ///     <para>
        ///         Gets a <see cref="Point"/> value representation of the Dpad,
        ///     </para>
        ///     <para>
        ///         <c>X</c> contains a value between <c>-1</c> and <c>1</c> to represent the
        ///         DPad-Left and Dpad-Right repsectivly. A value of <c>0</c> is
        ///         given if neither are pressed.
        ///     </para>
        ///         <c>Y</c> contains a value between <c>-1</c> and <c>1</c> to represent the
        ///         DPad-Up and Dpad-Down repsectivly. A value of <c>0</c> is
        ///         given if neither are pressed.
        ///     <para>
        ///     </para>
        /// </summary>
        public Point DPad
        {
            get
            {
                int x = ButtonCheck(Buttons.DPadLeft) ? -1 :
                        ButtonCheck(Buttons.DPadRight) ? 1 :
                        0;

                int y = ButtonCheck(Buttons.DPadUp) ? -1 :
                        ButtonCheck(Buttons.DPadDown) ? 1 :
                        0;

                return new Point(x, y);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets a <see cref="Vector2"/> value representation of the left thumbstick.
        ///     </para>
        ///     <para>
        ///         <c>X</c> contains a value between <c>-1.0f</c> and <c>1.0f</c> to reprsent
        ///         the horizontal x-axis of the left thumbstick.  
        ///     </para>
        ///     <para>
        ///         <c>Y</c> contains a value between <c>-1.0f</c> and <c>1.0f</c> to represent
        ///         the vertical y-axis of the left thumbstick.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     When retriving the values of the left thumbstick, the <see cref="LeftStickThreshold"/>
        ///     values are applied.  If the x or y axis values do not meet the threshold value then
        ///     a value of <c>0.0f</c> is given for them.
        /// </remarks>
        public Vector2 LeftStick
        {
            get
            {
                return GetLeftStickWithDeadzone(LeftStickThreshold.X, LeftStickThreshold.Y);
            }
        }

        /// <summary>
        ///     <para>
        ///         Gets a <see cref="Vector2"/> value representation of the right thumbstick.
        ///     </para>
        ///     <para>
        ///         <c>X</c> contains a value between <c>-1.0f</c> and <c>1.0f</c> to reprsent
        ///         the horizontal x-axis of the right thumbstick.  
        ///     </para>
        ///     <para>
        ///         <c>Y</c> contains a value between <c>-1.0f</c> and <c>1.0f</c> to represent
        ///         the vertical y-axis of the right thumbstick.
        ///     </para>
        /// </summary>
        /// <remarks>
        ///     When retriving the values of the left thumbstick, the <see cref="RightStickThreshold"/>
        ///     values are applied.  If the x or y axis values do not meet the threshold value then
        ///     a value of <c>0.0f</c> is given for them.
        /// </remarks>
        public Vector2 RightStick
        {
            get
            {
                return GetRightStickWithDeadzone(RightStickThreshold.X, RightStickThreshold.Y);
            }
        }

        /// <summary>
        ///     Gets the value of the left trigger.
        /// </summary>
        /// <remarks>
        ///     When retreving the value of the left trigger, the <see cref="LeftTriggerThreshold"/>
        ///     value is applied.  If the trigger value does not meet the threshold value then
        ///     a value of <c>0.0f</c> is returned.
        /// </remarks>
        public float LeftTrigger
        {
            get
            {
                return GetLeftTriggerWithThreshold(LeftTriggerThreshold);
            }
        }

        /// <summary>
        ///     Gets the value of the right trigger.
        /// </summary>
        /// <remarks>
        ///     When retreving the value of the right trigger, the <see cref="RightTriggerThrshold"/>
        ///     value is applied.  If the trigger value does not meet the threshold value then
        ///     a value of <c>0.0f</c> is returned.
        /// </remarks>
        public float RightTrigger
        {
            get
            {
                return GetRightTriggerWithThreshold(RightTriggerThreshold);
            }
        }

        /// <summary>
        ///     Creates a new <see cref="GamePadInfo"/> instance.
        /// </summary>
        /// <param name="player">
        ///     The <see cref="PlayerIndex"/> value of the player this info is for.
        /// </param>
        public GamePadInfo(PlayerIndex player)
        {
            Player = player;
        }

        /// <summary>
        ///     Updates the values for this instance.
        /// </summary>
        public void Update()
        {
            //  Cache the gamepad states
            PreviousState = CurrentState;
            CurrentState = GamePad.GetState(Player);

            //  If there is time remaining for the left motor to vibrate, then reduce
            //  the time by the update cycle delta time. If this bring it below zero
            //  then set the left vibrate to 0.
            if (_leftVibrateTimeRemaining > TimeSpan.Zero)
            {
                _leftVibrateTimeRemaining -= Engine.Time.ElapsedGameTime;
                if (_leftVibrateTimeRemaining <= TimeSpan.Zero)
                {
                    VibrateLeft(0, TimeSpan.Zero);
                }
            }

            //  If there is time remaining for the right motor to vibrate, then reduce
            //  the time by the update cycle delta time. If this bring it below zero
            //  then set the right vibrate to 0.
            if (_rightVibrateTimeRemaining > TimeSpan.Zero)
            {
                _rightVibrateTimeRemaining -= Engine.Time.ElapsedGameTime;
                if (_rightVibrateTimeRemaining <= TimeSpan.Zero)
                {
                    VibrateRight(0, TimeSpan.Zero);
                }
            }
        }

        /// <summary>
        ///     Tells the gamepad to vibrate the left and right motors.
        /// </summary>
        /// <param name="strength">
        ///     A value betwen 0.0f and 1.0f that describes the strength of the
        ///     vibration.
        /// </param>
        /// <param name="time">
        ///     The amount of time that the vibration should occur.
        /// </param>
        public void Vibrate(float strength, TimeSpan time)
        {
            VibrateLeft(strength, time);
            VibrateRight(strength, time);
        }

        /// <summary>
        ///     Tells the gamepad to vibrate the left and right motors.
        /// </summary>
        /// <param name="leftMotorStrength">
        ///     A value between 0.0f and 1.0f that describes the strenght of the
        ///     vibration for the left motor.
        /// </param>
        /// <param name="rightMotorStrength">
        ///     A value between 0.0f and 1.0f that describes the strenght of the
        ///     vibration for the right motor
        /// </param>
        /// <param name="time">
        ///     The amount of time that the vibration should occur.
        /// </param>
        public void Vibrate(float leftMotorStrength, float rightMotorStrength, TimeSpan time)
        {
            VibrateLeft(leftMotorStrength, time);
            VibrateRight(rightMotorStrength, time);
        }

        /// <summary>
        ///     Tells the gamepad to vibrate the left motor.
        /// </summary>
        /// <param name="strength">
        ///     A value between 0.0f and 1.0f that describes the strenght of the
        ///     vibration for the left motor.
        /// </param>
        /// <param name="time">
        ///     The amount of time that the vibration should occur.
        /// </param>
        public void VibrateLeft(float strength, TimeSpan time)
        {
            _leftVibrateStrength = strength;
            _leftVibrateTimeRemaining = time;
            GamePad.SetVibration(Player, _leftVibrateStrength, _rightVibrateStrength);
        }

        /// <summary>
        ///     Tells the gamepad to vibrate the right motor.
        /// </summary>
        /// <param name="strength">
        ///     A value between 0.0f and 1.0f that describes the strenght of the
        ///     vibration for the right motor.
        /// </param>
        /// <param name="time">
        ///     The amount of time that the vibration should occur.
        /// </param>
        public void VibrateRight(float strength, TimeSpan time)
        {
            _rightVibrateStrength = strength;
            _rightVibrateTimeRemaining = time;
            GamePad.SetVibration(Player, _leftVibrateStrength, _rightVibrateStrength);
        }

        /// <summary>
        ///     Tells the gamepad to stop vibration of all motors
        /// </summary>
        public void StopVibration()
        {
            _leftVibrateStrength = _rightVibrateStrength = 0;
            _leftVibrateTimeRemaining = _rightVibrateTimeRemaining = TimeSpan.Zero;
            Vibrate(0, TimeSpan.Zero);
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button is pressed down.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if any gamepad button is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonCheck()
        {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonCheck(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button is pressed down.
        /// </summary>
        /// <param name="buttons">
        ///     When this method returns, contains <see cref="Buttons"/> values for
        ///     each button that was fond to be pressed down.
        /// </param>
        /// <returns>
        ///     <c>true</c> if any gamepad button is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonCheck(out Buttons[] buttons)
        {
            List<Buttons> result = new List<Buttons>();

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonCheck(button))
                {
                    result.Add(button);
                }
            }

            buttons = result.ToArray();
            return buttons.Length > 0;
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button was pressed down on
        ///     the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if any gamepad button was pressed down on the current frame
        ///     only; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonPressed()
        {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonPressed(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button was pressed down on
        ///     the current frame only.
        /// </summary>
        /// <param name="buttons">
        ///     When this method returns, contains <see cref="Buttons"/> values for
        ///     each button that was fond to be pressed down on the current frame only.
        /// </param>
        /// <returns>
        ///     <c>true</c> if any gamepad button was pressed down on the current frame
        ///     only; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonPressed(out Buttons[] buttons)
        {
            List<Buttons> result = new List<Buttons>();

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonPressed(button))
                {
                    result.Add(button);
                }
            }

            buttons = result.ToArray();
            return buttons.Length > 0;
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button was released on
        ///     the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if any gamepad button was released on the current frame
        ///     only; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonReleased()
        {
            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonReleased(button))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        ///     Returns a value indicating if any gamepad button was released on
        ///     the current frame only.
        /// </summary>
        /// <param name="buttons">
        ///     When this method returns, contains <see cref="Buttons"/> values for
        ///     each button that was fond to be released on the current frame only.
        /// </param>
        /// <returns>
        ///     <c>true</c> if any gamepad button was released on the current frame
        ///     only; otherwise, <c>false</c>.
        /// </returns>
        public bool AnyButtonReleased(out Buttons[] buttons)
        {
            List<Buttons> result = new List<Buttons>();

            foreach (Buttons button in Enum.GetValues(typeof(Buttons)))
            {
                if (ButtonReleased(button))
                {
                    result.Add(button);
                }
            }

            buttons = result.ToArray();
            return buttons.Length > 0;
        }

        /// <summary>
        ///     Returns a value indicating if a gamepad button is pressed down.
        /// </summary>
        /// <param name="button">
        ///     The <see cref="Buttons"/> value to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <see cref="Buttons"/> value is pressed down;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool ButtonCheck(Buttons button)
        {
            if ((button & _excludeButtons) != 0)
            {
                return false;
            }
            else
            {
                return CurrentState.IsButtonDown(button);
            }
        }

        /// <summary>
        ///     Returns a value indicating if a gamepad button was just pressed on
        ///     the current frame only.
        /// </summary>
        /// <param name="button">
        ///     The <see cref="Buttons"/> value to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <see cref="Buttons"/> value was pressed on the
        ///     current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool ButtonPressed(Buttons button)
        {
            if ((button & _excludeButtons) != 0)
            {
                return false;
            }
            else
            {
                return CurrentState.IsButtonDown(button) &&
                       PreviousState.IsButtonUp(button);
            }
        }

        /// <summary>
        ///     Returns a value indicating if a gamepad button was just released on
        ///     the current frame only.
        /// </summary>
        /// <param name="button">
        ///     The <see cref="Buttons"/> value to check.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the <see cref="Buttons"/> value was released on the
        ///     current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool ButtonReleased(Buttons button)
        {
            if ((button & _excludeButtons) != 0)
            {
                return false;
            }
            else
            {
                return CurrentState.IsButtonUp(button) &&
                       PreviousState.IsButtonDown(button);
            }
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed to the left.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed left;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftCheck()
        {
            return LeftStickLeftCheck(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed to the left.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed left;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     pressed to the left on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed left on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftPressed()
        {
            return LeftStickLeftPressed(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     pressed to the left on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed left on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X < -deadzone &&
                   PreviousState.ThumbSticks.Left.X >= -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed to the left on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed to the left on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftReleased()
        {
            return LeftStickLeftReleased(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed to the left on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed to the left on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickLeftReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X >= -deadzone &&
                   PreviousState.ThumbSticks.Left.X < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightCheck()
        {
            return LeftStickRightCheck(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightPressed()
        {
            return LeftStickRightPressed(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X > deadzone &&
                   PreviousState.ThumbSticks.Left.X <= deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightReleased()
        {
            return LeftStickRightReleased(LeftStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickRightReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.X <= deadzone &&
                   PreviousState.ThumbSticks.Left.X > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed up.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed up;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpCheck()
        {
            return LeftStickUpCheck(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed up on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed up on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpPressed()
        {
            return LeftStickUpPressed(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed up on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed up on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y > deadzone &&
                   PreviousState.ThumbSticks.Left.Y <= deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed up on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpReleased()
        {
            return LeftStickUpReleased(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     relased from being pressed up on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickUpReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y <= deadzone &&
                   PreviousState.ThumbSticks.Left.Y > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed down.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed up;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownCheck()
        {
            return LeftStickDownCheck(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick is
        ///     pressed  down.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick is pressed down;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed down on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownPressed()
        {
            return LeftStickDownPressed(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     pressed down on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just pressed down on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y < -deadzone &&
                   PreviousState.ThumbSticks.Left.Y >= -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad left thumbstick was just
        ///     relased from being pressed down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownReleased()
        {
            return LeftStickDownReleased(LeftStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad left thumbstick was just
        ///     relased from being pressed down on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the left thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad left thumbstick was just released from
        ///     being pressed down on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool LeftStickDownReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Left.Y >= -deadzone &&
                   PreviousState.ThumbSticks.Left.Y < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftCheck()
        {
            return RightStickLeftCheck(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftPressed()
        {
            return RightStickLeftPressed(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X < -deadzone &&
                   PreviousState.ThumbSticks.Right.X >= -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftReleased()
        {
            return RightStickLeftReleased(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickLeftReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X >= -deadzone &&
                   PreviousState.ThumbSticks.Right.X < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightCheck()
        {
            return RightStickRightCheck(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightPressed()
        {
            return RightStickRightPressed(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed right on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X > deadzone &&
                   PreviousState.ThumbSticks.Right.X <= deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightReleased()
        {
            return RightStickRightReleased(RightStickThreshold.X);
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed to the right on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed to the right on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickRightReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.X <= deadzone &&
                   PreviousState.ThumbSticks.Right.X > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed up.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed up;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpCheck()
        {
            return RightStickUpCheck(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed to the right.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed right;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed up on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed up on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpPressed()
        {
            return RightStickUpPressed(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed up on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed up on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y > deadzone &&
                   PreviousState.ThumbSticks.Right.Y <= deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed up on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpReleased()
        {
            return RightStickUpReleased(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     relased from being pressed up on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickUpReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y <= deadzone &&
                   PreviousState.ThumbSticks.Right.Y > deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed down.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed up;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownCheck()
        {
            return RightStickDownCheck(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick is
        ///     pressed  down.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick is pressed down;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownCheck(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y < -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed down on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownPressed()
        {
            return RightStickDownPressed(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     pressed down on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just pressed down on
        ///     the current frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownPressed(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y < -deadzone &&
                   PreviousState.ThumbSticks.Right.Y >= -deadzone;
        }

        /// <summary>
        ///     Returns a value indicating fithe gamepad right thumbstick was just
        ///     relased from being pressed down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed up on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownReleased()
        {
            return RightStickDownReleased(RightStickThreshold.Y);
        }

        /// <summary>
        ///     Returns a value indicating if the gamepad right thumbstick was just
        ///     relased from being pressed down on the current frame only.
        /// </summary>
        /// <param name="deadzone">
        ///     A value that the right thumbstick must reach in order for
        ///     its value to be considered valid.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the gamepad right thumbstick was just released from
        ///     being pressed down on the current frame only;
        ///     otherwise, <c>false</c>.
        /// </returns>
        public bool RightStickDownReleased(float deadzone)
        {
            return CurrentState.ThumbSticks.Right.Y >= -deadzone &&
                   PreviousState.ThumbSticks.Right.Y < -deadzone;
        }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value where <see cref="Vector2.X"/> is
        ///     equal to the left thumbstick's x-axis value and <see cref="Vector2.Y"/>
        ///     is equal to the left thumbstick's y-axis value.
        /// </summary>
        /// <param name="deadzone">
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is the minimum
        ///     the left thumbstick must move on the x-axis for be considered valid and
        ///     <see cref="Vector2.Y"/> is the minimum the left thumbstick must move on
        ///     the y-axis to be considered valid.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is equal
        ///     to the left thumbstick's x-axis value and <see cref="Vector2.Y"/> is
        ///     equal to the left thumbstick's y-axis value.
        /// </returns>
        public Vector2 GetLeftStickWithDeadzone(Vector2 deadzone)
        {
            return GetLeftStickWithDeadzone(deadzone.X, deadzone.Y);
        }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value where <see cref="Vector2.X"/> is
        ///     equal to the left thumbstick's x-axis value and <see cref="Vector2.Y"/>
        ///     is equal to the left thumbstick's y-axis value.
        /// </summary>
        /// <param name="deadzoneX">
        ///     A <see cref="float"/> value equal to the minimum value the left thumstick's
        ///     x-axis must reach in order to be considered valid.
        ///  </param>
        /// <param name="deadzoneY">
        ///     A <see cref="float"/> value equal to the minimum value the left thumstick's
        ///     y-axis must reach in order to be considered valid.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is equal
        ///     to the left thumbstick's x-axis value and <see cref="Vector2.Y"/> is
        ///     equal to the left thumbstick's y-axis value.
        /// </returns>
        public Vector2 GetLeftStickWithDeadzone(float deadzoneX, float deadzoneY)
        {
            float x = CurrentState.ThumbSticks.Left.X;
            float y = CurrentState.ThumbSticks.Left.Y;

            x = Math.Abs(x) >= deadzoneX ? x : 0;
            y = Math.Abs(y) >= deadzoneY ? y : 0;

            return new Vector2(x, -y);
        }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value where <see cref="Vector2.X"/> is
        ///     equal to the right thumbstick's x-axis value and <see cref="Vector2.Y"/>
        ///     is equal to the right thumbstick's y-axis value.
        /// </summary>
        /// <param name="deadzone">
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is the minimum
        ///     the right thumbstick must move on the x-axis for be considered valid and
        ///     <see cref="Vector2.Y"/> is the minimum the right thumbstick must move on
        ///     the y-axis to be considered valid.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is equal
        ///     to the right thumbstick's x-axis value and <see cref="Vector2.Y"/> is
        ///     equal to the right thumbstick's y-axis value.
        /// </returns>
        public Vector2 GetRightStickWithDeadzone(Vector2 deadzone)
        {
            return GetRightStickWithDeadzone(deadzone.X, deadzone.Y);
        }

        /// <summary>
        ///     Gets a <see cref="Vector2"/> value where <see cref="Vector2.X"/> is
        ///     equal to the right thumbstick's x-axis value and <see cref="Vector2.Y"/>
        ///     is equal to the right thumbstick's y-axis value.
        /// </summary>
        /// <param name="deadzoneX">
        ///     A <see cref="float"/> value equal to the minimum value the right thumstick's
        ///     x-axis must reach in order to be considered valid.
        ///  </param>
        /// <param name="deadzoneY">
        ///     A <see cref="float"/> value equal to the minimum value the right thumstick's
        ///     y-axis must reach in order to be considered valid.
        /// </param>
        /// <returns>
        ///     A <see cref="Vector2"/> value where <see cref="Vector2.X"/> is equal
        ///     to the right thumbstick's x-axis value and <see cref="Vector2.Y"/> is
        ///     equal to the right thumbstick's y-axis value.
        /// </returns>
        public Vector2 GetRightStickWithDeadzone(float deadzoneX, float deadzoneY)
        {
            float x = CurrentState.ThumbSticks.Right.X;
            float y = CurrentState.ThumbSticks.Right.Y;

            x = Math.Abs(x) >= deadzoneX ? x : 0;
            y = Math.Abs(y) >= deadzoneY ? y : 0;

            return new Vector2(x, -y);
        }

        /// <summary>
        ///     Returns a value that indicates if the left trigger is pressed down.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the left rigger is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerCheck()
        {
            return LeftTriggerCheck(LeftTriggerThreshold);
        }

        /// <summary>
        ///     Returns a vlaue that indicates if the left trigger is pressed down.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the left trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the left rigger is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerCheck(float threshold)
        {
            return CurrentState.Triggers.Left > threshold;
        }

        /// <summary>
        ///     Returns a value that indicates if the left trigger was just pressed
        ///     down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the left rigger was just pressed down on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerPressed()
        {
            return LeftTriggerPressed(LeftTriggerThreshold);
        }

        /// <summary>
        ///     Returns a value that indicates if the left trigger was just pressed
        ///     down on the current frame only.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the left trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the left rigger was just pressed down on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerPressed(float threshold)
        {
            return CurrentState.Triggers.Left > threshold &&
                   PreviousState.Triggers.Left <= threshold;
        }

        /// <summary>
        ///     Returns a value that indicates if the left trigger was just released
        ///     on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the left rigger was just released on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerReleased()
        {
            return LeftTriggerReleased(LeftTriggerThreshold);
        }

        /// <summary>
        ///     Returns a value that indicates if the left trigger was just released
        ///     on the current frame only.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the left trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the left rigger was just released on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool LeftTriggerReleased(float threshold)
        {
            return CurrentState.Triggers.Left <= threshold &&
                   PreviousState.Triggers.Left > threshold;
        }

        /// <summary>
        ///     Returns a value that indicates if the right trigger is pressed down.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the right rigger is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerCheck()
        {
            return RightTriggerCheck(RightTriggerThreshold);
        }

        /// <summary>
        ///     Returns a vlaue that indicates if the right trigger is pressed down.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the right trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the right rigger is pressed down; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerCheck(float threshold)
        {
            return CurrentState.Triggers.Right > threshold;
        }

        /// <summary>
        ///     Returns a value that indicates if the right trigger was just pressed
        ///     down on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the right rigger was just pressed down on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerPressed()
        {
            return RightTriggerPressed(RightTriggerThreshold);
        }

        /// <summary>
        ///     Returns a value that indicates if the right trigger was just pressed
        ///     down on the current frame only.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the right trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the right rigger was just pressed down on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerPressed(float threshold)
        {
            return CurrentState.Triggers.Right > threshold &&
                   PreviousState.Triggers.Right <= threshold;
        }

        /// <summary>
        ///     Returns a value that indicates if the right trigger was just released
        ///     on the current frame only.
        /// </summary>
        /// <returns>
        ///     <c>true</c> if the right rigger was just released on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerReleased()
        {
            return RightTriggerReleased(RightTriggerThreshold);
        }

        /// <summary>
        ///     Returns a value that indicates if the right trigger was just released
        ///     on the current frame only.
        /// </summary>
        /// <param name="threshold">
        ///     The minimum value the right trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     <c>true</c> if the right rigger was just released on the current
        ///     frame only; otherwise, <c>false</c>.
        /// </returns>
        public bool RightTriggerReleased(float threshold)
        {
            return CurrentState.Triggers.Right <= threshold &&
                   PreviousState.Triggers.Right > threshold;
        }

        /// <summary>
        ///     Gets the value of the left trigger.
        /// </summary>
        /// <remarks>
        ///     If the value of the left trigger is less than the threshold value provided
        ///     then a value of <c>0.0f</c> will be returned.
        /// </remarks>
        /// <param name="threshold">
        ///     The minimum value the trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value between <c>0.0f</c> and <c>1.0f</c>
        /// </returns>
        public float GetLeftTriggerWithThreshold(float threshold)
        {
            float value = CurrentState.Triggers.Left;

            return Math.Abs(value) >= threshold ? value : 0.0f;
        }

        /// <summary>
        ///     Gets the value of the right trigger.
        /// </summary>
        /// <remarks>
        ///     If the value of the right trigger is less than the threshold value provided
        ///     then a value of <c>0.0f</c> will be returned.
        /// </remarks>
        /// <param name="threshold">
        ///     The minimum value the trigger must reach to be considered a
        ///     valid value.
        /// </param>
        /// <returns>
        ///     A <see cref="float"/> value between <c>0.0f</c> and <c>1.0f</c>
        /// </returns>
        public float GetRightTriggerWithThreshold(float threshold)
        {
            float value = CurrentState.Triggers.Right;

            return Math.Abs(value) >= threshold ? value : 0.0f;
        }

        public override string ToString()
        {
            return $"{GamePad.GetCapabilities(Player).DisplayName}\n" +
                   $"Left Stick: {LeftStick}\n" +
                   $"Left Stick Up: {LeftStickUpCheck()}\n" +
                   $"Left Stick Down: {LeftStickDownCheck()}\n" +
                   $"Left Stick Left: {LeftStickLeftCheck()}\n" +
                   $"Left Stick Right: {LeftStickRightCheck()}\n\n" +

                   $"Right Stick: {RightStick}\n" +
                   $"Right Stick Up: {RightStickUpCheck()}\n" +
                   $"Right Stick Down: {RightStickDownCheck()}\n" +
                   $"Right Stick Left: {RightStickLeftCheck()}\n" +
                   $"Right Stick Right: {RightStickRightCheck()}\n\n" +

                   $"Left Trigger: {LeftTrigger}\n" +
                   $"Left Trigger Check: {LeftTriggerCheck()}\n\n" +

                   $"Right Trigger: {RightTrigger}\n" +
                   $"Left Trigger Check: {RightTriggerCheck()}\n\n" +

                   $"DPad-Up: {ButtonCheck(Buttons.DPadUp)}\n" +
                   $"DPad-Down: {ButtonCheck(Buttons.DPadDown)}\n" +
                   $"DPad-Left: {ButtonCheck(Buttons.DPadLeft)}\n" +
                   $"DPad-Right: {ButtonCheck(Buttons.DPadRight)}\n\n" +

                   $"Button A: {ButtonCheck(Buttons.A)}\n" +
                   $"Button B: {ButtonCheck(Buttons.B)}\n" +
                   $"Button X: {ButtonCheck(Buttons.X)}\n" +
                   $"Button Y: {ButtonCheck(Buttons.Y)}\n\n" +

                   $"Left Shoulder: {ButtonCheck(Buttons.LeftShoulder)}\n" +
                   $"Right Shoulder: {ButtonCheck(Buttons.RightShoulder)}\n\n" +

                   $"Button Start: {ButtonCheck(Buttons.Start)}\n" +
                   $"Button Back: {ButtonCheck(Buttons.Back)}\n\n" +

                   $"Button BigButton: {ButtonCheck(Buttons.BigButton)}";



        }
    }
}
