using System.Collections.Generic;
using System.Collections;


namespace GameLibrary
{
    public class UnityCard : Card
    {
        Stack<double> actualPower;
        double powerCount = 0;

        public Rank Rank { get; private set; }
        public double ActualPower { get => actualPower.Peek(); }
        public double InitialPower { get => powerCount; set => powerCount = value; } 

        public UnityCard(string name, Faction faction, AttackType type, List<Zone> ranges, Rank level, double power = 0, Effect effect = null) : 
                        base(name, faction, type, ranges, power, effect)
        {
            this.Rank = level;
            actualPower = new Stack<double>();
            Change(power);
            ResetPower();
            
        }

        public void InitPower()
        {
            Change(initialPower);
            ResetPower();
        }
        public void ResetPower()
        {
            this.InitialPower = this.actualPower.Peek();
        }

        public void ChangeActualPower(double newPower, bool changed = true)
        {
            if (changed)
            {
                this.InitialPower = (newPower - this.actualPower.Peek() + this.InitialPower > 0) ? (newPower - this.actualPower.Peek() + this.InitialPower) : 0;
            }
            Change(newPower);
        }

        public void RestoreLast()
        {
            actualPower.Pop();
            ResetPower();
        }

        void Change(double newPower)
        {
            actualPower.Push(newPower);
        }
    }
}
