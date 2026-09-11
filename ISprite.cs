using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

public interface ISprite
{
    Texture2D textureAtlas { get; set; }
    List<Rectangle> Frames { get; set; }
    void Draw(SpriteBatch sb, Vector2 pos);
    void Update();
    void Constructor(Texture2D t, List<Rectangle> f);
}