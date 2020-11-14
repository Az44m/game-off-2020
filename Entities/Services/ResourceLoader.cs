using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using Godot;
using Directory = Godot.Directory;

namespace GameOff2020.Entities.Services
{
    public class ResourceLoader : Node
    {
        private List<Texture> _rocketTextures = new List<Texture>();
        public IReadOnlyList<Texture> RocketTextures => _rocketTextures;

        public override void _Ready()
        {
            LoadRocketTextures();
        }

        private void LoadRocketTextures()
        {
            var rocketTextureDirectory = new Directory();
            var rocketTextureDirectoryPath = "res://Resources/Rocket";
            if (rocketTextureDirectory.Open(rocketTextureDirectoryPath) != Error.Ok)
                throw new IOException("Could not open rocket resource folder");
            if (rocketTextureDirectory.ListDirBegin(true, true) != Error.Ok)
                throw new IOException("Could not list rocket textures");


            var textureName = rocketTextureDirectory.GetNext();
            while (!string.IsNullOrEmpty(textureName))
            {
                if (textureName.EndsWith(".png.import"))
                {
                    textureName = textureName.Replace(".import", string.Empty);
                    var rocketTexture = Godot.ResourceLoader.Load<Texture>($"{rocketTextureDirectoryPath}/{textureName}");
                    rocketTexture.ResourceName = Regex.Match(textureName, @"Rocket_Progress_(\d+)").Groups[1].Value;
                    _rocketTextures.Add(rocketTexture);
                }

                textureName = rocketTextureDirectory.GetNext();
            }

            _rocketTextures = _rocketTextures.OrderBy(x => int.Parse(x.ResourceName)).ToList();
        }
    }
}
