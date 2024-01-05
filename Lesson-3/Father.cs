
namespace Lesson_3
{
    public class Father
    {
        protected decimal money = 10000;
        public string Name{get;}

        public string Gender{get;set;}
        public Father(string name)
        {
            Name = name;
        }
        public virtual void EarnMoney()
        {
            this.money += 1000;
        }

        private void Eat()
        {

        }

    }
}