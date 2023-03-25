// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

ViewClass.C_View view = ViewClass.C_View.GetInstance();

view.Initialize(15, 15);
view.VeiwLoop();