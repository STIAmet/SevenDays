using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;


namespace Game1
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Cenario cenario;

        Elemento elemento;

        private SpriteFont font;

        Song music;

        heroi _personagem = new heroi();

        public List<BaseElementoX> listTree = new List<BaseElementoX>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
       
        protected override void Initialize()
        {
            music = Content.Load<Song>("GOTTA BE YOU");
            MediaPlayer.Play(music);

            graphics.PreferredBackBufferHeight = 600;
            graphics.PreferredBackBufferWidth = 1200;
            graphics.ApplyChanges();

            base.Initialize();
        }

        protected override void LoadContent()
        {
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Services.AddService(typeof(SpriteBatch), spriteBatch);

            font = Content.Load<SpriteFont>("score");

            _personagem.xFixoCenario = Window.ClientBounds.Width;

            //BARRA DE VIDA
            _personagem.barraVida = new BarraVida(0,0, 50, 25, 100, 25, Content.Load<Texture2D>("Vida"), Content.Load<Texture2D>("Barra_vida"),Content.Load<Texture2D>("fundo_barra"), Color.White, 3, Content.Load<Texture2D>("qtd_vida"));

            // INSTANCIAMENTO DO CINTO
            _personagem.cinto = new Cinto(400, 25, 400, 25, Content.Load<Texture2D>("cenoura"), Color.White);
            _personagem.mochila = new Mochila((Window.ClientBounds.Width-100), 25, 50, 25, Content.Load<Texture2D>("cenoura"), Color.White);

            cenario = new Cenario(0, 0, Content.Load<Texture2D>("background"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);
            listTree.AddRange(
             new List<BaseElementoX>()
             {
                  new BaseElementoX(50, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(250, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                  new BaseElementoX(450, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(650, 300, 200, 200, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(850, 200, 200, 300, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(1050, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(1250, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(1450, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                  new BaseElementoX(150, 300, 200, 200, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(350, 200, 200, 300, Content.Load<Texture2D>("Tree2"), Color.White),
                  new BaseElementoX(550, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(750, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(950, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(1150, 300, 200, 200, Content.Load<Texture2D>("Tree"), Color.White),
                  new BaseElementoX(1350, 200, 200, 300, Content.Load<Texture2D>("Tree3"), Color.White),
                  new BaseElementoX(1550, 300, 200, 200, Content.Load<Texture2D>("Tree2"), Color.White)
             }
             );

            elemento = new Elemento("pocao",500, 500, 100, 100, Content.Load<Texture2D>("poção"), Color.White);

            _personagem.LoadContent(Content, "character");
        }
        
        protected override void UnloadContent()
        {

        }
        
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            //elemento.Update(gameTime);

            _personagem.Mover(ref gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            //if (_personagem.barraVida.GameOver() != -1)
            //{
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();

                cenario.Draw(spriteBatch, gameTime);

                foreach (BaseElementoX var in listTree)
                {
                    var.Draw(spriteBatch, gameTime, _personagem);
                }

                _personagem.cinto.Draw(spriteBatch, gameTime, _personagem);
                _personagem.mochila.Draw(spriteBatch, gameTime, _personagem);
                _personagem.barraVida.Draw(spriteBatch, gameTime, _personagem);
                _personagem.Draw(ref spriteBatch);

                elemento.Draw(spriteBatch, gameTime, _personagem);
                spriteBatch.End();

                base.Draw(gameTime);
            //}

            //else if (_personagem.barraVida.qtdVida != 0)
            //{
            //    _personagem.barraVida.qtdVida = -1; //reinicia da fase
            //}

            //else
            //{
            //    spriteBatch.Draw(Content.Load<Texture2D>("GameOver"), new Rectangle(0,0, 50, 50), Color.White); //reinicia o jogo todo
            //}
        }
    }
}
