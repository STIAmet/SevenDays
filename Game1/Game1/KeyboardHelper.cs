// Developed by Danilo Borges
// 22/03/2019
// danilo.bsto@gmail.com
// Essa é uma classe de gerenciamento do teclado, referente ao meu projeto piloto Mono.Helper.
// O código está incompleto.
// Pode-se usar a vontade em outros projetos.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Mono.Helper.Input
{
    public class KeyboardHelper
    {
        public KeyboardState State;
        public KeyboardState LastState;

        public void Update(GameTime gameTime)
        {
            LastState = State;
            State = Keyboard.GetState();
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