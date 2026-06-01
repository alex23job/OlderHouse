using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Level
{
    public interface IMyCommand
    {
        public int ID { get; }
        public void Execute();
    }
}
