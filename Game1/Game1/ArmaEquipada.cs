using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Game1
{
    public class ArmaEquipada
    {
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;
        public Elemento elementos;
        public bool click=false;

        public ArmaEquipada(int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, bool visible = true)
        {
            
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
            elementos = null;
            
        }

        public void AddCinto(int posicao, heroi personagem)
        {
            var aux = personagem.cinto.elementos[posicao];
            if (click == false)
            {
                if (elementos == null)
                {
                    elementos = aux;
                    personagem.cinto.Remover(posicao, personagem.cinto.elementos[posicao].nome);
                }
                else
                {
                    personagem.cinto.Remover(posicao, personagem.cinto.elementos[posicao].nome);
                    personagem.cinto.Add(elementos);
                    this.elementos = aux;                    
                }
                personagem.ataque = elementos.ataque;
                personagem.range = elementos.range;
            }
           
        }

        public void AddMochila(heroi personagem)
        {
            var aux = personagem.mochila.elementos.Peek();
            if (click == false)
            {
                if (elementos == null)
                {
                    elementos = aux;
                    personagem.mochila.Remover(personagem.mochila.elementos.Peek().nome);
                }
                else
                {
                    personagem.mochila.Remover(personagem.mochila.elementos.Peek().nome);
                    personagem.mochila.Add(elementos);
                    this.elementos = aux;
                }
                personagem.ataque = elementos.ataque;
                personagem.range = elementos.range;
            }

        }
        public void Remover()
        {
            elementos = null;
        }

        public virtual void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {            
            
            if (elementos != null)
            {
                spriteBatch.Draw(elementos.textura, new Rectangle(xBase, yBase, 50, 50), Color.White);
            }
            else
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, 50, 50), Color.White);
            
        }
    }
}
