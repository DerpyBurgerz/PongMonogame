using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsDesktopApplication1;

public class Paddle
{
    public Vector2 position;
    public Paddle(Vector2 startPosition)
    {
        // constructor
        position = startPosition;
    }
    
    public void MovePaddle(float paddleLength, KeyboardState keyboardState, Keys upKey, Keys downKey)
    {
        Vector2 screen = Game1.screen;
        if (keyboardState.IsKeyDown(upKey) && position.Y > 0)
        {
            position.Y -= 5;
                }
        if (keyboardState.IsKeyDown(downKey) && position.Y + paddleLength < screen.Y)
        {
            position.Y += 5;
        }
    }
    public void DrawPaddle(SpriteBatch spriteBatch, Texture2D sprite)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}