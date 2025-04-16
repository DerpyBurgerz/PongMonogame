using System;
using System.Collections.Generic;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1;

public class Ball
{
    private Vector2 position;
    public Ball(Vector2 startPosition, Vector2 startSpeed)
    {
        position = startPosition;
        speed = startSpeed;
    }

    private Vector2 speed;
    
    public void UpdateBall(Vector2 screen, Vector2 ballSize)
    {
        position += speed;

        if (position.X < 0)
        {
            position.X = 0;
            speed.X *= -1;
        }
        
        if (position.X + ballSize.X > screen.X)
        {
            position.X = screen.X - ballSize.X;
            speed.X *= -1;
        }

        if (position.Y < 0)
        {
            position.Y = 0;
            speed.Y *= -1;
        }

        if (position.Y + ballSize.Y > screen.Y)
        {
            position.Y = screen.Y - ballSize.Y;
            speed.Y *= -1;
        }
    }

    public void DrawBall(SpriteBatch spriteBatch, Texture2D sprite)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}