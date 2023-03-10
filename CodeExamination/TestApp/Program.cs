using TestApp;

Console.WriteLine($"This is a simple application used to test CodeQL");

// Genereate a random number
Random rnd = new Random();
var result = rnd.Next();
try 
{
    // This is a deliberate a typo value
    var lenght = 0.1;
    Console.WriteLine($"length {lenght} has been sent to a default");

    // This is a code smell where the true condition has no code
    if(lenght<2) {}
    else {
        Console.WriteLine($"I can honestly say this line won't be execued!");
    }

    // Likely Bug as value could be negative in this loop
    for(var counter=20; counter>=-10; counter--)
        Console.WriteLine($"Checking Value {counter} is Odd; {counter.IsOdd()}.");

    // SQL injection example
    Console.WriteLine("Enter the name of the car (blank for all):");
    var name = Console.ReadLine();
    SQLInjection injection = new SQLInjection();
    Console.WriteLine($"DBVersion = {injection.Init()}");
    injection.CreateTable();
    foreach(var rec in injection.QueryTable(name!)!)
        Console.WriteLine($"match = {rec}");

    // Code smell of commented out code 
    // Console.WriteLine("I had some code here");
    // Console.WriteLine("But it was not used");
    // Console.WriteLine("There was a problem with it");
    // Console.WriteLine("Any developer should ignore this");

    // Hardcoded Symetric Key
    HardCodedKey key = new HardCodedKey();
    Console.WriteLine("Enter a string to encrypt:");
    var encrypt = Console.ReadLine();
    if (String.IsNullOrEmpty(encrypt)) encrypt = "Here is some text to test";
    Console.WriteLine($"Encrypt string : '{encrypt}' to '{key.EncryptString(encrypt)}'");

    // hardcoded Password
    HardCodedPassword pass = new HardCodedPassword();
    var password = "";
    while(!pass.CheckUser("myUser", password!))
    {
        Console.WriteLine("Enter your password ('Pa$$word' as default):");
        password = Console.ReadLine();
        if (String.IsNullOrEmpty(password)) password = "Pa$$word";
        if(pass.CheckUser("myUser", password!))
            Console.WriteLine("Welcome User.");
        else
            Console.WriteLine("Unknown password, please try again.");
    }

    // TODO: I will fill in some more code here to test things
    //  honest, I will.... At some stage, maybe.

    // Request Forgery - sadly not found
    Console.WriteLine("Enter the URL (blank for google):");
    var url = Console.ReadLine();
    if (String.IsNullOrEmpty(url)) url = "https://www.google.com";
    RequestForgery request = new RequestForgery();
    Console.WriteLine(await request.GetRequest(url!));

}
catch
{}

