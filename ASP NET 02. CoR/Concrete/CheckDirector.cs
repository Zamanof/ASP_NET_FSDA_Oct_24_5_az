namespace ASP_NET_02._CoR.Concrete;

class CheckDirector
{
    public bool MakeUserChecker(User user)
    {
        UserNameChecker userNameChecker = new();
        PasswordChecker passwordChecker = new();
        EmailChecker emailChecker = new();
        userNameChecker.Next = passwordChecker;
        passwordChecker.Next = emailChecker;

        return userNameChecker.Check(user);
    }

    public bool MakeOtherChecker(User user)
    {
        PasswordChecker passwordChecker = new();
        EmailChecker emailChecker = new();
        emailChecker.Next = passwordChecker;
 

        return emailChecker.Check(user);
    }
}
