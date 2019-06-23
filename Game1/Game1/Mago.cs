using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Mago
    {
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;

        public Mago(int _xbase, int _ybase, int xTamainho, int yTamainho, Texture2D text, Color _cor, bool visible=true)
        {
            xBase = _xbase;
            yBase = _ybase;
            textura = text;
            cor = _cor;
            IsVisible = visible;
            xTamanho = xTamainho;
            yTamanho = yTamainho;

        }
        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, Microsoft.Xna.Framework.Content.ContentManager Content, heroi personagem)
        {
            if (personagem.nome != "")
            {
                if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A))
                {
                    // Direita
                    if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                    {
                        if (personagem.Posicao.X >= (0.7 * personagem.xFixoCenario))
                        {
                            xBase -= (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                        }
                    }
                    else
                    {
                        if (personagem.Posicao.X <= (0.1 * personagem.xFixoCenario))
                        {
                            xBase += (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                        }
                    }
                }
            }
            if (!IsVisible)
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Transparent);
            else
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), cor);

        }
    }
}
