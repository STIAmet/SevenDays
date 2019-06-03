﻿// Developed by Danilo Borges
// 22/03/2019
// danilo.bsto@gmail.com
// Essa é uma classe para encapsulamento de textura, referente ao meu projeto piloto Mono.Helper.
// O código está incompleto.
// Pode-se usar a vontade em outros projetos.

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;

namespace Mono.Helper
{
    public class Sprite
    {
        public Texture2D Texture { get; private set; }     
        public List<Rectangle> TextureFrames { get; set; }
        public Rectangle TextureBounds { get { return Texture.Bounds; } }

        public Vector2 Position { get; set; }        
        public float X { get { return Position.X; } set { Position = new Vector2(value, Position.Y); } }        
        public float Y { get { return Position.Y; } set { Position = new Vector2(Position.X, value); } }
        
        public Point Size { get { return Texture.Bounds.Size; } }        
        public int Width { get { return Size.X; } }        
        public int Height { get { return Size.Y; } }
        
        public float Rotation { get; set; }        
        public Vector2 Scale { get; set; }        
        public Color Color { get; set; }        
        public SpriteEffects Effect { get; set; }        
        
        public Vector2 OriginCorrection { get; set; }        
        public float OriginCorrectionX { get { return OriginCorrection.X; } set { OriginCorrection = new Vector2(value, OriginCorrection.Y); } }        
        public float OriginCorrectionY { get { return OriginCorrection.Y; } set { OriginCorrection = new Vector2(OriginCorrection.X, value); } }

        public bool Disposed { get; private set; }        
        
        public Sprite(Texture2D texture)
        {
            Texture = texture;            
            TextureFrames = new List<Rectangle> { texture.Bounds };            
            Position = Vector2.Zero;
            Rotation = 0;
            Scale = Vector2.One;
            Color = Color.White;
            Effect = SpriteEffects.None;
            OriginCorrection = Vector2.Zero;            
            Disposed = false;
        }        

        public void Flip(bool x, bool y)
        {
            if (x && y) Effect = SpriteEffects.FlipHorizontally | SpriteEffects.FlipVertically;            
            else if (x) Effect = SpriteEffects.FlipHorizontally;
            else if (y) Effect = SpriteEffects.FlipVertically;
            else Effect = SpriteEffects.None;
        }        
        
        public void AddFrame(int x, int y, int w, int h, bool clear)
        {
            if (clear)
                TextureFrames.Clear();

            TextureFrames.Add(new Rectangle(x, y, w, h));
        }
                
        public void Draw(SpriteBatch spriteBatch)
        {
            if (Rotation != 0 || Scale != Vector2.One || Effect != SpriteEffects.None)
                spriteBatch.Draw(Texture, Position, TextureFrames[0], Color, Rotation, Vector2.Zero + OriginCorrection, Scale, Effect, 0);
            else
                spriteBatch.Draw(Texture, Position, TextureFrames[0], Color);
        }
        
        public void DrawAsTile(SpriteBatch spriteBatch, int repeatInX, int repeatInY)
        {
            int rx = (int)Position.X;
            int ry = (int)Position.Y;
            int rw = TextureFrames[0].Width * repeatInX;
            int rh = TextureFrames[0].Height * repeatInY;

            Rectangle destination = new Rectangle(rx, ry, rw, rh);
            Rectangle source = new Rectangle(TextureFrames[0].X, TextureFrames[0].Y, TextureFrames[0].Width * repeatInX, TextureFrames[0].Height * repeatInY);

            if (Rotation != 0 || Scale != Vector2.One || Effect != SpriteEffects.None)
                spriteBatch.Draw(Texture, destination, source, Color, Rotation, Vector2.Zero + OriginCorrection, Effect, 0);
            else
                spriteBatch.Draw(Texture, destination, source, Color);            
        }

        public static Sprite Rectangle(Game game, int x, int y, int width, int height, Color color) => Rectangle(game, new Rectangle(x, y, width, height), color);
        public static Sprite Rectangle(Game game, Rectangle rectangle, Color color)
        {
            Color[] data;
            Texture2D texture;

            texture = new Texture2D(game.GraphicsDevice, rectangle.Width, rectangle.Height);
            data = new Color[texture.Width * texture.Height];

            for (int i = 0; i < data.Length; ++i)
                data[i] = color;

            texture.SetData(data);

            var sprite = new Sprite(texture);
            sprite.X = rectangle.X;
            sprite.Y = rectangle.Y;

            return sprite;
        }        
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }        

        protected void Dispose(bool disposing)
        {
            if (Disposed)
                return;            

            if(disposing)
            {
                Texture?.Dispose();
            }

            Disposed = true;
        }        
    }
}