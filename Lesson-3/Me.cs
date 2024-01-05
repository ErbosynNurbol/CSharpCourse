
using System.Security.Cryptography.X509Certificates;

namespace Lesson_3
{
    public class sealed Me : Father  //public, private, protected
    {
        public Me(string name) : base(name)
        {

        }

        public override void EarnMoney()
        {
            this.money += 100;
        }
    }
}