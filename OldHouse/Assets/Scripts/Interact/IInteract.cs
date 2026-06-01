using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Interact
{
    public interface IInteract
    {
        public int ID { get; }
        public string GetHint();
    }
}
