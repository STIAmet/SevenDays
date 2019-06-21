using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class BarraVida
    {
        public Texture2D Vida;
        public int qtdVida = 3;
        public int vidaAgora = 100;
        public int [] ataque = new int [3];
        public int[] pocao = new int[2];
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D texturaVida;
        public Texture2D texturaBarra;
        public Texture2D texturaFundo;
        public Color cor;

        public BarraVida(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D textVida, Texture2D textBarra, Texture2D textFundo, Color corzinha, int qtd_vida, Texture2D textCorVida)
        {
            qtdVida = qtd_vida;
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            texturaVida = textVida;
            texturaBarra = textBarra;
            texturaFundo = textFundo;
            Vida = textCorVida;
            cor = corzinha;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {
            spriteBatch.Draw(texturaFundo, new Rectangle(xBase+20, yBase+15, 135, 40), Color.White);
            spriteBatch.Draw(texturaVida, new Rectangle(xBase+20, yBase+15, (int)(1.33f*vidaAgora), 35), Color.White);
            spriteBatch.Draw(texturaBarra, new Rectangle(xBase, yBase, 200, 100), Color.White);

            for (int i=0; i<qtdVida; i++)
            {
                spriteBatch.Draw(Vida, new Rectangle(xBase + (25 * i), yBase + 60, 20, 20), Color.White);
            }

        }


        public int Pocao(int nivelPocao)
        {
            pocao[0] = 5;
            pocao[1] = 100;

            vidaAgora += pocao[nivelPocao];

            if (vidaAgora >= 100)
                vidaAgora = 100;

            return vidaAgora;
        }

        public int GameOver (int nivelInimigo)
        {
            ataque[0]=5;
            ataque[1]=15;
            ataque[2]=25;

            vidaAgora -= ataque[nivelInimigo];

            if (vidaAgora <= 0 && qtdVida > 0)
                return - 1; //nesse caso retornaria ao inicio da fase

            else
            return vidaAgora;
        }

    }

    

    }
