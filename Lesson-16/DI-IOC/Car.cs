namespace Lesson_16.DI_IOC;

public class Car
{    

    //Inversion Of Control
    private IMator _iMator;
    public Car(IMator iMotor)
    {
        _iMator= iMotor;
    }
    
    public void Start()
    {
        _iMator.Start();
    }
}