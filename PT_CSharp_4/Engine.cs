using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PT_CSharp_4
{
    public class Engine : IComparable
    {
        public int Id { get; set; }
        public double Displacement { get; set; }
        public double HorsePower { get; set; }
        public String Model { get; set; }
        public Engine() { }
        public Engine(double displacement, double horsePower, string model)
        {
            this.Displacement = displacement;
            this.HorsePower = horsePower;
            this.Model = model;
        }
        public override string ToString()
        {
            return Model + " " + Displacement + " (" + HorsePower + " hp) ";
        }

        public int CompareTo(object obj)
        {
            var engine = obj as Engine;
            if (engine != null)
            {
                int byModel = string.Compare(this.Model, engine.Model, StringComparison.OrdinalIgnoreCase);
                if (byModel != 0)
                    return byModel;
                
                int byHorsePower = (this.HorsePower > engine.HorsePower)
                    ? 1
                    : ((this.HorsePower < engine.HorsePower) ? -1 : 0);
                return byHorsePower;
            }

            return 0;
        }
    }
}
