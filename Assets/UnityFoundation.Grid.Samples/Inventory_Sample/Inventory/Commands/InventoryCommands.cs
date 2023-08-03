using System.Collections.Generic;
using UnityFoundation.Code;

namespace UnityFoundation.Grid.Samples
{
    public class InventoryCommands : IUpdatable
    {
        private readonly List<IInventoryCommand> commands = new();

        public void Register(IInventoryCommand command)
        {
            commands.Add(command);
        }

        public void Update(float deltaTime = 0)
        {
            foreach(var command in commands)
                command.Execute();
        }
    }
}
