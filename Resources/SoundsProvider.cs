using Microsoft.Xna.Framework.Audio;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Resources
{
    public class SoundsProvider
    {
        private Dictionary<SoundType, SoundEffect> _sounds;

        public SoundsProvider(Dictionary<SoundType, SoundEffect> sounds)
        {
            _sounds = sounds;
        }

        public SoundEffect Get(SoundType type) => _sounds[type];
    }
}
