using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;

namespace MonoGame;

public class Game1 : Game
{
    public class Controller : IController
    {
        private MarioSprite _sprite; //connected directy to sprite class

        public void HandleInput(){
            var state = Keyboard.GetState();
            if(state.IsKeyDown(Keys.W)){
                _sprite.pos.Y -= 5; 
                _sprite.state = MarioSprite.movingState.up;
            }
            else if(state.IsKeyDown(Keys.S)){
                _sprite.pos.Y += 5; 
                _sprite.state = MarioSprite.movingState.down;
            }
            else if(state.IsKeyDown(Keys.A)){
                _sprite.pos.X -= 5; 
                _sprite.state = MarioSprite.movingState.left;
            }
            else if(state.IsKeyDown(Keys.D)){
                _sprite.pos.X += 5; 
                _sprite.state = MarioSprite.movingState.right;
            }
            else{
                _sprite.state = MarioSprite.movingState.idle;
            }

            MouseState mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed){
                
                if(mouse.X > _sprite.pos.X){
                    _sprite.facingDirection = true;
                }else{
                    _sprite.facingDirection = false;
                }
            }
        }

         public Controller(MarioSprite sprite){
            _sprite = sprite;
        }

    }
    public class MarioSprite : ISprite
    {
        public Texture2D textureAtlas { get; set; }
        public List<Rectangle> Frames { get; set; } = new List<Rectangle>(); //auto-property makes simple getter/setter
        public enum movingState { idle, left, right, up, down};
        public movingState state;
        public int FrameNum;
        public Vector2 pos;
        public bool facingDirection;//false is left, true is right


        public void Draw(SpriteBatch sb, Vector2 pos)
        {
            if (this.facingDirection == true){
                sb.Draw(textureAtlas,
                pos,
                Frames[FrameNum],
                Color.White,        // color
                0.0f,               // rotation
                new Vector2(        // origin
                Frames[FrameNum].Width/2,
                Frames[FrameNum].Height/2),
                4.0f,
                SpriteEffects.FlipHorizontally, //flip orientation
                0.0f);
            }
            else{
              sb.Draw(textureAtlas,
                pos,
                Frames[FrameNum],
                Color.White,        // color
                0.0f,               // rotation
                new Vector2(        // origin
                Frames[FrameNum].Width/2,
                Frames[FrameNum].Height/2),
                4.0f,
                SpriteEffects.None,
                0.0f);
            }

        }

        public int FrameBuffer; //change once every 8 frames instead of every frame
        public void Update()
        {
            if (this.state == movingState.idle){ //GOING NOWHERE
                FrameNum = 1;
            }

            else if (this.state == movingState.right || this.state == movingState.left){ //GOING RIGHT OR LEFT
                if (FrameBuffer > 8){
                    if (FrameNum >= Frames.Count - 1){
                        FrameNum = 2;
                    }
                    else{
                        FrameNum++;
                    }
                    FrameBuffer = 0;
                }else{
                    FrameBuffer++;
                }

                if (this.state == movingState.right){
                    facingDirection = true;
                }else{
                    facingDirection = false;  
                }  
            }

            else if (this.state == movingState.up){ //GOING UP 
                FrameNum = 2;
            }else if (this.state == movingState.down){ //GOING DOWN
                FrameNum = 0;
            }
   
        }

        public void Constructor(Texture2D t, List<Rectangle> f)
        {
            this.textureAtlas = t;
            this.Frames = f;
            FrameNum = 1;//idle frame
            pos = new Vector2(400, 200);//middle of screen
            FrameBuffer = 0;
            this.state = movingState.idle;
            this.facingDirection = false;
        }
    }
    MarioSprite marioSprite = new MarioSprite(); // loaded in LoadContent()
    Controller controller = null; // loaded in LoadContent()
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private SpriteFont _font;


    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;
    }

    protected override void Initialize()
    {
        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);
        _font = Content.Load<SpriteFont>("fonts/font");

        Texture2D marioTexAtlas = Content.Load<Texture2D>("images/mario");
        marioSprite.Constructor(marioTexAtlas, new List<Rectangle>{
            new Rectangle(0, 51, 22, 34),
            new Rectangle(176, 51, 22, 34), // x, y, width, height
            new Rectangle(145, 51, 22, 34),
            new Rectangle(120, 51, 22, 34),
            new Rectangle(85, 51, 22, 34)
        });
        controller = new Controller(marioSprite); 
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit(); //escape key exits!
        
        controller.HandleInput();
        marioSprite.Update();

        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        _spriteBatch.Begin();

        // Draw 
        marioSprite.Draw(_spriteBatch, marioSprite.pos);

        _spriteBatch.DrawString(_font, "CREDITS\nProgram made by: Leo Tombragel\nSprites taken from: \nhttps://osu.instructure.com/courses/219439/files/folder/Mario%20spritesheets\n%20in%20different%20formats?preview=91959919", new Vector2(10, 20), Color.White);

        // Always end the sprite batch when finished.
        _spriteBatch.End();

        base.Draw(gameTime);
    }
}
