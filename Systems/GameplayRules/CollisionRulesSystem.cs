using MarioLikePlatformerEngine.Application;
using MarioLikePlatformerEngine.Core;
using System.Collections.Generic;

namespace MarioLikePlatformerEngine.Systems.Collisions
{
    class CollisionRulesSystem
    {
        private List<ICollisionRule> _rules = new();

        public void AddRule(ICollisionRule rule)
        {
            _rules.Add(rule);
        }

        public void Apply(List<CollisionEvent> events, GameSession ctx)
        {
            foreach (var e in events) {
                Apply(e, ctx);
            }
        }

        public void Apply(CollisionEvent e, GameSession ctx)
        {
            foreach (var rule in _rules) {
                if (rule.Matches(e))
                    rule.Apply(e, ctx);
            }
        }
    }
}
