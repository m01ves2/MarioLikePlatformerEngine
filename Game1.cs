using MarioLikePlatformerEngine.Application;
using MarioLikePlatformerEngine.Application.Scenes;
using MarioLikePlatformerEngine.Inputs;
using MarioLikePlatformerEngine.Resources;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace MarioLikePlatformerEngine;

public class Game1 : Game
{
    private GraphicsDeviceManager _graphics;
    private SpriteBatch _spriteBatch;
    private Texture2D _whitePixel; //pixel — это инструмент рисования, создаётся в Game1 с помощью GraphicsDevice. используем для рисования частиц

    private int _screenWidth = 800;
    private int _screenHeight = 600;

    private GameAssets _assets;
    private GameResources _resources;
    private GameSettings _gameSettings;
    private TextureProvider _textures;
    private SoundsProvider _sounds;

    private SceneManager _sceneManager;

    public Game1()
    {
        _graphics = new GraphicsDeviceManager(this);
        Content.RootDirectory = "Content";
        IsMouseVisible = true;


        _graphics.PreferredBackBufferWidth = _screenWidth;
        _graphics.PreferredBackBufferHeight = _screenHeight;
        _graphics.ApplyChanges();

        _assets = new GameAssets();
        _resources = new GameResources();
        _gameSettings = new GameSettings();
    }

    protected override void Initialize()
    {
        // TODO: Add your initialization logic here

        base.Initialize();
    }

    protected override void LoadContent()
    {
        _spriteBatch = new SpriteBatch(GraphicsDevice);

        // TODO: use this.Content to load your game content here

        var whitePixel = new Texture2D(GraphicsDevice, 1, 1);
        whitePixel.SetData(new[] { Color.White });


        _assets.Load(Content);
        _resources.Load(Content, whitePixel, _screenWidth, _screenHeight);
        _textures = new TextureProvider(
            _assets.EntityTextures, 
            _assets.TileTextures, 
            _assets.Backgrounds);

        _sounds = new SoundsProvider(_assets.Sounds);
        
        _sceneManager = new SceneManager(_resources, _textures, _sounds, _gameSettings);
        //_sceneManager.SetScene(new GameScene(_textureProvider, _soundProvider, _gameSettings));
        _sceneManager.SetScene(new MenuScene(_textures, new GameResult()));
    }

    protected override void Update(GameTime gameTime)
    {
        // TODO: Add your update logic here
        float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
        
        Input.Update();
        _sceneManager.Update(dt);

        if (_sceneManager.ShouldExit)
            Exit();


        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.CornflowerBlue);

        //_spriteBatch.Begin();
        //_spriteBatch.Begin(transformMatrix: _camera.GetViewMatrix());

        // TODO: Add your drawing code here
        _sceneManager.Draw(_spriteBatch);

        //base.Draw(gameTime);

        //_spriteBatch.End();
    }
}
