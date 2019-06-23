using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class Mochila
    {
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;
        public Stack<Elemento> elementos;
        public bool click;

        public Mochila(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, bool visible = true)
        {
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
            elementos = new Stack<Elemento>();
            click = false;
        }

        public void Add(Elemento Item)
        {
            elementos.Push(Item);
        }

        public bool Remover(string nome)
        {

            if (elementos.Count != 0 && elementos.Peek().nome == nome && click == false)
            { elementos.Pop();
                return true;
            }
            return false;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {
      
                if (elementos.Count != 0)
                {
                    spriteBatch.Draw(elementos.Peek().textura, new Rectangle(xBase, yBase, 50, 50), elementos.Peek().cor);
                }
                else
                    spriteBatch.Draw(textura, new Rectangle(xBase, yBase, 50, 50), Color.White);
            
        }
    }
}
