using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameWindowsDesktopApplication1;

public class Ball : Object
{
    public Ball()
    {
        position = new Vector2(50, 50);//random getallen.
    }
    
    public override void LoadContent(ContentManager content)
    {
        sprite = content.Load<Texture2D>("avgBallSmall");
    }

    public override void Update(GameTime gameTime)
    {
        //Update stuff
    }
    
    //De ball kan de Update method gebruiken uit Object.cs Er hoeft dus niks overridden te worden om de ball te tekenen.
}