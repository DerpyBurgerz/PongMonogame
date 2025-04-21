using System;
using System.Collections.Generic;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpDX.XAudio2;

namespace MonoGameWindowsDesktopApplication1;

public class Ball
{
    private Vector2 startPosition;
    private Vector2 position;
    private Vector2 startSpeed;
    private Vector2 speed;
    public static Texture2D sprite;
    public Vector2 Size => new(sprite.Width, sprite.Height);

    public Ball(Vector2 startPosition, Vector2 startSpeed)
    {
        this.startPosition = startPosition;
        position = startPosition;
        this.startSpeed = startSpeed;
        speed = startSpeed;
    }
    
    public void ResetBall() 
    {
        // regel ooit wachttijd
        GameTime resetTime = new GameTime();
        position = startPosition;
        speed = new Vector2(0, 0);
        speed = startSpeed;
        
    }
    public void UpdateBall(Player player1, Player player2, Game1 game)
    {
        Paddle paddle1 = player1.paddle;
        Paddle paddle2 = player2.paddle;

        Vector2 screen = Game1.screen;
        Vector2 paddleSize = paddle1.Size;
        position += speed;

        if (position.X < 0)
        {
            //position.X = 0;
            //speed.X *= -1;
            game.EndRound("Player 2");
        }

        if (position.X + Size.X > screen.X)
        {
            //position.X = screen.X - ballSize.X;
            //speed.X *= -1;
            game.EndRound("Player 1");
        }

        if (position.Y < 0)
        {
            position.Y = 0;
            speed.Y *= -1;
        }

        if (position.Y + Size.Y > screen.Y)
        {
            position.Y = screen.Y - Size.Y;
            speed.Y *= -1;
        }

        PaddleCollision(paddle1);
        PaddleCollision(paddle2);

        void PaddleCollision(Paddle paddle)
        {
            Vector2 paddleDistance = paddle.position - position;
            if (paddleDistance.X < Size.X &&
                paddleDistance.X > -paddleSize.X &&
                paddleDistance.Y < Size.Y &&
                paddleDistance.Y > -paddleSize.Y)
            {
                float topIntersection = Size.Y - paddleDistance.Y;
                float bottomIntersection = paddleSize.Y + paddleDistance.Y;
                float leftIntersection = Size.X - paddleDistance.X;
                float rightIntersection = paddleSize.X + paddleDistance.X;

                float smallestIntersection = new float[4] { topIntersection, bottomIntersection, leftIntersection, rightIntersection }.Min();
                if (smallestIntersection == topIntersection)
                {
                    position.Y = paddle.position.Y - Size.Y;
                    speed.Y *= -1;
                }
                if (smallestIntersection == bottomIntersection)
                {
                    position.Y = paddle.position.Y + paddleSize.Y;
                    speed.Y *= -1;
                }
                if (smallestIntersection == leftIntersection)
                {
                    position.X = paddle.position.X - Size.X;
                    speed.X *= -1;
                    speed.Y = YSpeed();
                }
                if (smallestIntersection == rightIntersection)
                {
                    position.X = paddle.position.X + paddleSize.X;
                    speed.X *= -1;
                    speed.Y = YSpeed();
                }

                speed.X *= 1.1f;
            }

            float YSpeed() 
            {
                float maxSpin = 6;
                float neutralDistance = 0.5f * (Size.Y - paddleSize.Y);
                float a = -maxSpin/(neutralDistance + paddleSize.Y);
                float b = -a * neutralDistance;
                return (float) (a * paddleDistance.Y + b); 
            }
        }
    }

    public void DrawBall(SpriteBatch spriteBatch, Texture2D sprite)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}