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
    public class Cinto
    {
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;
        public int maxElementos=5;
        public double maxPeso=50.0;
        public List<Elemento> elementos;
        public List<bool> posicaoCinto;

        public Cinto(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, bool visible = true)
        {
            int i;
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
            elementos = new List<Elemento>() {null, null, null, null, null };
            posicaoCinto = new List<bool>();

            for (i = 0; i < maxElementos; i++)
            {
                posicaoCinto.Add(false);
            }
        }

        public string Add( Elemento Item)
        {
            if (elementos.Count(x=> x!= null) == maxElementos)
            {
                return "Cinto cheio";
            }
            for(int i = 0; i<maxElementos;i++)
            {
                if(posicaoCinto[i] == false)
                {
                    posicaoCinto[i] = true;           
                    elementos[i] = Item;
                    break;
                }
            }
            return "OK";

        }

        public List<Elemento> ListarElementos()
        {
            return elementos;
        }

        public bool Remover(int posicao, string nome)
        {
            if (posicaoCinto[posicao] == true && elementos[posicao].nome==nome)
            {
                elementos[posicao]=null;
                posicaoCinto[posicao] = false;
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {
            //spriteBatch.Draw(texture, new Rectangle(xBase, yBase, 50, 50), Color.White);
            //spriteBatch.Draw(textura, new Rectangle(xBase+75, yBase, 50, 50), Color.White);
            //spriteBatch.Draw(textura, new Rectangle(xBase + 150, yBase, 50, 50), Color.White);
            //spriteBatch.Draw(textura, new Rectangle(xBase + 225, yBase, 50, 50), Color.White);
            //spriteBatch.Draw(textura, new Rectangle(xBase + 300, yBase, 50, 50), Color.White);

            for (int i = 0; i < maxElementos; i++)
            {
                if (posicaoCinto[i] == true)
                {
                    spriteBatch.Draw(elementos[i].textura, new Rectangle(xBase+(i*75), yBase, 50, 50), Color.White);
                }
                else
                    spriteBatch.Draw(textura, new Rectangle(xBase + (i * 75), yBase, 50, 50), Color.White);
            }
        }
    }
}
