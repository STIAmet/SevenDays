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
    public class Inimigo:AnimacaoSprite
    {

        public string nome;
        public int vida;
        public bool isVisible=true;
        public TimeSpan lastHit = new TimeSpan();
        public TimeSpan lastAtak = new TimeSpan();
        public int nivelDano;
        public int frequenciaDano;
        public int distanciaPerseguir;
        public int AndarDireita, AndarEsquerda, AtacarDireita, AtacarEsquerda; //ISSO É USADO PARA PASSAR NA FUNÇÃO ANIMAÇÃO() Representar o Y da linha desejada na sprite do Inimigo.
        public int delayEntreFrames; // Indica a demora entre a passagem de cada frame da Sprite

        public Vector2 Velocidade;


        public Inimigo(string _nome, int _vida, int _posX, int _posY, int _nivelDano, int linhasSprite, int colunasSprite, int _AndarDireita, int _AndarEsquerda, int _AtacarDireita, int _AtacarEsquerda, int velocidade=100, int _delay=200, int _frequenciaDano = 3000, int _distanciaPerseguir = 1000)
        {
            nome = _nome;
            vida = _vida;
            Posicao = new Vector2(_posX, _posY);
            nivelDano = _nivelDano;
            frequenciaDano = _frequenciaDano;
            distanciaPerseguir = _distanciaPerseguir;


            TotalLinhasNaSprite = linhasSprite;
            TotalColunasNaSprite = colunasSprite;

            AndarDireita = _AndarDireita;
            AndarEsquerda = _AndarEsquerda;
            AtacarDireita = _AtacarDireita; 
            AtacarEsquerda = _AtacarEsquerda;

            delayEntreFrames = _delay;

            Velocidade = new Vector2(velocidade, 0);

        }

        public void Mover(ref GameTime gameTime, heroi personagem)
        {
            
            if (vida <= 0)
            {
                isVisible = false;
            }

            if (Keyboard.GetState().IsKeyDown(Keys.Left) || Keyboard.GetState().IsKeyDown(Keys.Right)|| Keyboard.GetState().IsKeyDown(Keys.D) || Keyboard.GetState().IsKeyDown(Keys.A))
            {
                // Direita
                if (Keyboard.GetState().IsKeyDown(Keys.Right) || Keyboard.GetState().IsKeyDown(Keys.D))
                {
                    if (personagem.Posicao.X >= (0.7 * personagem.xFixoCenario))
                    {
                        Posicao.X -= (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                    }
                }
                else
                {
                    if (personagem.Posicao.X <= (0.1 * personagem.xFixoCenario))
                    {
                        Posicao.X += (int)(personagem.Velocidade.X * (gameTime.ElapsedGameTime.TotalSeconds));
                    }
                }
            }

            if (isVisible)
            {
                lastHit += gameTime.ElapsedGameTime;
                if ((lastHit > TimeSpan.FromMilliseconds(1000)) && (new Rectangle((int)this.Posicao.X, (int)this.Posicao.Y, this._frameLargura, this._frameAltura).Contains(new Point(Mouse.GetState().X, Mouse.GetState().Y))) && (Mouse.GetState().LeftButton == ButtonState.Pressed))
                {
                    if (personagem.Posicao.X - this.Posicao.X < personagem.range && personagem.Posicao.X - this.Posicao.X > -1*personagem.range)
                    {
                        RecebeDano(personagem.ataque);
                        lastHit = TimeSpan.Zero;
                    }
                }
                else
                {
                    if (lastHit < TimeSpan.FromMilliseconds(400))
                    {
                        this.cor = Color.Red;
                    }
                    else
                    {
                        this.cor = Color.White;
                    }
                }
                double tempoDecorridoJogo = gameTime.ElapsedGameTime.TotalSeconds;
                if (isVisible && (personagem.Posicao.X - this.Posicao.X < distanciaPerseguir && personagem.Posicao.X - this.Posicao.X > -1*distanciaPerseguir))
                {

                    if (personagem.Posicao.X - this.Posicao.X > 75)
                    {
                        Ativado = true;
                        Animacao(ref gameTime, AndarDireita);
                        this.Posicao.X +=(float) (Velocidade.X* gameTime.ElapsedGameTime.TotalSeconds);
                        lastAtak = TimeSpan.Zero;
                    }
                    else if (personagem.Posicao.X - this.Posicao.X < -75)
                    {
                        Ativado = true;
                        Animacao(ref gameTime, AndarEsquerda);
                        this.Posicao.X -= (float)(Velocidade.X * gameTime.ElapsedGameTime.TotalSeconds);
                        lastAtak = TimeSpan.Zero;
                    }
                    else
                    {
                        #region Atacar!!
                        lastAtak += gameTime.ElapsedGameTime;
                        if (personagem.Posicao.X - this.Posicao.X > 0)
                        {
                            Ativado = true;
                            Animacao(ref gameTime, AtacarDireita, delayEntreFrames);
                            if (lastAtak >= TimeSpan.FromMilliseconds(frequenciaDano))
                            {
                                personagem.barraVida.GameOver(nivelDano);
                                lastAtak = TimeSpan.Zero;
                            }
                            
                        }
                        else if (personagem.Posicao.X - this.Posicao.X < 0)
                        {
                            Ativado = true;
                            Animacao(ref gameTime, AtacarEsquerda, delayEntreFrames);
                            if (lastAtak >= TimeSpan.FromMilliseconds(frequenciaDano))
                            {
                                personagem.barraVida.GameOver(nivelDano);
                                lastAtak = TimeSpan.Zero;
                            }
                        }
                        #endregion
                    }


                    Ativado = false;
                }

            }
            else {
                this.cor = Color.Transparent;
            }
        }

        public void RecebeDano(int dano)
        {
            vida -= dano;

        }

         

    }
}
