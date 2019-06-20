using Game1;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;

namespace Mono.Helper.Input
{
    public class KeyboardHelper
    {
        public KeyboardState State;
        public KeyboardState LastState;

        public string _stringValue = string.Empty;
        public Keys[] keys;

        private TimeSpan _acumulaTempo = TimeSpan.Zero;

        public void Update(GameTime gameTime, heroi personagem)
        {
            LastState = State;
            State = Keyboard.GetState();

            var keyboardState = Keyboard.GetState();
            keys = keyboardState.GetPressedKeys();

            _acumulaTempo += gameTime.ElapsedGameTime;

            if (keys.Length > 0 && _acumulaTempo >= TimeSpan.FromMilliseconds(200))
            {
                var keyValue = keys[0].ToString();
                if (keyValue == "Enter")
                {
                    personagem.nome = _stringValue;
                }
                else if(keyValue == "Space")
                {
                    _stringValue += " ";
                }
                else if (keyValue == "Back")
                {
                    if(_stringValue.Length > 0)
                        _stringValue = _stringValue.Remove(_stringValue.Length -1);
                }
                else if (keyValue == "D1" || keyValue == "NumPad1")
                {
                    _stringValue += "1";
                }
                else if (keyValue == "D2" || keyValue == "NumPad2")
                {
                    _stringValue += "2";
                }
                else if (keyValue == "D3" || keyValue == "NumPad3")
                {
                    _stringValue += "3";
                }
                else if (keyValue == "D4" || keyValue == "NumPad4")
                {
                    _stringValue += "4";
                }
                else if (keyValue == "D5" || keyValue == "NumPad5")
                {
                    _stringValue += "5";
                }
                else if (keyValue == "D6" || keyValue == "NumPad6")
                {
                    _stringValue += "6";
                }
                else if (keyValue == "D7" || keyValue == "NumPad7")
                {
                    _stringValue += "7";
                }
                else if (keyValue == "D8" || keyValue == "NumPad8")
                {
                    _stringValue += "8";
                }
                else if (keyValue == "D9" || keyValue == "NumPad9")
                {
                    _stringValue += "9";
                }
                else if (keyValue == "D0" || keyValue == "NumPad0")
                {
                    _stringValue += "0";
                }
                else if (keyValue != "Right" && keyValue != "Left" && keyValue != "Up" && keyValue != "Down" && keyValue != "Ç")
                {
                    _stringValue += keyValue;
                }
                _acumulaTempo = TimeSpan.Zero;
                
            }
        }

        public bool IsDown(Keys key)
        {
            bool result = false;

            if (LastState.IsKeyDown(key) && State.IsKeyDown(key))
                result = true;

            return result;
        }

        public bool IsPress(Keys key)
        {
            bool result = false;

            if (LastState.IsKeyUp(key) && State.IsKeyDown(key))
                result = true;

            return result;
        }

        public bool IsUp(Keys key)
        {
            bool result = false;

            if (LastState.IsKeyUp(key) && State.IsKeyUp(key))
                result = true;

            return result;
        }
    }
}