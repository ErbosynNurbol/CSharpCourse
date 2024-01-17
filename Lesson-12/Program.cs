using MODEL;
using COMMON;


//         List<Person> personList = new List<Person>();
//         Random random = new Random();
//         for (int i = 0; i < 100; i++)
//         {
//             personList.Add(new Person
//             {
//                 Name = "Person " + (i + 1),
//                 Age = random.Next(18, 100), // Random age between 18 and 99
//                 IsAlive = random.Next(2) == 0, // Randomly true or false
//                 Salary = Convert.ToDecimal(random.NextDouble() * 100000) // Random salary up to 100,000
//             });
//         }



// var newList  = personList.Select(x=>new {
//     Name = x.Name,
//     Age = x.Age
// }).ToList();


// var presJson  =  JsonHelper.SerializeObject(newList);


List<Task> taskList = new List<Task>();
//ThreadPool = 

SemaphoreSlim semaphoreSlim = new SemaphoreSlim(5);
for(int i = 0;i<10;i++)
{
    int taskNumber = i;
    taskList.Add(Task.Run(async () =>
    {
        semaphoreSlim.Wait();
        Console.WriteLine("Task " + (taskNumber + 1));
        await Task.Delay(1000);
        semaphoreSlim.Release();
        return 0; // Or some other appropriate return value.
    }));
}

Task.WaitAll(taskList.ToArray());

