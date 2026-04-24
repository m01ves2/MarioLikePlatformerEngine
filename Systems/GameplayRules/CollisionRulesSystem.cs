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

        public void Apply(List<CollisionEvent> events)
        {
            foreach (var e in events) {
                Apply(e);
            }
        }

        public void Apply(CollisionEvent e)
        {
            foreach (var rule in _rules) {
                if (rule.Matches(e))
                    rule.Apply(e);
            }
        }
    }
}
