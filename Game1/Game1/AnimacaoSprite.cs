using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using System;

namespace Game1
{
    public abstract class AnimacaoSprite
    {
        public bool Ativado = false;
        public Vector2 Posicao = new Vector2(0, 450);

        public abstract int TotalLinhasNaSprite { get; }
        public abstract int TotalColunasNaSprite { get; }

        public Texture2D _textura;
        public int _frameLargura;
        public int _frameAltura;
        public Rectangle _regiaoDaTextura = Rectangle.Empty;
        public int _frameAtualDaColuna;
        public TimeSpan _acumulaTempo = TimeSpan.Zero;
        public Color cor =Color.White;

        public void LoadContent(ContentManager content, string assetName)
        {
            _textura = content.Load<Texture2D>(assetName);

            _frameLargura = _textura.Width / TotalColunasNaSprite;
            _frameAltura = _textura.Height / TotalLinhasNaSprite;

            _regiaoDaTextura = new Rectangle(0, 0, _frameLargura, _frameAltura);
        }

        public void Animacao(ref GameTime gameTime, int regiaoPosY, int ms=100)
        {
            if (!Ativado)
                return;

            _acumulaTempo += gameTime.ElapsedGameTime;

            if (_acumulaTempo >= TimeSpan.FromMilliseconds(ms))
            {
                _frameAtualDaColuna++;

                if (_frameAtualDaColuna == TotalColunasNaSprite)
                    _frameAtualDaColuna = 0;

                _acumulaTempo = TimeSpan.Zero;
            }

            _regiaoDaTextura.X = _frameAtualDaColuna * _frameLargura;
            _regiaoDaTextura.Y = regiaoPosY;
        }

        public void Draw(ref SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(_textura, Posicao, _regiaoDaTextura, cor);
        }
    }
}
