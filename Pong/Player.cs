using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsDesktopApplication1;
public class Player
{
    public int Score;
    public Paddle paddle;
    public Player(int startScore, Vector2 startPosition, Keys upKey, Keys downKey)
    {
        Score = startScore;
        paddle = new(startPosition, upKey, downKey);
    }

    public void UpdateScore()
    {
        Score += 1;
    }
    public void UpdatePlayer(KeyboardState currentKeyboardState)
    {
        paddle.MovePaddle(currentKeyboardState);
    }
    public void DrawPlayer(SpriteBatch spriteBatch, Vector2 scorePosition)
    {
        paddle.DrawPaddle(spriteBatch);

        spriteBatch.DrawString(Game1.spriteFont, Score.ToString(), scorePosition, Color.Black);
    }
}
