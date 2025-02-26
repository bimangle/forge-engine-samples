using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bimangle.ForgeEngine.Georeferncing
{
    class TestRunState
    {
        private static TestRunState _LastState;

        public static TestRunState LastState
        {
            get => _LastState ?? (_LastState = new TestRunState());
            set => _LastState = value;
        }

        public string Y { get; set; } = "0";
        public string X { get; set; } = "0";
        public string Z { get; set; } = "0";
    }
}
