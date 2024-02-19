using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1;

public class Ball : Object
{
    public Ball()
    {
        //Dit is de constructor
    }
    
    public static void LoadContent(ContentManager content)
    {
        sprite = content.Load<Texture2D>("PaddleBlue");
    }

    public override void Update(GameTime gameTime)
    {
        //Update stuff
    }

    public override void Draw(SpriteBatch spriteBatch)
    {
        //Draw stuff
    }
}