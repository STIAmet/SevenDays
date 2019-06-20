using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;
using Mono.Helper.Input;
using Mono.Helper;


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

        Elemento elemento, elemento2;

        private SpriteFont font;

        Song music;

        heroi _personagem = new heroi();
        Inimigo inimigo = new Inimigo();

        public List<BaseElementoX> listTree = new List<BaseElementoX>();

        KeyboardHelper key = new KeyboardHelper();
        
        public int faseAtual = 0;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }
       
        protected override void Initialize()
        {
            music = Content.Load<Song>("GOTTA BE YOU");
            //MediaPlayer.Play(music);

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
            _personagem.cinto = new Cinto(600, 25, 400, 25, Content.Load<Texture2D>("buraco"), Color.White);
            _personagem.mochila = new Mochila((Window.ClientBounds.Width-100), 25, 50, 25, Content.Load<Texture2D>("buraco"), Color.White);

            switch (faseAtual)
            {
                case 0:
                    {
                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("back1"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

                        listTree.AddRange(
                         new List<BaseElementoX>()
                         {
                              new BaseElementoX(-50, 130, 300, 400, Content.Load<Texture2D>("casa2"), Color.White),
                              new BaseElementoX(350, 130, 300, 400, Content.Load<Texture2D>("casa3"), Color.White),
                              new BaseElementoX(750, 130, 300, 400, Content.Load<Texture2D>("casa2"), Color.White),
                              new BaseElementoX(1150, 160, 300, 400, Content.Load<Texture2D>("casa4"), Color.White)
                              
                         }
                         );

                        elemento = new Elemento("pocao", 1500, 500, 60, 60, Content.Load<Texture2D>("poção"), Color.White);
                        //elemento = new Elemento("pocao",500, 500, 100, 100, Content.Load<Texture2D>("poção"), Color.White);
                        elemento2 = new Elemento("espada", 800, 500, 100, 100, Content.Load<Texture2D>("espada"), Color.White);
                        

                        break;
                    }
                case 1:
                    {

                        cenario = new Cenario(0, 0, Content.Load<Texture2D>("back1"), Color.White, Window.ClientBounds.Width, Window.ClientBounds.Height);

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

                        break;
                    }

                    
            }
            

            

            

            _personagem.LoadContent(Content, "character");
            inimigo.LoadContent(Content, "zumbie2");
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
            
            if(_personagem.nome == "")
            {
                key.Update(gameTime,_personagem);
            }
            

            inimigo.Mover(ref gameTime);

            base.Update(gameTime);
        }
        
        protected override void Draw(GameTime gameTime)
        {
            //if (_personagem.barraVida.GameOver() != -1)
            //{
                GraphicsDevice.Clear(Color.CornflowerBlue);

                spriteBatch.Begin();

            cenario.Draw(spriteBatch, gameTime);
            spriteBatch.Draw(Content.Load<Texture2D>("Shawn"), new Rectangle(0, 0, Window.ClientBounds.Width, Window.ClientBounds.Height), Color.White);

            spriteBatch.DrawString(font, "Distancia: " + _personagem.distance.ToString(), new Vector2(800, 100), Color.Red);

            foreach (BaseElementoX var in listTree)
            {
                var.Draw(spriteBatch,gameTime,_personagem);
            }

            spriteBatch.DrawString(font, key._stringValue, new Vector2(0, 100), Color.Red);

            spriteBatch.Draw(Content.Load<Texture2D>("mochila"), new Rectangle((Window.ClientBounds.Width - 180), 25, 80, 60), Color.White);
            spriteBatch.Draw(Content.Load<Texture2D>("cinto2"), new Rectangle(500, 25, 80, 60), Color.White);

            _personagem.cinto.Draw(spriteBatch, gameTime,_personagem);
            _personagem.mochila.Draw(spriteBatch, gameTime, _personagem);
            _personagem.Draw(ref spriteBatch);
            inimigo.Draw(ref spriteBatch);
            _personagem.barraVida.Draw(spriteBatch, gameTime, _personagem);
                

            //TUTORIAL
            
            if (_personagem.distance >= 600 && _personagem.distance < 900)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                spriteBatch.DrawString(font, "USE AS SETAS:\nSETA ESQUERDA: DESLOCAR PARA ESQUERDA\nSETA DIREITA: DESLOCAR A DIREITA", new Vector2(60, 110), Color.Black);
            }
            if (_personagem.distance > 1400 && _personagem.distance <1600)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 90, 800, 160), Color.White);
                spriteBatch.DrawString(font, "PEGUE SEU ELEMENTO:\nBOTAO ESQUERDO: ELEMENTO VAI PARA O CINTO\nBOTAO DIREITO: ELEMENTO VAI PARA MOCHILA\nPARA JOGAR FORA BASTA APERTAR O SCROLL DO MOUSE", new Vector2(60, 110), Color.Black);
            }
            if (_personagem.distance > 1800 && _personagem.distance < 2000)
            {
                spriteBatch.Draw(Content.Load<Texture2D>("Balawn"), new Rectangle(-20, 70, 700, 160), Color.White);
                spriteBatch.DrawString(font, "Digite seu nome e aperte enter para salvar:\n" + key._stringValue, new Vector2(60, 110), Color.Black);
            }

            elemento.Draw(spriteBatch, gameTime, _personagem);
            elemento2.Draw(spriteBatch, gameTime, _personagem);

            spriteBatch.End();
            
            base.Draw(gameTime);
        }
    }
}
