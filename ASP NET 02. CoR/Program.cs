using ASP_NET_02._CoR;
using ASP_NET_02._CoR.Concrete;

User user = new() 
{ 
    UserName = "Salam", 
    Password = "P@ss123456", 
    Email = "zamanov@itstep.org" 
};
CheckDirector check = new CheckDirector();
Console.WriteLine(check.MakeUserChecker(user));
