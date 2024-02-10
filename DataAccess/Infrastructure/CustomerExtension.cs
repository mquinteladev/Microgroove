namespace DataAccess.Infrastructure;

public static class CustomerExtension
{
    public static int Age(this DateOnly dob)
    {
        int age = DateTime.Now.Year - dob.Year;

        if (DateTime.Now.Month < dob.Month || (DateTime.Now.Month == dob.Month && DateTime.Now.Day < dob.Day))
            age--;

        return age;
    }

}