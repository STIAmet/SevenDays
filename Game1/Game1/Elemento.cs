using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace Game1
{
    public class Elemento
    {
        public string nome;
        public int peso;
        public int ataque=0; //arma
        public int cura=0; //poção
        private SpriteFont font;
        private int score = 0;
        public int xBase;
        public int yBase;
        public int xTamanho;
        public int yTamanho;
        public Texture2D textura;
        public Color cor;
        public bool IsVisible;

        //public Elemento(Game game) : base(game)
        //{
        //    Texture = game.Content.Load<Texture2D>("poção");
        //    font = game.Content.Load<SpriteFont>("score");

        //}
        public Elemento(string nomezinho,int xBasinha, int yBasinha, int xTamainho, int yTamainho, Texture2D text, Color corzinha, int _cura, int _ataque, bool visible = true)
        {
            xBase = xBasinha;
            yBase = yBasinha;
            xTamanho = xTamainho;
            yTamanho = yTamainho;
            textura = text;
            cor = corzinha;
            IsVisible = visible;
            nome = nomezinho;
            cura = _cura;
            ataque = _ataque;
        }


        public void Draw(SpriteBatch spriteBatch, GameTime gameTime, heroi personagem)
        {
            #region USAR_ITENS

                #region Cinto
                for (int i = 0; i < personagem.cinto.maxElementos; i++)
                {
                    if (personagem.cinto.posicaoCinto[i] == true)
                    {
                        if ((new Rectangle(personagem.cinto.xBase + (i * 75), personagem.cinto.yBase, 50, 50).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Keyboard.GetState().IsKeyDown(Keys.Space)))
                        {

                            if (personagem.cinto.elementos[i].ataque == 0)
                            {
                                personagem.barraVida.Pocao(cura);
                                personagem.cinto.Remover(i, personagem.cinto.elementos[i].nome);
                            }
                            else if(this.ataque>0) {
                            
                                personagem.armaEquipada.AddCinto(i, personagem);
                                personagem.armaEquipada.click = true;
                            }
                            return;

                        }
                    }
                }
            #endregion

                #region Mochila
                if (personagem.mochila.elementos.Count != 0)
                {
                    if (personagem.mochila.elementos.Peek().nome == this.nome)
                    {
                        if ((new Rectangle(personagem.mochila.xBase, personagem.mochila.yBase, 50, 50).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Keyboard.GetState().IsKeyDown(Keys.Space)))
                        {
                        
                            if (personagem.mochila.elementos.Peek().ataque == 0)
                            {
                                personagem.barraVida.Pocao(cura);
                                personagem.mochila.Remover(personagem.mochila.elementos.Peek().nome);
                            }
                            else if (this.ataque > 0)
                            {

                                personagem.armaEquipada.AddMochila(personagem);
                                personagem.armaEquipada.click = true;
                            }
                            return;

                        }
                    }
                }
                #endregion

            if (Keyboard.GetState().IsKeyUp(Keys.Space)) //ISSO É USADO PARA EVITAR MULTIPLOS CLIQUES
            {
                personagem.armaEquipada.click = false;
            }

            #endregion


            #region Dropar_Itens

            #region Cinto
            for (int i = 0; i < personagem.cinto.maxElementos; i++)
            {
                if (personagem.cinto.posicaoCinto[i] == true)
                {
                    if ((new Rectangle(personagem.cinto.xBase + (i * 75), personagem.cinto.yBase, 50, 50).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().MiddleButton == ButtonState.Pressed))
                    {
                        if (personagem.cinto.Remover(i, this.nome))
                        {
                            var t = this;
                            xBase = new Random().Next(Convert.ToInt32(personagem.Posicao.X - 100), Convert.ToInt32(personagem.Posicao.X + 100));
                            yBase = Convert.ToInt32((personagem.Posicao.Y + 100) - this.yTamanho);
                            IsVisible = true;
                        }
                        

                    }
                }
            }
            #endregion

            #region Mochila
            if (personagem.mochila.elementos.Count!=0) 
            {
                if (personagem.mochila.elementos.Peek().nome == this.nome)
                {
                    if ((new Rectangle(personagem.mochila.xBase, personagem.mochila.yBase, 50, 50).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().MiddleButton == ButtonState.Pressed))
                    {
                        if (personagem.mochila.click == false)// Isso serve apenas para evitar que quando o usuario clique, seja contado varios cliques.(Sem isso, a mochila inteira seria dropada)
                        {
                            if (personagem.mochila.Remover(this.nome))
                            {
                                personagem.mochila.click = true;
                                xBase = new Random().Next(Convert.ToInt32(personagem.Posicao.X - 100), Convert.ToInt32(personagem.Posicao.X + 100));
                                yBase = Convert.ToInt32((personagem.Posicao.Y + 100) - this.yTamanho);
                                IsVisible = true;
                                personagem.mochila.Remover(this.nome);
                            }
                        }

                    }
                }
            }
            if (Mouse.GetState().MiddleButton == ButtonState.Released)
            {
                personagem.mochila.click = false;
            }
            #endregion

            #endregion


            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right))
            {
                // Direita
                if (Keyboard.GetState().IsKeyDown(Keys.Right))
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
            if (!IsVisible)
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Transparent);
            else
            if ((new Rectangle(xBase, yBase, xTamanho, yTamanho).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && !(Mouse.GetState().LeftButton == ButtonState.Pressed || Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Blue);
            }
            else if ((new Rectangle(xBase, yBase, xTamanho, yTamanho).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
            {
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Red);
                IsVisible = false;
                personagem.cinto.Add(this);
            }
            else if ((new Rectangle(xBase, yBase, xTamanho, yTamanho).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().RightButton == ButtonState.Pressed))
            {
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), Color.Red);
                IsVisible = false;
                personagem.mochila.Add(this);
            }
            else
                spriteBatch.Draw(textura, new Rectangle(xBase, yBase, xTamanho, yTamanho), cor);


            
        }
    }
}
