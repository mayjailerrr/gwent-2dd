// using System.Collections.Generic;

// public class Context
// {
//     private Dictionary<string, Effect> effects;

//     public Context()
//     {
//         effects = new Dictionary<string, Effect>();
//     }

//     public void AddEffect(string name, Effect effect)
//     {
//         effects[name] = effect;
//     }

//     public Effect GetEffect(string name)
//     {
//         if (effects.ContainsKey(name))
//         {
//             return effects[name];
//         }
//         else
//         {
//             throw new KeyNotFoundException($"Effect '{name}' not found in the context.");
//         }
//     }
// }
