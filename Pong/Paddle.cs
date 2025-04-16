using Microsoft.VisualBasic.Devices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MonoGameWindowsDesktopApplication1;

public class Paddle
{
    private Vector2 position;
    public Paddle(Vector2 startPosition)
    {
        // constructor
        position = startPosition;
    }
    
    public void MovePaddle(KeyboardState keyboardState, Keys upKey, Keys downKey)
    {
        if (keyboardState.IsKeyDown(upKey))
        {
            position.Y -= 1;
                }
        if (keyboardState.IsKeyDown(downKey))
        {
            position.Y += 1;
        }
    }
    public void DrawPaddle(SpriteBatch spriteBatch, Texture2D sprite)
    {
        spriteBatch.Draw(sprite, position, Color.White);
    }
}