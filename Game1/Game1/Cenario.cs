using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Cenario
    {
        public int xPosicao;
        public int yPosicao;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;

        public Cenario(int xPosicaod, int yPosicaod, Texture2D text,Color corzinha,int xTamainho, int yTamainho)
        {
            xPosicao = xPosicaod;
            yPosicao = yPosicaod;
            textura = text;
            cor = corzinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;


        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime)
        {
            spriteBatch.Draw(textura, new Rectangle(xPosicao, yPosicao, xTamanho, yTamanho), cor);
        }
    }
}