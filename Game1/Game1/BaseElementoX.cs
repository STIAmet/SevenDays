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
    public class BaseElementoX
    {
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;
        
        public BaseElementoX(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, bool visible= true)
        {
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime,heroi personagem)
        { if (personagem.nome != "")
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
                if (xBase <= xTamanho * -1)
                {
                    xBase = personagem.xFixoCenario + xTamanho;
                }
                else if (xBase >= xTamanho + personagem.xFixoCenario)
                {
                    xBase = -xTamanho;
                }
            }
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), cor);
            
            
        }
    }
}
